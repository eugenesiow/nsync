﻿#pragma checksum "..\..\VisualPreviewWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "892A21E1550465D699C470FAB934B5C5"
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
using nsync.Properties;


namespace nsync {
    
    
    /// <summary>
    /// VisualPreviewWindow
    /// </summary>
    public partial class VisualPreviewWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\VisualPreviewWindow.xaml"
        internal nsync.VisualPreviewWindow WindowVisualPreview;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\VisualPreviewWindow.xaml"
        internal System.Windows.Controls.TextBlock TitleBar;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\VisualPreviewWindow.xaml"
        internal System.Windows.Controls.Button ButtonClose;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\VisualPreviewWindow.xaml"
        internal System.Windows.Controls.StackPanel BoxLeftPath;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\VisualPreviewWindow.xaml"
        internal System.Windows.Controls.Label LabelLeftPath;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\VisualPreviewWindow.xaml"
        internal System.Windows.Controls.StackPanel BoxRightPath;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\VisualPreviewWindow.xaml"
        internal System.Windows.Controls.Label LabelRightPath;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\VisualPreviewWindow.xaml"
        internal System.Windows.Controls.StackPanel BoxVisualPreview;
        
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
            System.Uri resourceLocater = new System.Uri("/nsync;component/visualpreviewwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\VisualPreviewWindow.xaml"
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
            this.WindowVisualPreview = ((nsync.VisualPreviewWindow)(target));
            
            #line 6 "..\..\VisualPreviewWindow.xaml"
            this.WindowVisualPreview.Loaded += new System.Windows.RoutedEventHandler(this.WindowVisualPreview_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TitleBar = ((System.Windows.Controls.TextBlock)(target));
            
            #line 12 "..\..\VisualPreviewWindow.xaml"
            this.TitleBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.titleBar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonClose = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\VisualPreviewWindow.xaml"
            this.ButtonClose.Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BoxLeftPath = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.LabelLeftPath = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.BoxRightPath = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.LabelRightPath = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.BoxVisualPreview = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
