﻿#pragma checksum "..\..\HelperWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EA89AB4E25965B010CFCDA9B30206A82"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
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
    /// HelperWindow
    /// </summary>
    public partial class HelperWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 4 "..\..\HelperWindow.xaml"
        internal nsync.HelperWindow windowHelper;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\HelperWindow.xaml"
        internal System.Windows.Media.Animation.Storyboard FormFade;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\HelperWindow.xaml"
        internal System.Windows.Media.Animation.DoubleAnimation FormFadeAnimation;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\HelperWindow.xaml"
        internal System.Windows.Media.Animation.Storyboard FormFadeOut;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\HelperWindow.xaml"
        internal System.Windows.Media.Animation.DoubleAnimation FormFadeOutAnimation;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\HelperWindow.xaml"
        internal System.Windows.Controls.Button ButtonClose;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\HelperWindow.xaml"
        internal System.Windows.Controls.TextBlock ContentText;
        
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
            System.Uri resourceLocater = new System.Uri("/nsync;component/helperwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\HelperWindow.xaml"
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
            this.windowHelper = ((nsync.HelperWindow)(target));
            return;
            case 2:
            this.FormFade = ((System.Windows.Media.Animation.Storyboard)(target));
            return;
            case 3:
            this.FormFadeAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(target));
            return;
            case 4:
            this.FormFadeOut = ((System.Windows.Media.Animation.Storyboard)(target));
            
            #line 33 "..\..\HelperWindow.xaml"
            this.FormFadeOut.Completed += new System.EventHandler(this.FormFadeOut_Completed);
            
            #line default
            #line hidden
            return;
            case 5:
            this.FormFadeOutAnimation = ((System.Windows.Media.Animation.DoubleAnimation)(target));
            return;
            case 6:
            
            #line 48 "..\..\HelperWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.windowHelper_MouseRightButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonClose = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\HelperWindow.xaml"
            this.ButtonClose.Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ContentText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}