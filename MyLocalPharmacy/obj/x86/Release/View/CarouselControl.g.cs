﻿#pragma checksum "D:\Joseph\SPRINT3\SPRINT3_30Sep635PM\30Sept6.30pm\30Sept6.30pm\30Sept6.30pm\MyLocalPharmacy\MyLocalPharmacy\View\CarouselControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DB8F3447BA1042C99ACEC71405BF77DF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
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


namespace MyLocalPharmacy.CarouselControl {
    
    
    public partial class CarouselControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid gridMainCarousel;
        
        internal System.Windows.Controls.Grid carouselArea;
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Controls.Button button_left;
        
        internal System.Windows.Controls.Button button_right;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLocalPharmacy;component/View/CarouselControl.xaml", System.UriKind.Relative));
            this.gridMainCarousel = ((System.Windows.Controls.Grid)(this.FindName("gridMainCarousel")));
            this.carouselArea = ((System.Windows.Controls.Grid)(this.FindName("carouselArea")));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.button_left = ((System.Windows.Controls.Button)(this.FindName("button_left")));
            this.button_right = ((System.Windows.Controls.Button)(this.FindName("button_right")));
        }
    }
}

