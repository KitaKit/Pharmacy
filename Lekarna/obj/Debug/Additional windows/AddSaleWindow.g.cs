﻿#pragma checksum "..\..\..\Additional windows\AddSaleWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FC09DC127341AE0CDECA92A41A82C5F4F949372CCAD79191DD8F2B5469BF144C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Pharmacy.Additional_windows;
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


namespace Pharmacy.Additional_windows {
    
    
    /// <summary>
    /// AddSaleWindow
    /// </summary>
    public partial class AddSaleWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\Additional windows\AddSaleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Pharmacy.Additional_windows.AddSaleWindow addSaleWindow;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Additional windows\AddSaleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dateDatePicker;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Additional windows\AddSaleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer wrapPanelScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Additional windows\AddSaleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel medicationsWrapPanel;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Additional windows\AddSaleWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Pharmacy;component/additional%20windows/addsalewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Additional windows\AddSaleWindow.xaml"
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
            this.addSaleWindow = ((Pharmacy.Additional_windows.AddSaleWindow)(target));
            
            #line 8 "..\..\..\Additional windows\AddSaleWindow.xaml"
            this.addSaleWindow.Loaded += new System.Windows.RoutedEventHandler(this.addSaleWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dateDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.wrapPanelScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 4:
            this.medicationsWrapPanel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 5:
            this.saveButton = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\Additional windows\AddSaleWindow.xaml"
            this.saveButton.Click += new System.Windows.RoutedEventHandler(this.saveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

