﻿#pragma checksum "..\..\TrackBackPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AE70FDFDAF5BCD0BB523DAA705551470"
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
    /// TrackBackPage
    /// </summary>
    public partial class TrackBackPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\TrackBackPage.xaml"
        internal System.Windows.Controls.Image ImageTrackBack;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\TrackBackPage.xaml"
        internal System.Windows.Controls.ComboBox ComboBoxSourceFolder;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\TrackBackPage.xaml"
        internal System.Windows.Controls.StackPanel BoxTrackBack;
        
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
            System.Uri resourceLocater = new System.Uri("/nsync;component/trackbackpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TrackBackPage.xaml"
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
            
            #line 5 "..\..\TrackBackPage.xaml"
            ((nsync.TrackBackPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ImageTrackBack = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.ComboBoxSourceFolder = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.BoxTrackBack = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
