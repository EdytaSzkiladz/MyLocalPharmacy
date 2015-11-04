using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using MyLocalPharmacy.ViewModel;
using MyLocalPharmacy.Utils;

namespace MyLocalPharmacy.View
{
    public partial class AccountDisabled : PhoneApplicationPage
    {
        string message;
        string title;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountDisabled()
        {
            InitializeComponent();
            
            this.DataContext = new AccountDisabledViewModel();
        } 
        #endregion

        #region Events
        /// <summary>
        /// On back key press Event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Exit();
        }
        /// <summary>
        /// On Navigated To event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            NavigationContext.QueryString.TryGetValue("message", out message);
            NavigationContext.QueryString.TryGetValue("title", out title);
            tbkMessage.Text = message;
            tbkTitle.Text = title;
        }

        /// <summary>
        /// Event on tapping call button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stplCall_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string phoneNumber = App.PharmacyPhoneNo;
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.PhoneNumber = phoneNumber;
                phoneCallTask.Show();
            }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Remove entries from back stack and exits the app
        /// </summary>
        private void Exit()
        {
            while (NavigationService.BackStack.Any())
            {
                NavigationService.RemoveBackEntry();

            }
        } 
        
        /// <summary>
        /// Refresh click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icoRefresh_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.RelativeOrAbsolute));
        }
        #endregion
    }
}