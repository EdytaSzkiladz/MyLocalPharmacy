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
    public partial class ConfirmRepeat : PhoneApplicationPage
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ConfirmRepeat()
        {
            InitializeComponent();
            lbxDrugs.ItemsSource = App.prescriptionCollection;
            this.DataContext = new ConfirmRepeatViewModel();
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

            if (App.TombStonedPageURL.Equals("RequestSent"))
            {
                App.TombStonedPageURL = PageURL.navigateToHomePanoramaURL;
            }
            else
            {
                App.TombStonedPageURL = PageURL.navigateToConfirmRepeatURL;
            }

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }

        }
        /// <summary>
        /// Overriding BackKeyPress
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (popupError.IsOpen)
            {
                popupError.IsOpen = false;
                e.Cancel = true;
                LayoutRoot.IsHitTestVisible = true;
            }
            else if (popupConfirm.IsOpen)
            {
                popupConfirm.IsOpen = false;
                e.Cancel = true;
                LayoutRoot.IsHitTestVisible = true;
            }
            else if (popupSent.IsOpen)
            {
                popupSent.IsOpen = false;
                e.Cancel = true;
                LayoutRoot.IsHitTestVisible = true;
            }
            else
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToNewRepeatSummaryURL, UriKind.Relative));
            }
        }

        #endregion
    }
}