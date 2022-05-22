﻿#pragma checksum "..\..\..\Views\BillView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7EC480AB8AFCC83B4E8059027AA328B01554CFB3DD9A6EBC1C92FB0702A8BD0C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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
using WPFBakeryShopAdmin.Converters;
using WPFBakeryShopAdmin.ViewModels;
using WPFBakeryShopAdmin.Views;


namespace WPFBakeryShopAdmin.Views {
    
    
    /// <summary>
    /// BillView
    /// </summary>
    public partial class BillView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Transitions.TransitioningContent TrainsitionigContentSlide;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadPage;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Snackbar GreenSB;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel GreenContent;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock GreenMessage;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Snackbar RedSB;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RedMessage;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid RowItemBills;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadLastPage;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadNextPage;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadPreviousPage;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadFirstPage;
        
        #line default
        #line hidden
        
        
        #line 167 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PageIndicator;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander DetailExpander;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid BillDetails_Details;
        
        #line default
        #line hidden
        
        
        #line 286 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_FormattedTotal;
        
        #line default
        #line hidden
        
        
        #line 301 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_CustomerName;
        
        #line default
        #line hidden
        
        
        #line 357 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_CreatedAt;
        
        #line default
        #line hidden
        
        
        #line 371 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_PaidMethodName;
        
        #line default
        #line hidden
        
        
        #line 409 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_ReceiverInfo_Address;
        
        #line default
        #line hidden
        
        
        #line 441 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_ReceiverInfo_Phone;
        
        #line default
        #line hidden
        
        
        #line 471 "..\..\..\Views\BillView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BillDetails_ReceiverInfo_Note;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFBakeryShopAdmin;component/views/billview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\BillView.xaml"
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
            this.TrainsitionigContentSlide = ((MaterialDesignThemes.Wpf.Transitions.TransitioningContent)(target));
            return;
            case 2:
            this.LoadPage = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.GreenSB = ((MaterialDesignThemes.Wpf.Snackbar)(target));
            return;
            case 4:
            this.GreenContent = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.GreenMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.RedSB = ((MaterialDesignThemes.Wpf.Snackbar)(target));
            return;
            case 7:
            this.RedMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.RowItemBills = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 9:
            this.LoadLastPage = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.LoadNextPage = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.LoadPreviousPage = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.LoadFirstPage = ((System.Windows.Controls.Button)(target));
            return;
            case 13:
            this.PageIndicator = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.DetailExpander = ((System.Windows.Controls.Expander)(target));
            return;
            case 15:
            this.BillDetails_Details = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 16:
            this.BillDetails_FormattedTotal = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 17:
            this.BillDetails_CustomerName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 18:
            this.BillDetails_CreatedAt = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            this.BillDetails_PaidMethodName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 20:
            this.BillDetails_ReceiverInfo_Address = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 21:
            this.BillDetails_ReceiverInfo_Phone = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 22:
            this.BillDetails_ReceiverInfo_Note = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

