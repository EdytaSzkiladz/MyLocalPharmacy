using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
namespace MyLocalPharmacy.View
{
    public partial class TermsAndConditions : PhoneApplicationPage
    {
        #region Declaration
        WebBrowser webBrowser = new WebBrowser();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TermsAndConditions()
        {
            InitializeComponent();
            this.DataContext = new TermsAndConditionsViewModel();
        }
        #endregion

        #region Events

        /// <summary>
        /// Page_Loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            progress.Visibility = Visibility.Visible;
            
            
            webBrowser.Navigate(new Uri(PageURL.termsandConditionLink, UriKind.Absolute));
            webBrowser.Navigated += new EventHandler<NavigationEventArgs>(NavigateHandler);                 
            webBrowser.Visibility = Visibility.Visible;
            
            webBrowser.IsScriptEnabled = true;
            ContentPanel.Children.Add(webBrowser);
           
        }
        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.TombStonedPageURL = PageURL.navigateToTermsandConditionURL;

            if ((!e.IsNavigationInitiator)&&(!App.IsFromLoginScreen))
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
        }
        /// <summary>
        /// Event for back key
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (App.IsPageHomePanorama)
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToSettingPageURL, UriKind.RelativeOrAbsolute));
            }
            else if (App.IsFromLoginScreen)
            {
                if (App.IsFromsignUp)
                {
                    NavigationService.Navigate(new Uri(PageURL.navigateToSignUpPanelURL, UriKind.RelativeOrAbsolute));
                    App.IsFromsignUp = false;
                }
                else
                    NavigationService.Navigate(new Uri(PageURL.navigateToLoginPanelURL, UriKind.RelativeOrAbsolute));
            }
            else if (App.IsPageUpdateYourDetailsafterLogin)
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToYourDetailswithTCURL, UriKind.RelativeOrAbsolute));
            }
            else
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToYourDetailsLoginURL, UriKind.RelativeOrAbsolute));
            }
        }
        /// <summary>
        /// Navigate event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateHandler(object sender, NavigationEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            if (wb != null)
            {
                wb.Visibility = Visibility.Visible;
                progress.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// Refresh click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoRefresh_Click(object sender, EventArgs e)
        {
            progress.Visibility = Visibility.Visible;
            webBrowser.Navigate(new Uri(PageURL.termsandConditionLink, UriKind.Absolute));
            webBrowser.Navigated += new EventHandler<NavigationEventArgs>(NavigateHandler); 

        }
        /// <summary>
        /// Forward click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoForward_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoForward)
            {
                webBrowser.GoForward();
            }
        }
        /// <summary>
        /// Backward click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoBackward_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoBack)
            {
                webBrowser.GoBack();
            }
        }

        #endregion
    }
}