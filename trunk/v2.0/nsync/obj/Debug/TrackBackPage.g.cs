﻿#pragma checksum "..\..\TrackBackPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2102055028AEEDA85896DD158C049C23"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
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
        
        
        #line 11 "..\..\TrackBackPage.xaml"
        internal System.Windows.Controls.ListView ListViewForLeftFolder;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\TrackBackPage.xaml"
        internal System.Windows.Controls.ListView ListViewForRightFolder;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\TrackBackPage.xaml"
        internal System.Windows.Controls.Label LabelProgress;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\TrackBackPage.xaml"
        internal System.Windows.Controls.Button ButtonRestore;
        
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
            
            #line 9 "..\..\TrackBackPage.xaml"
            this.ComboBoxSourceFolder.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxSourceFolder_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BoxTrackBack = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.ListViewForLeftFolder = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.ListViewForRightFolder = ((System.Windows.Controls.ListView)(target));
            return;
            case 7:
            this.LabelProgress = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.ButtonRestore = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\TrackBackPage.xaml"
            this.ButtonRestore.Click += new System.Windows.RoutedEventHandler(this.ButtonRestore_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
