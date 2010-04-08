﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;
using Microsoft.Synchronization.MetadataStorage;
using System.Windows;

namespace nsync
{
    //CLASS FOR PREVIEW
    class Preview
    {
        #region Class Variables
        public System.ComponentModel.BackgroundWorker backgroundWorkerForPreview;
        public System.ComponentModel.BackgroundWorker backgroundWorkerForSummary;
        private List<FileData> fileData;
        private string leftPath;
        private string rightPath;
        private List<string> excludeTypeList = new List<string>();

        private readonly string TRACKBACK_FOLDER_NAME = "_nsync_trackback";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for Preview
        /// </summary>
        public Preview()
        {
            backgroundWorkerForPreview = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerForPreview.DoWork += new DoWorkEventHandler(backgroundWorkerForPreview_DoWork);
            backgroundWorkerForPreview.WorkerReportsProgress = true;

            backgroundWorkerForSummary = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerForSummary.DoWork += new DoWorkEventHandler(backgroundWorkerForSummary_DoWork);
            backgroundWorkerForSummary.WorkerReportsProgress = true;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Setter and Getter method for left folder path
        /// </summary>
        public string LeftPath
        {
            get { return leftPath; }
            set { leftPath = value; }
        }

        /// <summary>
        /// Setter and Getter method for right folder path
        /// </summary>
        public string RightPath
        {
            get { return rightPath; }
            set { rightPath = value; }
        }

        /// <summary>
        /// Setter and Getter method exclude list which contains file types
        /// </summary>
        public List<string> ExcludeTypeList
        {
            get { return excludeTypeList; }
            set { excludeTypeList = value; }
        }

        /// <summary>
        /// Gets backgroundWorkerForPreview to do synchronization preparations
        /// </summary>
        public void PreviewSync()
        {
            // Start the asynchronous operation.
            backgroundWorkerForPreview.RunWorkerAsync();
        }

        /// <summary>
        /// Gets backgroundWorkerForSummary to do synchronization preparations
        /// </summary>
        public void SummarySync()
        {
            // Start the asynchronous operation.
            backgroundWorkerForSummary.RunWorkerAsync();
        }

        /// <summary>
        /// Does sync operation and returns list of file data
        /// </summary>
        /// <returns>Returns a list of file data objects</returns>
        public List<FileData> GetData()
        {
            return fileData;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// This method is called when backgroundWorkerForPreview is called to start working
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerForPreview_DoWork(object sender, DoWorkEventArgs e)
        {
            InternalPreviewSync();
        }

        /// <summary>
        /// This method is called when backgroundWorkerForSummary is called to start working
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerForSummary_DoWork(object sender, DoWorkEventArgs e)
        {
            InternalPreviewSync();
        }

        /// <summary>
        /// Does Sync operation to store change events into a list of FileData objects
        /// </summary>
        private void InternalPreviewSync()
        {
            try
            {
                fileData = new List<FileData>();

                // Configure sync options
                FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges |
                    FileSyncOptions.RecycleConflictLoserFiles |
                    FileSyncOptions.RecycleDeletedFiles |
                    FileSyncOptions.RecyclePreviousFileOnUpdates;


                // Configure sync filters
                FileSyncScopeFilter filter = new FileSyncScopeFilter();
                for (int i = 0; i < excludeTypeList.Count; i++)
                {
                    filter.FileNameExcludes.Add("*" + excludeTypeList[i]);
                }

                filter.SubdirectoryExcludes.Add(TRACKBACK_FOLDER_NAME);

                // Update metadata of the folders before sync to
                // check for any changes or modifications
                DetectChangesonFileSystemReplica(leftPath, filter, options);
                DetectChangesonFileSystemReplica(rightPath, filter, options);

                // Start the 2-way sync
                SyncFileSystemReplicasOneWay(leftPath, rightPath, null, options);
                SyncFileSystemReplicasOneWay(rightPath, leftPath, null, options);
            }
            catch (System.UnauthorizedAccessException exceptionError)
            {
                throw exceptionError;
            }
            catch (Exception exceptionError)
            {
                throw exceptionError;
            }
        }

        /// <summary>
        /// Detect the changes done to the folder
        /// <para>Updates the metadata</para>
        /// </summary>
        /// <param name="replicaRootPath">This parameter is the folder path to be checked</param>
        /// <param name="filter">This parameter is the filter which will be used during synchronization</param>
        /// <param name="options">This parameter holds the synchronization options</param>
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

        private void SyncFileSystemReplicasOneWay(string sourcePath, string destPath,
        FileSyncScopeFilter filter, FileSyncOptions options)
        {
            FileSyncProvider sourceProvider = null;
            FileSyncProvider destProvider = null;

            try
            {
                sourceProvider = new FileSyncProvider(sourcePath, filter, options);
                destProvider = new FileSyncProvider(destPath, filter, options);

                sourceProvider.PreviewMode = true;
                destProvider.PreviewMode = true;


                destProvider.Configuration.ConflictResolutionPolicy = ConflictResolutionPolicy.ApplicationDefined;
                SyncCallbacks destinationCallBacks = destProvider.DestinationCallbacks;
                destinationCallBacks.ItemConflicting += new EventHandler<ItemConflictingEventArgs>(OnItemConflicting);
                destinationCallBacks.ItemConstraint += new EventHandler<ItemConstraintEventArgs>(OnItemConstraint);


                destProvider.ApplyingChange += new EventHandler<ApplyingChangeEventArgs>(OnApplyingChange);


                SyncOrchestrator agent = new SyncOrchestrator();
                agent.LocalProvider = sourceProvider;
                agent.RemoteProvider = destProvider;
                agent.Direction = SyncDirectionOrder.Upload;

                agent.Synchronize();
            }
            finally
            {
                if (sourceProvider != null) sourceProvider.Dispose();
                if (destProvider != null) destProvider.Dispose();
            }
        }
        /// <summary>
        /// This method is called when changes are going to be done to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnApplyingChange(object sender, ApplyingChangeEventArgs args)
        {
            FileSyncProvider provider = null;
            string rootPath = null;
            try
            {
                provider = (FileSyncProvider)sender;
                rootPath = provider.RootDirectoryPath;
                switch (args.ChangeType)
                {
                    case ChangeType.Delete:
                        fileData.Add(new FileData(rootPath, args.CurrentFileData.Name, args.CurrentFileData.RelativePath, Changes.Delete,
                            args.CurrentFileData.IsDirectory));
                        break;

                    case ChangeType.Create:
                        fileData.Add(new FileData(rootPath, args.NewFileData.Name, args.NewFileData.RelativePath, Changes.Create,
                            args.NewFileData.IsDirectory));
                        break;

                    case ChangeType.Update:
                        fileData.Add(new FileData(rootPath, args.NewFileData.Name, args.NewFileData.RelativePath, Changes.Update,
                            args.NewFileData.IsDirectory));
                        break;
                    case ChangeType.Rename:
                        fileData.Add(new FileData(rootPath, args.NewFileData.Name, args.NewFileData.RelativePath, Changes.Rename,
                            args.NewFileData.IsDirectory));
                        break;
                }
            }
            finally
            {
                provider.Dispose();
            }
        }

        /// <summary>
        /// This method is called when there are conflicting items during synchronization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnItemConflicting(object sender, ItemConflictingEventArgs args)
        {
            // Currently, latest change wins
            args.SetResolutionAction(ConflictResolutionAction.Merge);
        }

        /// <summary>
        /// This method is called when there are constraint items during synchronization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnItemConstraint(object sender, ItemConstraintEventArgs args)
        {
            args.SetResolutionAction(ConstraintConflictResolutionAction.Merge);
        }
        #endregion
    }
}