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
            //Note1
            //Note2
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
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        
        private void WndMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void BtnMinimise_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnPageSettings_Click(object sender, RoutedEventArgs e)
        {
            viewList.SelectedIndex = 0;
        }

        private void BtnPageBackTrack_Click(object sender, RoutedEventArgs e)
        {
            viewList.SelectedIndex = 2;
        }

        private void WndMain_MouseMove(object sender, MouseEventArgs e)
        {
            

            Point mousePos = e.GetPosition(this);
            if (mousePos.X < 40)
            {
                if (viewList.SelectedIndex != 0)
                {
                    BtnSideTabLeft.Visibility = Visibility.Visible;
                }
            }
            else if (mousePos.X > this.Width - 40)
            {
                if (viewList.SelectedIndex != viewList.Items.Count - 1)
                {
                    BtnSideTabRight.Visibility = Visibility.Visible;
                }
            }
            else
            {
                BtnSideTabLeft.Visibility = Visibility.Hidden;
                BtnSideTabRight.Visibility = Visibility.Hidden;
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

            BtnPageBackTrack.IsEnabled = true;
            BtnPageHome.IsEnabled = true;
            BtnPageSettings.IsEnabled = true;

            if(viewList.SelectedIndex == 0) 
            {
                BtnPageSettings.IsEnabled = false;
            }
            else if (viewList.SelectedIndex == 1)
            {
                BtnPageHome.IsEnabled = false;
            }
            else
            {
                BtnPageBackTrack.IsEnabled = false;
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

        private void BtnPageHome_Click(object sender, RoutedEventArgs e)
        {
            viewList.SelectedIndex = 1;
        }

        private void BtnSideTabLeft_Click(object sender, RoutedEventArgs e)
        {
            if (viewList.SelectedIndex > 0)
            {
                viewList.SelectedIndex--;
            }
        }

        private void BtnSideTabRight_Click(object sender, RoutedEventArgs e)
        {
            if (viewList.SelectedIndex < viewList.Items.Count)
            {
                viewList.SelectedIndex++;
            }
        }

        private void BtnTesting_Click(object sender, RoutedEventArgs e)
        {
            TestEngine testEngine = new TestEngine();
            testEngine.ShowDialog();
        }
    }
}