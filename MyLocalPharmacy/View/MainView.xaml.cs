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
    public partial class MainView : PhoneApplicationPage
    {
        public MainView()
        {
            InitializeComponent();
            App.PinResetFromSettingsPage = false;
            if (App.IsToombStoned)
            {
                this.Loaded += new RoutedEventHandler(CancelNavigationPage_Loaded);

            }
            else
            {
                MainViewViewModel objHPViewModel = new MainViewViewModel();
                this.DataContext = objHPViewModel;
                App.IsPageHomePanorama = true;
                App.IsPageUpdateYourDetailsafterLogin = false;
                App.IsFromLoginScreen = false;
                userControlCarousel.ListImagesCarousel = App.AddImages;
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
    }
}