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
    public partial class ConfirmPin : PhoneApplicationPage
    {
        #region Declaration
        ConfirmPinViewModel objConfirmPinVM = new ConfirmPinViewModel();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ConfirmPin()
        {
            InitializeComponent();
            this.DataContext = new ConfirmPinViewModel();
        }
        #endregion

        #region Events  
        /// <summary>
        /// Event to capture the buttons clicked
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
        /// Image Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_Click(object sender, RoutedEventArgs e)
        {
            String val = objConfirmPinVM.Pin;
            
            if (!string.IsNullOrEmpty(val))
            {
                objConfirmPinVM.Pin = val.Remove(val.Length - 1, 1);
            }
            else
            {
                objConfirmPinVM.Pin = val;
            }

            this.DataContext = objConfirmPinVM;
        }

        /// <summary>
        /// Method for back key
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToSetPinURL, UriKind.Relative));
        }  
        /// <summary>
        /// GotFocus Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwrdbxPin_GotFocus(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }
        /// <summary>
        /// OnNavigatedTo Event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToConfirmPinURL;

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }

        }

        #endregion

        #region Methods
        /// <summary>
        /// Method to append the digits pressed
        /// </summary>
        /// <param name="buttonValue"></param>
        private void Append(String buttonValue)
        {
            String val = objConfirmPinVM.Pin;

            if ((val + buttonValue).Length <= 4)
            {
                objConfirmPinVM.Pin = val + buttonValue;
            }
            else
            {
                objConfirmPinVM.Pin = val;
            }

            this.DataContext = objConfirmPinVM;
        }
        #endregion
    }
}