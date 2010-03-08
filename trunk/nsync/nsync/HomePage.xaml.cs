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
        private ImageSource previousImgLeft;
        private string previousTextRight;
        private ImageSource previousImgRight;
        private bool hasLeftPath = false;
        private bool hasRightPath = false;
        private string settingsFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/settings.xml";
        private SyncEngine synchronizer;
        private TrackBackEngine trackBack;
        private LinearGradientBrush blankOpacityMask;
        private HelperManager helper;
        private Window mainWindow = Application.Current.MainWindow;

        private const string ICON_LINK_REMOVABLE_DRIVE = "pack://siteoforigin:,,,/Resources/Icons/removabledrive.png";
        private const string ICON_LINK_FOLDER = "pack://siteoforigin:,,,/Resources/Icons/folder.png";

        public HomePage()
        {
            InitializeComponent();

            //Initialise Helper
            helper = new HelperManager(mainWindow);

            mainWindow.Closing += new CancelEventHandler(mainWindow_Closing);
        }

        void mainWindow_Closing(object sender, CancelEventArgs e)
        {
            SaveFolderPaths();
        }

        private void BoxLeft_Drop(object sender, DragEventArgs e)
        {
            hasLeftPath = true;
            e.Handled = true;
            showSync();
        }

        private void BoxRight_Drop(object sender, DragEventArgs e)
        {
            hasRightPath = true;
            e.Handled = true;
            showSync();
        }

        private void BoxLeft_DragEnter(object sender, DragEventArgs e)
        {
            previousImgLeft = LeftIcon.Source;
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
            synchronizer.SetPath1(LeftText.Text);

            bool isRemovableDrive = CheckDriveType(LeftText.Text);

            if (isRemovableDrive)
            {
                LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_REMOVABLE_DRIVE));
            }
            else
            {
                LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
            }
        }

        private void BoxRight_DragEnter(object sender, DragEventArgs e)
        {
            previousImgRight = RightIcon.Source;
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
            synchronizer.SetPath2(RightText.Text);

            bool isRemovableDrive = CheckDriveType(RightText.Text);

            if (isRemovableDrive)
            {
                RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_REMOVABLE_DRIVE));
            }
            else
            {
                RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
            }
        }

        private void BoxRight_DragLeave(object sender, DragEventArgs e)
        {
            RightText.Text = previousTextRight;
            synchronizer.SetPath2(RightText.Text);
            RightIcon.Source = previousImgRight;
        }

        private void BoxLeft_DragLeave(object sender, DragEventArgs e)
        {
            LeftText.Text = previousTextLeft;
            synchronizer.SetPath1(LeftText.Text);
            LeftIcon.Source = previousImgLeft;
        }

        private void LeftIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string currentPath = "";
            if (hasLeftPath)
            {
                currentPath = LeftText.Text;
            }
            string directoryPath = FolderSelect(currentPath);
            if (directoryPath != "")
            {
                LeftText.Text = directoryPath;

                bool isRemovableDrive = CheckDriveType(LeftText.Text);

                if (isRemovableDrive)
                {
                    LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_REMOVABLE_DRIVE));
                }
                else
                {
                    LeftIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
                }

                hasLeftPath = true;
            }
            synchronizer.SetPath1(LeftText.Text);
            showSync();
        }

        private void RightIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string currentPath = "";
            if (hasRightPath)
            {
                currentPath = RightText.Text;
            }
            string directoryPath = FolderSelect(currentPath);
            if (directoryPath != "")
            {
                RightText.Text = directoryPath;
                bool isRemovableDrive = CheckDriveType(RightText.Text);

                if (isRemovableDrive)
                {
                    RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_REMOVABLE_DRIVE));
                }
                else
                {
                    RightIcon.Source = new BitmapImage(new Uri(ICON_LINK_FOLDER));
                }
                hasRightPath = true;
            }
            synchronizer.SetPath2(RightText.Text);
            showSync();
        }

        #region User Defined Functions

        //Checks if drive is logical or removable
        private bool CheckDriveType(string path)
        {
            ManagementObjectSearcher mosDisks = new ManagementObjectSearcher(@"SELECT * FROM Win32_LogicalDisk WHERE DriveType=2"); //Finds all removable drives

            foreach (ManagementObject moDisk in mosDisks.Get())
            {
                if (moDisk["DeviceID"].ToString() == Directory.GetDirectoryRoot(path).Remove(2))
                {
                    return true;
                }
            }

            return false;
        }

        // Checks if the sync button should appear
        private bool showSync()
        {
            progressLabel.Visibility = Visibility.Hidden;
            progressPercentLabel.Visibility = Visibility.Hidden;

            bool rightFolderExists = Directory.Exists(synchronizer.GetRightPath());
            bool leftFolderExists = Directory.Exists(synchronizer.GetLeftPath());

            //Show the Sync Button
            if (hasLeftPath && hasRightPath)
            {
                if (!rightFolderExists || !leftFolderExists)
                {
                    if (!rightFolderExists && !leftFolderExists)
                    {
                        helper.Show("Both the selected folders do not exist.", 5, HelperWindow.windowStartPosition.windowTop);
                        LeftIcon.Source = RightIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder_missing.png"));
                    }
                    else if (!rightFolderExists)
                    {
                        helper.Show("The right folder does not exist.", 5, HelperWindow.windowStartPosition.windowTop);
                        RightIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder_missing.png"));
                    }
                    else
                    {
                        helper.Show("The left folder does not exist.", 5, HelperWindow.windowStartPosition.windowTop);
                        LeftIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder_missing.png"));
                    }
                    BtnSync.Visibility = Visibility.Hidden;
                    return false;
                }

                //Intelligent Path Management
                if (LeftText.Text == RightText.Text)
                {
                    helper.Show("The left and right folders cannot be similar.", 5, HelperWindow.windowStartPosition.windowTop);
                    BtnSync.Visibility = Visibility.Hidden;
                    return false;
                }

                //Another Intelligent Path Management
                if (!synchronizer.IsPathValid())
                {
                    helper.Show("Folders that are to be synchronized cannot be a subfolder of each other.", 5, HelperWindow.windowStartPosition.windowTop);
                    BtnSync.Visibility = Visibility.Hidden;
                    return false;
                }

                BtnSync.Visibility = Visibility.Visible;
                return true;
            }
            else
            {
                BtnSync.Visibility = Visibility.Hidden;
                return false;
            }
        }

        // Save folder paths to settings.xml
        private void SaveFolderPaths()
        {
            XmlTextWriter textWriter = new XmlTextWriter(settingsFile, null);
            textWriter.Formatting = Formatting.Indented;
            textWriter.WriteStartDocument();
            string leftPath = "";
            string rightPath = "";

            if (hasLeftPath)
                leftPath = LeftText.Text;
            if (hasRightPath)
                rightPath = RightText.Text;

            //Start Root
            textWriter.WriteStartElement("nsync");
            
            //Write last opened information
            textWriter.WriteStartElement("MRU");

            textWriter.WriteStartElement("left");
            textWriter.WriteString(leftPath);
            textWriter.WriteEndElement();

            textWriter.WriteStartElement("right");
            textWriter.WriteString(rightPath);
            textWriter.WriteEndElement();

            //End last opened information
            textWriter.WriteEndElement();

            //End Root
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();

            textWriter.Close();
        }

        // Load folder paths when program starts running
        private void LoadFolderPaths()
        {
            if (File.Exists(settingsFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(settingsFile);
                XmlElement root = doc.DocumentElement;

                //Load MRU Information
                XmlNode gamenode = root.SelectSingleNode("//MRU");

                LeftText.Text = gamenode["left"].InnerText;
                synchronizer.SetPath1(LeftText.Text); //NEED TO CHANGE: CODING GUIDELINES

                if (LeftText.Text == "")// || !Directory.Exists(LeftText.Text))
                    LeftText.Text = nsync.Properties.Resources.PanelText;
                else
                {
                    bool isRemovableDrive = CheckDriveType(LeftText.Text);

                    if (isRemovableDrive)
                    {
                        LeftIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/removabledrive.png"));
                    }
                    else
                    {
                        LeftIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder.png"));
                    }
                    hasLeftPath = true;
                }

                RightText.Text = gamenode["right"].InnerText;
                synchronizer.SetPath2(RightText.Text);

                if (RightText.Text == "") //|| !Directory.Exists(RightText.Text))
                    RightText.Text = nsync.Properties.Resources.PanelText;
                else
                {
                    bool isRemovableDrive = CheckDriveType(RightText.Text);

                    if (isRemovableDrive)
                    {
                        RightIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/removabledrive.png"));
                    }
                    else
                    {
                        RightIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder.png"));
                    }
                    hasRightPath = true;
                }

            }
            showSync();
        }

        private string FolderSelect(string originalPath)
        {
            System.Windows.Forms.FolderBrowserDialog FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            FolderDialog.Description = nsync.Properties.Resources.FolderExplorerText;

            if (originalPath != "")
            {
                FolderDialog.SelectedPath = originalPath;
            }
            FolderDialog.ShowDialog();
            return FolderDialog.SelectedPath;
        }

        // Executed when user click on the sync button
        private void BtnSync_Click(object sender, RoutedEventArgs e)
        {
            /*this.Opacity = 0.5;
            SyncWindow WndSync = new SyncWindow();
            WndSync.Owner = this;
            WndSync.ShowDialog();
            this.Opacity = 1;*/

            progressLabel.Visibility = Visibility.Visible;
            progressPercentLabel.Visibility = Visibility.Visible;

            //Do PreSync Calculations: count how many changes need to be done
            if (!synchronizer.PreSync())
            {
                helper.Show("Insufficient disk space", 5, HelperWindow.windowStartPosition.windowTop);
                BtnSync.Visibility = Visibility.Hidden;
                return;
            }

            //Change the Opacity
            BoxLeft.Opacity = BoxRight.Opacity = 0.5;

            //Disable the interface
            BoxLeft.IsEnabled = false;
            BoxRight.IsEnabled = false;
            Button BtnClose = (Button)mainWindow.FindName("BtnClose");
            BtnClose.IsEnabled = false;
            BtnSync.IsEnabled = false;
            BtnSync.Visibility = Visibility.Hidden;
            SyncingImage.Visibility = Visibility.Visible;

            progressPercentLabel.Content = "0 %";
            if (!synchronizer.AreFoldersSync()) trackBack.BackupFolders(LeftText.Text, RightText.Text);
            synchronizer.StartSync();
        }



        // Handle the event when progress percentage has changed
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            double percentage = (double)e.ProgressPercentage / 100;

            //Set team14 progress bar
            LinearGradientBrush opacityMask = new LinearGradientBrush();
            opacityMask.StartPoint = new Point(percentage, 0);
            opacityMask.EndPoint = new Point(1, 0);
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 0, 0), 0));
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.2));
            ImgTeam14over.OpacityMask = opacityMask;

            progressPercentLabel.Content = e.ProgressPercentage.ToString() + " %";
        }

        // Handle the event when sync is completed
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            //Restore Opacity
            BoxLeft.Opacity = BoxRight.Opacity = 1;

            //Enable the interface
            BoxLeft.IsEnabled = BoxRight.IsEnabled = BtnSync.IsEnabled =true;
            Button BtnClose = (Button)mainWindow.FindName("BtnClose");
            BtnClose.IsEnabled = true;
            BtnSync.Visibility = Visibility.Visible;
            SyncingImage.Visibility = Visibility.Hidden;

            if (!(bool)e.Result)
            {
                progressPercentLabel.Content = "Failed";
                return;
            }

            if (synchronizer.AreFoldersSync())
            {
                ImgTeam14over.OpacityMask = blankOpacityMask;

                progressPercentLabel.Content = "100 %";
                helper.Show("Folders are already synchronized.", 5, HelperWindow.windowStartPosition.windowTop);
                return;
            }

            ImgTeam14over.OpacityMask = blankOpacityMask;

            progressPercentLabel.Content = "100 %";
            helper.Show("Sync done!", 5, HelperWindow.windowStartPosition.windowTop);
        }

        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Blank opacity mask
            blankOpacityMask = new LinearGradientBrush();
            blankOpacityMask.GradientStops.Add(new GradientStop(Colors.Transparent, 0));
            blankOpacityMask.GradientStops.Add(new GradientStop(Colors.Transparent, 1));
            ImgTeam14over.OpacityMask = blankOpacityMask;

            //Create SyncEngine object
            synchronizer = new SyncEngine();

            trackBack = new TrackBackEngine();

            // Create event handlers for backgroundWorker
            synchronizer.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            synchronizer.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            //Load the previous folder paths from settings.xml
            LoadFolderPaths();
        }

        // Before program is closed, save the last folder paths
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveFolderPaths();
        }

        private void BoxLeft_MouseEnter(object sender, MouseEventArgs e)
        {
            MRUIconLeft.Visibility = Visibility.Visible;
        }

        private void BoxLeft_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!LeftListBox.IsVisible)
            {
                MRUIconLeft.Visibility = Visibility.Hidden;
            }
        }

        private void MRUIconLeft_MouseEnter(object sender, MouseEventArgs e)
        {
            MRUIconLeft.Opacity = 0.5;
            MRUIconLeft.Cursor = Cursors.Hand;
            LeftBarScrollLeft.Visibility = Visibility.Visible;
            LeftBarScrollRight.Visibility = Visibility.Visible;
        }

        private void MRUIconLeft_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!LeftListBox.IsVisible)
            {
                MRUIconLeft.Opacity = 0.2;
                LeftBarScrollLeft.Visibility = Visibility.Hidden;
                LeftBarScrollRight.Visibility = Visibility.Hidden;
            }
        }

        private void BoxRight_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!RightListBox.IsVisible)
            {
                MRUIconRight.Visibility = Visibility.Hidden;
            }
        }

        private void BoxRight_MouseEnter(object sender, MouseEventArgs e)
        {
            MRUIconRight.Visibility = Visibility.Visible;
        }

        private void MRUIconRight_MouseEnter(object sender, MouseEventArgs e)
        {
            MRUIconRight.Opacity = 0.5;
            MRUIconRight.Cursor = Cursors.Hand;
            RightBarScrollLeft.Visibility = Visibility.Visible;
            RightBarScrollRight.Visibility = Visibility.Visible;
        }

        private void MRUIconRight_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!RightListBox.IsVisible)
            {
                MRUIconRight.Opacity = 0.2;
                RightBarScrollLeft.Visibility = Visibility.Hidden;
                RightBarScrollRight.Visibility = Visibility.Hidden;
            }
        }

        private void MRUIconRight_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Can move this code to page load
            RightListBox.Height = RightListBox.Items.Count * 20;

            if (RightListBox.IsVisible)
            {
                RightListBox.Visibility = Visibility.Hidden;
            }
            else
            {
                RightListBox.Visibility = Visibility.Visible;
            }

        }

        private void MRUIconLeft_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Can move this code to page load
            LeftListBox.Height = LeftListBox.Items.Count * 20;

            if (LeftListBox.IsVisible)
            {
                LeftListBox.Visibility = Visibility.Hidden;
            }
            else
            {
                LeftListBox.Visibility = Visibility.Visible;
            }
        }

    }
}
