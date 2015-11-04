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
using MyLocalPharmacy.Entities;

namespace MyLocalPharmacy.View
{
    public partial class ConditionSearch : PhoneApplicationPage
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ConditionSearch()
        {
            InitializeComponent();
            this.DataContext = new ConditionSearchViewModel();
        }
        #endregion

        #region Events

        /// <summary>
        /// Loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            tbxSearch.Focus();
        }
        /// <summary>
        /// Search Icon Tap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSearch_ActionIconTapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxSearch.Text) || !string.IsNullOrWhiteSpace(tbxSearch.Text))
            {
                App.ConditionSearchName = tbxSearch.Text;

                this.Focus();
                ConditionSearchViewModel searchViewModel = new ConditionSearchViewModel();
                searchViewModel.FillList();
                this.DataContext = searchViewModel;

            }
        }
        /// <summary>
        /// Search Key Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSearch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                
                if (!string.IsNullOrEmpty(tbxSearch.Text) || !string.IsNullOrWhiteSpace(tbxSearch.Text))
                {
                    App.ConditionSearchName = tbxSearch.Text;

                    this.Focus();
                    ConditionSearchViewModel searchViewModel = new ConditionSearchViewModel();
                    searchViewModel.FillList();
                    this.DataContext = searchViewModel;

                }
                
            }
        }
        /// <summary>
        /// Method for back key
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL + "?goto=4", UriKind.Relative));
        }
        /// <summary>
        /// OnNaviagtedTo Event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToLeafletSearchURL;

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }

        }
        /// <summary>
        /// List box selection changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbxLeaflets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = (sender as ListBox).SelectedItem as ConditionSearchResponse;

            App.LeafletWebLink = data.WebLink;
            NavigationService.Navigate(new Uri(PageURL.navigateToLeafletURL, UriKind.Relative));

        }
        #endregion
    }
}