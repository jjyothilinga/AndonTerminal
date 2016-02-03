﻿#pragma checksum "..\..\..\LineManagement\LineManagement.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5F6FA852C50D58ECF31A6353C3C657B8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using IAS;
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


namespace IAS {
    
    
    /// <summary>
    /// LineManagement
    /// </summary>
    public partial class LineManagement : System.Windows.Navigation.PageFunction<string>, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\LineManagement\LineManagement.xaml"
        internal System.Windows.Controls.TabControl tbcLineControl;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\LineManagement\LineManagement.xaml"
        internal IAS.addModifyDeleteControl lineControl;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\LineManagement\LineManagement.xaml"
        internal IAS.addModifyDeleteControl stationControl;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\LineManagement\LineManagement.xaml"
        internal IAS.addModifyDeleteControl breakdownControl;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\LineManagement\LineManagement.xaml"
        internal IAS.addModifyDeleteControl qualityControl;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\LineManagement\LineManagement.xaml"
        internal Microsoft.Windows.Controls.DataGrid dgOpenIssuesGrid;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\LineManagement\LineManagement.xaml"
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\LineManagement\LineManagement.xaml"
        internal System.Windows.Controls.Button btnCloseAll;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\LineManagement\LineManagement.xaml"
        internal System.Windows.Controls.ComboBox cmbViewTypeSelector;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\LineManagement\LineManagement.xaml"
        internal System.Windows.Controls.Label lblFrom;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\LineManagement\LineManagement.xaml"
        internal Microsoft.Windows.Controls.DatePicker dpFrom;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\LineManagement\LineManagement.xaml"
        internal System.Windows.Controls.Label lblTo;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\LineManagement\LineManagement.xaml"
        internal Microsoft.Windows.Controls.DatePicker dpTo;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\LineManagement\LineManagement.xaml"
        internal System.Windows.Controls.Button btnGenerate;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\LineManagement\LineManagement.xaml"
        internal System.Windows.Controls.Button btnExport;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\LineManagement\LineManagement.xaml"
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
            System.Uri resourceLocater = new System.Uri("/IAS;component/linemanagement/linemanagement.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\LineManagement\LineManagement.xaml"
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
            this.tbcLineControl = ((System.Windows.Controls.TabControl)(target));
            
            #line 23 "..\..\..\LineManagement\LineManagement.xaml"
            this.tbcLineControl.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabControl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lineControl = ((IAS.addModifyDeleteControl)(target));
            return;
            case 3:
            this.stationControl = ((IAS.addModifyDeleteControl)(target));
            return;
            case 4:
            this.breakdownControl = ((IAS.addModifyDeleteControl)(target));
            return;
            case 5:
            this.qualityControl = ((IAS.addModifyDeleteControl)(target));
            return;
            case 6:
            this.dgOpenIssuesGrid = ((Microsoft.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\..\LineManagement\LineManagement.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnCloseAll = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\LineManagement\LineManagement.xaml"
            this.btnCloseAll.Click += new System.Windows.RoutedEventHandler(this.btnCloseAll_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cmbViewTypeSelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 119 "..\..\..\LineManagement\LineManagement.xaml"
            this.cmbViewTypeSelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbViewTypeSelector_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lblFrom = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.dpFrom = ((Microsoft.Windows.Controls.DatePicker)(target));
            return;
            case 12:
            this.lblTo = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.dpTo = ((Microsoft.Windows.Controls.DatePicker)(target));
            return;
            case 14:
            this.btnGenerate = ((System.Windows.Controls.Button)(target));
            
            #line 130 "..\..\..\LineManagement\LineManagement.xaml"
            this.btnGenerate.Click += new System.Windows.RoutedEventHandler(this.btnGenerate_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.btnExport = ((System.Windows.Controls.Button)(target));
            
            #line 131 "..\..\..\LineManagement\LineManagement.xaml"
            this.btnExport.Click += new System.Windows.RoutedEventHandler(this.btnExport_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.dgReportGrid = ((Microsoft.Windows.Controls.DataGrid)(target));
            return;
            case 17:
            
            #line 169 "..\..\..\LineManagement\LineManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

