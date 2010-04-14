﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "29424A49081BBCDE465668D8E99B7000"
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
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 5 "..\..\MainWindow.xaml"
        internal nsync.MainWindow WindowMain;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.TextBlock TitleBar;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button ButtonClose;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button ButtonPageSettings;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button ButtonPageHome;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button ButtonPageTrackBack;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button ButtonMinimise;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.ListBox viewList;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Border bordervisual;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\MainWindow.xaml"
        internal System.Windows.Shapes.Rectangle rectanglevisual;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.ItemsControl viewer;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button ButtonSideTabLeft;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button ButtonSideTabRight;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Border PageToolTip;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.TextBlock PageToolTipText;
        
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
            System.Uri resourceLocater = new System.Uri("/nsync;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            this.WindowMain = ((nsync.MainWindow)(target));
            
            #line 5 "..\..\MainWindow.xaml"
            this.WindowMain.AddHandler(System.Windows.Input.Mouse.MouseMoveEvent, new System.Windows.Input.MouseEventHandler(this.WindowMain_MouseMove));
            
            #line default
            #line hidden
            return;
            case 2:
            this.TitleBar = ((System.Windows.Controls.TextBlock)(target));
            
            #line 31 "..\..\MainWindow.xaml"
            this.TitleBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.titleBar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonClose = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\MainWindow.xaml"
            this.ButtonClose.Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonPageSettings = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\MainWindow.xaml"
            this.ButtonPageSettings.Click += new System.Windows.RoutedEventHandler(this.ButtonPageSettings_Click);
            
            #line default
            #line hidden
            
            #line 34 "..\..\MainWindow.xaml"
            this.ButtonPageSettings.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ButtonPageSettings_MouseEnter);
            
            #line default
            #line hidden
            
            #line 34 "..\..\MainWindow.xaml"
            this.ButtonPageSettings.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ButtonPage_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonPageHome = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\MainWindow.xaml"
            this.ButtonPageHome.Click += new System.Windows.RoutedEventHandler(this.ButtonPageHome_Click);
            
            #line default
            #line hidden
            
            #line 35 "..\..\MainWindow.xaml"
            this.ButtonPageHome.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ButtonPageHome_MouseEnter);
            
            #line default
            #line hidden
            
            #line 35 "..\..\MainWindow.xaml"
            this.ButtonPageHome.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ButtonPage_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonPageTrackBack = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\MainWindow.xaml"
            this.ButtonPageTrackBack.Click += new System.Windows.RoutedEventHandler(this.ButtonPageTrackBack_Click);
            
            #line default
            #line hidden
            
            #line 36 "..\..\MainWindow.xaml"
            this.ButtonPageTrackBack.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ButtonPageTrackBack_MouseEnter);
            
            #line default
            #line hidden
            
            #line 36 "..\..\MainWindow.xaml"
            this.ButtonPageTrackBack.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ButtonPage_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonMinimise = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\MainWindow.xaml"
            this.ButtonMinimise.Click += new System.Windows.RoutedEventHandler(this.ButtonMinimise_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.viewList = ((System.Windows.Controls.ListBox)(target));
            
            #line 44 "..\..\MainWindow.xaml"
            this.viewList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.viewList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.bordervisual = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.rectanglevisual = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 11:
            this.viewer = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 12:
            this.ButtonSideTabLeft = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\MainWindow.xaml"
            this.ButtonSideTabLeft.Click += new System.Windows.RoutedEventHandler(this.ButtonSideTabLeft_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.ButtonSideTabRight = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\MainWindow.xaml"
            this.ButtonSideTabRight.Click += new System.Windows.RoutedEventHandler(this.ButtonSideTabRight_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.PageToolTip = ((System.Windows.Controls.Border)(target));
            return;
            case 15:
            this.PageToolTipText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
