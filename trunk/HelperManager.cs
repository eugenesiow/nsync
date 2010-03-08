using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsync
{
    class HelperManager
    {
        private HelperWindow wndHelper;

        public HelperManager(Window ownerWindow)
        {
            wndHelper = new HelperWindow();
            wndHelper.Owner = ownerWindow;
            wndHelper.Show();
            wndHelper.Visibility = Visibility.Hidden;
        }

        public void Show(string helpString, int time)
        {
            wndHelper.SetSettings(helpString, time);
            if (wndHelper.Visibility != Visibility.Visible)
            {
                wndHelper.Visibility = Visibility.Visible;
                wndHelper.FormFade.Begin();
            }
        }
    }
}
