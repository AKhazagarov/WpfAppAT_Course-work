﻿#pragma checksum "..\..\NotificationWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6AF9976EE594A4B52C5F6E28F0E5DF0B9E2CEF3C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using System.Windows.Shell;
using WpfAppAT_Course_work;


namespace WpfAppAT_Course_work {
    
    
    /// <summary>
    /// NotificationWindow
    /// </summary>
    public partial class NotificationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\NotificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CurrentTB;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\NotificationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NewTabTB;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfAppAT_Course work;component/notificationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NotificationWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CurrentTB = ((System.Windows.Controls.TextBlock)(target));
            
            #line 26 "..\..\NotificationWindow.xaml"
            this.CurrentTB.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TextBlock_MouseEnter);
            
            #line default
            #line hidden
            
            #line 26 "..\..\NotificationWindow.xaml"
            this.CurrentTB.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CurrentTB_MouseLeave);
            
            #line default
            #line hidden
            
            #line 26 "..\..\NotificationWindow.xaml"
            this.CurrentTB.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.CurrentTB_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NewTabTB = ((System.Windows.Controls.TextBlock)(target));
            
            #line 29 "..\..\NotificationWindow.xaml"
            this.NewTabTB.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NewTabTB_MouseEnter);
            
            #line default
            #line hidden
            
            #line 29 "..\..\NotificationWindow.xaml"
            this.NewTabTB.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NewTabTB_MouseLeave);
            
            #line default
            #line hidden
            
            #line 29 "..\..\NotificationWindow.xaml"
            this.NewTabTB.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.NewTabTB_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

