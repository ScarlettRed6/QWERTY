﻿#pragma checksum "..\..\..\..\..\View\CashierView\newSaleView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8BA4C146A72A0480E2D42E394F81E9B848EF3F0F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CIRCUIT.ViewModel;
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
    /// NewSale
    /// </summary>
    public partial class NewSale : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 32 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ControlBar;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMaximize;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimize;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userinput;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox watermark;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl ProductsItemsControl;
        
        #line default
        #line hidden
        
        
        #line 230 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logoutBtn;
        
        #line default
        #line hidden
        
        
        #line 267 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl CartItemsControl;
        
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
            System.Uri resourceLocater = new System.Uri("/CIRCUIT;V1.0.0.0;component/view/cashierview/newsaleview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
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
            this.ControlBar = ((System.Windows.Controls.StackPanel)(target));
            
            #line 35 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
            this.ControlBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ControlBar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
            this.ControlBar.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ControlBar_MouseEnter);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.btnMaximize = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
            this.btnMaximize.Click += new System.Windows.RoutedEventHandler(this.btnMaximize_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnMinimize = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
            this.btnMinimize.Click += new System.Windows.RoutedEventHandler(this.btnMinimize_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 92 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BackButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.userinput = ((System.Windows.Controls.TextBox)(target));
            
            #line 131 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
            this.userinput.LostFocus += new System.Windows.RoutedEventHandler(this.userinput_LostFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.watermark = ((System.Windows.Controls.TextBox)(target));
            
            #line 139 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
            this.watermark.GotFocus += new System.Windows.RoutedEventHandler(this.watermark_GotFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ProductsItemsControl = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 10:
            this.logoutBtn = ((System.Windows.Controls.Button)(target));
            
            #line 238 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
            this.logoutBtn.Click += new System.Windows.RoutedEventHandler(this.logoutBtn_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.CartItemsControl = ((System.Windows.Controls.ItemsControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 9:
            
            #line 151 "..\..\..\..\..\View\CashierView\newSaleView.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ProductBox_MouseDown);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

