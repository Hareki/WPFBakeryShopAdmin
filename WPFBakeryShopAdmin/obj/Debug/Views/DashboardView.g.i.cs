﻿#pragma checksum "..\..\..\Views\DashboardView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F2E4ED275D7ACF7AABA3F3B88794B7D50EA33371A1F5D704EE8D28CF45C366F4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WPFBakeryShopAdmin.ViewModels;
using WPFBakeryShopAdmin.Views;


namespace WPFBakeryShopAdmin.Views {
    
    
    /// <summary>
    /// DashboardView
    /// </summary>
    public partial class DashboardView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Transitions.TransitioningContent TrainsitionigContentSlide;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadPage;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Dashboard_TotalOrdersNum;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Dashboard_TotalAvailableProductVariantsNum;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Dashboard_TotalSoldProductVariantsNum;
        
        #line default
        #line hidden
        
        
        #line 188 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar PendingPercent;
        
        #line default
        #line hidden
        
        
        #line 211 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ShippingPercent;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ShippedPercent;
        
        #line default
        #line hidden
        
        
        #line 257 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar CancelledPercent;
        
        #line default
        #line hidden
        
        
        #line 279 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Dashboard_TodayProcessingOrdersNum;
        
        #line default
        #line hidden
        
        
        #line 291 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Dashboard_TodayDispatchOrdersNum;
        
        #line default
        #line hidden
        
        
        #line 304 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Dashboard_TodayShippedOrdersNum;
        
        #line default
        #line hidden
        
        
        #line 317 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Dashboard_TodayCancelOrdersNum;
        
        #line default
        #line hidden
        
        
        #line 339 "..\..\..\Views\DashboardView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Dashboard_TopRecentOrders;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFBakeryShopAdmin;component/views/dashboardview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\DashboardView.xaml"
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
            this.Dashboard_TotalOrdersNum = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.Dashboard_TotalAvailableProductVariantsNum = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Dashboard_TotalSoldProductVariantsNum = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.PendingPercent = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 7:
            this.ShippingPercent = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 8:
            this.ShippedPercent = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 9:
            this.CancelledPercent = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 10:
            this.Dashboard_TodayProcessingOrdersNum = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.Dashboard_TodayDispatchOrdersNum = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.Dashboard_TodayShippedOrdersNum = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.Dashboard_TodayCancelOrdersNum = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.Dashboard_TopRecentOrders = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

