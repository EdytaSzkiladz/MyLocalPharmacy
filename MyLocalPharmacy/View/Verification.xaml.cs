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
using Windows.Storage.Streams;
using Windows.Security;
using MyLocalPharmacy.Utils;

namespace MyLocalPharmacy.View
{
    public partial class Verification : PhoneApplicationPage
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Verification()
        {
            InitializeComponent();
            if (App.IsToombStoned || App.IsApplicationInstancePreserved)
            {
                this.Loaded += new RoutedEventHandler(CancelNavigationPage_Loaded);

            }
            else
            {
                this.DataContext = new VerificationViewModel();
            }

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
            App.TombStonedPageURL = PageURL.navigateToVerificationByEmailURL;

            if (App.IsApplicationInstancePreserved)
            {
                App.IsApplicationInstancePreserved = false;
                App.IsToombStoned = false;
                
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }

        }

        /// <summary>
        /// Event to navigate to enter pin page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CancelNavigationPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.IsToombStoned = false;
            //App.IsApplicationInstancePreserved = false;
            this.NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Overriding back key press
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (popupMailResent.IsOpen)
            {
                popupMailResent.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;         
                e.Cancel = true;
            }
            else if (popupNoInternet.IsOpen)
            {
                popupNoInternet.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else if (popupConfirmationSend.IsOpen)
            {
                popupConfirmationSend.IsOpen = false;
                LayoutRoot.IsHitTestVisible = false;
               
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

        #region Method
        /// <summary>
        /// Remove entries from backstack
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