using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Globalization;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Globalization;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using MyLocalPharmacy.Entities;
using Microsoft.Phone.Tasks;
using MyLocalPharmacy.Contract;


namespace MyLocalPharmacy.View
{
    public partial class HomePanorama : PhoneApplicationPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public HomePanorama()
        {
            InitializeComponent();
            
            App.PinResetFromSettingsPage = false;
            if (App.IsToombStoned )
            {
                this.Loaded += new RoutedEventHandler(CancelNavigationPage_Loaded);
                
            }
            else
            {
                HomePanoramaViewModel objHPViewModel = new HomePanoramaViewModel();
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

        #endregion

        #region PharamcyDetails
        /// <summary>
        /// Tap Call image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgCall_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string phoneNumber = App.PharmacyPhoneNo;
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.PhoneNumber = phoneNumber;
                phoneCallTask.Show();
            }
        }

       
        /// <summary>
        /// Tap Get Direction image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToPharmacyDetailsMapURL, UriKind.Relative));
        }
        #endregion

        #region ConditionLeaflet
        /// <summary>
        /// ConditionLeaflet LongList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conditionLeafletsLongList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var selectedElement = e.OriginalSource as FrameworkElement;

            if (selectedElement != null)
            {
                ConditionLeafletsResponse selectedData = selectedElement.DataContext as ConditionLeafletsResponse;

                if (selectedData != null)
                {
                    App.LeafletWebLink = selectedData.WebLink;
                    NavigationService.Navigate(new Uri(PageURL.navigateToLeafletURL, UriKind.Relative));
                }
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// On Navigatedto event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {           
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToHomePanoramaURL;
            

            if (App.IsApplicationInstancePreserved)
            {
                App.IsApplicationInstancePreserved = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }

            if (!Utilities.IsConnectedToNetwork())
            {
                if (App.ObjLgResponse != null)
                {
                    if (App.ObjLgResponse.status == 0)
                    {

                        if (App.ObjLgResponse.payload.status.Equals(RxConstants.userStatusRemoved))
                        {
                            NavigationService.Navigate(new Uri(PageURL.navigateToAccountDisabledURL + "?message=" + App.ObjLgResponse.message + "&title=account removed", UriKind.RelativeOrAbsolute));
                        }
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri(PageURL.navigateToAccountDisabledURL + "?message=" + App.ObjLgResponse.message + "&title=account disabled", UriKind.RelativeOrAbsolute));

                    }
                }
            }

            string strItemIndex;
            if (NavigationContext.QueryString.TryGetValue("goto", out strItemIndex))
            {
                panoramaMenu.DefaultItem = (PanoramaItem)panoramaMenu.Items[Convert.ToInt32(strItemIndex)];
            }
        }
        /// <summary>
        /// OnNavigated From event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (!(e.NavigationMode == NavigationMode.Back))
            {
    
            }
        }
        /// <summary>
        /// Override back key press
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (popup.IsOpen == true)
            {
                popup.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
                NavigationService.Navigate(new Uri(PageURL.navigateToLoginPanelURL, UriKind.RelativeOrAbsolute));


            }
            else if (popupRejected.IsOpen == true)
            {
                popup.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
                NavigationService.Navigate(new Uri(PageURL.navigateToYourDetailsUpdateURL, UriKind.RelativeOrAbsolute));
            }

            else
            {
                App.IsToombStoned = true;
                new Utils.StateHelper().SaveAppLevelPersistantData();
                new Utils.StateHelper().SaveAppLevelTransientData();
                Exit();
            }
        }

        /// <summary>
        /// On Selection change navigate to "OrderDetails" screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOrder = (sender as ListBox).SelectedItem as OrderedPillDetails;
            if (selectedOrder != null)
            {
                App.selectedOrder = selectedOrder;
                NavigationService.Navigate(new Uri(PageURL.navigateToOrderDetailsURL, UriKind.Relative));
            }
        }
        /// <summary>
        /// Delete Drug from list 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteDrug_Click(object sender, RoutedEventArgs e)
        {
            List<int> IdsToDelete = new List<int>();
            var selectedOrder = (sender as MenuItem).DataContext as OrderedPillDetails;
            if (selectedOrder.status.Equals(RxConstants.OrderStatusToCancelled) || selectedOrder.status.Equals(RxConstants.OrderStatusToCollected) || selectedOrder.status.Equals(RxConstants.OrderStatusToDelivered))
            {
                IdsToDelete.Add(selectedOrder.id);
                HomePanoramaViewModel _objHomePanoramaViewModel = this.DataContext as HomePanoramaViewModel;
                _objHomePanoramaViewModel.DeleteOrder(IdsToDelete);

            }
            else
            {
                MessageBox.Show("Cannot be deleted");
            }

        }
        #endregion

        #region PillsReminder

        #region Properties

        #endregion

        #region Methods
        /// <summary>
        /// Method for Navigating on Tapping daily morning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dMorning_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.PillsReminderModelColLocalStorage = new List<PillsReminderModel>();
            App.HeaderPillsReminder = "daily morning";
            App.IsReminderToombstoned = false;
            NavigationService.Navigate(new Uri(PageURL.navigateToPillsReminderURL, UriKind.Relative));
        }
        /// <summary>
        /// Method for Navigating on Tapping daily morning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dAfternoon_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.PillsReminderModelColLocalStorage = new List<PillsReminderModel>();
            App.HeaderPillsReminder = "daily afternoon";
            App.IsReminderToombstoned = false;
            NavigationService.Navigate(new Uri(PageURL.navigateToPillsReminderURL, UriKind.Relative));
        }
        /// <summary>
        /// Method for Navigating on Tapping daily evening
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dEvening_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.PillsReminderModelColLocalStorage = new List<PillsReminderModel>();
            App.HeaderPillsReminder = "daily evening";
            App.IsReminderToombstoned = false;
            NavigationService.Navigate(new Uri(PageURL.navigateToPillsReminderURL, UriKind.Relative));
        }
        /// <summary>
        /// Method for Navigating on Tapping daily night
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dNight_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.PillsReminderModelColLocalStorage = new List<PillsReminderModel>();
            App.HeaderPillsReminder = "daily night";
            App.IsReminderToombstoned = false;
            NavigationService.Navigate(new Uri(PageURL.navigateToPillsReminderURL, UriKind.Relative));
        }
        /// <summary>
        /// Method for Navigating on Tapping Weekly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Weekly_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.PillsReminderModelColLocalStorage = new List<PillsReminderModel>();
            App.HeaderPillsReminder = "weekly";
            App.IsReminderToombstoned = false;
            NavigationService.Navigate(new Uri(PageURL.navigateToPillsReminderURL, UriKind.Relative));
        }
        /// <summary>
        /// Method for Navigating on Tapping Monthly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Monthly_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.PillsReminderModelColLocalStorage = new List<PillsReminderModel>();
            App.HeaderPillsReminder = "monthly";
            App.IsReminderToombstoned = false;
            NavigationService.Navigate(new Uri(PageURL.navigateToPillsReminderURL, UriKind.Relative));
        }
        /// <summary>
        /// Method for Navigating on Tapping Every28Days
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void E28Days_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.PillsReminderModelColLocalStorage = new List<PillsReminderModel>();
            App.HeaderPillsReminder = "every 28 days";
            App.IsReminderToombstoned = false;
            NavigationService.Navigate(new Uri(PageURL.navigateToPillsReminderURL, UriKind.Relative));
        }
        #endregion
        #endregion

        #region Method
        /// <summary>
        /// Method to remove back entry
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