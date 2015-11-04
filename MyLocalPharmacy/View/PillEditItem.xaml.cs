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
using Windows.Storage;
using SQLite;
using System.IO.IsolatedStorage;
using MyLocalPharmacy.Utils;
using System.IO;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Model;


namespace MyLocalPharmacy.View
{
    public partial class PillEditItem : PhoneApplicationPage
    {
        #region Declarations
        private string dataReadName;
        private string dataReadMilligram;
        private string dataReadNameMilligram;
        #endregion  
     
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PillEditItem()
        {
            InitializeComponent();
            this.DataContext = new PillEditItemViewModel();
          
             
        }
        #endregion

        #region Events
        /// <summary>
        /// To display pill search PopUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSearch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            lstDrugSearch.ItemsSource = null;
            acbDrugSearch.Text = string.Empty;
            PopupSearch.IsOpen = true;
            acbDrugSearch.Focus();
        }

        private  void acbDrugSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (acbDrugSearch.SelectedItem != null)
            {
                string data = acbDrugSearch.SelectedItem.ToString();
                tbxEditDrugSearchitems.Text = data;
               
            }
            PopupSearch.IsOpen = false;
        }

        /// <summary>
        /// For Displaying search result from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void acbDrugSearch_TextChanged(object sender, RoutedEventArgs e)
        {

            var folder = ApplicationData.Current.LocalFolder.GetFolderAsync("DataFolder");

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
                        dataReadMilligram = item.strenght;
                        dataReadNameMilligram = dataReadName + dataReadMilligram;
                        drugnamelist.Add(dataReadNameMilligram);
                    }
                    lstDrugSearch.ItemsSource = drugnamelist;
                }


            }
        }
        /// <summary>
        /// To select a pill from search result list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDrugSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDrugSearch.SelectedItem != null)
            {
                string data = (sender as ListBox).SelectedItem as string;
                tbxEditDrugSearchitems.Text = data;
            }
            PopupSearch.IsOpen = false;
          

           
        }
        /// <summary>
        /// Navigate to select surgen
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToPillEditItemURL;

         
            if (App.objPillsReminderModel != null)
            {
                if (!string.IsNullOrEmpty(App.objPillsReminderModel.NumberOfPills) && !string.IsNullOrWhiteSpace(App.objPillsReminderModel.NumberOfPills) && !string.IsNullOrEmpty(App.objPillsReminderModel.PillName) && !string.IsNullOrWhiteSpace(App.objPillsReminderModel.PillName))
                {
                    tbxEditDrugSearchitems.Text = App.objPillsReminderModel.PillName;
                    if (App.objPillsReminderModel.NumberOfPills.Contains('x'))
                    {
                        tbxQty.Text = App.objPillsReminderModel.NumberOfPills.Substring(3);
                    }
                    else
                    
                        tbxQty.Text = App.objPillsReminderModel.NumberOfPills;
                }
            }

            if ((!e.IsNavigationInitiator))
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
            
        }

       
        /// <summary>
        /// Set app Pill Colection Variables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxEditDrugSearchitems_TextChanged(object sender, TextChangedEventArgs e)
        {
            App.objPillsReminderModel.PillName = tbxEditDrugSearchitems.Text;
        }
        /// <summary>
        /// Method for back key
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {

            if (!string.IsNullOrEmpty(tbxEditDrugSearchitems.Text) && !string.IsNullOrWhiteSpace(tbxEditDrugSearchitems.Text) && !string.IsNullOrEmpty(tbxQty.Text) && !string.IsNullOrWhiteSpace(tbxQty.Text))
                NavigationService.Navigate(new Uri(PageURL.navigateToPillsReminderURL, UriKind.Relative));
            else
            {
                MessageBox.Show("Fill pill name and quantity.");
                e.Cancel = true;
            }
        }


        private void tbxQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            App.objPillsReminderModel.NumberOfPills = tbxQty.Text;
        }

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

        private void LoadEvery28DaysAutoCompleteSavedData()
        {
            if ((App.Every28DaysPillsCollection != null) && (App.Every28DaysPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.Every28DaysPillsCollection);
            }
        }

        private void LoadMonthlyAutoCompleteSavedData()
        {
            if ((App.MonthlyPillsCollection != null) && (App.MonthlyPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.MonthlyPillsCollection);
            }
        }

        private void LoadWeeklyAutoCompleteSavedData()
        {
            if ((App.WeeklyPillsCollection != null) && (App.WeeklyPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.WeeklyPillsCollection);
            }
        }

        private void LoadDNAutoCompleteSavedData()
        {
            if ((App.DailyNightPillsCollection != null) && (App.DailyNightPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.DailyNightPillsCollection);
            }
        }

        private void LoadDEAutoCompleteSavedData()
        {
            if ((App.DailyEveningPillsCollection != null) && (App.DailyEveningPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.DailyEveningPillsCollection);
            }
        }

        private void LoadDAAutoCompleteSavedData()
        {
            if ((App.DailyAfternoonPillsCollection != null) && (App.DailyAfternoonPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.DailyAfternoonPillsCollection);
            }
        }

        private void LoadDMAutoCompleteSavedData()
        {
            if ((App.DailyMorningPillsCollection != null) && (App.DailyMorningPillsCollection.Count > 0))
            {
                BindPillsinAutoCompleteList(App.DailyMorningPillsCollection);
            }
        }

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
        #endregion

    }
}