﻿#pragma checksum "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FB0E0EBA163486BD0F8213B9382F06B4D634990C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MilkSt.WpfApp.UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace MilkSt.WpfApp.UI {
    
    
    /// <summary>
    /// wSearchVoucher
    /// </summary>
    public partial class wSearchVoucher : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 33 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameVoucher;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Discount;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker Expiry;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Quantity;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSearch;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAdd;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid grdVoucher;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MilkSt.WpfApp;component/ui/voucher/wsearchvoucher.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NameVoucher = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Discount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Expiry = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.Quantity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ButtonSearch = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
            this.ButtonSearch.Click += new System.Windows.RoutedEventHandler(this.ButtonSearch_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonAdd = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
            this.ButtonAdd.Click += new System.Windows.RoutedEventHandler(this.ButtonAdd_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.grdVoucher = ((System.Windows.Controls.DataGrid)(target));
            
            #line 47 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
            this.grdVoucher.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.grdVoucher_MouseDouble_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 8:
            
            #line 64 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.grdVoucher_ButtonView_Click);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 71 "..\..\..\..\..\UI\Voucher\wSearchVoucher.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.grdVoucher_ButtonDelete_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

