﻿#pragma checksum "E:\Documents\Abhi\QBurst\Projects\Samples\Test Good\Temp\MyLocalPharmacy\MyLocalPharmacy\View\Verification.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "033B61B2E626A67F3E89136AA33C776C"
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
    
    
    public partial class Verification : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Primitives.Popup popupVerified;
        
        internal System.Windows.Controls.Button btnOk;
        
        internal System.Windows.Controls.Primitives.Popup popupConfirmationSend;
        
        internal System.Windows.Controls.Button btnConfirmationOk;
        
        internal System.Windows.Controls.Primitives.Popup popupMailResent;
        
        internal System.Windows.Controls.Button btnResendOk;
        
        internal System.Windows.Controls.Button btnResend;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLocalPharmacy;component/View/Verification.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.popupVerified = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupVerified")));
            this.btnOk = ((System.Windows.Controls.Button)(this.FindName("btnOk")));
            this.popupConfirmationSend = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupConfirmationSend")));
            this.btnConfirmationOk = ((System.Windows.Controls.Button)(this.FindName("btnConfirmationOk")));
            this.popupMailResent = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupMailResent")));
            this.btnResendOk = ((System.Windows.Controls.Button)(this.FindName("btnResendOk")));
            this.btnResend = ((System.Windows.Controls.Button)(this.FindName("btnResend")));
        }
    }
}
