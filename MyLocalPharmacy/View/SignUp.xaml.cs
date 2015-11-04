using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyLocalPharmacy.ViewModel;
using System.Threading;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Windows.Storage.Streams;
using MyLocalPharmacy.Utils;

using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using SQLite;
using MyLocalPharmacy.Entities;
using System.Windows.Data;
using System.Globalization;

namespace MyLocalPharmacy.View
{
    public partial class SignUp : PhoneApplicationPage
    {
        #region Declaration
        bool isNewPageInstance = false;
        #endregion

        #region Constructor
        /// Constructor
        /// </summary>
        public SignUp()
        {
            InitializeComponent();
            isNewPageInstance = true;
            App.IsFromLoginScreen = true;
            if (!tbxEmail.Text.Equals(App.transientMailId))
                Utilities.ClearReminderData();
            btnPharmacyDet.IsHitTestVisible = true;
           
        }  
        #endregion

        #region Events

        /// <summary>
        /// OnNavigatedFrom event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode != System.Windows.Navigation.NavigationMode.Back)
            {
                
                    if (!string.IsNullOrEmpty(tbxID.Text))
                        App.SignUpPharId = tbxID.Text;
                    if (!string.IsNullOrEmpty(tbxLoginID.Text))
                        App.LoginPharId = tbxLoginID.Text;
                    if (!string.IsNullOrEmpty(tbxEmail.Text))
                    {
                        if (!App.IsUserNotExist)
                        {
                            App.LoginEmailId = tbxEmail.Text;
                            App.MailIdToFillAfterPin = tbxEmail.Text;
                        }
                        else
                        {
                            App.LoginEmailId = string.Empty;
                            App.MailIdToFillAfterPin = tbxEmail.Text;
                        }
                    }

                    if (!string.IsNullOrEmpty(tbkpharname.Text))
                        App.LoginPharmacyname = tbkpharname.Text;
                    if (!string.IsNullOrEmpty(tbkadd1.Text))
                        App.LoginPharmacyAddress1 = tbkadd1.Text;
                    if (!string.IsNullOrEmpty(tbkadd2.Text))
                        App.LoginPharmacyAddress2 = tbkadd2.Text;
                    if (!string.IsNullOrEmpty(tbkadd3.Text))
                        App.LoginPharmacyAddress3 = tbkadd3.Text;
                    if (!string.IsNullOrEmpty(tbkpin.Text))
                        App.PostCode = tbkpin.Text;
                
            }
        }
        /// <summary>
        /// OnNaviagted to event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SignUpViewModel vmSignUp = null;
            if (isNewPageInstance)
            {
               
                if (vmSignUp == null)
                {
                    vmSignUp = new SignUpViewModel();
                    if (App.LoginPharmacyname != null)
                    {
                        vmSignUp.PharmacyName = App.LoginPharmacyname;
                        vmSignUp.AddressLine1 = App.LoginPharmacyAddress1;
                        vmSignUp.AddressLine2 = App.LoginPharmacyAddress2;
                        vmSignUp.AddressLine3 = App.LoginPharmacyAddress3;
                        vmSignUp.PinCode = App.PostCode;
                    }
                    else
                    {
                        vmSignUp.IsPharmacyDetailsVisible = Visibility.Collapsed;
                        
                    }

                }
                DataContext = vmSignUp;
            }
          

            BaseViewModel viewModel = this.DataContext as BaseViewModel;
            viewModel.Initialize(this.NavigationContext.QueryString);
            isNewPageInstance = false;
        }

        /// <summary>
        /// Navigate to Setup pin page for signup user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxPIN_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri((PageURL.navigateToSetPinURL), UriKind.Relative));
        }

        /// <summary>
        /// Navigate to enter pin oage for login user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxloginPIN_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.MailIdToFillAfterPin = tbxEmail.Text;
            NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL + "?sender=login", UriKind.Relative));
        }
        /// <summary>
        /// Overriding back key press
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (popup.IsOpen)
            {
                popup.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;               
            }
            else if (popupConfirm.IsOpen)
            {
                popupConfirm.IsOpen = false;
                e.Cancel = true;
                LayoutRoot.IsHitTestVisible = true;
            }
            else if (popupWait.IsOpen)
            {
                popupWait.IsOpen = false;
                e.Cancel = true;
                LayoutRoot.IsHitTestVisible = true;
            }
            else if (popupIncorrectPin.IsOpen)
            {
                popupIncorrectPin.IsOpen = false;
                e.Cancel = true;
                LayoutRoot.IsHitTestVisible = true;
            }    
            else
            {
                Exit();
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method to remove back entry
        /// </summary>
        private void Exit()
        {
            while (NavigationService.BackStack.Any())
            {
                NavigationService.RemoveBackEntry();
            }
        }
        #endregion

        private void tbxID_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbxID.Text = tbxID.Text.ToUpper();
            tbxID.SelectionStart = tbxID.Text.Length;
        }

        private void tbxLoginID_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbxLoginID.Text = tbxLoginID.Text.ToUpper();
            tbxLoginID.SelectionStart = tbxLoginID.Text.Length;
        }
    }
}