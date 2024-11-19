﻿#pragma checksum "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CF50DE40E898F04E53B460E6DA886255E0B8FC35"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CIRCUIT.View.AdminDashboard;
using CIRCUIT.View.AdminDashboardViews;
using CIRCUIT.ViewModel.AdminDashboardViewModel;
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


namespace CIRCUIT.View.AdminDashboardViews {
    
    
    /// <summary>
    /// EditProductView
    /// </summary>
    public partial class EditProductView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 96 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox prodName;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox prodModel;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descriptionBox;
        
        #line default
        #line hidden
        
        
        #line 206 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sellingPrice;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox minStockLevel;
        
        #line default
        #line hidden
        
        
        #line 242 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox stockQuantity;
        
        #line default
        #line hidden
        
        
        #line 259 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UnitCost;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CIRCUIT;component/view/admindashboardviews/editproductview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.prodName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.prodModel = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.descriptionBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.sellingPrice = ((System.Windows.Controls.TextBox)(target));
            
            #line 211 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
            this.sellingPrice.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 5:
            this.minStockLevel = ((System.Windows.Controls.TextBox)(target));
            
            #line 228 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
            this.minStockLevel.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 6:
            this.stockQuantity = ((System.Windows.Controls.TextBox)(target));
            
            #line 248 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
            this.stockQuantity.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 7:
            this.UnitCost = ((System.Windows.Controls.TextBox)(target));
            
            #line 265 "..\..\..\..\..\View\AdminDashboardViews\EditProductView.xaml"
            this.UnitCost.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

