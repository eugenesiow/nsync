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

        public HelperWindow()
        {
            InitializeComponent();
            SetWindowPosition();
            SetTime();
        }   

        #region User Defined Functions

        public void SetSettings(string helpText, int helpDuration)
        {
            ContentText.Text = helpText;
            delayTime = helpDuration;
            dispatcherTimer.Start();
        }

        private void CloseWindow()
        {
            FormFadeOut.Begin();
        }

        private void SetWindowPosition()
        {
            this.Left = SystemParameters.PrimaryScreenWidth - (double)GetValue(WidthProperty) - 10;
            this.Top = 10;
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
            if (delayTime!=0)
            {
                delayTime--;
            }
            else
            {
                CloseWindow();
            }
        }

        private void winHelp_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CloseWindow();

        }

        private void FormFadeOut_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            dispatcherTimer.Stop();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }
    }
}
