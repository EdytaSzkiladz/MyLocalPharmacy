using System;
using System.Windows;
using Microsoft.Phone.Controls;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Entities;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsViewModel()
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
        }
        #endregion
       

        #region Properties

        /// <summary>
        /// Property for YourDetails click
        /// </summary>
        private RelayCommand _yourdetailsupdate;
        public RelayCommand UpdateDetails
        {
            get
            {
                if (_yourdetailsupdate == null)
                {

                    _yourdetailsupdate = new RelayCommand(UpdateYourDetails);
                    _yourdetailsupdate.Enabled = true;
                }
                return _yourdetailsupdate;
            }
            set
            {
                _yourdetailsupdate = value;
            }
        }

        

        /// <summary>
        /// Property for ChangePIN click
        /// </summary>
        private RelayCommand _changePIN;
        public RelayCommand ChangePIN
        {
            get
            {
                if (_changePIN == null)
                {

                    _changePIN = new RelayCommand(PINChange);
                    _changePIN.Enabled = true;
                }
                return _changePIN;
            }
            set
            {
                _changePIN = value;
            }
        }

        /// <summary>
        /// Property for LocalServices click
        /// </summary>
        private RelayCommand _localserviceDistance;
        public RelayCommand LocalServiceDistance
        {
            get
            {
                if (_localserviceDistance == null)
                {

                    _localserviceDistance = new RelayCommand(LocalServicesDistance);
                    _localserviceDistance.Enabled = true;
                }
                return _localserviceDistance;
            }
            set
            {
                _localserviceDistance = value;
            }
        }

      

        /// <summary>
        /// Property for Terms&Conditions click
        /// </summary>
        private RelayCommand _termsconditions;
        public RelayCommand TermsConditions
        {
            get
            {
                if (_termsconditions == null)
                {

                    _termsconditions = new RelayCommand(TermsandConditions);
                    _termsconditions.Enabled = true;
                }
                return _termsconditions;
            }
            set
            {
                _termsconditions = value;
            }
        }

        

        /// <summary>
        /// Property for Support click
        /// </summary>
        private RelayCommand _support;
        public RelayCommand Support
        {
            get
            {
                if (_support == null)
                {

                    _support = new RelayCommand(SupportPage);
                    _support.Enabled = true;
                }
                return _support;
            }
            set
            {
                _support = value;
            }
        }

        /// <summary>
        /// Property for OK button of popup 
        /// </summary>
        private RelayCommand _OkCommand;

        public RelayCommand OkCommand
        {
            get
            {
                if (_OkCommand == null)
                {

                    _OkCommand = new RelayCommand(SubmitOk);
                    _OkCommand.Enabled = true;


                }
                return _OkCommand;
            }
            set
            {
                _OkCommand = value;
            }
        }
        
        /// <summary>
        /// Property for Cancel button of popup 
        /// </summary>
        private RelayCommand _cancelCommand;

        public RelayCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {

                    _cancelCommand = new RelayCommand(Cancel);
                    _cancelCommand.Enabled = true;


                }
                return _cancelCommand;
            }
            set
            {
                _cancelCommand = value;
            }
        }
      
        /// <summary>
        /// Property set distance in miles
        /// </summary>
        private string _milesDistance;

        public string MilesDistance
        {
            get { return _milesDistance; }
            set { _milesDistance = value; OnPropertyChanged("MilesDistance"); }
        }

        /// <summary>
        /// Property to show and hide popup 
        /// </summary>
        private string _isPopupOpen;

        public string IsPopupOpen
        {
            get { return _isPopupOpen; }
            set { _isPopupOpen = value; OnPropertyChanged("IsPopupOpen"); }
        }

        /// <summary>
        /// For Font Color
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
        /// For Primary Color
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
        /// <summary>
        /// For Secondary Color
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
        #endregion

        #region Methods
        /// <summary>
        /// Method of Relay Command CancelCommand
        /// </summary>
        private void Cancel()
        {
            IsPopupOpen = "false";
        }
        /// <summary>
        /// Method of Relay Command OkCommand
        /// </summary>
        private void SubmitOk()
        {
            IsPopupOpen = "false";
            App.LocalServiceDistance = Convert.ToInt32(MilesDistance);
        }
        /// <summary>
        /// Method to Update Details
        /// </summary>
        private void UpdateYourDetails()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToYourDetailsUpdateURL);
        }
        /// <summary>
        /// Method to ChangePIN
        /// </summary>
        private void PINChange()
        {
            App.PinResetFromSettingsPage = true;
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToEnterPinSettingsChangePinURL);
        }
        /// <summary>
        /// Method to get local services based on distance
        /// </summary>
        private void LocalServicesDistance()
        {
            MilesDistance = Convert.ToString(App.LocalServiceDistance);
            IsPopupOpen = "true";
        }
        /// <summary>
        /// Method to show the Terms and Condition
        /// </summary>
        private void TermsandConditions()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToTermsandConditionURL);
        }
        /// <summary>
        /// Method to navigate to Support page
        /// </summary>
        private void SupportPage()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.supportPageUrl);
        }
        #endregion
    }
}
