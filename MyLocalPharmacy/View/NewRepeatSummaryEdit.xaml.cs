using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
using SQLite;
using MyLocalPharmacy.Entities;
using System.IO;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;

namespace MyLocalPharmacy.View
{
    public partial class NewRepeatSummaryEdit : PhoneApplicationPage
    {
        #region Declarations
        PrescriptionCollection prescriptionCollection = new PrescriptionCollection();
        DrugDetails selectedData;
        Prescription prescription;
        bool _isSearchDone = false;
         int QtyValidator;
         string QtyValidatorToString;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public NewRepeatSummaryEdit()
        {
            InitializeComponent();
            this.DataContext = new NewRepeatSummaryEditViewModel();
        } 
        #endregion

        #region Events
        /// <summary>
        /// Event invoked on tapping search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgSearch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            acbDrugSearch.Text = string.Empty;
            PopupSearch.IsOpen = true;
            lstDrugSearch.ItemsSource = null;
            acbDrugSearch.Focus();
        }
        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToNewRepeatSummaryEditURL;

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
        }
        /// <summary>
        /// OnNavigatedFrom event
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
        /// Search drugs when text input
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
                    List<DrugDetails> druglist = new List<DrugDetails>();
                    foreach (var item in result)
                    {
                        DrugDetails dataRead = new DrugDetails();
                        dataRead._id = item._id;
                        dataRead.amp = item.amp;
                        dataRead.ampp =item.ampp;
                        dataRead.drug_code =item.drug_code;
                        dataRead.drugform = item.drugform;
                        dataRead.strenght =item.strenght;
                        dataRead.drugname = string.Format("{0}  {1}", item.drugname, item.strenght);
                        dataRead.size =item.size;
                        dataRead.vmp =item.vmp;
                        dataRead.vmpp =item.vmpp;
                        druglist.Add(dataRead);
                    }
                    lstDrugSearch.ItemsSource = druglist;
                }
            }
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
                string selectedDrug = acbDrugSearch.SelectedItem.ToString();
                tbxDrugSearch.Text = selectedDrug;
            }
                PopupSearch.IsOpen = false;
        }

        /// <summary>
        /// Drug search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDrugSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedData = (sender as ListBox).SelectedItem as DrugDetails;
            _isSearchDone = true;
            if (selectedData != null)
            {
                prescription = new Prescription();

                if (!string.IsNullOrEmpty(selectedData.amp))
                    prescription.amp = selectedData.amp;
                else
                    prescription.amp = string.Empty;
                if (!string.IsNullOrEmpty(selectedData.ampp))
                    prescription.ampp = selectedData.ampp;
                else
                    prescription.ampp = string.Empty;

                tbxDrugSearch.Text = selectedData.drugname;

                if (!string.IsNullOrEmpty(selectedData.vmp))
                    prescription.vmp = selectedData.vmp;
                else
                    prescription.vmp = string.Empty;

                if (!string.IsNullOrEmpty(selectedData.vmpp))
                    prescription.vmpp = selectedData.vmpp;
                else
                    prescription.vmpp = string.Empty;

                PopupSearch.IsOpen = false;
            }
            else
            {
                PopupSearch.IsOpen = true;
            }
        }

        /// <summary>
        /// Load the saved searches
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acbDrugSearch_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.autoCompleteEditListDrug != null)
            {
                acbDrugSearch.ItemsSource = App.autoCompleteEditListDrug;
            }
        }

        /// <summary>
        /// Back key press event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (PopupSearch.IsOpen)
            {
                PopupSearch.IsOpen = false;
                e.Cancel = true;
            }
            else
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToNewRepeatSummaryURL, UriKind.Relative));
            }
        }

        /// <summary>
        /// Method invoked on cliking update button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool _isValid = false;
            if (_isSearchDone == false)
            {
                prescription = new Prescription();

                if (!string.IsNullOrEmpty(tbxDrugSearch.Text) && !string.IsNullOrWhiteSpace(tbxDrugSearch.Text))
                {
                    if (!string.IsNullOrEmpty(tbxQty.Text) || !string.IsNullOrWhiteSpace(tbxQty.Text))
                    {
                        try
                        {

                            QtyValidator = Convert.ToInt16(tbxQty.Text);
                            QtyValidatorToString = Convert.ToString(QtyValidator);
                            if ((QtyValidator <= 0) || (QtyValidator >= 10000))
                            {
                                MessageBox.Show("Please enter a quantity between 1 and 9999 ");

                            }
                            else
                            {
                                prescription.quantity = QtyValidatorToString;
                                _isValid = true;
                                prescription.drugname = tbxDrugSearch.Text;
                                if (!string.IsNullOrEmpty(tbxReason.Text) || !string.IsNullOrWhiteSpace(tbxReason.Text))
                                {
                                    prescription.reason = tbxReason.Text;
                                }
                                else
                                {
                                    prescription.reason = string.Empty;
                                }
                                prescription.amp = string.Empty;
                                prescription.vmp = string.Empty;
                                prescription.vmpp = string.Empty;
                                prescription.ampp = string.Empty;
                            }
                        }
                        catch (OverflowException ex)
                        {
                            MessageBox.Show("Please enter a quantity between 1 and 9999");
                            tbxQty.Text = string.Empty;
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show("Enter a number");
                            tbxQty.Text = string.Empty;
                        }
                    }
                    else
                    {
                        _isValid = false;
                        MessageBox.Show("Enter quantity of drug.");
                    }
                }

                else
                {
                    _isValid = false;
                    MessageBox.Show("Enter drug name.");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(tbxDrugSearch.Text) || !string.IsNullOrWhiteSpace(tbxDrugSearch.Text))
                {
                    if (!string.IsNullOrEmpty(tbxQty.Text) || !string.IsNullOrWhiteSpace(tbxQty.Text))
                    {
                        try
                        {
                            QtyValidator = Convert.ToInt16(tbxQty.Text);
                            QtyValidatorToString = Convert.ToString(QtyValidator);
                            if ((QtyValidator <= 0) || (QtyValidator >= 10000))
                            {
                                MessageBox.Show("Please enter a quantity between 1 and 9999 ");

                            }
                            else
                            {
                                prescription.quantity = QtyValidatorToString;
                                _isValid = true;
                                prescription.drugname = tbxDrugSearch.Text;
                                if (!string.IsNullOrEmpty(tbxReason.Text) || !string.IsNullOrWhiteSpace(tbxReason.Text))
                                {
                                    prescription.reason = tbxReason.Text;
                                }
                                else
                                {
                                    prescription.reason = string.Empty;
                                }
                            }
                        }
                        catch (OverflowException ex)
                        {
                            MessageBox.Show("Please enter a quantity between 1 and 9999");
                            tbxQty.Text = string.Empty;
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show("Enter a number");
                            tbxQty.Text = string.Empty;
                        }
                    }

                    else
                    {
                        _isValid = false;
                        MessageBox.Show("Enter quantity of drug.");
                    }
                }

                else
                {
                    _isValid = false;
                    MessageBox.Show("Enter drug name.");
                }

            }

            if (_isValid == true)
            {
                App.autoCompleteEditListDrug.Add(tbxDrugSearch.Text);
                App.prescriptionCollection.ElementAt(App.selectedDrugIndex).amp = prescription.amp;
                App.prescriptionCollection.ElementAt(App.selectedDrugIndex).ampp = prescription.ampp;
                App.prescriptionCollection.ElementAt(App.selectedDrugIndex).drugname = prescription.drugname;
                App.prescriptionCollection.ElementAt(App.selectedDrugIndex).quantity = prescription.quantity;
                App.prescriptionCollection.ElementAt(App.selectedDrugIndex).reason = prescription.reason;
                App.prescriptionCollection.ElementAt(App.selectedDrugIndex).vmp = prescription.vmp;
                App.prescriptionCollection.ElementAt(App.selectedDrugIndex).vmpp = prescription.vmpp;
                NavigationService.Navigate(new Uri(PageURL.navigateToNewRepeatSummaryURL, UriKind.Relative));
            }
        }
        #endregion
        
    }
}