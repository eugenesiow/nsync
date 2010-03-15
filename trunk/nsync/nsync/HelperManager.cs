using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsync
{
    public class HelperManager
    {
        private HelperWindow windowHelper;
        private Settings settingsManager;

        public HelperManager(Window ownerWindow)
        {
            settingsManager = Settings.Instance;
            windowHelper = new HelperWindow();
            windowHelper.Owner = ownerWindow;
            windowHelper.Show();
            windowHelper.Visibility = Visibility.Hidden;
        }

        public void Show(string helpString, int time, HelperWindow.windowStartPosition windowPosition)
        {
            if (true == helperWindowIsOn())
            {
                windowHelper.SetSettings(helpString, time, windowPosition);
                if (windowHelper.Visibility != Visibility.Visible)
                {
                    windowHelper.Visibility = Visibility.Visible;
                    windowHelper.FormFade.Begin();
                }
            }
        }

        private bool helperWindowIsOn()
        {
            return settingsManager.GetHelperWindowStatus();
        }

        public void CloseWindow()
        {
            windowHelper.Close();
        }

        public void UpdateMove()
        {
            windowHelper.MoveWindow();
        }

        public void HideWindow()
        {
            windowHelper.Visibility = Visibility.Hidden;
        }
    }
}
