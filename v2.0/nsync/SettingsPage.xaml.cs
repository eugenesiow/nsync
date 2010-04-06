﻿using System.Windows;
using System.Windows.Controls;

namespace nsync
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private Settings settingsManager;
        private bool pageIsLoaded = false;

        /// <summary>
        /// Constructor for SettingsPage
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
            CheckSettings();
        }

        /// <summary>
        /// Update the checkbox on SettingsPage
        /// </summary>
        private void CheckSettings()
        {
            settingsManager = Settings.Instance;
            int loadedTimer = settingsManager.GetHelperWindowStatus();
            if (loadedTimer == -1)
                loadedTimer = 11;
            HelperWindowSlider.Value = loadedTimer;
            HelperWindowSliderValue.SelectedIndex = loadedTimer;
            pageIsLoaded = true;
        }

        /// <summary>
        /// Event when comboBox value is changed
        /// </summary>
        private void HelperWindowSliderValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pageIsLoaded)
            {
                if (HelperWindowSlider.Value != HelperWindowSliderValue.SelectedIndex)
                {
                    HelperWindowSlider.Value = HelperWindowSliderValue.SelectedIndex;
                    settingsManager.SetHelperWindowStatus((int)HelperWindowSlider.Value);
                }
            }
        }

        /// <summary>
        /// Event when slider value is changed
        /// </summary>
        private void HelperWindowSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pageIsLoaded)
            {
                if (HelperWindowSliderValue.SelectedIndex != HelperWindowSlider.Value)
                {
                    HelperWindowSliderValue.SelectedIndex = (int)HelperWindowSlider.Value;
                    settingsManager.SetHelperWindowStatus((int)HelperWindowSlider.Value);
                }
            }
        }


    }
}