﻿#pragma checksum "..\..\SettingsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C4D93ABBCB0CA0A5E5A1C3DCC0954BFB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace nsync {
    
    
    /// <summary>
    /// SettingsPage
    /// </summary>
    public partial class SettingsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\SettingsPage.xaml"
        internal System.Windows.Controls.Image ImageSettings;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\SettingsPage.xaml"
        internal System.Windows.Controls.StackPanel PanelSettings;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\SettingsPage.xaml"
        internal System.Windows.Controls.CheckBox CheckboxToggleHelperWindow;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/nsync;component/settingspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SettingsPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ImageSettings = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.PanelSettings = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.CheckboxToggleHelperWindow = ((System.Windows.Controls.CheckBox)(target));
            
            #line 10 "..\..\SettingsPage.xaml"
            this.CheckboxToggleHelperWindow.Checked += new System.Windows.RoutedEventHandler(this.CheckboxToggleHelperWindow_Checked);
            
            #line default
            #line hidden
            
            #line 10 "..\..\SettingsPage.xaml"
            this.CheckboxToggleHelperWindow.Unchecked += new System.Windows.RoutedEventHandler(this.CheckboxToggleHelperWindow_UnChecked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
