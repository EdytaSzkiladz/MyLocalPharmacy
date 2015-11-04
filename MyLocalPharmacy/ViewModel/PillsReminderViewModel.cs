﻿using Microsoft.Phone.Scheduler;
using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace MyLocalPharmacy.ViewModel
{

    public class PillsReminderViewModel : BaseViewModel
    {
        #region Declarations

        PillsReminderModelCol objPillsReminderModelCol = new PillsReminderModelCol();
        public Reminder reminder;
        bool IsLimitExceed = false;
        string pillNames = string.Empty;
      
       

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PillsReminderViewModel()
        {
            HeaderPillsReminder = App.HeaderPillsReminder;

            if (App.ObjBrandingResponse != null)
            {
                AppBarFontColour = App.ObjBrandingResponse.payload.branding_data.appearance.font_colour;
                AppBarPrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;
                PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);
                SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
                FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
            }
            else
            {
                AppBarFontColour = RxConstants.FontColourCode;
                AppBarPrimaryColour = RxConstants.PrimaryColourCodeAppbar;
                PrimaryColour = SecondaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);
                FontColor = Utilities.GetColorFromHexa(RxConstants.FontColourCode);
            }
            DisplayValues();
            ReminderTitle = App.HeaderPillsReminder;
            HitVisibility = true;
            if (App.IsReminderToombstoned)
            {
                ReminderDate = App.ReminderDateTemp;
                ReminderTime = App.ReminderTimeTemp;
                App.IsReminderToombstoned = false;
            }
            else
            {
                if (ReminderStatus.Equals("Off") || ReminderStatus.Equals(string.Empty))
                {
                    App.IsCheckedTempStore = false;
                    App.IsReminderToSet = false;
                }
                else
                {
                    App.IsCheckedTempStore = true;
                    App.IsReminderToSet = true; ;
                }
            }
           
        } 
        #endregion

        #region Properties
        /// <summary>
        /// For AppBar FontColor
        /// </summary>
        private string _appBarFontColour;
        public string AppBarFontColour
        {
            get { return _appBarFontColour; }
            set
            {
                _appBarFontColour = value;
                OnPropertyChanged("AppBarFontColour");
            }
        }
        /// <summary>
        /// For AppBar PrimaryColor
        /// </summary>
        private string _appBarPrimaryColour;
        public string AppBarPrimaryColour
        {
            get { return _appBarPrimaryColour; }
            set
            {
                _appBarPrimaryColour = value;
                OnPropertyChanged("AppBarPrimaryColour");
            }
        }
        /// <summary>
        /// Pills Collection
        /// </summary>
        private PillsReminderModelCol _pillsremindercol;
        public PillsReminderModelCol PillsReminderCollection
        {
            get { return _pillsremindercol; }
            set
            {
               
                _pillsremindercol = value;
                OnPropertyChanged("PillsReminderCollection");
            }
        }
        /// <summary>
        /// For Pill name
        /// </summary>
        private string _pillName;

        public string PillNames
        {
            get { return _pillName; }
            set
            {
                _pillName = value;
                OnPropertyChanged("PillNames");
            }
        }
        /// <summary>
        /// Pill HeaderName
        /// </summary>
        private string _pillHeaderName;

        public string HeaderPillsReminder
        {
            get { return _pillHeaderName; }
            set
            {
                _pillHeaderName = value;
                OnPropertyChanged("HeaderPillsReminder");
            }
        }

        /// <summary>
        /// Pill HeaderName
        /// </summary>
        private string _reminderTitle;

        public string ReminderTitle
        {
            get { return _reminderTitle; }
            set
            {
                _reminderTitle = value;
                OnPropertyChanged("ReminderTitle");
            }
        }
        /// <summary>
        /// Property to show and hide Confirm popup before leave page 
        /// </summary>
        private bool _isConfirmPopupOpenLeavePage;

        public bool IsConfirmPopupOpenLeavePage
        {
            get { return _isConfirmPopupOpenLeavePage; }
            set { _isConfirmPopupOpenLeavePage = value; OnPropertyChanged("IsConfirmPopupOpenLeavePage"); }
        }

        /// <summary>
        /// For Quantity
        /// </summary>
        private string qty;
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
        /// For Date
        /// </summary>
        private DateTime _date;
        public DateTime ReminderDate
        {
            get { return _date; }
            set
            {
               
                _date = value;
                if(!App.IsReminderToombstoned)
                    App.ReminderDateTemp = value;
                OnPropertyChanged("ReminderDate");
            }
        }
        /// <summary>
        /// For Time
        /// </summary>
        private DateTime _time;
        public DateTime ReminderTime
        {
            get { return _time; }
            set
            {
               
                _time = value;
                if (!App.IsReminderToombstoned)
                App.ReminderTimeTemp = value;
                OnPropertyChanged("ReminderTime");
            }
        }
        /// <summary>
        /// Reminder OnOff status
        /// </summary>
        private string _reminderOnOff = "Off";
        public string ReminderStatus
        {
            get { return _reminderOnOff; }
            set
            {
                _reminderOnOff = value;
                OnPropertyChanged("ReminderStatus");
            }
        }

        /// <summary>
        /// Property to set hit visibility
        /// </summary>
        private bool _hitVisibility;
        public bool HitVisibility
        {
            get { return _hitVisibility; }
            set { _hitVisibility = value; OnPropertyChanged("HitVisibility"); }
        }
        /// <summary>
        /// For Add click
        /// </summary>
        private RelayCommand addTapCommand;
        public RelayCommand AddTapCommand
        {
            get
            {
                if (addTapCommand == null)
                {
                    addTapCommand = new RelayCommand(AddingToList);
                    addTapCommand.Enabled = true;
                }
                return addTapCommand;
            }
            set { addTapCommand = value; OnPropertyChanged("AddTapCommand"); }
        }
        /// <summary>
        /// Save Pill
        /// </summary>
        private RelayCommand _SavePillCommand;
        public RelayCommand SavePillCommand
        {
            get
            {
                if (_SavePillCommand == null)
                {
                    _SavePillCommand = new RelayCommand(AddReminder);
                    _SavePillCommand.Enabled = true;
                }
                return _SavePillCommand;
            }
            set
            {
                _SavePillCommand = value;
            }
        }
        /// <summary>
        /// Cancel
        /// </summary>
        private RelayCommand _cancelPillCommand;
        public RelayCommand CancelPillCommand
        {
            get
            {
                if (_cancelPillCommand == null)
                {
                    _cancelPillCommand = new RelayCommand(Cancel);
                    _cancelPillCommand.Enabled = true;
                }
                return _cancelPillCommand;
            }
            set
            {
                _cancelPillCommand = value;
            }
        }

        /// <summary>
        /// Property for OK button to confirm reset 
        /// </summary>
        private RelayCommand _popupOkLeavePage;

        public RelayCommand PopupOkLeavePage
        {
            get
            {
                if (_popupOkLeavePage == null)
                {

                    _popupOkLeavePage = new RelayCommand(ConfirmPopupOkLeavePage);
                    _popupOkLeavePage.Enabled = true;


                }
                return _popupOkLeavePage;
            }
            set
            {
                _popupOkLeavePage = value;
            }
        }
       

        /// <summary>
        /// Property for cancel button of confirm popup 
        /// </summary>
        private RelayCommand _popupcancelLeavePage;

        public RelayCommand PopupcancelLeavePage
        {
            get
            {
                if (_popupcancelLeavePage == null)
                {

                    _popupcancelLeavePage = new RelayCommand(ConfirmPopupCancelLeavePage);
                    _popupcancelLeavePage.Enabled = true;


                }
                return _popupcancelLeavePage;
            }
            set
            {
                _popupcancelLeavePage = value;
            }
        }
        /// <summary>
        /// For toggle switch
        /// </summary>
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
               
                if (value != _isChecked)
                {
                    _isChecked = value;
                    OnPropertyChanged("IsChecked");

                    App.IsChecked = _isChecked;
                    if (App.IsChecked)
                    {
                        Bgcolor = "#00FF00";
                        ReminderOn();
                    }
                    else
                    {
                        Bgcolor = "#1cc183";
                        App.IsReminderToSet = false;
                        DisplayReminderstatusOff();
                    }
                }
            }
        }
        /// <summary>
        /// For Background color
        /// </summary>
        private string _bgcolor = "#1cc183";
        public string Bgcolor
        {
            get { return _bgcolor; }
            set
            {
                if (value != _bgcolor)
                {
                    _bgcolor = value;
                    OnPropertyChanged("Bgcolor");
                    if (string.IsNullOrEmpty(App.Bgcolor) || string.IsNullOrWhiteSpace(App.Bgcolor))
                        App.Bgcolor = _bgcolor;
                    else
                        _bgcolor = App.Bgcolor;
                }
            }
        }

        // <summary>
        /// Secondary Color
        /// </summary>
        private SolidColorBrush _secondaryColour;
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
        public SolidColorBrush PrimaryColour
        {
            get { return _primaryColour; }
            set
            {
                _primaryColour = value;
                OnPropertyChanged("PrimaryColour");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to Confirm and set reminder
        /// </summary>
        private void ConfirmPopupOkLeavePage()
        {
            IsConfirmPopupOpenLeavePage = false;
            HitVisibility = true;
            if (App.IsChecked)
            {
                ReminderOn();
                AddReminder();
            }
            else
                AddReminder();
        }
        /// <summary>
        /// Method to cancel
        /// </summary>
        private void ConfirmPopupCancelLeavePage()
        {
            IsConfirmPopupOpenLeavePage = false;
            HitVisibility = true;
            MovePillsFromLocalStorage();
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToHomePanoramaURL + "?goto=3");
        }
        /// <summary>
        /// Method to reset pills collection to previous state
        /// </summary>
        private void MovePillsFromLocalStorage()
        {
            if (HeaderPillsReminder.Equals("daily morning"))
            {
                if (App.DailyMorningPillsCollection!=null)
                App.DailyMorningPillsCollection.Clear();
                foreach (var item in App.PillsReminderModelColLocalStorage)
                {
                    App.DailyMorningPillsCollection.Add(item);
                }
                
                if(!IsLimitExceed)
                App.DailyMorningReminderOnOff = App.IsCheckedTempStore ? "On" : "Off";
                else
                    App.DailyMorningReminderOnOff = "Off";

            }
            else if (HeaderPillsReminder.Equals("daily afternoon"))
            {
                if (App.DailyAfternoonPillsCollection != null)
                App.DailyAfternoonPillsCollection.Clear();
                foreach (var item in App.PillsReminderModelColLocalStorage)
                {
                    App.DailyAfternoonPillsCollection.Add(item);
                }
                
                if (!IsLimitExceed)
                App.DailyAfternoonReminderOnOff = App.IsCheckedTempStore ? "On" : "Off";
                else
                    App.DailyAfternoonReminderOnOff = "Off";
            }
            else if (HeaderPillsReminder.Equals("daily evening"))
            {
                if (App.DailyEveningPillsCollection != null)
                App.DailyEveningPillsCollection.Clear();
                foreach (var item in App.PillsReminderModelColLocalStorage)
                {
                    App.DailyEveningPillsCollection.Add(item);
                }
                
                if (!IsLimitExceed)
                App.DailyEveningReminderOnOff = App.IsCheckedTempStore ? "On" : "Off";
                else
                    App.DailyEveningReminderOnOff = "Off";
            }
            else if (HeaderPillsReminder.Equals("daily night"))
            {
                if (App.DailyNightPillsCollection != null)
                App.DailyNightPillsCollection.Clear();
                foreach (var item in App.PillsReminderModelColLocalStorage)
                {
                    App.DailyNightPillsCollection.Add(item);
                }
                
                if (!IsLimitExceed)
                App.DailyNightReminderOnOff = App.IsCheckedTempStore ? "On" : "Off";
                else
                    App.DailyNightReminderOnOff = "Off";
            }
            else if (HeaderPillsReminder.Equals("weekly"))
            {
                if (App.WeeklyPillsCollection != null)
                App.WeeklyPillsCollection.Clear();
                foreach (var item in App.PillsReminderModelColLocalStorage)
                {
                    App.WeeklyPillsCollection.Add(item);
                }
                
                if (!IsLimitExceed)
                App.WeeklyReminderOnOff = App.IsCheckedTempStore ? "On" : "Off";
                else
                    App.WeeklyReminderOnOff = "Off";
            }
            else if (HeaderPillsReminder.Equals("monthly"))
            {
                if (App.MonthlyPillsCollection != null)
                App.MonthlyPillsCollection.Clear();
                foreach (var item in App.PillsReminderModelColLocalStorage)
                {
                    App.MonthlyPillsCollection.Add(item);
                }
                
                if (!IsLimitExceed)
                App.MonthlyReminderOnOff = App.IsCheckedTempStore ? "On" : "Off";
                else
                    App.MonthlyReminderOnOff = "Off";
            }
            else if (HeaderPillsReminder.Equals("every 28 days"))
            {
                if (App.Every28DaysPillsCollection != null)
                App.Every28DaysPillsCollection.Clear();
                foreach (var item in App.PillsReminderModelColLocalStorage)
                {
                    App.Every28DaysPillsCollection.Add(item);
                }
                
                if (!IsLimitExceed)
                App.Every28DaysReminderOnOff = App.IsCheckedTempStore ? "On" : "Off";
                else
                    App.Every28DaysReminderOnOff = "Off";
            }

        }

        /// <summary>
        /// Cancel
        /// </summary>
        private void Cancel()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToHomePanoramaURL + "?goto=3");
        }

        /// <summary>
        /// Display saved values
        /// </summary>
        private void DisplayValues()
        {
            if (HeaderPillsReminder.Equals("daily morning"))
            {
                PillsReminderCollection = App.DailyMorningPillsCollection != null ? App.DailyMorningPillsCollection : null;

                if (PillsReminderCollection != null)
                {
                    
                    objPillsReminderModelCol = AppendSpecialCharacter(PillsReminderCollection);
                    if (App.PillsReminderModelColLocalStorage.Count == 0 && !App.IsReminderToombstoned)
                    {
                       
                      if(!App.IsEditCancelled)
                            App.PillsReminderModelColLocalStorage = App.DailyMorningPillsCollection.ToList(); 
                       
                      
                    }
                }
                else
                    objPillsReminderModelCol = PillsReminderCollection;
                App.ReminderToDisplay = "daily morning reminder";
                ReminderDate = App.DailyMorningReminderDate;
                ReminderTime = App.DailyMorningReminderTime;
                ReminderStatus = App.DailyMorningReminderOnOff;
                if (ReminderStatus.Equals("Off") || ReminderStatus.Equals(string.Empty))
                {
                    IsChecked = false;
                    App.IsChecked = IsChecked;
                   
                    Bgcolor = "#1cc183";
                    App.Bgcolor = Bgcolor;
                }
               
                else if (ReminderStatus.Equals("On"))
                {
                    IsChecked = true;
                    App.IsChecked = IsChecked;
                   
                    Bgcolor = "#00FF00";
                    App.Bgcolor = Bgcolor;
                }
            }
            else if (HeaderPillsReminder.Equals("daily afternoon"))
            {
                PillsReminderCollection = App.DailyAfternoonPillsCollection != null ? App.DailyAfternoonPillsCollection : null;

                if (PillsReminderCollection != null)
                {
                    objPillsReminderModelCol = AppendSpecialCharacter(PillsReminderCollection);
                    if (App.PillsReminderModelColLocalStorage.Count == 0 && !App.IsReminderToombstoned)
                    {
                        if (!App.IsEditCancelled)
                        App.PillsReminderModelColLocalStorage = App.DailyAfternoonPillsCollection.ToList();
                    }
                }
                else
                    objPillsReminderModelCol = PillsReminderCollection;
                App.ReminderToDisplay = "daily afternoon reminder";
                ReminderDate = App.DailyAfternoonReminderDate;
                ReminderTime = App.DailyAfternoonReminderTime;
                ReminderStatus = App.DailyAfternoonReminderOnOff;
                if (ReminderStatus.Equals("Off") )
                {
                    IsChecked = false;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#1cc183";
                    App.Bgcolor = Bgcolor;
                }
                else
                {
                    IsChecked = true;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#00FF00";
                    App.Bgcolor = Bgcolor;
                }
            }
            else if (HeaderPillsReminder.Equals("daily evening"))
            {
                PillsReminderCollection = App.DailyEveningPillsCollection != null ? App.DailyEveningPillsCollection : null;

                if (PillsReminderCollection != null)
                {
                    objPillsReminderModelCol = AppendSpecialCharacter(PillsReminderCollection);
                    if (App.PillsReminderModelColLocalStorage.Count == 0 && !App.IsReminderToombstoned)
                    {
                        if (!App.IsEditCancelled)
                        App.PillsReminderModelColLocalStorage = App.DailyEveningPillsCollection.ToList();
                    }
                }
                else
                    objPillsReminderModelCol = PillsReminderCollection;

                App.ReminderToDisplay = "daily evening reminder";
                ReminderDate = App.DailyEveningReminderDate;
                ReminderTime = App.DailyEveningReminderTime;
                ReminderStatus = App.DailyEveningReminderOnOff;
                if (ReminderStatus.Equals("Off"))
                {
                    IsChecked = false;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#1cc183";
                    App.Bgcolor = Bgcolor;
                }
                else
                {
                    IsChecked = true;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#00FF00";
                    App.Bgcolor = Bgcolor;
                }
            }
            else if (HeaderPillsReminder.Equals("daily night"))
            {
                PillsReminderCollection = App.DailyNightPillsCollection != null ? App.DailyNightPillsCollection : null;

                if (PillsReminderCollection != null)
                {
                    objPillsReminderModelCol = AppendSpecialCharacter(PillsReminderCollection);
                    if (App.PillsReminderModelColLocalStorage.Count == 0 && !App.IsReminderToombstoned)
                    {
                        if (!App.IsEditCancelled)
                        App.PillsReminderModelColLocalStorage = App.DailyNightPillsCollection.ToList();
                    }
                }
                else
                    objPillsReminderModelCol = PillsReminderCollection;
                App.ReminderToDisplay = "daily night reminder";
                ReminderDate = App.DailyNightReminderDate;
                ReminderTime = App.DailyNightReminderTime;
                ReminderStatus = App.DailyNightReminderOnOff;
                if (ReminderStatus.Equals("Off"))
                {
                    IsChecked = false;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#1cc183";
                    App.Bgcolor = Bgcolor;
                }
                else
                {
                    IsChecked = true;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#00FF00";
                    App.Bgcolor = Bgcolor;
                }
            }
            else if (HeaderPillsReminder.Equals("weekly"))
            {
                PillsReminderCollection = App.WeeklyPillsCollection != null ? App.WeeklyPillsCollection : null;

                if (PillsReminderCollection != null)
                {
                    objPillsReminderModelCol = AppendSpecialCharacter(PillsReminderCollection);
                    if (App.PillsReminderModelColLocalStorage.Count == 0 && !App.IsReminderToombstoned)
                    {
                        if (!App.IsEditCancelled)
                        App.PillsReminderModelColLocalStorage = App.WeeklyPillsCollection.ToList();
                    }
                }
                else
                    objPillsReminderModelCol = PillsReminderCollection;
                App.ReminderToDisplay = "weekly reminder";
                ReminderDate = App.WeeklyReminderDate;
                ReminderTime = App.WeeklyReminderTime;
                ReminderStatus = App.WeeklyReminderOnOff;
                if (ReminderStatus.Equals("Off"))
                {
                    IsChecked = false;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#1cc183";
                    App.Bgcolor = Bgcolor;
                }
                else
                {
                    IsChecked = true;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#00FF00";
                    App.Bgcolor = Bgcolor;
                }
            }
            else if (HeaderPillsReminder.Equals("monthly"))
            {
                PillsReminderCollection = App.MonthlyPillsCollection != null ? App.MonthlyPillsCollection : null;

                if (PillsReminderCollection != null)
                {
                    objPillsReminderModelCol = AppendSpecialCharacter(PillsReminderCollection);
                    if (App.PillsReminderModelColLocalStorage.Count == 0 && !App.IsReminderToombstoned)
                    {
                        if (!App.IsEditCancelled)
                        App.PillsReminderModelColLocalStorage = App.MonthlyPillsCollection.ToList();
                    }
                }
                else
                    objPillsReminderModelCol = PillsReminderCollection;
                App.ReminderToDisplay = "monthly reminder";
                ReminderDate = App.MonthlyReminderDate;
                ReminderTime = App.MonthlyReminderTime;
                ReminderStatus = App.MonthlyReminderOnOff;
                if (ReminderStatus.Equals("Off"))
                {
                    IsChecked = false;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#1cc183";
                    App.Bgcolor = Bgcolor;
                }
                else
                {
                    IsChecked = true;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#00FF00";
                    App.Bgcolor = Bgcolor;
                }
            }
            else if (HeaderPillsReminder.Equals("every 28 days"))
            {
                PillsReminderCollection = App.Every28DaysPillsCollection != null ? App.Every28DaysPillsCollection : null;

                if (PillsReminderCollection != null)
                {
                    objPillsReminderModelCol = AppendSpecialCharacter(PillsReminderCollection);
                    if (App.PillsReminderModelColLocalStorage.Count == 0 && !App.IsReminderToombstoned)
                    {
                        if (!App.IsEditCancelled)
                        App.PillsReminderModelColLocalStorage = App.Every28DaysPillsCollection.ToList();
                    }
                }
                else
                    objPillsReminderModelCol = PillsReminderCollection;
                App.ReminderToDisplay = "every 28 days reminder";
                ReminderDate = App.Every28DaysReminderDate;
                ReminderTime = App.Every28DaysReminderTime;
                ReminderStatus = App.Every28DaysReminderOnOff;
                if (ReminderStatus.Equals("Off"))
                {
                    IsChecked = false;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#1cc183";
                    App.Bgcolor = Bgcolor;
                }
                else
                {
                    IsChecked = true;
                    App.IsChecked = IsChecked;
                    Bgcolor = "#00FF00";
                    App.Bgcolor = Bgcolor;
                }
            }
        }
        /// <summary>
        /// Method to append special characters
        /// </summary>
        /// <param name="PillsReminderCollection"></param>
        /// <returns></returns>
        private PillsReminderModelCol AppendSpecialCharacter(PillsReminderModelCol PillsReminderCollection)
        {
            PillsReminderModelCol obj = new PillsReminderModelCol();
            foreach (var item in PillsReminderCollection)
            {
                if (item.NumberOfPills.Contains("x"))
                { }
                else
                {
                    item.NumberOfPills = string.Concat(" x ", item.NumberOfPills);
                }
                obj.Add(item);
            }
            return obj;
        }
        /// <summary>
        /// Method to add pills to list
        /// </summary>
        private void AddingToList()
        {
            if (!string.IsNullOrEmpty(PillNames) && !string.IsNullOrWhiteSpace(PillNames))
            {
               
                if (!string.IsNullOrEmpty(Qty) && !string.IsNullOrWhiteSpace(Qty))
                {
                    try
                    {
                       int QtyToInt = Convert.ToInt16(Qty);

                       
                        string appendQty = String.Concat(" x ", Qty);
                            var pillCol = new PillsReminderModel { PillName = PillNames, NumberOfPills = appendQty };
                            
                            if (PillsReminderCollection == null)
                            {
                                PillsReminderCollection = new PillsReminderModelCol();
                                PillsReminderCollection.Add(pillCol);
                            }
                                 
                            else
                            {
                                PillsReminderCollection.Add(pillCol);
                            }
                            
                            if (HeaderPillsReminder.Equals("daily morning"))
                           
                                App.DailyMorningPillsCollection = PillsReminderCollection;
                                
                          
                            else if (HeaderPillsReminder.Equals("daily afternoon"))
                                App.DailyAfternoonPillsCollection = PillsReminderCollection;
                            else if (HeaderPillsReminder.Equals("daily evening"))
                                App.DailyEveningPillsCollection = PillsReminderCollection;
                            else if (HeaderPillsReminder.Equals("daily night"))
                                App.DailyNightPillsCollection = PillsReminderCollection;
                            else if (HeaderPillsReminder.Equals("weekly"))
                                App.WeeklyPillsCollection = PillsReminderCollection;
                            else if (HeaderPillsReminder.Equals("monthly"))
                                App.MonthlyPillsCollection = PillsReminderCollection;
                            else if (HeaderPillsReminder.Equals("every 28 days"))
                                App.Every28DaysPillsCollection = PillsReminderCollection;
                            
                            Qty = string.Empty;
                            PillNames = string.Empty;
                          
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
            {
                MessageBox.Show("Please enter pill name.");
            }
        }
        /// <summary>
        /// ReminderOn
        /// </summary>
        private void ReminderOn()
        {
            if (HeaderPillsReminder.Equals("daily morning"))
            {
                App.DailyMorningReminderOnOff = "On";
                ReminderStatus = App.DailyMorningReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("daily afternoon"))
            {
                App.DailyAfternoonReminderOnOff = "On";
                ReminderStatus = App.DailyAfternoonReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("daily evening"))
            {
                App.DailyEveningReminderOnOff = "On";
                ReminderStatus = App.DailyEveningReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("daily night"))
            {
                App.DailyNightReminderOnOff = "On";
                ReminderStatus = App.DailyNightReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("weekly"))
            {
                App.WeeklyReminderOnOff = "On";
                ReminderStatus = App.WeeklyReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("monthly"))
            {
                App.MonthlyReminderOnOff = "On";
                ReminderStatus = App.MonthlyReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("every 28 days"))
            {
                App.Every28DaysReminderOnOff = "On";
                ReminderStatus = App.Every28DaysReminderOnOff;
            }
            App.IsReminderToSet = true;
        }
        /// <summary>
        /// Set Status "Off"
        /// </summary>
        private void DisplayReminderstatusOff()
        {
            if (HeaderPillsReminder.Equals("daily morning"))
            {
                App.DailyMorningReminderOnOff = "Off";
                ReminderStatus = App.DailyMorningReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("daily afternoon"))
            {
                App.DailyAfternoonReminderOnOff = "Off";
                ReminderStatus = App.DailyAfternoonReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("daily evening"))
            {
                App.DailyEveningReminderOnOff = "Off";
                ReminderStatus = App.DailyEveningReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("daily night"))
            {
                App.DailyNightReminderOnOff = "Off";
                ReminderStatus = App.DailyNightReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("weekly"))
            {
                App.WeeklyReminderOnOff = "Off";
                ReminderStatus = App.WeeklyReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("monthly"))
            {
                App.MonthlyReminderOnOff = "Off";
                ReminderStatus = App.MonthlyReminderOnOff;
            }
            else if (HeaderPillsReminder.Equals("every 28 days"))
            {
                App.Every28DaysReminderOnOff = "Off";
                ReminderStatus = App.Every28DaysReminderOnOff;
            }
        }
        /// <summary>
        /// Add Reminder
        /// </summary>
        private void AddReminder()
        {
            DateTime date = ReminderDate;

            TimeSpan time = ReminderTime.TimeOfDay;

            date = date.Date + time;
            if (PillsReminderCollection != null)
            {
                if (PillsReminderCollection.Count == 0)
                    MessageBox.Show("Please enter pill name and quantity.");
                else if (App.IsChecked)
                {
                    if (date <= DateTime.Now)
                        MessageBox.Show("Invalid Date/Time!");
                    else
                    {
                        RemoveOldReminder();
                        SetReminderTimeToDisplay();
                        SetReminder(date, HeaderPillsReminder);
                    }
                }
                else
                {
                    RemoveOldReminder();
                   SetReminderTimeToDisplay();
                    SetReminder(date, HeaderPillsReminder);
                }
            }
            else
            {
                MessageBox.Show("Please enter pill name and quantity.");
            }
        }
        /// <summary>
        /// Remove Old reminder
        /// </summary>
        private void RemoveOldReminder()
        {
            ScheduledAction oldReminder = ScheduledActionService.Find(App.ReminderToDisplay);
            if (oldReminder != null)
                ScheduledActionService.Remove(oldReminder.Name);
        }
        /// <summary>
        /// Validate the time range for daily type reminder
        /// </summary>
        /// <param name="tsStart"></param>
        /// <param name="tsEnd"></param>
        /// <returns></returns>
        private bool IsValidTimeRange(DateTime dt, TimeSpan tsStart, TimeSpan tsEnd)
        {
            return ((dt.TimeOfDay >= tsStart) && (dt.TimeOfDay <= tsEnd)) ? true : false;
        }
        /// <summary>
        /// Set the reminder
        /// </summary>
        /// <param name="date"></param>
        /// <param name="HeaderPillsReminder"></param>
        private void SetReminder(DateTime date, string HeaderPillsReminder)
        {
            foreach (var item in PillsReminderCollection)
            {
                if (string.IsNullOrEmpty(pillNames) || (string.IsNullOrWhiteSpace(pillNames)))
                    pillNames = string.Join("\n", string.Concat(item.PillName, " ", item.NumberOfPills));
                else
                    pillNames = string.Join("\n", pillNames, string.Concat(item.PillName, " ", item.NumberOfPills));
            }
            if (App.IsChecked)
            {
                if (pillNames.Length <= 220)
                {
                    CreateReminderIfLimitNotExceed( date, HeaderPillsReminder);
                  //  App.IsReminderToSet = false;
                }
                else
                {
                    MessageBox.Show("Reduce the number of pills.");
                    IsLimitExceed = true;
                }
            }
            else
                CreateReminderIfLimitNotExceed(date, HeaderPillsReminder);
        }
        /// <summary>
        /// Create  reminder
        /// </summary>
        /// <param name="date"></param>
        /// <param name="HeaderPillsReminder"></param>
        private void CreateReminderIfLimitNotExceed(DateTime date, string HeaderPillsReminder)
        {
            if (HeaderPillsReminder.Equals("daily morning"))
            {
                CreateDMReminder(date, pillNames);
            }
            if (HeaderPillsReminder.Equals("daily afternoon"))
            {
                CreateDAReminder(date, pillNames);
            }
            if (HeaderPillsReminder.Equals("daily evening"))
            {
                CreateDEReminder(date, pillNames);
            }
            if (HeaderPillsReminder.Equals("daily night"))
            {
                CreateDNReminder(date, pillNames);
            }
            if (HeaderPillsReminder.Equals("weekly"))
            {
                CreateWeeklyReminder(date, pillNames);
            }

            if (HeaderPillsReminder.Equals("monthly"))
            {
                CreateMonthlyReminder(date, pillNames);
            }

            if (HeaderPillsReminder.Equals("every 28 days"))
            {
                CreateEvery28DaysReminder(date, pillNames);

            }
        }
        /// <summary>
        /// Create Every 28 days Reminder
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pillNames"></param>
        private void CreateEvery28DaysReminder(DateTime date, string pillNames)
        {
            if (App.IsReminderToSet)
            {

                CreateReminder(App.ReminderToDisplay, date, pillNames, RecurrenceInterval.Monthly - 2);
                ReminderSetAlert();
            }
            else
            {
                ReminderOffAlert();
            }
        }
        /// <summary>
        /// Create Monthly Reminder
        /// </summary>
        private void CreateMonthlyReminder(DateTime date, string pillNames)
        {
            if (App.IsReminderToSet)
            {
                CreateReminder(App.ReminderToDisplay, date, pillNames, RecurrenceInterval.Monthly);
                ReminderSetAlert();
            }
            else
            {
                ReminderOffAlert();
            }
        }
        /// <summary>
        /// Create Weekly Reminder
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pillNames"></param>
        private void CreateWeeklyReminder(DateTime date, string pillNames)
        {
            if (App.IsReminderToSet)
            {
                CreateReminder(App.ReminderToDisplay, date, pillNames, RecurrenceInterval.Weekly);
                ReminderSetAlert();
            }
            else
            {
                ReminderOffAlert();
            }
        }
        /// <summary>
        /// Create Daily Night Reminder
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pillNames"></param>
        private void CreateDNReminder(DateTime date, string pillNames)
        {
            TimeSpan tsStart = new TimeSpan(20, 00, 00);
            TimeSpan tsEnd = new TimeSpan(23, 59, 59);
            string errorMessage = "Daily Night reminder time should be between 08:00:00 PM - 11:59:59 PM";

            if (App.IsReminderToSet)
            {
                if (IsValidTimeRange(date, tsStart, tsEnd))
                {
                    CreateReminder(App.ReminderToDisplay, date, pillNames, RecurrenceInterval.Daily);
                    ReminderSetAlert();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            else
            {
                ReminderOffAlert();
            }
        }


        /// <summary>
        /// Create Daily Evening Reminder
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pillNames"></param>
        private void CreateDEReminder(DateTime date, string pillNames)
        {
            TimeSpan tsStart = new TimeSpan(16, 00, 00);
            TimeSpan tsEnd = new TimeSpan(19, 59, 59);
            string errorMessage = "Daily Evening reminder time should be between 04:00:00 PM - 07:59:59 PM";

            if (App.IsReminderToSet)
            {
                if (IsValidTimeRange(date, tsStart, tsEnd))
                {
                    CreateReminder(App.ReminderToDisplay, date, pillNames, RecurrenceInterval.Daily);
                    ReminderSetAlert();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            else
            {
                ReminderOffAlert();
            }


        }
        /// <summary>
        /// Create Daily Afternoon Reminder
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pillNames"></param>
        private void CreateDAReminder(DateTime date, string pillNames)
        {
            TimeSpan tsStart = new TimeSpan(12, 00, 00);
            TimeSpan tsEnd = new TimeSpan(15, 59, 59);
            string errorMessage = "Daily Afternoon reminder time should be between 12:00:00 PM - 03:59:59 PM";

            if (App.IsReminderToSet)
            {
                if (IsValidTimeRange(date, tsStart, tsEnd))
                {
                    CreateReminder(App.ReminderToDisplay, date, pillNames, RecurrenceInterval.Daily);
                        ReminderSetAlert();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            else
            {
                ReminderOffAlert();
            }


        }
        /// <summary>
        /// Create daily morning reminder
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pillNames"></param>
        private void CreateDMReminder(DateTime date, string pillNames)
        {
            TimeSpan tsStart = new TimeSpan(00, 00, 00);
            TimeSpan tsEnd = new TimeSpan(11, 59, 59);
            string errorMessage = "Daily Morning reminder time should be between 12:00:00 AM - 11:59:59 AM";

            if (App.IsReminderToSet)
            {
                if (IsValidTimeRange(date, tsStart, tsEnd))
                {
                    CreateReminder(App.ReminderToDisplay, date, pillNames, RecurrenceInterval.Daily);
                    ReminderSetAlert();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            else
            {
                ReminderOffAlert();
            }


        }
        /// <summary>
        /// Reminder Off Alert
        /// </summary>
        private void ReminderOffAlert()
        {
            if (IsConfirmPopupOpenLeavePage)
            {
                IsConfirmPopupOpenLeavePage = false;
                HitVisibility = true;
            }

            MessageBoxResult objResult = MessageBox.Show("Reminder is Off.", "Reminder Off Alert.", MessageBoxButton.OK);

            if (objResult.Equals(MessageBoxResult.OK))
            {
                App.IsReminderToSet = false;
                INavigationService navigationService = this.GetService<INavigationService>();
                navigationService.Navigate(PageURL.navigateToHomePanoramaURL + "?goto=3");
            }
            
        }
        /// <summary>
        /// Reminder Set Alert
        /// </summary>
        private void ReminderSetAlert()
        {
            if (IsConfirmPopupOpenLeavePage)
            {
                IsConfirmPopupOpenLeavePage = false;
                HitVisibility = true;
            }
            
                MessageBox.Show("Reminder is set.");
            
                INavigationService navigationService = this.GetService<INavigationService>();
                navigationService.Navigate(PageURL.navigateToHomePanoramaURL + "?goto=3");
        }
        /// <summary>
        /// Create Reminder and schedule it
        /// </summary>
        /// <param name="reminderToDisplay"></param>
        /// <param name="date"></param>
        /// <param name="pillNames"></param>
        /// <param name="recurrenceInterval"></param>
        private void CreateReminder(string reminderToDisplay, DateTime date, string pillNames, RecurrenceInterval recurrenceInterval)
        {
                reminder = new Reminder(reminderToDisplay)
                {
                    BeginTime = date,
                    Title = reminderToDisplay,
                    Content = pillNames,
                    RecurrenceType = recurrenceInterval
                };
                ScheduledActionService.Add(reminder);
               
        }

        /// <summary>
        /// Set reminder time/date for display
        /// </summary>
        private void SetReminderTimeToDisplay()
        {

            if (HeaderPillsReminder.Equals("daily morning"))
            {
                App.DailyMorningReminderDate = ReminderDate;
                App.DailyMorningReminderTime = ReminderTime;
            }
            else if (HeaderPillsReminder.Equals("daily afternoon"))
            {
                App.DailyAfternoonReminderDate = ReminderDate;
                App.DailyAfternoonReminderTime = ReminderTime;
            }
            else if (HeaderPillsReminder.Equals("daily evening"))
            {
                App.DailyEveningReminderDate = ReminderDate;
                App.DailyEveningReminderTime = ReminderTime;
            }
            else if (HeaderPillsReminder.Equals("daily night"))
            {
                App.DailyNightReminderDate = ReminderDate;
                App.DailyNightReminderTime = ReminderTime;
            }
            else if (HeaderPillsReminder.Equals("weekly"))
            {
                App.WeeklyReminderDate = ReminderDate;
                App.WeeklyReminderTime = ReminderTime;
            }
            else if (HeaderPillsReminder.Equals("monthly"))
            {
                App.MonthlyReminderDate = ReminderDate;
                App.MonthlyReminderTime = ReminderTime;
            }
            else if (HeaderPillsReminder.Equals("every 28 days"))
            {
                App.Every28DaysReminderDate = ReminderDate;
                App.Every28DaysReminderTime = ReminderTime;
            }
        }

        #endregion
    }


}


