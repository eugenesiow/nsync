using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;
using System.Windows.Forms;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;

namespace nsync
{
    public delegate void ProgressBarHandler(object o, ProgressBarEventArgs e);

    public class ProgressBarEventArgs : EventArgs
    {
        public readonly int TheNumber;

        public ProgressBarEventArgs(int num)
        {
            TheNumber = num;
        }

    }

    class SyncEngine
    {
        ////////////////////
        // Code for ProgressBar event
        ////////////////////
        public event ProgressBarHandler EventProgress;
        public void OnEventProgress(ProgressBarEventArgs e)
        {
            if (EventProgress != null)
                EventProgress(new object(), e);
        }   

        ////////////////////
        // Private variables
        ////////////////////

        private ulong freeDiskSpace = 0;
        private ulong diskSpaceNeeded = 0;
        private string path1;
        private string path2;
        private static int countDoneChanges = 0;
        private static int countChanges = 0;

        ////////////////////
        // Private methods
        ////////////////////
        private ulong GetFreeDiskSpaceInBytes(string drive)
        {
            ManagementObject disk = new ManagementObject(
            "win32_logicaldisk.deviceid=\"" + drive + ":\"");
            disk.Get();
            return (ulong)disk["FreeSpace"];
        }

        private string ConvertBytes(ulong amount)
        {
            return (Convert.ToUInt64(amount.ToString()) / 1000000).ToString() + " MB";
        }

        private static void DetectChangesonFileSystemReplica(
            string replicaRootPath,
            FileSyncScopeFilter filter, FileSyncOptions options)
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

        private bool SyncFileSystemReplicasOneWay(string sourcePath, string destPath,
            FileSyncScopeFilter filter, FileSyncOptions options, bool isPreview)
        {
            FileSyncProvider sourceProvider = null;
            FileSyncProvider destProvider = null;

            try
            {
                sourceProvider = new FileSyncProvider(sourcePath, filter, options);
                destProvider = new FileSyncProvider(destPath, filter, options);

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

        private static void OnItemConstraint(object sender, ItemConstraintEventArgs args)
        {
            MessageBox.Show("constraint");
            args.SetResolutionAction(ConstraintConflictResolutionAction.RenameSource);
        }

        private void OnAppliedChange(object sender, AppliedChangeEventArgs args)
        {
            
            countDoneChanges++;
            ProgressBarEventArgs e1 = new ProgressBarEventArgs((int)((double)countDoneChanges/countChanges*100));
            OnEventProgress(e1);

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
        
        private void OnApplyingChange(object sender, ApplyingChangeEventArgs args)
        {
            countChanges++;
            diskSpaceNeeded += (ulong) args.NewFileData.Size;
        }
        
        ////////////////////
        // Public methods
        ////////////////////

        public void Reset()
        {
            countDoneChanges = 0;
            countChanges = 0;
            freeDiskSpace = 0;
            diskSpaceNeeded = 0;
        }

        // Return paths
        public string[] GetPath()
        {
            string[] listOfPaths = new string[2];
            listOfPaths[0] = path1;
            listOfPaths[1] = path2;
            return listOfPaths;
        }

        // Set path and return a boolean to indicate success
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

        // Start the sync process
        public bool StartSync()
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

                ProgressBarEventArgs e1 = new ProgressBarEventArgs(100);
                OnEventProgress(e1);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public bool PreSync()
        {
            try
            {
                countChanges = 0;
                countDoneChanges = 0;

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
                if(!SyncFileSystemReplicasOneWay(path1, path2, null, options, true))
                    return false;
                if(!SyncFileSystemReplicasOneWay(path2, path1, null, options, true))
                    return false;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}
