using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace nsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private string previousTextLeft;
        private ImageSource previousImgLeft;
        private string previousTextRight;
        private ImageSource previousImgRight;
        private bool hasLeftPath = false;
        private bool hasRightPath = false;
        private string settingsFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/settings.xml";
        private HelperManager helper;
        private SyncEngine synchronizer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Create SyncEngine object
            synchronizer = new SyncEngine();

            //Create event handler for progress bar
            synchronizer.EventProgress += new ProgressBarHandler(ShowOnScreen);
        
            //Initialise Helper
            helper = new HelperManager(this);

            //Load the previous folder paths from settings.xml
            LoadFolderPaths(); 
        }

        public void ShowOnScreen(object o, ProgressBarEventArgs e)
        {
            label2.Content = e.TheNumber.ToString() + " %";
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
            LeftIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder.png"));
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
            RightIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder.png"));
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

        private void BtnMinimise_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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
                LeftIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder.png"));
                hasLeftPath = true;
                showSync();
            }
            synchronizer.SetPath1(LeftText.Text);
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
                RightIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder.png"));
                hasRightPath = true;
                showSync();
            }
            synchronizer.SetPath2(RightText.Text);
        }

        #region User Defined Functions

        private bool showSync()
        {
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;

            //Show the Sync Button
            if (hasLeftPath && hasRightPath)
            {
                if (!Directory.Exists(RightText.Text) || !Directory.Exists(LeftText.Text))
                {
                    helper.Show("The selected folder path(s) do not exist.", 5);
                    BtnSync.Visibility = Visibility.Hidden;
                    return false;
                }

                //Intelligent Path Managment
                if (LeftText.Text == RightText.Text)
                {
                    helper.Show("The left and right folders are the same.", 5);
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
                synchronizer.SetPath1(LeftText.Text);

                if (LeftText.Text == "")
                    LeftText.Text = nsync.Properties.Resources.PanelText;
                else
                {
                    LeftIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder.png"));
                    hasLeftPath = true;
                }

                RightText.Text = gamenode["right"].InnerText;
                synchronizer.SetPath2(RightText.Text);

                if (RightText.Text == "")
                    RightText.Text = nsync.Properties.Resources.PanelText;
                else
                {
                    RightIcon.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Icons/folder.png"));
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
        
        #endregion

        private void BtnSync_Click(object sender, RoutedEventArgs e)
        {
            
            /*this.Opacity = 0.5;
            SyncWindow WndSync = new SyncWindow();
            WndSync.Owner = this;
            WndSync.ShowDialog();
            this.Opacity = 1;*/

            label1.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;

            //Do PreSync Calculations: count how many changes need to be done
            if (!synchronizer.PreSync())
            {
                helper.Show("Insufficient disk space", 5);
                return;
            }

            this.Opacity = 0.5;
            synchronizer.StartSync();
            helper.Show("Sync done", 5);
            synchronizer.Reset();
            this.Opacity = 1;
        }

        private void WndMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveFolderPaths();
        }
    }
}