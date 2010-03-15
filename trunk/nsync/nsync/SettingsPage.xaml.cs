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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nsync
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private Settings settingsManager;

        public SettingsPage()
        {
            InitializeComponent();
            CheckSettings();
        }

        private void CheckSettings()
        {
            settingsManager = Settings.Instance;
            if (!settingsManager.GetHelperWindowStatus())
            {
                CheckboxToggleHelperWindow.IsChecked = true;
            }
        }

        private void CheckboxToggleHelperWindow_Checked(object sender, RoutedEventArgs e)
        {
            settingsManager = Settings.Instance;
            settingsManager.SetHelperWindowStatus(false);
        }

        private void CheckboxToggleHelperWindow_UnChecked(object sender, RoutedEventArgs e)
        {
            settingsManager = Settings.Instance;
            settingsManager.SetHelperWindowStatus(true);
        }
    }
}
