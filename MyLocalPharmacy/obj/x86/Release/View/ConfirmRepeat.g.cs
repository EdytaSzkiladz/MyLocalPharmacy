﻿#pragma checksum "C:\Users\jisan_000\Downloads\23Oct2.00pm\23Oct2.00pm\MyLocalPharmacy\MyLocalPharmacy\View\ConfirmRepeat.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "08B9F2636D265B864CE87D1FDD503E17"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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
    
    
    public partial class ConfirmRepeat : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Primitives.Popup popupError;
        
        internal System.Windows.Controls.Button btnErrorPopupOk;
        
        internal System.Windows.Controls.Primitives.Popup popupSent;
        
        internal System.Windows.Controls.Button btnSentPopupOk;
        
        internal System.Windows.Controls.Primitives.Popup popupConfirm;
        
        internal System.Windows.Controls.Button btnPopupcancel;
        
        internal System.Windows.Controls.Button btnPopupOk;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.ListBox lbxDrugs;
        
        internal System.Windows.Controls.Button btnSend;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLocalPharmacy;component/View/ConfirmRepeat.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.popupError = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupError")));
            this.btnErrorPopupOk = ((System.Windows.Controls.Button)(this.FindName("btnErrorPopupOk")));
            this.popupSent = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupSent")));
            this.btnSentPopupOk = ((System.Windows.Controls.Button)(this.FindName("btnSentPopupOk")));
            this.popupConfirm = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupConfirm")));
            this.btnPopupcancel = ((System.Windows.Controls.Button)(this.FindName("btnPopupcancel")));
            this.btnPopupOk = ((System.Windows.Controls.Button)(this.FindName("btnPopupOk")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.lbxDrugs = ((System.Windows.Controls.ListBox)(this.FindName("lbxDrugs")));
            this.btnSend = ((System.Windows.Controls.Button)(this.FindName("btnSend")));
        }
    }
}

