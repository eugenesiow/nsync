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

namespace nsync
{
    /// <summary>
    /// Interaction logic for SyncWindow.xaml
    /// </summary>
    public partial class SyncWindow : Window
    {
        public SyncWindow()
        {
            InitializeComponent();
        }

        private void BtnDropDown_Click(object sender, RoutedEventArgs e)
        {
            if (DropDownDialog.Visibility == Visibility.Visible)
            {
                DropDownDialog.Visibility = Visibility.Hidden;
            }
            else
            {
                DropDownDialog.Visibility = Visibility.Visible;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            DropDownDialog.Visibility = Visibility.Hidden;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            //Stop
            this.Close();
        }
    }
}
