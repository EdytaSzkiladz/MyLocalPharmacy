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
    public partial class OrderDetails : PhoneApplicationPage
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderDetails()
        {
            InitializeComponent();
            this.DataContext = new OrderDetailsViewModel();
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
            App.TombStonedPageURL = PageURL.navigateToOrderDetailsURL;

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
        }
        
        /// <summary>
        /// Back key press event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (popupCancelled.IsOpen)
            {
                popupCancelled.IsOpen = false;
                e.Cancel = true;
            }
            else if (popupConfirm.IsOpen)
            {
                popupConfirm.IsOpen = false;
                e.Cancel = true;
            }
            else
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL + "?goto=2", UriKind.Relative));
            }
        } 
        #endregion
    }
}