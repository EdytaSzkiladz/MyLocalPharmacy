using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLocalPharmacy.ViewModel
{

    public class EnterPinToSettingsChangePinViewModel : BaseViewModel
    {
        #region Constructor
        public EnterPinToSettingsChangePinViewModel()
        {
            _isPinValidatorVisible = Visibility.Collapsed;
            _hitVisibility = true;
        }
        #endregion
       

        #region Properties

        /// <summary>
        /// Property to set hit visibility
        /// </summary>
        private bool _hitVisibility;
        [DataMember]
        public bool HitVisibility
        {
            get { return _hitVisibility; }
            set { _hitVisibility = value; OnPropertyChanged("HitVisibility"); }
        }
        /// <summary>
        /// To set the Incorrect Pin Popup
        /// </summary>
        private bool _isIncorrectPinPopupOpen;
        public bool IsIncorrectPinPopupOpen
        {
            get { return _isIncorrectPinPopupOpen; }
            set
            {
                _isIncorrectPinPopupOpen = value;
                OnPropertyChanged("IsIncorrectPinPopupOpen");
            }
        }

        /// <summary>
        /// To set the Incorrect Pin Message
        /// </summary>
        private string _incorrectPinMessage;
        public string IncorrectPinMessage
        {
            get { return _incorrectPinMessage; }
            set
            {
                _incorrectPinMessage = value;
                OnPropertyChanged("IncorrectPinMessage");
            }
        }
        /// <summary>
        /// To set the Pin Message
        /// </summary>
        private string _pinLengthMessage;
        public string PinLengthMessage
        {
            get { return _pinLengthMessage; }
            set
            {
                _pinLengthMessage = value;
                OnPropertyChanged("PinLengthMessage");
            }
        }
        /// <summary>
        /// To set the Pin Message Visibilty
        /// </summary>
        private Visibility _isPinValidatorVisible;
        public Visibility IsPinValidatorVisible
        {
            get { return _isPinValidatorVisible; }
            set
            {
                _isPinValidatorVisible = value;
                OnPropertyChanged("IsPinValidatorVisible");
            }
        }
        /// <summary>
        /// To set the Pin
        /// </summary>
        private string _pin;
        public string Pin
        {
            get { return _pin; }
            set
            {
                _pin = value;
                OnPropertyChanged("Pin");
            }
        }
        /// <summary>
        /// Property for navigation to ConfirmPin Page
        /// </summary>
        private RelayCommand _enterPin;
        public RelayCommand EnterPin
        {

            get
            {
                if (_enterPin == null)
                {
                    _enterPin = new RelayCommand(NavigateToSettingsChangePin);
                    _enterPin.Enabled = true;
                }

                return _enterPin;
            }
            set { _enterPin = value; }
        }
        //
        /// <summary>
        /// Property for navigation to ConfirmPin Page
        /// </summary>
        private RelayCommand _toSettingsPage;
        public RelayCommand ToSettingsPage
        {

            get
            {
                if (_toSettingsPage == null)
                {
                    _toSettingsPage = new RelayCommand(ToSettingPage);
                    _toSettingsPage.Enabled = true;
                }

                return _toSettingsPage;
            }
            set { _toSettingsPage = value; }
        }


        /// <summary>
        /// Property for ForgotPINCommand
        /// </summary>
        private RelayCommand _forgotPin;
        public RelayCommand ForgotPINCommand
        {

            get
            {
                if (_forgotPin == null)
                {
                    _forgotPin = new RelayCommand(ForgotPIN);
                    _forgotPin.Enabled = true;
                }

                return _forgotPin;
            }
            set { _forgotPin = value; }
        }

        /// <summary>
        /// Property for OKCommand
        /// </summary>
        private RelayCommand _oKCommand;
        public RelayCommand OKCommand
        {

            get
            {
                if (_oKCommand == null)
                {
                    _oKCommand = new RelayCommand(OnOKClick);
                    _oKCommand.Enabled = true;
                }

                return _oKCommand;
            }
            set { _oKCommand = value; }
        }
        /// <summary>
        /// Property for ResetPINCancelCommand
        /// </summary>
        private RelayCommand _resetPINCancelCommand;
        public RelayCommand ResetPINCancelCommand
        {

            get
            {
                if (_resetPINCancelCommand == null)
                {
                    _resetPINCancelCommand = new RelayCommand(CancelResetPIN);
                    _resetPINCancelCommand.Enabled = true;
                }

                return _resetPINCancelCommand;
            }
            set { _resetPINCancelCommand = value; }
        }

        /// <summary>
        /// Property for ResetPINOKCommand
        /// </summary>
        private RelayCommand _resetPINOKCommand;
        public RelayCommand ResetPINOKCommand
        {

            get
            {
                if (_resetPINOKCommand == null)
                {
                    _resetPINOKCommand = new RelayCommand(OKResetPIN);
                    _resetPINOKCommand.Enabled = true;
                }

                return _resetPINOKCommand;
            }
            set { _resetPINOKCommand = value; }
        }


        /// <summary>
        /// For Pin reset popup
        /// </summary>
        private bool _isPinResetPopUpOpen;
        public bool IsPinResetPopUpOpen
        {
            get { return _isPinResetPopUpOpen; }
            set
            {
                _isPinResetPopUpOpen = value;
                OnPropertyChanged("IsPinResetPopUpOpen");
            }
        }
        /// <summary>
        /// For PreventAccess popup
        /// </summary>
        private bool _isPreventAccessPopupOpen;
        public bool IsPreventAccessPopupOpen
        {
            get { return _isPreventAccessPopupOpen; }
            set
            {
                _isPreventAccessPopupOpen = value;
                OnPropertyChanged("IsPreventAccessPopupOpen");
            }
        }
        /// <summary>
        /// For PreventAccess text message
        /// </summary>
        private string _preventAccessText;
        public string PreventAccessText
        {
            get { return _preventAccessText; }
            set
            {
                _preventAccessText = value;
                OnPropertyChanged("PreventAccessText");
            }
        }

        /// <summary>
        /// Property for OkPreventAccessCommand
        /// </summary>
        private RelayCommand _okPreventAccessCommand;
        public RelayCommand OkPreventAccessCommand
        {

            get
            {
                if (_okPreventAccessCommand == null)
                {
                    _okPreventAccessCommand = new RelayCommand(OkPreventAccess);
                    _okPreventAccessCommand.Enabled = true;
                }

                return _okPreventAccessCommand;
            }
            set { _okPreventAccessCommand = value; }
        }

        /// <summary>
        /// Close the PreventAccess Popup
        /// </summary>
        private void OkPreventAccess()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            IsPreventAccessPopupOpen = false;
            HitVisibility = true;
            navigationService.Navigate(PageURL.navigateToSignUpURL);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Navigate to App Exit Page
        /// </summary>
        private void ToSettingPage()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            App.PinResetFromSettingsPage = false;
            navigationService.Navigate(PageURL.navigateToSettingPageURL);
          
        }
        /// <summary>
        /// Method to navigate to reset pin page
        /// </summary>
        private void OKResetPIN()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToResetPinLoginURL);
        }

        /// <summary>
        /// Method to close the IsPinResetPopUp
        /// </summary>
        private void CancelResetPIN()
        {
            IsPinResetPopUpOpen = false;
            HitVisibility = true;
        }
        /// <summary>
        /// Method to close the IsIncorrectPin popup
        /// </summary>
        private void OnOKClick()
        {
            IsIncorrectPinPopupOpen = false;
            HitVisibility = true;
          
        }

        /// <summary>
        /// Method to open confirm pin reset popup
        /// </summary>
        private void ForgotPIN()
        {
            IsIncorrectPinPopupOpen = false;
           
            IsPinResetPopUpOpen = true;
            HitVisibility = false;
        }

         /// <summary>
        /// Method to navigate 
        /// </summary>
        private void NavigateToSettingsChangePin()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            if (!string.IsNullOrEmpty(Pin) || !string.IsNullOrWhiteSpace(Pin))
            {
                if (App.NoofTriesLeft > 0)
                {
                    if (Pin.Length < 4)
                    {
                        IsPinValidatorVisible = Visibility.Visible;
                        PinLengthMessage = "Pin should be 4 digits";
                        Pin = string.Empty;
                    }

                    else if ((App.PIN == Pin))
                    {
                        navigationService.Navigate(PageURL.navigateToSettingsChangePinURL);
                       
                    }
                    else
                    {
                        IsIncorrectPinPopupOpen = true;
                        HitVisibility = false;
                        IncorrectPinMessage = "Incorrect PIN entered please try again.\nYou have " + App.NoofTriesLeft + " tries left before your data is wiped.";
                        App.NoofTriesLeft = App.NoofTriesLeft - 1;
                        Pin = string.Empty;
                    }
                }
                else
                {
                    IsPreventAccessPopupOpen = true;
                    HitVisibility = false;
                    PreventAccessText = "You have exceeded your limit of 10 tries.\n Your app will now exit,\n your saved information will be cleared.";
                    
                    Utilities.ClearAllAppVariables();
                }
            }
            else
            {
                IsPinValidatorVisible = Visibility.Visible;
                PinLengthMessage = "Please enter a PIN";
            }
        }

        
        #endregion
    }

}
