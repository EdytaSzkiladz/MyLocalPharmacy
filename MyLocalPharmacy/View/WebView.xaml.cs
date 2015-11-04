using System;
using System.Windows;
using Microsoft.Phone.Controls;
using MyLocalPharmacy.ViewModel;
using MyLocalPharmacy.Utils;
using System.Windows.Navigation;

namespace MyLocalPharmacy.View
{
    public partial class WebView : PhoneApplicationPage
    {
        public WebView()
        {
            InitializeComponent();
            App.PinResetFromSettingsPage = false;
            if (App.IsToombStoned)
            {
                this.Loaded += new RoutedEventHandler(CancelNavigationPage_Loaded);

            }
            else
            {
                WebViewViewModel objHPViewModel = new WebViewViewModel();
                this.DataContext = objHPViewModel;
                App.IsPageHomePanorama = true;
                App.IsPageUpdateYourDetailsafterLogin = false;
                App.IsFromLoginScreen = false;
            }
        }

        /// <summary>
        /// Loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CancelNavigationPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.IsToombStoned = false;
            this.NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string tag = "";

            if (NavigationContext.QueryString.TryGetValue("imageTag", out tag))
            {
                string url = App.ImagesUrl[Convert.ToInt32(tag)];
                WebBrowser webBrowser = new WebBrowser();
                webBrowser.Navigate(new Uri(url, UriKind.Absolute));
                webBrowser.Navigated += new EventHandler<NavigationEventArgs>(NavigateHandler);
                webBrowser.NavigationFailed += WebBrowser_NavigationFailed;
                webBrowser.Margin.Top.Equals(-12);
                webBrowser.IsScriptEnabled = true;
                browserGrid.Children.Add(webBrowser);
                imageName.Text = App.ImagesName[Convert.ToInt32(tag)];
            }
        }

        private void WebBrowser_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            progress.Visibility = Visibility.Collapsed;
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


    
    }
}