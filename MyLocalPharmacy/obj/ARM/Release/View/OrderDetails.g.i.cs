﻿#pragma checksum "E:\Documents\Abhi\QBurst\RX-Share\24Nov_WithSQL\MyLocalPharmacy\MyLocalPharmacy\View\OrderDetails.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "19E6D4C8A253E2F945A96D2D40ED09DD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
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
    
    
    public partial class OrderDetails : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock tbxOrderDate;
        
        internal System.Windows.Controls.Primitives.Popup popupCancelled;
        
        internal System.Windows.Controls.Button btnCancelledPopupOk;
        
        internal System.Windows.Controls.Primitives.Popup popupConfirm;
        
        internal System.Windows.Controls.Button btnPopupcancel;
        
        internal System.Windows.Controls.Button btnPopupOk;
        
        internal System.Windows.Controls.Button btnCancelOrder;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLocalPharmacy;component/View/OrderDetails.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tbxOrderDate = ((System.Windows.Controls.TextBlock)(this.FindName("tbxOrderDate")));
            this.popupCancelled = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupCancelled")));
            this.btnCancelledPopupOk = ((System.Windows.Controls.Button)(this.FindName("btnCancelledPopupOk")));
            this.popupConfirm = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupConfirm")));
            this.btnPopupcancel = ((System.Windows.Controls.Button)(this.FindName("btnPopupcancel")));
            this.btnPopupOk = ((System.Windows.Controls.Button)(this.FindName("btnPopupOk")));
            this.btnCancelOrder = ((System.Windows.Controls.Button)(this.FindName("btnCancelOrder")));
        }
    }
}

