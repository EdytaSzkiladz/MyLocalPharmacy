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
using System.Windows.Input;

namespace MyLocalPharmacy.View
{
    public partial class ResetPinLogin : PhoneApplicationPage
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ResetPinLogin()
        {
            InitializeComponent();
            this.DataContext = new ResetPinLoginViewModel();
            ((ApplicationBarIconButton)this.ApplicationBar.Buttons[0]).IsEnabled = true;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event to navigate on tapping the PIN textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxPIN_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.resetLoginAuthCode = tbxID.Text;
            NavigationService.Navigate(new Uri(PageURL.navigateToResetPinURL, UriKind.Relative));            
        }
        /// <summary>
        /// Overriding the backkey press 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (popupIncorrectCode.IsOpen)
            {
                popupIncorrectCode.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else if (popupNoInternet.IsOpen)
            {
                popupNoInternet.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else if (popupReset.IsOpen)
            {
                popupReset.IsOpen = false;
                LayoutRoot.IsHitTestVisible = false;
                ((ApplicationBarIconButton)this.ApplicationBar.Buttons[0]).IsEnabled = false;
                e.Cancel = true;
            }
            else if (LayoutRoot.IsHitTestVisible == false)
            {
                Exit();
            }
            else if ((!string.IsNullOrEmpty(App.TombStonedPageURL)) && (!string.IsNullOrWhiteSpace(App.TombStonedPageURL)))
            {
                App.TombStonedPageURL = string.Empty;
                NavigationService.Navigate(new Uri(PageURL.navigateToLoginPanelURL, UriKind.Relative));
            }
            else
            {
                App.resetLoginAuthCode = null;
                App.resetLoginPIN = null;
                App.PIN = null;
                App.TombStonedPageURL = string.Empty;
                NavigationService.Navigate(new Uri(PageURL.navigateToLoginPanelURL, UriKind.Relative));
            }
        }
       
        /// <summary>
        /// Updating when text is changed in the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxID_TextChanged(object sender, TextChangedEventArgs e)
        {
            var focusedElement = FocusManager.GetFocusedElement();
            var focusedTextBox = focusedElement as TextBox;
            if (focusedTextBox != null)
            {
                var binding = focusedTextBox.GetBindingExpression(TextBox.TextProperty);
                if (binding != null)
                {
                    binding.UpdateSource();
                }
            }
        }
        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToResetPinLoginURL;

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
        }

        #endregion

        #region Methods
         /// <summary>
        /// Remove back stack entry
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