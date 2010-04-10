﻿using System.Windows;
using System.Windows.Controls;
using System.IO;

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
            //gets helper window settings
            int loadedTimer = settingsManager.GetHelperWindowStatus();
            if (loadedTimer == -1)
                loadedTimer = 11;
            HelperWindowSlider.Value = loadedTimer;
            HelperWindowSliderValue.SelectedIndex = loadedTimer;

            //gets exclude window settings
            if (!settingsManager.GetExcludeWindowStatus())
                CheckboxToggleExcludeWindow.IsChecked = true;

            //gets trackback settings
            if (!settingsManager.GetTrackBackStatus())
                CheckboxToggleTrackBack.IsChecked = true;

            //flag for enabling the boxes for user input
            pageIsLoaded = true;
        }

        /// <summary>
        /// Event when comboBox value is changed
        /// </summary>
        private void HelperWindowSliderValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetUI();
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
            ResetUI();
            if (pageIsLoaded)
            {
                if (HelperWindowSliderValue.SelectedIndex != HelperWindowSlider.Value)
                {
                    HelperWindowSliderValue.SelectedIndex = (int)HelperWindowSlider.Value;
                    settingsManager.SetHelperWindowStatus((int)HelperWindowSlider.Value);
                }
            }
        }

        /// <summary>
        /// Event when Exclude Checkbox is checked
        /// </summary>
        private void CheckboxToggleExcludeWindow_Checked(object sender, RoutedEventArgs e)
        {
            ResetUI();
            settingsManager.SetExcludeWindowStatus(false);
        }

        private void CheckboxToggleExcludeWindow_UnChecked(object sender, RoutedEventArgs e)
        {
            ResetUI();
            settingsManager.SetExcludeWindowStatus(true);
        }

        private void CheckboxToggleTrackBack_Checked(object sender, RoutedEventArgs e)
        {
            ResetUI();
            settingsManager.SetTrackBackStatus(false);
        }

        private void CheckboxToggleTrackBack_UnChecked(object sender, RoutedEventArgs e)
        {
            ResetUI();
            settingsManager.SetTrackBackStatus(true);
        }

        private void ButtonClearLogFolder_Click(object sender, RoutedEventArgs e)
        {
            ResetUI();
            settingsManager.ClearLogFolder(this);
        }

        private void ButtonOpenLogFolder_Click(object sender, RoutedEventArgs e)
        {
            ResetUI();
            System.Diagnostics.Process.Start(@Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
            "\\log\\");
        }

        private void ResetUI()
        {
            LabelProgress.Visibility = Visibility.Hidden;
        }
    }
}
