﻿#pragma checksum "..\..\Reports.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "86B3B3F1EA480A9BF92B56455127048D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
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


namespace WareHouseUtility {
    
    
    /// <summary>
    /// Reports
    /// </summary>
    public partial class Reports : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\Reports.xaml"
        internal System.Windows.Controls.Label lblFrom;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Reports.xaml"
        internal Microsoft.Windows.Controls.DatePicker dpFrom;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Reports.xaml"
        internal System.Windows.Controls.Label lblTo;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Reports.xaml"
        internal Microsoft.Windows.Controls.DatePicker dpTo;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Reports.xaml"
        internal System.Windows.Controls.Button btnGenerate;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Reports.xaml"
        internal System.Windows.Controls.Button btnExport;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\Reports.xaml"
        internal Microsoft.Windows.Controls.DataGrid dgReportGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/WareHouseUtility;component/reports.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Reports.xaml"
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
            this.lblFrom = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.dpFrom = ((Microsoft.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.lblTo = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.dpTo = ((Microsoft.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.btnGenerate = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\Reports.xaml"
            this.btnGenerate.Click += new System.Windows.RoutedEventHandler(this.btnGenerate_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnExport = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\Reports.xaml"
            this.btnExport.Click += new System.Windows.RoutedEventHandler(this.btnExport_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dgReportGrid = ((Microsoft.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
