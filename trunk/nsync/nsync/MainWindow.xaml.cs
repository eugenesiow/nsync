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
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private static int oldSelectedIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimise_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonPageSettings_Click(object sender, RoutedEventArgs e)
        {
            viewList.SelectedIndex = 0;
        }

        private void ButtonPageBackTrack_Click(object sender, RoutedEventArgs e)
        {
            viewList.SelectedIndex = 2;
        }

        //Shows the slider bar
        private void WindowMain_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(this);
            if (mousePos.X < 40)
            {
                if (viewList.SelectedIndex != 0)
                {
                    ButtonSideTabLeft.Visibility = Visibility.Visible;
                }
            }
            else if (mousePos.X > this.Width - 40)
            {
                if (viewList.SelectedIndex != viewList.Items.Count - 1)
                {
                    if(viewList.SelectedIndex != 1)
                        ButtonSideTabRight.Visibility = Visibility.Visible;
                }
            }
            else
            {
                ButtonSideTabLeft.Visibility = Visibility.Hidden;
                ButtonSideTabRight.Visibility = Visibility.Hidden;
            }
        }

        private void viewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            XmlElement root = (XmlElement)viewer.DataContext;
            XmlNodeList xnl = root.SelectNodes("Page");

            if (viewer.ActualHeight > 0 && viewer.ActualWidth > 0)
            {
                RenderTargetBitmap rtb = RenderBitmap(viewer);
                rectanglevisual.Fill = new ImageBrush(BitmapFrame.Create(rtb));
            }

            viewer.ItemsSource = xnl;

            if (oldSelectedIndex < viewList.SelectedIndex)
            {
                viewer.BeginStoryboard((Storyboard)this.Resources["slideLeftToRight"]);
            }
            else
            {
                viewer.BeginStoryboard((Storyboard)this.Resources["slideRightToLeft"]);
            }

            oldSelectedIndex = viewList.SelectedIndex;

            ButtonPageBackTrack.IsEnabled = true;
            ButtonPageHome.IsEnabled = true;
            ButtonPageSettings.IsEnabled = true;

            if(viewList.SelectedIndex == 0) 
            {
                ButtonPageSettings.IsEnabled = false;
            }
            else if (viewList.SelectedIndex == 1)
            {
                ButtonPageHome.IsEnabled = false;
            }
            else
            {
                ButtonPageBackTrack.IsEnabled = false;
            }

            //Change slider tooltips
            UpdateToolTips();
        }

        private void UpdateToolTips()
        {
            int leftIndex = viewList.SelectedIndex - 1;
            int rightIndex = viewList.SelectedIndex + 1;

            if (leftIndex == 0)
            {
                ButtonSideTabLeft.ToolTip = nsync.Properties.Resources.settingsToolTip;
            }
            else if (leftIndex == 1)
            {
                ButtonSideTabLeft.ToolTip = nsync.Properties.Resources.homeToolTip;
            }

            if (rightIndex == 1)
            {
                ButtonSideTabRight.ToolTip = nsync.Properties.Resources.homeToolTip;
            }
            else if (rightIndex == 2)
            {
                ButtonSideTabRight.ToolTip = nsync.Properties.Resources.trackBackToolTip;
            }
            
        }

        public RenderTargetBitmap RenderBitmap(FrameworkElement element)
        {
            double topLeft = 0;
            double topRight = 0;
            int width = (int)element.ActualWidth;
            int height = (int)element.ActualHeight;
            double dpiX = 96; // this is the magic number
            double dpiY = 96; // this is the magic number

            PixelFormat pixelFormat = PixelFormats.Default;
            VisualBrush elementBrush = new VisualBrush(element);
            DrawingVisual visual = new DrawingVisual();
            DrawingContext dc = visual.RenderOpen();

            dc.DrawRectangle(elementBrush, null, new Rect(topLeft, topRight, width, height));
            dc.Close();

            RenderTargetBitmap bitmap = new RenderTargetBitmap(width, height, dpiX, dpiY, pixelFormat);

            bitmap.Render(visual);
            return bitmap;
        }

        private void ButtonPageHome_Click(object sender, RoutedEventArgs e)
        {
            viewList.SelectedIndex = 1;
        }

        private void ButtonSideTabLeft_Click(object sender, RoutedEventArgs e)
        {
            if (viewList.SelectedIndex > 0)
            {
                viewList.SelectedIndex--;
            }
            if (viewList.SelectedIndex == 0)
            {
                ButtonSideTabLeft.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonSideTabRight_Click(object sender, RoutedEventArgs e)
        {
            if (viewList.SelectedIndex < viewList.Items.Count)
            {
                viewList.SelectedIndex++;
            }
            if (viewList.SelectedIndex == viewList.Items.Count -1 || viewList.SelectedIndex==1)
            {
                ButtonSideTabRight.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonTesting_Click(object sender, RoutedEventArgs e)
        {
            TestEngine testEngine = new TestEngine();
            testEngine.ShowDialog();
        }

    }
}