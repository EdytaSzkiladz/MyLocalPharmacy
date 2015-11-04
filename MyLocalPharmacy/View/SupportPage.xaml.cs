using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;

namespace MyLocalPharmacy.View
{
    public partial class SupportPage : PhoneApplicationPage
    {
        #region Declaration
        ProgressBar pgBar = new ProgressBar();
        WebBrowser webBrowser = new WebBrowser();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SupportPage()
        {
            InitializeComponent();

            this.DataContext = new SupportPageViewModel();

        }
        #endregion

        #region  Events
        /// <summary>
        /// Page_Loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            WebBrowser webBrowser = new WebBrowser();
           

            RowDefinition rdWebBrowser=new RowDefinition();
            RowDefinition rdProgressBar = new RowDefinition();

            rdProgressBar.Height = new GridLength(0.3, GridUnitType.Star);
            rdWebBrowser.Height = new GridLength(13, GridUnitType.Star);
            pgBar.Height = 10;
            pgBar.IsIndeterminate = true;
            pgBar.Visibility = Visibility.Visible;
            webBrowser.Navigate(new Uri(RxConstants.myLocalPharmacySupport, UriKind.Absolute));

            webBrowser.Navigated += new EventHandler<NavigationEventArgs>(NavigateHandler);
            webBrowser.Visibility = Visibility.Visible;
            webBrowser.Margin.Top.Equals(-12);
            webBrowser.IsScriptEnabled = true;
            
            ContentPanel.RowDefinitions.Insert(0, rdProgressBar);
            ContentPanel.RowDefinitions.Insert(1, rdWebBrowser);

            Grid.SetRow(pgBar, 0);
            Grid.SetRow(webBrowser, 1);

            ContentPanel.Children.Add(pgBar);
            ContentPanel.Children.Add(webBrowser);
           
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
                pgBar.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.TombStonedPageURL = PageURL.supportPageUrl;
            
            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
        }
        /// <summary>
        /// Method for back key
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToSettingPageURL, UriKind.Relative));
        }
        /// <summary>
        /// Refresh click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoRefresh_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(new Uri(RxConstants.myLocalPharmacySupport, UriKind.Absolute));
            pgBar.Visibility = Visibility.Visible;
            webBrowser.Navigated += new EventHandler<NavigationEventArgs>(NavigateHandler);
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
        /// <summary>
        /// GoForward click
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

        #endregion
    }
}