﻿#pragma checksum "D:\Joseph\SPRINT3\MyLocalPharmacy\MyLocalPharmacy\View\ConditionSearch.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E65986D559C334D6DE0095078E954F19"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MyLocalPharmacy.View {
    
    
    public partial class ConditionSearch : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ProgressBar progress;
        
        internal Microsoft.Phone.Controls.PhoneTextBox tbxSearch;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock tbknoLeaflet;
        
        internal System.Windows.Controls.ListBox LbxLeaflets;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLocalPharmacy;component/View/ConditionSearch.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.progress = ((System.Windows.Controls.ProgressBar)(this.FindName("progress")));
            this.tbxSearch = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("tbxSearch")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tbknoLeaflet = ((System.Windows.Controls.TextBlock)(this.FindName("tbknoLeaflet")));
            this.LbxLeaflets = ((System.Windows.Controls.ListBox)(this.FindName("LbxLeaflets")));
        }
    }
}

