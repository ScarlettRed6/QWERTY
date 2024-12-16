﻿#pragma checksum "..\..\..\..\..\View\CashierView\CashierProfileView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F34ECE31AE7F5BFD453D835A10909EC295165A57"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CIRCUIT.ViewModel.CashierViewModel;
using FontAwesome.Sharp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace CIRCUIT.View.CashierView {
    
    
    /// <summary>
    /// CashierProfileView
    /// </summary>
    public partial class CashierProfileView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 55 "..\..\..\..\..\View\CashierView\CashierProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ProfileImage;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\..\View\CashierView\CashierProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox CurrentPasswordBox;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\..\View\CashierView\CashierProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox NewPasswordBox;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\..\View\CashierView\CashierProfileView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox ConfirmNewPasswordBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CIRCUIT;component/view/cashierview/cashierprofileview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\CashierView\CashierProfileView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 34 "..\..\..\..\..\View\CashierView\CashierProfileView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ProfileImage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.CurrentPasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 79 "..\..\..\..\..\View\CashierView\CashierProfileView.xaml"
            this.CurrentPasswordBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.CurrentPasswordBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.NewPasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 88 "..\..\..\..\..\View\CashierView\CashierProfileView.xaml"
            this.NewPasswordBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.NewPasswordBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ConfirmNewPasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 97 "..\..\..\..\..\View\CashierView\CashierProfileView.xaml"
            this.ConfirmNewPasswordBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.ConfirmNewPasswordBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

