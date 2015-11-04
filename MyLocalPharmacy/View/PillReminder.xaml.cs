using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyLocalPharmacy.ViewModel;
using System.Windows.Media;
using System.IO.IsolatedStorage;
using MyLocalPharmacy.Utils;
using Windows.Storage;
using SQLite;
using System.IO;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Model;
using System.Windows.Controls.Primitives;
using System.Windows.Automation.Peers;
using System.Collections.Generic;
using System;

namespace MyLocalPharmacy.View
{
    public partial class PillReminder : PhoneApplicationPage
    {
        #region Declarations

        private string dataReadName;
        private string dataReadMilligram;
        private string dataReadNameMilligram;
        PillsReminderModel objPillsReminderModel = new PillsReminderModel();

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PillReminder()
        {
            InitializeComponent();
            App.objPillsReminderModel.PillName = string.Empty;
            App.objPillsReminderModel.NumberOfPills = string.Empty;
            if (App.IsToombStoned)
            {
                App.IsReminderToombstoned = true;
                this.Loaded += new RoutedEventHandler(CancelNavigationPage_Loaded);
            }
            else
            {
                this.DataContext = new PillsReminderViewModel();
            }
        }
        #endregion
        
        #region Events
        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
           
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToPillsReminderURL;

            if (App.IsApplicationInstancePreserved)
            {
                App.IsReminderToombstoned = true;
                App.IsApplicationInstancePreserved = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
        }

        /// <summary>
        /// Edit selection changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PillsReminderListEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.SelectedIndexToEdit = PillsReminderList.SelectedIndex;
            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            bool success = false;
            if (App.SelectedIndexToEdit >= 0)
            {
                success = frame.Navigate(new Uri(PageURL.navigateToPillEditItemURL, UriKind.Relative));
            }
        }
        /// <summary>
        /// Drug search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDrugSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string data = string.Empty;
            data = (sender as ListBox).SelectedItem as string;
            PillsReminderViewModel vm = this.DataContext as PillsReminderViewModel;
            vm.PillNames = data;
            this.DataContext = vm;
            PopupSearch.IsOpen = false;
            ApplicationBar.IsVisible = true;
            ((ApplicationBarIconButton)this.ApplicationBar.Buttons[0]).IsEnabled = true;
        }
        /// <summary>
        /// Search drugs when text input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void acbDrugSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            var folder = ApplicationData.Current.LocalFolder.GetFolderAsync("DataFolder");

            try
            {
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Path.Combine(ApplicationData.Current.LocalFolder.Path, "DataFile.sqlite"), true);
               
                string pillname = acbDrugSearch.Text;
                if (!string.IsNullOrEmpty(pillname) && !string.IsNullOrWhiteSpace(pillname) && pillname.Length > 3)
                {

                    var query = conn.Table<drugs>().Where(x => x.drugname.Contains(pillname));
                    var result = await query.ToListAsync();
                    if (result == null)
                    {
                        MessageBox.Show("Pill not found.");
                    }
                    else
                    {
                        List<string> drugnamelist = new List<string>();
                        foreach (var item in result)
                        {
                            dataReadName =item.drugname;
                            dataReadMilligram =item.strenght;
                            dataReadNameMilligram = dataReadName + dataReadMilligram;
                            drugnamelist.Add(dataReadNameMilligram);
                        }
                        lstDrugSearch.ItemsSource = drugnamelist;
                    }
                }
            }
            catch (Exception ex)
            {

              
            }


        }
        /// <summary>
        /// Open search popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSearch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            lstDrugSearch.ItemsSource = null;
            acbDrugSearch.Text = string.Empty;
            PopupSearch.IsOpen = true;
            ApplicationBar.IsVisible = false;
            ((ApplicationBarIconButton)this.ApplicationBar.Buttons[0]).IsEnabled = false;
            acbDrugSearch.Focus();
          
            
        }

      
        /// <summary>
        /// Auto Complete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acbDrugSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (acbDrugSearch.SelectedItem != null)
            {
                string data = acbDrugSearch.SelectedItem.ToString();
                PillsReminderViewModel vm = this.DataContext as PillsReminderViewModel;
                vm.PillNames = data;
                this.DataContext = vm;
            }
            PopupSearch.IsOpen = false;
            ApplicationBar.IsVisible = true;
            ((ApplicationBarIconButton)this.ApplicationBar.Buttons[0]).IsEnabled = true;
        }
        /// <summary>
        /// Method for back key
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            PillsReminderModelCol objReminderCollection = new PillsReminderModelCol();
            DateTime DateSet=new DateTime();
            DateTime TimeSet = new DateTime();
          

            if (App.HeaderPillsReminder.Equals("daily morning"))
            {
                objReminderCollection = App.DailyMorningPillsCollection;
                DateSet = App.DailyMorningReminderDate;
                TimeSet=App.DailyMorningReminderTime;
            }
            else if (App.HeaderPillsReminder.Equals("daily afternoon"))
            {
                objReminderCollection = App.DailyAfternoonPillsCollection;
                DateSet = App.DailyAfternoonReminderDate;
                TimeSet=App.DailyAfternoonReminderTime;
            }
            else if (App.HeaderPillsReminder.Equals("daily evening"))
            {
                objReminderCollection = App.DailyEveningPillsCollection;
                DateSet = App.DailyEveningReminderDate;
                TimeSet=App.DailyEveningReminderTime;
            }
            else if (App.HeaderPillsReminder.Equals("daily night"))
            {
                objReminderCollection = App.DailyNightPillsCollection;
                DateSet = App.DailyNightReminderDate;
                TimeSet=App.DailyNightReminderTime;
            }
            else if (App.HeaderPillsReminder.Equals("weekly"))
            {
                objReminderCollection = App.WeeklyPillsCollection;
                DateSet = App.WeeklyReminderDate;
                TimeSet=App.WeeklyReminderTime;
            }
            else if (App.HeaderPillsReminder.Equals("monthly"))
            {
                objReminderCollection = App.MonthlyPillsCollection;
                DateSet = App.MonthlyReminderDate;
                TimeSet=App.MonthlyReminderTime;
            }
            else if (App.HeaderPillsReminder.Equals("every 28 days"))
            {
                objReminderCollection = App.Every28DaysPillsCollection;
                DateSet = App.Every28DaysReminderDate;
                TimeSet = App.Every28DaysReminderTime;
            }

            if (PopupSearch.IsOpen)
            {
                PopupSearch.IsOpen = false;
                ApplicationBar.IsVisible = true;
                ((ApplicationBarIconButton)this.ApplicationBar.Buttons[0]).IsEnabled = true;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else if (popupConfirmLeavePage.IsOpen)
            {
                popupConfirmLeavePage.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
            else   if (objReminderCollection != null)
            {
                if (App.IsCheckedTempStore != App.IsChecked || App.ReminderDateTemp != DateSet || App.ReminderTimeTemp != TimeSet || (App.PillsReminderModelColLocalStorage.Count != objReminderCollection.ToList().Count && objReminderCollection != null) || App.IsPillUpdated == true)
                {
                    App.IsPillUpdated = false;
                    popupConfirmLeavePage.IsOpen = true;
                    LayoutRoot.IsHitTestVisible = false;
                    e.Cancel = true;
                }
                else
                {
                    NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL + "?goto=3", UriKind.RelativeOrAbsolute));

                }
            }
            else if (objReminderCollection==null)
            {
                if (App.IsCheckedTempStore != App.IsChecked || App.ReminderDateTemp != DateSet || App.ReminderTimeTemp != TimeSet || App.IsPillUpdated == true)
                {
                    App.IsPillUpdated = false;
                    popupConfirmLeavePage.IsOpen = true;
                    LayoutRoot.IsHitTestVisible = false;
                    e.Cancel = true;
                }
                else
                {
                    NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL + "?goto=3", UriKind.RelativeOrAbsolute));

                }
            }

           
        }
        /// <summary>
        /// Load the saved searches
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acbDrugSearch_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.ReminderToDisplay.Equals("daily morning reminder"))
            {
                LoadDMAutoCompleteSavedData();
            }
            else if (App.ReminderToDisplay.Equals("daily afternoon reminder"))
            {
                LoadDAAutoCompleteSavedData();
            }
            else if (App.ReminderToDisplay.Equals("daily evening reminder"))
            {
                LoadDEAutoCompleteSavedData();
            }
            else if (App.ReminderToDisplay.Equals("daily night reminder"))
            {
                LoadDNAutoCompleteSavedData();
            }
            else if (App.ReminderToDisplay.Equals("weekly reminder"))
            {
                LoadWeeklyAutoCompleteSavedData();
            }
            else if (App.ReminderToDisplay.Equals("monthly reminder"))
            {
                LoadMonthlyAutoCompleteSavedData();
            }
            else if (App.ReminderToDisplay.Equals("every 28 days reminder"))
            {
                LoadEvery28DaysAutoCompleteSavedData();
            }
        }
        /// <summary>
        /// Close popup on cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPopupcancel_Click(object sender, RoutedEventArgs e)
        {
            popupConfirm.IsOpen = false;
            LayoutRoot.IsHitTestVisible = true;
            
        }
        /// <summary>
        /// Delete when ok of popup clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPopupOk_Click(object sender, RoutedEventArgs e)
        {
            PillsReminderViewModel vm = this.DataContext as PillsReminderViewModel;
            App.IsPillUpdated = true;
            List<PillsReminderModel> local = new List<PillsReminderModel>();
            local = App.PillsReminderModelColLocalStorage.ToList();

            if (App.HeaderPillsReminder.Equals("daily morning"))
                App.DailyMorningPillsCollection.Remove(objPillsReminderModel);
            else if (App.HeaderPillsReminder.Equals("daily afternoon"))
                App.DailyAfternoonPillsCollection.Remove(objPillsReminderModel);
            else if (App.HeaderPillsReminder.Equals("daily evening"))
                App.DailyEveningPillsCollection.Remove(objPillsReminderModel);
            else if (App.HeaderPillsReminder.Equals("daily night"))
                App.DailyNightPillsCollection.Remove(objPillsReminderModel);
            else if (App.HeaderPillsReminder.Equals("weekly"))
                App.WeeklyPillsCollection.Remove(objPillsReminderModel);
            else if (App.HeaderPillsReminder.Equals("monthly"))
                App.MonthlyPillsCollection.Remove(objPillsReminderModel);
            else if (App.HeaderPillsReminder.Equals("every 28 days"))
                App.Every28DaysPillsCollection.Remove(objPillsReminderModel);

            popupConfirm.IsOpen = false;
            LayoutRoot.IsHitTestVisible = true;

        }
        /// <summary>
        /// Get the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PillsReminderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            popupConfirm.IsOpen = true;
            LayoutRoot.IsHitTestVisible = false;
            objPillsReminderModel = (sender as ListBox).SelectedItem as PillsReminderModel;
        }

       
        #endregion

        #region Methods

        /// <summary>
        /// Method to navigate to enter pin page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CancelNavigationPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.IsToombStoned = false;
            this.NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Method to show daily morning auto complete data
        /// </summary>
        private void LoadDMAutoCompleteSavedData()
        {
            if ((App.DailyMorningPillsCollection != null) && (App.DailyMorningPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.DailyMorningPillsCollection);
            }
        }
        /// <summary>
        ///  Method to show daily afternoon auto complete data
        /// </summary>
        private void LoadDAAutoCompleteSavedData()
        {
            if ((App.DailyAfternoonPillsCollection != null) && (App.DailyAfternoonPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.DailyAfternoonPillsCollection);
            }
        }
        /// <summary>
        /// Method to show daily evening auto complete data
        /// </summary>
        private void LoadDEAutoCompleteSavedData()
        {
            if ((App.DailyEveningPillsCollection != null) && (App.DailyEveningPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.DailyEveningPillsCollection);
            }
        }
        /// <summary>
        ///  Method to show daily night auto complete data
        /// </summary>
        private void LoadDNAutoCompleteSavedData()
        {
            if ((App.DailyNightPillsCollection != null) && (App.DailyNightPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.DailyNightPillsCollection);
            }
        }
        /// <summary>
        /// Method to show weekly auto complete data
        /// </summary>
        private void LoadWeeklyAutoCompleteSavedData()
        {
            if ((App.WeeklyPillsCollection != null) && (App.WeeklyPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.WeeklyPillsCollection);
            }
        }
        /// <summary>
        ///  Method to show monthly auto complete data
        /// </summary>
        private void LoadMonthlyAutoCompleteSavedData()
        {
            if ((App.MonthlyPillsCollection != null) && (App.MonthlyPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.MonthlyPillsCollection);
            }
        }
        /// <summary>
        /// Method to show every28days auto complete data
        /// </summary>
        private void LoadEvery28DaysAutoCompleteSavedData()
        {
            if ((App.Every28DaysPillsCollection != null) && (App.Every28DaysPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.Every28DaysPillsCollection);
            }
        }
        /// <summary>
        /// Method to bind the autocomplete list
        /// </summary>
        /// <param name="pillsReminderModelCol"></param>
        private void BindPillsinAutoCompleteList(PillsReminderModelCol pillsReminderModelCol)
        {
            List<string> autoCompleteLst = new List<string>();

            PillsReminderModel objPillsPillsReminderModel;

            foreach (var item in pillsReminderModelCol)
            {
                objPillsPillsReminderModel = new PillsReminderModel { PillName = item.PillName, NumberOfPills = item.NumberOfPills };
                autoCompleteLst.Add(objPillsPillsReminderModel.PillName);
            }
            acbDrugSearch.ItemsSource = autoCompleteLst;
        }
        /// <summary>
        /// Delete pills
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePill(object sender, RoutedEventArgs e)
        {
            popupConfirm.IsOpen = true;
            LayoutRoot.IsHitTestVisible = false;
            objPillsReminderModel = (sender as MenuItem).DataContext as PillsReminderModel;
        }

        #endregion

        
    }
}