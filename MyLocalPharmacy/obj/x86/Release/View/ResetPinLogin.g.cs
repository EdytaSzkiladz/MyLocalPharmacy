﻿#pragma checksum "E:\Documents\Abhi\QBurst\Projects\Samples\Test Good\Integrated\21Oct6.00pm\21Oct6.00pm\MyLocalPharmacy\MyLocalPharmacy\View\ResetPinLogin.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "800ECEB2D994E89EADA240799C1487B6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
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
    
    
    public partial class ResetPinLogin : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.PhoneTextBox tbxID;
        
        internal System.Windows.Controls.TextBlock tbkValidateAuthCode;
        
        internal Microsoft.Phone.Controls.PhoneTextBox tbxPIN;
        
        internal System.Windows.Controls.PasswordBox passwrdbxPin;
        
        internal System.Windows.Controls.TextBlock tbkValidatePin;
        
        internal System.Windows.Controls.Primitives.Popup popupIncorrectCode;
        
        internal System.Windows.Controls.Button btnPopupIncorrectOk;
        
        internal System.Windows.Controls.Primitives.Popup popupReset;
        
        internal System.Windows.Controls.Button btnOk;
        
        internal System.Windows.Controls.Primitives.Popup popupNoUser;
        
        internal System.Windows.Controls.Button btnNoUserOk;
        
        internal System.Windows.Controls.Primitives.Popup popupNoInternet;
        
        internal System.Windows.Controls.Button btnNointernetOk;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLocalPharmacy;component/View/ResetPinLogin.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tbxID = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("tbxID")));
            this.tbkValidateAuthCode = ((System.Windows.Controls.TextBlock)(this.FindName("tbkValidateAuthCode")));
            this.tbxPIN = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("tbxPIN")));
            this.passwrdbxPin = ((System.Windows.Controls.PasswordBox)(this.FindName("passwrdbxPin")));
            this.tbkValidatePin = ((System.Windows.Controls.TextBlock)(this.FindName("tbkValidatePin")));
            this.popupIncorrectCode = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupIncorrectCode")));
            this.btnPopupIncorrectOk = ((System.Windows.Controls.Button)(this.FindName("btnPopupIncorrectOk")));
            this.popupReset = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupReset")));
            this.btnOk = ((System.Windows.Controls.Button)(this.FindName("btnOk")));
            this.popupNoUser = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupNoUser")));
            this.btnNoUserOk = ((System.Windows.Controls.Button)(this.FindName("btnNoUserOk")));
            this.popupNoInternet = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupNoInternet")));
            this.btnNointernetOk = ((System.Windows.Controls.Button)(this.FindName("btnNointernetOk")));
        }
    }
}

