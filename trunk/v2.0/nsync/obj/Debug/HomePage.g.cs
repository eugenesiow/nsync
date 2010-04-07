﻿#pragma checksum "..\..\HomePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6878886D11EE58418F6B2EE58F44E412"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
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
using nsync;
using nsync.Properties;


namespace nsync {
    
    
    /// <summary>
    /// HomePage
    /// </summary>
    public partial class HomePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\HomePage.xaml"
        internal System.Windows.Controls.StackPanel BoxLeft;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\HomePage.xaml"
        internal System.Windows.Controls.TextBlock LeftText;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Image LeftIcon;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\HomePage.xaml"
        internal System.Windows.Controls.ListBox LeftListBox;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Border BarMRULeft;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\HomePage.xaml"
        internal System.Windows.Controls.StackPanel BoxRight;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\HomePage.xaml"
        internal System.Windows.Controls.TextBlock RightText;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Image RightIcon;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\HomePage.xaml"
        internal System.Windows.Controls.ListBox RightListBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Border BarMRURight;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Button ButtonSync;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Button ButtonPreview;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\HomePage.xaml"
        internal nsync.GIFImageControl SyncingImage;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Image ImageTeam14;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Image ImageTeam14Over;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Label LabelProgress;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\HomePage.xaml"
        internal System.Windows.Controls.Label LabelProgressPercent;
        
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
            System.Uri resourceLocater = new System.Uri("/nsync;component/homepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\HomePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 6 "..\..\HomePage.xaml"
            ((nsync.HomePage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            
            #line 6 "..\..\HomePage.xaml"
            ((nsync.HomePage)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Page_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 9 "..\..\HomePage.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.BoxLeft_MouseEnter);
            
            #line default
            #line hidden
            
            #line 9 "..\..\HomePage.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.BoxLeft_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BoxLeft = ((System.Windows.Controls.StackPanel)(target));
            
            #line 10 "..\..\HomePage.xaml"
            this.BoxLeft.AddHandler(System.Windows.DragDrop.DropEvent, new System.Windows.DragEventHandler(this.BoxLeft_Drop));
            
            #line default
            #line hidden
            
            #line 10 "..\..\HomePage.xaml"
            this.BoxLeft.DragEnter += new System.Windows.DragEventHandler(this.BoxLeft_DragEnter);
            
            #line default
            #line hidden
            
            #line 10 "..\..\HomePage.xaml"
            this.BoxLeft.DragLeave += new System.Windows.DragEventHandler(this.BoxLeft_DragLeave);
            
            #line default
            #line hidden
            
            #line 10 "..\..\HomePage.xaml"
            this.BoxLeft.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LeftIcon_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LeftText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.LeftIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.LeftListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.BarMRULeft = ((System.Windows.Controls.Border)(target));
            
            #line 16 "..\..\HomePage.xaml"
            this.BarMRULeft.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BarMRULeft_MouseEnter);
            
            #line default
            #line hidden
            
            #line 16 "..\..\HomePage.xaml"
            this.BarMRULeft.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BarMRULeft_MouseLeave);
            
            #line default
            #line hidden
            
            #line 16 "..\..\HomePage.xaml"
            this.BarMRULeft.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.BarMRULeft_MouseUp);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 22 "..\..\HomePage.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.BoxRight_MouseEnter);
            
            #line default
            #line hidden
            
            #line 22 "..\..\HomePage.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.BoxRight_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BoxRight = ((System.Windows.Controls.StackPanel)(target));
            
            #line 23 "..\..\HomePage.xaml"
            this.BoxRight.AddHandler(System.Windows.DragDrop.DropEvent, new System.Windows.DragEventHandler(this.BoxRight_Drop));
            
            #line default
            #line hidden
            
            #line 23 "..\..\HomePage.xaml"
            this.BoxRight.DragEnter += new System.Windows.DragEventHandler(this.BoxRight_DragEnter);
            
            #line default
            #line hidden
            
            #line 23 "..\..\HomePage.xaml"
            this.BoxRight.DragLeave += new System.Windows.DragEventHandler(this.BoxRight_DragLeave);
            
            #line default
            #line hidden
            
            #line 23 "..\..\HomePage.xaml"
            this.BoxRight.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RightIcon_MouseDown);
            
            #line default
            #line hidden
            return;
            case 10:
            this.RightText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.RightIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            this.RightListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 13:
            this.BarMRURight = ((System.Windows.Controls.Border)(target));
            
            #line 29 "..\..\HomePage.xaml"
            this.BarMRURight.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BarMRURight_MouseEnter);
            
            #line default
            #line hidden
            
            #line 29 "..\..\HomePage.xaml"
            this.BarMRURight.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BarMRURight_MouseLeave);
            
            #line default
            #line hidden
            
            #line 29 "..\..\HomePage.xaml"
            this.BarMRURight.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.BarMRURight_MouseUp);
            
            #line default
            #line hidden
            return;
            case 14:
            this.ButtonSync = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\HomePage.xaml"
            this.ButtonSync.Click += new System.Windows.RoutedEventHandler(this.ButtonSync_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.ButtonPreview = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\HomePage.xaml"
            this.ButtonPreview.Click += new System.Windows.RoutedEventHandler(this.ButtonPreview_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.SyncingImage = ((nsync.GIFImageControl)(target));
            return;
            case 17:
            this.ImageTeam14 = ((System.Windows.Controls.Image)(target));
            return;
            case 18:
            this.ImageTeam14Over = ((System.Windows.Controls.Image)(target));
            return;
            case 19:
            this.LabelProgress = ((System.Windows.Controls.Label)(target));
            return;
            case 20:
            this.LabelProgressPercent = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}