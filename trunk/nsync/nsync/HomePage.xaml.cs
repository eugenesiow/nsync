using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Xml;

namespace nsync
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private string previousTextLeft;
        private ImageSource previousImageLeft;
        private string previousTextRight;
        private ImageSource previousImageRight;
        private bool hasLeftPath = false;
        private bool hasRightPath = false;
        private string settingsFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + nsync.Properties.Resources.settingsFilePath;
        private SyncEngine synchronizer;
        //private TrackBackEngine trackBack;
        private LinearGradientBrush blankOpacityMask;
        private HelperManager helper;
        private Window mainWindow = Application.Current.MainWindow;
        private Settings settingsManager;
        
        private string[] originalFolderPaths;

        private string NULL_STRING = nsync.Properties.Resources.nullString;
        private string ICON_LINK_REMOVABLE_DRIVE = nsync.Properties.Resources.thumbdriveIconPath;
        private string ICON_LINK_FOLDER = nsync.Properties.Resources.folderIconPath;
        private string ICON_LINK_FOLDER_MISSING = nsync.Properties.Resources.folderMissingIconPath;


        public HomePage()
        {
            InitializeComponent();

            //Initialise Helper
            helper = new HelperManager(mainWindow);

            settingsManager = Settings.Instance;
            
            mainWindow.Closing += new CancelEventHandler(mainWindow_Closing);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Create blank opacity mask
            blankOpacityMask = new LinearGradientBrush();
            blankOpacityMask.GradientStops.Add(new GradientStop(Colors.Transparent, 0));
            blankOpacityMask.GradientStops.Add(new GradientStop(Colors.Transparent, 1));
            ImageTeam14Over.OpacityMask = blankOpacityMask;

            //Create SyncEngine object
            synchronizer = new SyncEngine();

            // Initialize folder path array
            originalFolderPaths = new string[10];
            for (int i = 0; i < 10; i++)
            {
                originalFolderPaths[i] = "";
            }

            //trackBack = new TrackBackEngine();

            // Create event handlers for backgroundWorker
            synchronizer.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            synchronizer.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);

            synchronizer.backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);

            //Load the previous folder paths from settings.xml
            LoadFolderPaths();

            //Add event handler to check when main window is moved, move helper window too
            mainWindow.LocationChanged += new EventHandler(mainWindow_LocationChanged);
        }

        void mainWindow_LocationChanged(object sender, EventArgs e)
        {
            helper.UpdateMove(); 
        }

        // Before program is closed, save the last folder paths
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveFolderPaths();
            helper.CloseWindow();
        }

        void mainWindow_Closing(object sender, CancelEventArgs e)
        {
            SaveFolderPaths();
        }

        private void BoxLeft_Drop(object sender, DragEventArgs e)
        {
            hasLeftPath = true;
            e.Handled = true;
            ShowSync();
        }

        private void BoxRight_Drop(object sender, DragEventArgs e)
        {
            hasRightPath = true;
            e.Handled = true;
            ShowSync();
        }

        private void BoxLeft_DragEnter(object sender, DragEventArgs e)
        {
            previousImageLeft = LeftIcon.Source;
            previousTextLeft = LeftText.Text;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                foreach (string i in fileNames)
                {
                    DirectoryInfo dirTemp = new DirectoryInfo(i);
                    FileInfo fileTemp = new FileInfo(i);
                    if (dirTemp.Exists)
                    {
                        LeftText.Text = i;
                    }
                    else
                    {
                        LeftText.Text = fileTemp.DirectoryName;
                    }
                }
            }
            synchronizer.LeftPath = LeftText.Text;

            LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
            ShowRemovableDrives(LeftText.Text, "left");
        }

        private void BoxRight_DragEnter(object sender, DragEventArgs e)
        {
            previousImageRight = RightIcon.Source;
            previousTextRight = RightText.Text;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                foreach (string i in fileNames)
                {
                    DirectoryInfo dirTemp = new DirectoryInfo(i);
                    FileInfo fileTemp = new FileInfo(i);
                    if (dirTemp.Exists)
                    {
                        RightText.Text = i;
                    }
                    else
                    {
                        RightText.Text = fileTemp.DirectoryName;
                    }
                }
            }
            synchronizer.RightPath = RightText.Text;

            RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
            ShowRemovableDrives(RightText.Text, "right");
        }

        private void BoxRight_DragLeave(object sender, DragEventArgs e)
        {
            RightText.Text = previousTextRight;
            synchronizer.RightPath = RightText.Text;
            RightIcon.Source = previousImageRight;
        }

        private void BoxLeft_DragLeave(object sender, DragEventArgs e)
        {
            LeftText.Text = previousTextLeft;
            synchronizer.LeftPath = LeftText.Text;
            LeftIcon.Source = previousImageLeft;
        }

        private void LeftIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string currentPath = NULL_STRING;
            if (hasLeftPath)
            {
                currentPath = LeftText.Text;
            }
            string directoryPath = FolderSelect(currentPath);
            if (directoryPath != NULL_STRING)
            {
                LeftText.Text = directoryPath;

                ShowRemovableDrives(LeftText.Text, "left");

                hasLeftPath = true;
            }
            synchronizer.LeftPath = LeftText.Text;
            ShowSync();
        }

        private void RightIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string currentPath = NULL_STRING;
            if (hasRightPath)
            {
                currentPath = RightText.Text;
            }
            string directoryPath = FolderSelect(currentPath);
            if (directoryPath != NULL_STRING)
            {
                RightText.Text = directoryPath;
                ShowRemovableDrives(RightText.Text, "right");
                hasRightPath = true;
            }
            synchronizer.RightPath = RightText.Text;
            ShowSync();
        }

        private void BoxLeft_MouseEnter(object sender, MouseEventArgs e)
        {
            if (BarMRULeft.IsEnabled == true)
            {
                BarMRULeft.Visibility = Visibility.Visible;
                BarMRURight.Visibility = Visibility.Visible;
            }
        }

        private void BoxLeft_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!LeftListBox.IsVisible)
            {
                BarMRULeft.Visibility = Visibility.Hidden;
                BarMRURight.Visibility = Visibility.Hidden;
            }
        }

        private void BarMRULeft_MouseEnter(object sender, MouseEventArgs e)
        {
            
                BarMRULeft.Opacity = 0.5;
                BarMRULeft.Cursor = Cursors.Hand;
                LeftBarScrollLeft.Visibility = Visibility.Visible;
                LeftBarScrollRight.Visibility = Visibility.Visible;
            
        }

        private void BarMRULeft_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!LeftListBox.IsVisible)
            {
                BarMRULeft.Opacity = 0.2;
                LeftBarScrollLeft.Visibility = Visibility.Hidden;
                LeftBarScrollRight.Visibility = Visibility.Hidden;
            }
        }

        private void BoxRight_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!RightListBox.IsVisible)
            {
                BarMRURight.Visibility = Visibility.Hidden;
                BarMRULeft.Visibility = Visibility.Hidden;
            }
        }

        private void BoxRight_MouseEnter(object sender, MouseEventArgs e)
        {
            if (BarMRURight.IsEnabled == true)
            {
                BarMRURight.Visibility = Visibility.Visible;
                BarMRULeft.Visibility = Visibility.Visible;
            }
        }

        private void BarMRURight_MouseEnter(object sender, MouseEventArgs e)
        {
            
                BarMRURight.Opacity = 0.5;
                BarMRURight.Cursor = Cursors.Hand;
                RightBarScrollLeft.Visibility = Visibility.Visible;
                RightBarScrollRight.Visibility = Visibility.Visible;
            
        }

        private void BarMRURight_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!RightListBox.IsVisible)
            {
                BarMRURight.Opacity = 0.2;
                RightBarScrollLeft.Visibility = Visibility.Hidden;
                RightBarScrollRight.Visibility = Visibility.Hidden;
            }
        }

        private void BarMRURight_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (RightListBox.IsVisible)
            {
                RightListBox.Visibility = Visibility.Hidden;
                LeftListBox.Visibility = Visibility.Hidden;
            }
            else
            {
                RightListBox.Visibility = Visibility.Visible;
                LeftListBox.Visibility = Visibility.Visible;
            }

        }

        private void BarMRULeft_MouseUp(object sender, MouseButtonEventArgs e)
        {            
            if (LeftListBox.IsVisible)
            {
                RightListBox.Visibility = Visibility.Hidden;
                LeftListBox.Visibility = Visibility.Hidden;
            }
            else
            {
                RightListBox.Visibility = Visibility.Visible;
                LeftListBox.Visibility = Visibility.Visible;
            }
        }

        #region User Defined Functions

        private void ShowRemovableDrives(string path, string leftOrRight)
        {
            if (synchronizer.CheckRemovableDrive(path))
            {
                if (leftOrRight == "left" || leftOrRight == "Left")
                {
                    LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_REMOVABLE_DRIVE));
                }
                else if (leftOrRight == "right" || leftOrRight == "Right")
                {
                    RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_REMOVABLE_DRIVE));
                }

            }
        }

        // Checks if folders exist
        private bool FolderCheck()
        {
            bool rightFolderExists = synchronizer.CheckFolderExists("right");
            bool leftFolderExists = synchronizer.CheckFolderExists("left");

            if (!rightFolderExists && !leftFolderExists)
            {
                helper.Show(nsync.Properties.Resources.bothFoldersNotExist, 5, HelperWindow.windowStartPosition.windowTop);
                LeftIcon.Source = RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER_MISSING));
                return false;
            }
            else if (!rightFolderExists)
            {
                helper.Show(nsync.Properties.Resources.rightFolderNotExist, 5, HelperWindow.windowStartPosition.windowTop);
                RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER_MISSING));
                LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
                return false;
            }
            else if (!leftFolderExists)
            {
                helper.Show(nsync.Properties.Resources.leftFolderNotExist, 5, HelperWindow.windowStartPosition.windowTop);
                RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
                LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER_MISSING));
                return false;
            }
            else
            {
                RightIcon.Source = LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
                return true;
            }
        }

        // Checks if folders are similar
        private bool SimilarFolderCheck()
        {
            if (synchronizer.CheckSimilarFolder())
            {
                helper.Show(nsync.Properties.Resources.similarFolders, 5, HelperWindow.windowStartPosition.windowTop);
                return true;
            }
            return false;
        }

        // Checks if one folder is a subfolder of another
        private bool SubFolderCheck()
        {
            if (!synchronizer.CheckSubFolder())
            {
                helper.Show(nsync.Properties.Resources.subfolderOfFolder, 5, HelperWindow.windowStartPosition.windowTop);
                return false;
            }
            return true;
        }

        // Checks if the sync button should appear
        private bool ShowSync()
        {
            helper.HideWindow();

            LabelProgress.Visibility = Visibility.Hidden;
            LabelProgressPercent.Visibility = Visibility.Hidden;

            // Updates the folder icons accordingly first, if the folder path exists in the first place
            if (hasLeftPath)
            {
                LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
                ShowRemovableDrives(LeftText.Text, "left");
            }
            if (hasRightPath)
            {
                RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
                ShowRemovableDrives(RightText.Text, "right");
            }

            // Only if both boxes are filled with folder paths, then we need to check validity
            if (!hasLeftPath || !hasRightPath)
                return false;

            if (!FolderCheck() || SimilarFolderCheck() || !SubFolderCheck())
            {
                ShowRemovableDrives(LeftText.Text, "left");
                ShowRemovableDrives(RightText.Text, "right");
                ButtonSync.Visibility = Visibility.Hidden;
                return false;
            }

            ShowRemovableDrives(LeftText.Text, "left");
            ShowRemovableDrives(RightText.Text, "right");
            ButtonSync.Visibility = Visibility.Visible;
            return true;
        }

        // Save folder paths to settings.xml
        private void SaveFolderPaths()
        {
            // pass to settingsmanager the 2 current folderpaths, if any
            if (hasLeftPath && hasRightPath)
                settingsManager.SaveFolderPaths(LeftText.Text, RightText.Text);
            else
                return;
        }

        private void ReloadFolderPaths()
        {
            LeftListBox.Items.Clear();
            RightListBox.Items.Clear();
            LoadFolderPaths();
        }

        // Load folder paths when program starts running
        private void LoadFolderPaths()
        {
            List<string> folderPaths = settingsManager.LoadFolderPaths();
            int counter;

            if (folderPaths.Count == 0)
                return;

            LeftText.Text = folderPaths[0];
            synchronizer.LeftPath = LeftText.Text;

            RightText.Text = folderPaths[1];
            synchronizer.RightPath = RightText.Text;

            if (LeftText.Text == NULL_STRING)
            {
                LeftText.Text = nsync.Properties.Resources.panelText;
                hasLeftPath = false;
            }
            else
                hasLeftPath = true;

            if (RightText.Text == NULL_STRING)
            {
                RightText.Text = nsync.Properties.Resources.panelText;
                hasRightPath = false;
            }
            else
                hasRightPath = true;
                
            // If first pair of folder paths in settings.xml is already empty, it's guranteed settings.xml is empty. No point trying to load MRU
            if (LeftText.Text == nsync.Properties.Resources.panelText && RightText.Text == nsync.Properties.Resources.panelText)
                return;

            counter = 0;
            // Setup MRU listbox items
            for (int i = 1; i <= 5; i++)
            {
                ListBoxItem listBoxLeft = new ListBoxItem();
                if (folderPaths[i + (i-2)] == NULL_STRING)
                    continue;

                listBoxLeft.Content = ShortenPath(folderPaths[i + (i-2)]);
                listBoxLeft.ToolTip = folderPaths[i + (i-2)];

                originalFolderPaths[counter] = folderPaths[i + (i-2)];
                counter += 2;

                listBoxLeft.MouseUp += new MouseButtonEventHandler(ListBoxLeft_MouseUp);
                listBoxLeft.MouseEnter += new MouseEventHandler(listBoxLeft_MouseEnter);
                listBoxLeft.MouseLeave += new MouseEventHandler(listBoxLeft_MouseLeave);
                listBoxLeft.Tag = i;

                LeftListBox.Items.Add(listBoxLeft);
            }
            counter = 1;

            for (int i = 1; i <= 5; i++)
            {
                ListBoxItem listBoxRight = new ListBoxItem();
                if (folderPaths[i + (i-1)] == NULL_STRING)
                    continue;

                listBoxRight.Content = ShortenPath(folderPaths[i + (i-1)]);
                listBoxRight.ToolTip = folderPaths[i + (i-1)];

                originalFolderPaths[counter] = folderPaths[i + (i-1)];
                counter += 2;

                listBoxRight.MouseUp += new MouseButtonEventHandler(ListBoxRight_MouseUp);
                listBoxRight.MouseEnter += new MouseEventHandler(listBoxRight_MouseEnter);
                listBoxRight.MouseLeave += new MouseEventHandler(listBoxRight_MouseLeave);
                listBoxRight.Tag = i;

                RightListBox.Items.Add(listBoxRight);
            }
            ShowSync();
        }

        private string ShortenPath(string oldPath)
        {
            if (oldPath.Length > 40)
            {
                return oldPath.Substring(0, 28) + "..." + oldPath.Substring(oldPath.Length - 10, 10);
            }
            else
            {
                return oldPath;
            }
        }

        void listBoxRight_MouseLeave(object sender, MouseEventArgs e)
        {
            LeftListBox.SelectedIndex = -1;
        }

        void listBoxLeft_MouseLeave(object sender, MouseEventArgs e)
        {
            RightListBox.SelectedIndex = -1;
        }


        void listBoxLeft_MouseEnter(object sender, MouseEventArgs e)
        {
            ListBoxItem lb = new ListBoxItem();
            lb = (ListBoxItem)sender;
            int index = Convert.ToInt32(lb.Tag);
            RightListBox.SelectedIndex = index - 1;
        }

        void listBoxRight_MouseEnter(object sender, MouseEventArgs e)
        {
            ListBoxItem lb = new ListBoxItem();
            lb = (ListBoxItem)sender;
            int index = Convert.ToInt32(lb.Tag);
            LeftListBox.SelectedIndex = index - 1;
        }

        void ListBoxLeft_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lb = new ListBoxItem();
            lb = (ListBoxItem)e.Source;
            LeftText.Text = originalFolderPaths[Convert.ToInt32(lb.Tag) + (Convert.ToInt32(lb.Tag) - 2)];
            int index = Convert.ToInt32(lb.Tag);
            RightListBox.SelectedIndex = index - 1;
            lb = (ListBoxItem)RightListBox.SelectedItem;
            RightText.Text = originalFolderPaths[Convert.ToInt32(lb.Tag) + (Convert.ToInt32(lb.Tag) - 1)];

            synchronizer.LeftPath = LeftText.Text;
            synchronizer.RightPath = RightText.Text;

            LeftListBox.SelectedIndex = -1;
            LeftListBox.Visibility = Visibility.Hidden;
            RightListBox.Visibility = Visibility.Hidden;

            ShowSync();
        }

        void ListBoxRight_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lb = new ListBoxItem();
            lb = (ListBoxItem)e.Source;
            // Change path label to this one and update synchronizer
            RightText.Text = originalFolderPaths[Convert.ToInt32(lb.Tag) + (Convert.ToInt32(lb.Tag) - 1)];
            int index = Convert.ToInt32(lb.Tag);
            LeftListBox.SelectedIndex = index - 1;
            lb = (ListBoxItem)LeftListBox.SelectedItem;
            LeftText.Text = originalFolderPaths[Convert.ToInt32(lb.Tag) + (Convert.ToInt32(lb.Tag) - 2)];

            synchronizer.LeftPath = LeftText.Text;
            synchronizer.RightPath = RightText.Text;

            RightListBox.SelectedIndex = -1;
            LeftListBox.Visibility = Visibility.Hidden;
            RightListBox.Visibility = Visibility.Hidden;

            ShowSync();
        }

        private string FolderSelect(string originalPath)
        {
            System.Windows.Forms.FolderBrowserDialog FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            FolderDialog.Description = nsync.Properties.Resources.folderExplorerText;

            if (originalPath != NULL_STRING)
            {
                FolderDialog.SelectedPath = originalPath;
            }
            FolderDialog.ShowDialog();
            return FolderDialog.SelectedPath;
        }

        // Executed when user click on the sync button
        private void ButtonSync_Click(object sender, RoutedEventArgs e)
        {
            // check one more time
            // handle the situation when after a sync job is done,
            // user deletes the 2 folders n click sync again
            if (!ShowSync())
                return;

            LeftListBox.Visibility = Visibility.Hidden;
            RightListBox.Visibility = Visibility.Hidden;

            LabelProgress.Visibility = Visibility.Visible;
            LabelProgress.Content = "Preparing folders...";

            EnableInterface(false);

            // Do PreSync Calculations: count how many changes need to be done
            // If not enough disk space, return
            // If enough, continue to start the real sync
            synchronizer.PreSync();
        }

        // Handle the event when progress percentage has changed
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            double percentage = (double)e.ProgressPercentage / 100;

            //Set team14 progress bar
            LinearGradientBrush opacityMask = new LinearGradientBrush();
            opacityMask.StartPoint = new Point(percentage, 0);
            opacityMask.EndPoint = new Point(1, 0);
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 0, 0), 0));
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.2));
            ImageTeam14Over.OpacityMask = opacityMask;

            LabelProgressPercent.Content = e.ProgressPercentage.ToString() + " %";
        }

        private void EnableInterface(bool enableOrDisable)
        {
            double opacityValue;
            bool enableButtons;

            if (enableOrDisable)
            {
                opacityValue = 1;
                enableButtons = true;
                ButtonSync.Visibility = Visibility.Visible;
                SyncingImage.Visibility = Visibility.Hidden;
            }
            else
            {
                enableButtons = false;
                opacityValue = 0.5;
                ButtonSync.Visibility = Visibility.Hidden;
                SyncingImage.Visibility = Visibility.Visible;
            }

            //Enable/Disable the interface
            BoxLeft.IsEnabled = BoxRight.IsEnabled = ButtonSync.IsEnabled = enableButtons;
            BarMRULeft.IsEnabled = BarMRURight.IsEnabled = enableButtons;
            Button ButtonClose = (Button)mainWindow.FindName("ButtonClose");
            ButtonClose.IsEnabled = enableButtons;

            //Enable/Disable the scroller
            Button ButtonSideTabLeft = (Button)mainWindow.FindName("ButtonSideTabLeft");
            ButtonSideTabLeft.IsEnabled = enableButtons;
            Button ButtonSideTabRight = (Button)mainWindow.FindName("ButtonSideTabRight");
            ButtonSideTabRight.IsEnabled = enableButtons;

            //Enable/Disable the dotmenu
            Button ButtonPageSettings = (Button)mainWindow.FindName("ButtonPageSettings");
            ButtonPageSettings.IsEnabled = enableButtons;
            Button ButtonPageHome = (Button)mainWindow.FindName("ButtonPageHome");
            Button ButtonPageBackTrack = (Button)mainWindow.FindName("ButtonPageBackTrack");
            ButtonPageBackTrack.IsEnabled = enableButtons;

            //Set Opacity
            BoxLeft.Opacity = BoxRight.Opacity = opacityValue;
            ButtonSideTabLeft.Opacity = ButtonSideTabRight.Opacity = opacityValue;
            ButtonPageSettings.Opacity = ButtonPageHome.Opacity = ButtonPageBackTrack.Opacity = opacityValue;
        }

        // Handle the event when sync is completed
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableInterface(true);

            if (!(bool)e.Result)
            {
                LabelProgress.Content = "Error detected";
                LabelProgressPercent.Visibility = Visibility.Hidden;
                return;
            }

            if (synchronizer.AreFoldersSync())
            {
                ImageTeam14Over.OpacityMask = blankOpacityMask;

                LabelProgress.Content = "Sync completed";
                LabelProgressPercent.Content = "100 %";
                helper.Show(nsync.Properties.Resources.synchronizedFolders, 5, HelperWindow.windowStartPosition.windowTop);
                return;
            }

            ImageTeam14Over.OpacityMask = blankOpacityMask;

            // When all sync job done, save the folder pairs to MR and settings.xml
            SaveFolderPaths();
            ReloadFolderPaths();

            helper.Show(nsync.Properties.Resources.syncComplete, 5, HelperWindow.windowStartPosition.windowTop);
            LabelProgress.Visibility = Visibility.Visible;
            LabelProgressPercent.Visibility = Visibility.Visible;

            LabelProgress.Content = "Sync completed";
            LabelProgressPercent.Content = "100 %";
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!(bool) e.Result)
            {
                EnableInterface(true);
                helper.Show(nsync.Properties.Resources.insufficientDiskSpace, 5, HelperWindow.windowStartPosition.windowTop);
                LabelProgress.Visibility = Visibility.Hidden;
                LabelProgressPercent.Visibility = Visibility.Hidden;
                ButtonSync.Visibility = Visibility.Hidden;
                return;
            }

            EnableInterface(false);

            LabelProgress.Content = "Syncing folders...";
            LabelProgressPercent.Visibility = Visibility.Visible;
            LabelProgressPercent.Content = "0 %";
            //if (!synchronizer.AreFoldersSync()) trackBack.BackupFolders(LeftText.Text, RightText.Text);
            synchronizer.StartSync();
        }

        #endregion

    }
}
