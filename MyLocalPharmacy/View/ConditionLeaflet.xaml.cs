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
    public partial class ConditionLeaflet : PhoneApplicationPage
    {
        #region Declarations
        WebBrowser webBrowser = new WebBrowser();
        string source = "leaflet";
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ConditionLeaflet()
        {
            InitializeComponent();
            this.DataContext = new ConditionLeafletViewModel();
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
            if (App.LeafletWebLink.Contains("http://") || App.LeafletWebLink.Contains("https://"))
                webBrowser.Navigate(new Uri(App.LeafletWebLink, UriKind.Absolute));
            else
            {
                string qualifiedUri = string.Concat("http://", App.LeafletWebLink);
                webBrowser.Navigate(new Uri(qualifiedUri, UriKind.Absolute));
            }
            webBrowser.Navigated += new EventHandler<NavigationEventArgs>(NavigateHandler);

            webBrowser.Visibility = Visibility.Visible;
            webBrowser.Margin.Top.Equals(-12);

            webBrowser.IsScriptEnabled = true;

            LayoutRoot.Children.Add(webBrowser);

        }
        /// <summary>
        /// Navigate Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateHandler(object sender, NavigationEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            if (wb != null)
            {
                progress.Visibility = Visibility.Collapsed;
                wb.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.TombStonedPageURL = PageURL.navigateToLeafletURL;

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }

            
            if (NavigationContext.QueryString.TryGetValue("source", out source))
            {
                source = "pharmacy";
            }
        }
        /// <summary>
        /// Method for back key
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (source!=null)
            {
                if (source.Equals("pharmacy"))
                {
                    NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.Relative));
                }
            }
            else
                NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL + "?goto=4", UriKind.Relative));
        } 
       

        /// <summary>
        /// Refresh Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoRefresh_Click(object sender, EventArgs e)
        {
            progress.Visibility = Visibility.Visible;
            if (App.LeafletWebLink.Contains("http://") || App.LeafletWebLink.Contains("https://"))
                webBrowser.Navigate(new Uri(App.LeafletWebLink, UriKind.Absolute));
            else
            {
                string qualifiedUri = string.Concat("http://", App.LeafletWebLink);
                webBrowser.Navigate(new Uri(qualifiedUri, UriKind.Absolute));
            }
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
        #endregion
    }
}