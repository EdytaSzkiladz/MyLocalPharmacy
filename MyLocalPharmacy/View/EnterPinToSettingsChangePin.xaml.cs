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
    public partial class EnterPinToSettingsChangePin : PhoneApplicationPage
    {
        #region Declaration
        EnterPinToSettingsChangePinViewModel objViewModel = new EnterPinToSettingsChangePinViewModel();
        #endregion

        #region Constructor
        public EnterPinToSettingsChangePin()
        {
            InitializeComponent();
            this.DataContext = new EnterPinToSettingsChangePinViewModel();
        }
        #endregion

        #region Events       
        /// <summary>
        /// Event to capture button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string buttonTag = (sender as FrameworkElement).Tag.ToString();
            switch (buttonTag)
            {
                case "Tag1":
                    Append("1");
                    break;
                case "Tag2":
                    Append("2");
                    break;
                case "Tag3":
                    Append("3");
                    break;
                case "Tag4":
                    Append("4");
                    break;
                case "Tag5":
                    Append("5");
                    break;
                case "Tag6":
                    Append("6");
                    break;
                case "Tag7":
                    Append("7");
                    break;
                case "Tag8":
                    Append("8");
                    break;
                case "Tag9":
                    Append("9");
                    break;
                case "Tag0":
                    Append("0");
                    break;
            }
        }
        
        /// <summary>
        /// Remove image click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_Click(object sender, RoutedEventArgs e)
        {
            String val = objViewModel.Pin;
            if (!string.IsNullOrEmpty(val))
            {
                objViewModel.Pin = val.Remove(val.Length - 1, 1);
            }
            else
            {
                objViewModel.Pin = val;
            }

            this.DataContext = objViewModel;
        }
        /// <summary>
        /// GotFocus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt1_GotFocus(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }
        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.TombStonedPageURL = PageURL.navigateToEnterPinSettingsChangePinURL;

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
            if (popupIncorrectPin.IsOpen)
            {
                popupIncorrectPin.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else if (popupPreventAccess.IsOpen)
            {
                popupPreventAccess.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else if (popupResetPin.IsOpen)
            {
                popupResetPin.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else
                NavigationService.Navigate(new Uri(PageURL.navigateToSettingPageURL, UriKind.Relative));
        }

        #endregion

        #region Methods
            /// <summary>
            /// Method to append the digits pressed
            /// </summary>
            /// <param name="buttonValue"></param>
            private void Append(String buttonValue)
            {
                String val = objViewModel.Pin;

                if ((val + buttonValue).Length <= 4)
                {
                    objViewModel.Pin = val + buttonValue;
                }
                else
                {
                    objViewModel.Pin = val;
                }

                this.DataContext = objViewModel;
            }
        #endregion
    }
}