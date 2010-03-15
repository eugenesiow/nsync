using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace nsync
{
    /// <summary>
    /// Interaction logic for HelperWindow.xaml
    /// </summary>
    public partial class HelperWindow : Window
    {
        private int delayTime=10;
        private DispatcherTimer dispatcherTimer;
        private windowStartPosition windowPostionType;
        private bool windowActive;

        public enum windowStartPosition
        {
            topLeft, topRight, bottomLeft, bottomRight, center, windowTop
        };

        public HelperWindow()
        {
            InitializeComponent();
            SetTime();
        }   

        #region User Defined Functions

        public void SetSettings(string helpText, int helpDuration, windowStartPosition windowPosition)
        {
            windowActive = true;
            ContentText.Text = helpText;
            delayTime = helpDuration;
            windowPostionType = windowPosition;
            dispatcherTimer.Start();

            switch (windowPosition)
            {
                case windowStartPosition.topLeft:
                    this.Left = 10;
                    this.Top = 10;
                    break;
                case windowStartPosition.topRight:
                    this.Left = SystemParameters.PrimaryScreenWidth - (double)GetValue(WidthProperty) - 10;
                    this.Top = 10;
                    break;
                case windowStartPosition.bottomLeft:
                    this.Left = 10;
                    this.Top = SystemParameters.PrimaryScreenHeight - (double)GetValue(HeightProperty) - 50;
                    break;
                case windowStartPosition.bottomRight:
                    this.Left = SystemParameters.PrimaryScreenWidth - (double)GetValue(WidthProperty) - 10;
                    this.Top = SystemParameters.PrimaryScreenHeight - (double)GetValue(HeightProperty) - 50;
                    break;
                case windowStartPosition.center:
                    this.Left = (SystemParameters.PrimaryScreenWidth / 2) - ((double)GetValue(WidthProperty) / 2);
                    this.Top = (SystemParameters.PrimaryScreenHeight / 2) - ((double)GetValue(HeightProperty) / 2);
                    break;
                case windowStartPosition.windowTop:
                    MoveWindow();
                    break;
            }
        }

        public bool WindowActiveState
        {
            get { return windowActive; }
            set { windowActive = value; }
        }

        public void MoveWindow()
        {
            if (windowPostionType == windowStartPosition.windowTop)
            {
                double leftPos = Owner.Left + ((double)GetValue(WidthProperty) / 4);
                double rightPos = leftPos + (double)GetValue(WidthProperty);
                double rightMostLeftPos = SystemParameters.PrimaryScreenWidth - (double)GetValue(WidthProperty);
                double topPos = Owner.Top - (double)GetValue(HeightProperty) - 10;
                double bottomPos = Owner.Top + Owner.Height + 10;

                if (rightPos > SystemParameters.PrimaryScreenWidth)
                    this.Left = rightMostLeftPos;
                else if (leftPos > 0)
                    this.Left = leftPos;
                else
                    this.Left = 1;

                if (topPos > 0)
                    this.Top = topPos;
                else
                    this.Top = bottomPos;
            }
        }

        private void CloseWindow()
        {
            if (windowActive == true)
            {
                windowActive = false;
                FormFadeOut.Begin();
            }
        }

        private void SetTime()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        #endregion

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Focus();
            if (delayTime!=0)
            {
                delayTime--;
            }
            else
            {
                CloseWindow();
            }
        }

        private void windowHelper_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CloseWindow();
        }

        private void FormFadeOut_Completed(object sender, EventArgs e)
        {
            if (!windowActive)
            {
                this.Visibility = Visibility.Hidden;
                dispatcherTimer.Stop();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }
    }
}
