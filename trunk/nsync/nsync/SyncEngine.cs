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

        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ulong freeDiskSpace = 0;
        private ulong diskSpaceNeeded = 0;
        private string path1;
        private string path2;
        private static int countDoneChanges = 0;
        private static int countChanges = 0;

        ////////////////////
        // CONSTRUCTOR
        ////////////////////

        public SyncEngine()
        {
            // Set up the BackgroundWorker object by 
            // attaching event handlers.
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.WorkerReportsProgress = true;
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

                freeDiskSpace = GetFreeDiskSpaceInBytes(sourcePath.Substring(0, 1));

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
            return diskSpaceNeeded < freeDiskSpace;
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
            // This method will raise an event to the backgroundWorker via backgroundWorker1_ProgressChanged
            backgroundWorker1.ReportProgress((int)((double)countDoneChanges / countChanges * 100));

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
            diskSpaceNeeded += (ulong) args.NewFileData.Size;
        }

        // This method is called when the backgroundWorker starts working.
        // The 'real' work to be done is called from here.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker = sender as BackgroundWorker;
            // e.Result will be available to RunWorkerCompletedEventArgs later
            // when the work assigned is completed.
            e.Result = InternalStartSync();
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
                DetectChangesonFileSystemReplica(path1, filter, options);
                DetectChangesonFileSystemReplica(path2, filter, options);

                // Start the 2-way sync
                SyncFileSystemReplicasOneWay(path1, path2, null, options, false);
                SyncFileSystemReplicasOneWay(path2, path1, null, options, false);

                return true;
            }
            catch
            {
                MessageBox.Show("Error");
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
            listOfPaths[0] = path1;
            listOfPaths[1] = path2;
            return listOfPaths;
        }

        // Set 2 folder paths and return a boolean to indicate success
        public bool SetPath(string p1, string p2)
        {
            if (string.IsNullOrEmpty(p1) || string.IsNullOrEmpty(p2) ||
                !Directory.Exists(p1) || !Directory.Exists(p2))
            {
                return false;
            }
            else
            {
                path1 = p1;
                path2 = p2;
                return true;
            }
        }

        // Get left folder path
        public string GetLeftPath()
        {
            if (path1 != null)
                return path1;
            else
                return null;
        }

        // Get left folder path
        public string GetRightPath()
        {
            if (path2 != null)
                return path2;
            else
                return null;
        }

        // Set left folder path
        public bool SetPath1(string p1)
        {
            if (string.IsNullOrEmpty(p1) || !Directory.Exists(p1))
            {
                return false;
            }
            else
            {
                path1 = p1;
                return true;
            }
        }

        // Set right folder path
        public bool SetPath2(string p2)
        {
            if (string.IsNullOrEmpty(p2) || !Directory.Exists(p2))
            {
                return false;
            }
            else
            {
                path2 = p2;
                return true;
            }
        }

        // Do pre sync preparations
        // E.g. check if there is sufficient disk space for sync
        // E.g. count number of changes to be done by the sync framework
        public bool PreSync()
        {
            try
            {
                // Reset all counters before every synchronization
                countChanges = 0;
                countDoneChanges = 0;
                freeDiskSpace = 0;
                diskSpaceNeeded = 0;

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
                DetectChangesonFileSystemReplica(path1, filter, options);
                DetectChangesonFileSystemReplica(path2, filter, options);

                // Start the 2-way sync
                if (!SyncFileSystemReplicasOneWay(path1, path2, null, options, true))
                    return false;
                if (!SyncFileSystemReplicasOneWay(path2, path1, null, options, true))
                    return false;

                return true;
            }
            catch
            {
                MessageBox.Show("Error");
                return false;
            }
        }

        // When sync button is click from UI, this method will be called.
        // This will call the backgroundWorker to start doing its work.
        public void StartSync()
        {
            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync();
        }

        // Checks if the paths are valid. 
        // One cannot be the subfolder of the other.
        public bool IsPathValid()
        {
            DirectoryInfo leftPathDir = new DirectoryInfo(path1);
            DirectoryInfo rightPathDir = new DirectoryInfo(path2);
            string leftPathDirParent;
            string rightPathDirParent;

            if(leftPathDir.Parent != null)
                leftPathDirParent = leftPathDir.Parent.FullName.ToString();
            else
                leftPathDirParent = leftPathDir.FullName.ToString();
            if(rightPathDir.Parent != null)
                rightPathDirParent = rightPathDir.Parent.FullName.ToString();
            else
                rightPathDirParent = rightPathDir.FullName.ToString();

            if (leftPathDirParent.Contains(path2) || rightPathDirParent.Contains(path1))
                return false;
            else
                return true;
        }

        // Checks if the 2 folders are already sync
        public bool AreFoldersSync()
        {
            if (countChanges == 0)
                return true;
            else
                return false;
        }
    }
}
