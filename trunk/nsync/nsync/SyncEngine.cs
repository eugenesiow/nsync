using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;

namespace nsync
{
    class SyncEngine
    {
        ////////////////////
        // CLASS VARIABLES
        ////////////////////

        public System.ComponentModel.BackgroundWorker backgroundWorker;
        public System.ComponentModel.BackgroundWorker backgroundWorker2;
        private ulong freeDiskSpaceForLeft = 0;
        private ulong freeDiskSpaceForRight = 0;
        private ulong diskSpaceNeededForLeft = 0;
        private ulong diskSpaceNeededForRight = 0;
        private bool isCheckForLeftDone = false;
        private string leftPath;
        private string rightPath;
        private static int countDoneChanges = 0;
        private static int countChanges = 0;
        private Intelligence intelligentManager;

        ////////////////////
        // CONSTRUCTOR
        ////////////////////

        public SyncEngine()
        {
            // Set up the BackgroundWorker object by 
            // attaching event handlers.
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.WorkerReportsProgress = true;

            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            backgroundWorker2.WorkerReportsProgress = true;

            // Create the intelligence manager
            intelligentManager = new Intelligence();
        }

        ////////////////////
        // PRIVATE METHODS
        ////////////////////

        // Calculate the amount of free disk space.
        // Units is in bytes.
        private ulong GetFreeDiskSpaceInBytes(string drive)
        {
            ManagementObject disk = new ManagementObject(
            "win32_logicaldisk.deviceid=\"" + drive + ":\"");
            disk.Get();
            return (ulong)disk["FreeSpace"];
        }

        // Convert bytes to megabytes.
        private string ConvertBytesToMegabytes(ulong amount)
        {
            return (Convert.ToUInt64(amount.ToString()) / 1000000).ToString() + " MB";
        }

        // Detect the changes to the folder and write to metadata file.
        // Most of the work done by the sync framework.
        private static void DetectChangesonFileSystemReplica(
            string replicaRootPath, FileSyncScopeFilter filter, FileSyncOptions options)
        {
            FileSyncProvider provider = null;

            try
            {
                provider = new FileSyncProvider(replicaRootPath, filter, options);
                provider.DetectChanges();
            }
            finally
            {
                // Release resources or memory
                if (provider != null)
                    provider.Dispose();
            }
        }

        // Telling the sync framework to start propogating changes
        private bool SyncFileSystemReplicasOneWay(string sourcePath, string destPath,
            FileSyncScopeFilter filter, FileSyncOptions options, bool isPreview)
        {
            FileSyncProvider sourceProvider = null;
            FileSyncProvider destProvider = null;

            try
            {
                sourceProvider = new FileSyncProvider(sourcePath, filter, options);
                destProvider = new FileSyncProvider(destPath, filter, options);

                // When it's in preview mode, no actual changes are done.
                // This mode is used to check the number of changes that will be carried out later
                if (isPreview)
                {
                    sourceProvider.PreviewMode = true;
                    destProvider.PreviewMode = true;
                }
                else
                {
                    sourceProvider.PreviewMode = false;
                    destProvider.PreviewMode = false;
                }

                if (isPreview)
                {
                    if (!isCheckForLeftDone)
                    {
                        freeDiskSpaceForLeft = GetFreeDiskSpaceInBytes(sourcePath.Substring(0, 1));
                        freeDiskSpaceForRight = GetFreeDiskSpaceInBytes(destPath.Substring(0, 1));
                    }
                }

                destProvider.Configuration.ConflictResolutionPolicy = ConflictResolutionPolicy.ApplicationDefined;
                SyncCallbacks destinationCallBacks = destProvider.DestinationCallbacks;
                destinationCallBacks.ItemConflicting += new EventHandler<ItemConflictingEventArgs>(OnItemConflicting);
                destinationCallBacks.ItemConstraint += new EventHandler<ItemConstraintEventArgs>(OnItemConstraint);

                if(isPreview)
                    destProvider.ApplyingChange += new EventHandler<ApplyingChangeEventArgs>(OnApplyingChange);
                else
                    destProvider.AppliedChange += new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);

                SyncOrchestrator agent = new SyncOrchestrator();
                agent.LocalProvider = sourceProvider;
                agent.RemoteProvider = destProvider;
                agent.Direction = SyncDirectionOrder.Upload;

                agent.Synchronize();

                if (isPreview)
                    return CheckSpace();

                return true;
            }
            finally
            {
                if (sourceProvider != null) sourceProvider.Dispose();
                if (destProvider != null) destProvider.Dispose();
            }
        }

        // Check if there is sufficient space
        // for sync to take place
        private bool CheckSpace()
        {
            if (!isCheckForLeftDone)
            {
                isCheckForLeftDone = !isCheckForLeftDone;
                return diskSpaceNeededForLeft < freeDiskSpaceForRight;
            }
            return diskSpaceNeededForLeft < freeDiskSpaceForRight &&
                   diskSpaceNeededForRight < freeDiskSpaceForLeft;
        }

        // Setting conflicting rules
        private static void OnItemConflicting(object sender, ItemConflictingEventArgs args)
        {
            // Currently, latest change wins
            args.SetResolutionAction(ConflictResolutionAction.SourceWins);
        }

        // Setting constraint rules
        private static void OnItemConstraint(object sender, ItemConstraintEventArgs args)
        {
            MessageBox.Show("constraint");
            args.SetResolutionAction(ConstraintConflictResolutionAction.RenameSource);
        }

        // Count the number of changes already done by the sync framework
        // and report the progress percentage to the backgroundWorker
        private void OnAppliedChange(object sender, AppliedChangeEventArgs args)
        {
            
            countDoneChanges++;
            // This method will raise an event to the backgroundWorker via backgroundWorker_ProgressChanged
            backgroundWorker.ReportProgress((int)((double)countDoneChanges / countChanges * 100));

            /*
            switch (args.ChangeType)
            {
                case ChangeType.Create:
                    _summary.InsertMsg("-- Applied CREATE for file " + args.NewFilePath);
                    break;
                case ChangeType.Delete:
                    _summary.InsertMsg("-- Applied DELETE for file " + args.OldFilePath);
                    break;
                case ChangeType.Update:
                    _summary.InsertMsg("-- Applied OVERWRITE for file " + args.OldFilePath);
                    break;
                case ChangeType.Rename:
                    _summary.InsertMsg("-- Applied RENAME for file " + args.OldFilePath +
                                      " as " + args.NewFilePath);
                    break;
            }
            */ 
        }
        
        // Counts the number of changes to be made later.
        // Calculate amount of disk space needed for sync later.
        // Note: no changes are actually made during pre sync.
        private void OnApplyingChange(object sender, ApplyingChangeEventArgs args)
        {
            countChanges++;
            if (!isCheckForLeftDone)
                diskSpaceNeededForLeft += (ulong) args.NewFileData.Size;
            else
                diskSpaceNeededForRight += (ulong)args.NewFileData.Size;
        }

        // This method is called when the backgroundWorker starts working.
        // The 'real' work to be done is called from here.
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker = sender as BackgroundWorker;
            // e.Result will be available to RunWorkerCompletedEventArgs later
            // when the work assigned is completed.
            e.Result = InternalStartSync();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = InternalPreSync();
        }

        // Start the sync process
        private bool InternalStartSync()
        {
            try
            {
                // Configure sync options
                FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges |
                    FileSyncOptions.RecycleConflictLoserFiles |
                    FileSyncOptions.RecycleDeletedFiles |
                    FileSyncOptions.RecyclePreviousFileOnUpdates;

                // Configure sync filters
                // This example will exclude shortcut links during sync
                FileSyncScopeFilter filter = new FileSyncScopeFilter();
                filter.FileNameExcludes.Add("*.lnk");

                // Update metadata of the folders before sync to
                // check for any changes or modifications
                DetectChangesonFileSystemReplica(leftPath, filter, options);
                DetectChangesonFileSystemReplica(rightPath, filter, options);

                // Start the 2-way sync
                SyncFileSystemReplicasOneWay(leftPath, rightPath, null, options, false);
                SyncFileSystemReplicasOneWay(rightPath, leftPath, null, options, false);

                return true;
            }
            catch
            {
                MessageBox.Show("Error"); // should call helper to show the message instead
                return false;
            }
        }

        ////////////////////
        // PUBLIC METHODS
        ////////////////////

        // Return folder paths
        public string[] GetPath()
        {
            string[] listOfPaths = new string[2];
            listOfPaths[0] = leftPath;
            listOfPaths[1] = rightPath;
            return listOfPaths;
        }

        /* SHAOQI: MAYBE CAN REMOVE, NOT IN USE ANYMORE
        // Set 2 folder paths and return a boolean to indicate success
        public bool SetPath(string newLeftPath, string newRightPath)
        {
            if (string.IsNullOrEmpty(newLeftPath) || string.IsNullOrEmpty(newRightPath) ||
                !Directory.Exists(newLeftPath) || !Directory.Exists(newRightPath))
            {
                return false;
            }
            else
            {
                leftPath = newLeftPath;
                rightPath = newRightPath;
                return true;
            }
        }
        */

        // Set left folder path
        public string LeftPath
        {
            get { return leftPath; }
            set { leftPath = value; }
        }

        // Set right folder path
        public string RightPath
        {
            get { return rightPath; }
            set { rightPath = value; }
        }

        public void PreSync()
        {
            backgroundWorker2.RunWorkerAsync();
        }
        
        // Do pre sync preparations
        // E.g. check if there is sufficient disk space for sync
        // E.g. count number of changes to be done by the sync framework
        private bool InternalPreSync()
        {
            try
            {
                // Reset all counters before every synchronization
                countChanges = 0;
                countDoneChanges = 0;
                freeDiskSpaceForLeft = 0;
                freeDiskSpaceForRight = 0;
                diskSpaceNeededForLeft = 0;
                diskSpaceNeededForRight = 0;
                isCheckForLeftDone = false;

                // Configure sync options
                FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges |
                    FileSyncOptions.RecycleConflictLoserFiles |
                    FileSyncOptions.RecycleDeletedFiles |
                    FileSyncOptions.RecyclePreviousFileOnUpdates;

                // Configure sync filters
                // Currently, this will exclude shortcut links during sync
                FileSyncScopeFilter filter = new FileSyncScopeFilter();
                filter.FileNameExcludes.Add("*.lnk");

                // Update metadata of the folders before sync to
                // check for any changes or modifications
                DetectChangesonFileSystemReplica(leftPath, filter, options);
                DetectChangesonFileSystemReplica(rightPath, filter, options);

                // Start the 2-way sync
                if (!SyncFileSystemReplicasOneWay(leftPath, rightPath, null, options, true))
                    return false;
                if (!SyncFileSystemReplicasOneWay(rightPath, leftPath, null, options, true))
                    return false;

                return true;
            }
            catch
            {
                MessageBox.Show("Error!"); // should ask helper to show error message instead
                return false;
            }
        }

        // When sync button is click from UI, this method will be called.
        // This will call the backgroundWorker to start doing its work.
        public void StartSync()
        {
            // Start the asynchronous operation.
            backgroundWorker.RunWorkerAsync();
        }

        // Checks if the paths are valid. 
        // One cannot be the subfolder of the other.
        public bool CheckSubFolder()
        {
            return !intelligentManager.IsFolderSubFolder(leftPath, rightPath);
        }

        // Checks if the 2 folders are already sync
        public bool AreFoldersSync()
        {
            if (countChanges == 0)
                return true;
            else
                return false;
        }

        public bool CheckFolderExists(string leftOrRight)
        {
            if (leftOrRight == "left" || leftOrRight == "Left")
            {
                return intelligentManager.IsFolderExists(leftPath);
            }
            else if(leftOrRight == "right" || leftOrRight == "Right")
            {
                return intelligentManager.IsFolderExists(rightPath);
            }
            return false;
        }

        public bool CheckSimilarFolder()
        {
            return intelligentManager.IsFoldersSimilar(leftPath, rightPath);
        }

        public bool CheckRemovableDrive(string path)
        {
            return intelligentManager.IsRemovableDrive(path);
        }
    }
}
