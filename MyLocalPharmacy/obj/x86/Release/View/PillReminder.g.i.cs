﻿#pragma checksum "C:\Users\jisan_000\Downloads\30Oct12.30pm_jisna\MyLocalPharmacy\MyLocalPharmacy\View\PillReminder.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "860A30F7F7C53CAA528F3C106B955C5C"
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
using Microsoft.Phone.Shell;
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
    
    
    public partial class PillReminder : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Primitives.Popup PopupSearch;
        
        internal Microsoft.Phone.Controls.AutoCompleteBox acbDrugSearch;
        
        internal System.Windows.Controls.ListBox lstDrugSearch;
        
        internal System.Windows.Controls.Primitives.Popup popupConfirm;
        
        internal System.Windows.Controls.Button btnPopupcancel;
        
        internal System.Windows.Controls.Button btnPopupOk;
        
        internal System.Windows.Controls.Primitives.Popup popupConfirmLeavePage;
        
        internal System.Windows.Controls.Button btnPopupcancelLeavePage;
        
        internal System.Windows.Controls.Button btnPopupOkLeavePage;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.ToggleSwitch toggleName;
        
        internal Microsoft.Phone.Controls.DatePicker dpkDate;
        
        internal Microsoft.Phone.Controls.TimePicker tpkTime;
        
        internal Microsoft.Phone.Controls.PhoneTextBox tbxDrugSearch;
        
        internal System.Windows.Controls.Image imgSearch;
        
        internal Microsoft.Phone.Controls.PhoneTextBox tbxQty;
        
        internal System.Windows.Controls.ListBox PillsReminderList;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton btnSave;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyLocalPharmacy;component/View/PillReminder.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PopupSearch = ((System.Windows.Controls.Primitives.Popup)(this.FindName("PopupSearch")));
            this.acbDrugSearch = ((Microsoft.Phone.Controls.AutoCompleteBox)(this.FindName("acbDrugSearch")));
            this.lstDrugSearch = ((System.Windows.Controls.ListBox)(this.FindName("lstDrugSearch")));
            this.popupConfirm = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupConfirm")));
            this.btnPopupcancel = ((System.Windows.Controls.Button)(this.FindName("btnPopupcancel")));
            this.btnPopupOk = ((System.Windows.Controls.Button)(this.FindName("btnPopupOk")));
            this.popupConfirmLeavePage = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popupConfirmLeavePage")));
            this.btnPopupcancelLeavePage = ((System.Windows.Controls.Button)(this.FindName("btnPopupcancelLeavePage")));
            this.btnPopupOkLeavePage = ((System.Windows.Controls.Button)(this.FindName("btnPopupOkLeavePage")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.toggleName = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("toggleName")));
            this.dpkDate = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("dpkDate")));
            this.tpkTime = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("tpkTime")));
            this.tbxDrugSearch = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("tbxDrugSearch")));
            this.imgSearch = ((System.Windows.Controls.Image)(this.FindName("imgSearch")));
            this.tbxQty = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("tbxQty")));
            this.PillsReminderList = ((System.Windows.Controls.ListBox)(this.FindName("PillsReminderList")));
            this.btnSave = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("btnSave")));
        }
    }
}

