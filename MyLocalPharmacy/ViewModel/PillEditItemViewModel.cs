using Microsoft.Phone.Controls;
using MyLocalPharmacy.Common;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    class PillEditItemViewModel : BaseViewModel
    {
        #region Declarations
        PillsReminderModel objPillsReminderModelToDaply = new PillsReminderModel();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PillEditItemViewModel()
        {
            if (App.ObjBrandingResponse != null)
            {
                PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);
                SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
                FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
            }
            else
            {
                PrimaryColour = SecondaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);
                FontColor = Utilities.GetColorFromHexa(RxConstants.FontColourCode);
            }
            App.IsEditCancelled = true;
            if (App.HeaderPillsReminder.Equals("daily morning"))
                objPillsReminderModelToDaply = App.DailyMorningPillsCollection[App.SelectedIndexToEdit];
            if (App.HeaderPillsReminder.Equals("daily afternoon"))
                objPillsReminderModelToDaply = App.DailyAfternoonPillsCollection[App.SelectedIndexToEdit];
            if (App.HeaderPillsReminder.Equals("daily evening"))
                objPillsReminderModelToDaply = App.DailyEveningPillsCollection[App.SelectedIndexToEdit];
            if (App.HeaderPillsReminder.Equals("daily night"))
                objPillsReminderModelToDaply = App.DailyNightPillsCollection[App.SelectedIndexToEdit];
            if (App.HeaderPillsReminder.Equals("weekly"))
                objPillsReminderModelToDaply = App.WeeklyPillsCollection[App.SelectedIndexToEdit];
            if (App.HeaderPillsReminder.Equals("monthly"))
                objPillsReminderModelToDaply = App.MonthlyPillsCollection[App.SelectedIndexToEdit];
            if (App.HeaderPillsReminder.Equals("every 28 days"))
                objPillsReminderModelToDaply = App.Every28DaysPillsCollection[App.SelectedIndexToEdit];

            qty = objPillsReminderModelToDaply.NumberOfPills;
            qty = qty.Replace(" x ", string.Empty);
            _pillName = objPillsReminderModelToDaply.PillName;
            _buttonValue = "Update";
        }

        #endregion

        #region Properties
        /// <summary>
        /// Secondary Color
        /// </summary>
        private SolidColorBrush _secondaryColour;
        [IgnoreDataMember]
        public SolidColorBrush SecondaryColour
        {
            get { return _secondaryColour; }
            set
            {
                _secondaryColour = value;
                OnPropertyChanged("SecondaryColour");
            }
        }

        /// <summary>
        /// Font Color
        /// </summary>
        private SolidColorBrush _fontColor;
        [IgnoreDataMember]
        public SolidColorBrush FontColor
        {
            get { return _fontColor; }
            set
            {
                _fontColor = value;
                OnPropertyChanged("FontColor");
            }
        }
        /// <summary>
        /// Primary Color
        /// </summary>
        private SolidColorBrush _primaryColour;
        [IgnoreDataMember]
        public SolidColorBrush PrimaryColour
        {
            get { return _primaryColour; }
            set
            {
                _primaryColour = value;
                OnPropertyChanged("PrimaryColour");
            }
        }
        /// <summary>
        /// For Quantity
        /// </summary>
        private string qty;
        [DataMember]
        public string Qty
        {
            get { return qty; }
            set
            {
                qty = value;
                OnPropertyChanged("Qty");
            }
        }

        /// <summary>
        /// For Pill name
        /// </summary>
        private string _pillName;
        [DataMember]
        public string PillNamesToUpdate
        {
            get { return _pillName; }
            set
            {
                _pillName = value;
                OnPropertyChanged("PillNamesToUpdate");
            }
        }
        /// <summary>
        /// For button content
        /// </summary>
        private string _buttonValue;
        [DataMember]
        public string ButtonValue
        {
            get { return _buttonValue; }
            set
            {
                _buttonValue = value;
                OnPropertyChanged("ButtonValue");
            }
        }

        /// <summary>
        /// Update pill editing
        /// </summary>
        private RelayCommand _navigateToPillsreminderPage;
        [IgnoreDataMember]
        public RelayCommand NavigateToPillsreminderPage
        {
            get
            {
                if (_navigateToPillsreminderPage == null)
                {
                    _navigateToPillsreminderPage = new RelayCommand(AddToPillsCollection);
                    _navigateToPillsreminderPage.Enabled = true;
                }
                return _navigateToPillsreminderPage;
            }
            set
            {
                _navigateToPillsreminderPage = value;
            }
        }
        
        #endregion

        #region Methods
        /// <summary>
        /// Method to add pills to collection
        /// </summary>
        private void AddToPillsCollection()
        {
            App.IsPillUpdated = true;
            if (!string.IsNullOrEmpty(PillNamesToUpdate) && !string.IsNullOrWhiteSpace(PillNamesToUpdate))
            {

                if ((!string.IsNullOrEmpty(Qty) && !string.IsNullOrWhiteSpace(Qty)))
                {
                    int QtyToInt = 0;
                    try
                    {
                        QtyToInt = Convert.ToInt16(Qty);
                            Qty = String.Concat(" x ", Qty);


                            if (App.HeaderPillsReminder.Equals("daily morning"))
                            {
                                App.DailyMorningPillsCollection[App.SelectedIndexToEdit].PillName = PillNamesToUpdate;
                                App.DailyMorningPillsCollection[App.SelectedIndexToEdit].NumberOfPills = Qty;

                            }
                            if (App.HeaderPillsReminder.Equals("daily afternoon"))
                            {
                                App.DailyAfternoonPillsCollection[App.SelectedIndexToEdit].PillName = PillNamesToUpdate;
                                App.DailyAfternoonPillsCollection[App.SelectedIndexToEdit].NumberOfPills = Qty;
                            }
                            if (App.HeaderPillsReminder.Equals("daily evening"))
                            {
                                App.DailyEveningPillsCollection[App.SelectedIndexToEdit].PillName = PillNamesToUpdate;
                                App.DailyEveningPillsCollection[App.SelectedIndexToEdit].NumberOfPills = Qty;
                            }
                            if (App.HeaderPillsReminder.Equals("daily night"))
                            {
                                App.DailyNightPillsCollection[App.SelectedIndexToEdit].PillName = PillNamesToUpdate;
                                App.DailyNightPillsCollection[App.SelectedIndexToEdit].NumberOfPills = Qty;
                            }
                            if (App.HeaderPillsReminder.Equals("weekly"))
                            {
                                App.WeeklyPillsCollection[App.SelectedIndexToEdit].PillName = PillNamesToUpdate;
                                App.WeeklyPillsCollection[App.SelectedIndexToEdit].NumberOfPills = Qty;
                            }
                            if (App.HeaderPillsReminder.Equals("monthly"))
                            {
                                App.MonthlyPillsCollection[App.SelectedIndexToEdit].PillName = PillNamesToUpdate;
                                App.MonthlyPillsCollection[App.SelectedIndexToEdit].NumberOfPills = Qty;
                            }
                            if (App.HeaderPillsReminder.Equals("every 28 days"))
                            {
                                App.Every28DaysPillsCollection[App.SelectedIndexToEdit].PillName = PillNamesToUpdate;
                                App.Every28DaysPillsCollection[App.SelectedIndexToEdit].NumberOfPills = Qty;
                            }


                            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                            bool success = false;
                            success = frame.Navigate(new Uri(PageURL.navigateToPillsReminderURL, UriKind.Relative));
                       

                    }
                    catch (OverflowException ex)
                    {
                        MessageBox.Show("Please enter a quantity between 1 and 9999");
                        Qty = string.Empty;
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("Enter a number");
                        Qty = string.Empty;
                    }
                }
                else
                    MessageBox.Show("Please enter quantity.");
            }
            else
                MessageBox.Show("Please enter pill name and quantity.");
        }
        #endregion

    }
}
