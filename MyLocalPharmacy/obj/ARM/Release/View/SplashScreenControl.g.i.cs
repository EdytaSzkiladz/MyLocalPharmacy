﻿#pragma checksum "E:\Documents\Abhi\QBurst\RX-Share\24Nov_WithSQL\MyLocalPharmacy\MyLocalPharmacy\View\SplashScreenControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9E17B4BF9FD3362B096675C792883305"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    
    
    public partial class SplashScreenControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Image image1;
        
        internal System.Windows.Controls.TextBlock tbxLocalPharmacylabel;
        
        internal System.Windows.Controls.TextBlock tbxRxLabel;
        
        internal System.Windows.Controls.ProgressBar progBarSplash;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLocalPharmacy;component/View/SplashScreenControl.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.image1 = ((System.Windows.Controls.Image)(this.FindName("image1")));
            this.tbxLocalPharmacylabel = ((System.Windows.Controls.TextBlock)(this.FindName("tbxLocalPharmacylabel")));
            this.tbxRxLabel = ((System.Windows.Controls.TextBlock)(this.FindName("tbxRxLabel")));
            this.progBarSplash = ((System.Windows.Controls.ProgressBar)(this.FindName("progBarSplash")));
        }
    }
}

