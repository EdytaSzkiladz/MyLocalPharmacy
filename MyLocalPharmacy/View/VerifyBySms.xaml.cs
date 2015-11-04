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
using MyLocalPharmacy.Utils;

namespace MyLocalPharmacy.View
{
    public partial class VerifyBySms : PhoneApplicationPage
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public VerifyBySms()
        {
            InitializeComponent();
            this.DataContext = new VerifyBySmsViewModel();
        }
        #endregion

        #region Events

        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToVerificationBySMSURL;

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
        }
        /// <summary>
        /// Overriding back key press
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (popupCodeResent.IsOpen)
            {
                popupCodeResent.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;                
                e.Cancel = true;
            }
            else if (popupNoInternet.IsOpen)
            {
                popupNoInternet.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else if (popupIncorrectCode.IsOpen)
            {
                popupIncorrectCode.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else if (popupRequestCode.IsOpen)
            {
                popupRequestCode.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else if (popupVerified.IsOpen)
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.Relative));
            }

            else if (LayoutRoot.IsHitTestVisible == false)
            {
                Exit();
            }
            else
            {
                Exit();
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Remove entries from back stack
        /// </summary>
        private void Exit()
        {
            while (NavigationService.BackStack.Any())
            {
                NavigationService.RemoveBackEntry();

            }
        }
        #endregion
       
    }
}