﻿#pragma checksum "D:\MyLocalPharmacy_18Aug_7.20pm\MyLocalPharmacy_18Aug_7.20pm\MyLocalPharmacy\MyLocalPharmacy\View\VerifyBySms.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "762CA0304FC73A932CD7A4F3967AF2DD"
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
    
    
    public partial class VerifyBySms : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.PhoneTextBox tbxID;
        
        internal System.Windows.Controls.Primitives.Popup popupRequestCode;
        
        internal System.Windows.Controls.Button btnPopupWaitOk;
        
        internal System.Windows.Controls.Primitives.Popup popupCodeResent;
        
        internal System.Windows.Controls.Button btnPopupResentOk;
        
        internal System.Windows.Controls.Primitives.Popup popupIncorrectCode;
        
        internal System.Windows.Controls.Button btnPopupIncorrectOk;
        
        internal System.Windows.Controls.Primitives.Popup popupVerified;
        
        internal System.Windows.Controls.Button btnOk;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLocalPharmacy;component/View/VerifyBySms.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tbxID = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("tbxID")));
            this.popupRequestCode = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupRequestCode")));
            this.btnPopupWaitOk = ((System.Windows.Controls.Button)(this.FindName("btnPopupWaitOk")));
            this.popupCodeResent = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupCodeResent")));
            this.btnPopupResentOk = ((System.Windows.Controls.Button)(this.FindName("btnPopupResentOk")));
            this.popupIncorrectCode = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupIncorrectCode")));
            this.btnPopupIncorrectOk = ((System.Windows.Controls.Button)(this.FindName("btnPopupIncorrectOk")));
            this.popupVerified = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupVerified")));
            this.btnOk = ((System.Windows.Controls.Button)(this.FindName("btnOk")));
        }
    }
}

