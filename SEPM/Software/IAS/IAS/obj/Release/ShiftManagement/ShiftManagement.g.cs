﻿#pragma checksum "..\..\..\ShiftManagement\ShiftManagement.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "518B453AF62D9D0F5910745DD13CE737"
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
    /// ShiftManagement
    /// </summary>
    public partial class ShiftManagement : System.Windows.Navigation.PageFunction<string>, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\ShiftManagement\ShiftManagement.xaml"
        internal IAS.ShiftAddDelete shiftControl;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\ShiftManagement\ShiftManagement.xaml"
        internal IAS.ShiftAddDelete sessionControl;
        
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
            System.Uri resourceLocater = new System.Uri("/IAS;component/shiftmanagement/shiftmanagement.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ShiftManagement\ShiftManagement.xaml"
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
            this.shiftControl = ((IAS.ShiftAddDelete)(target));
            return;
            case 2:
            this.sessionControl = ((IAS.ShiftAddDelete)(target));
            return;
            case 3:
            
            #line 45 "..\..\..\ShiftManagement\ShiftManagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

