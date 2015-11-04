using System;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.Common;
using Microsoft.Phone.Controls;
using System.Windows;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Model;
using System.Runtime.Serialization;
namespace MyLocalPharmacy.ViewModel
{
    public class SettingsConfirmChangePinViewModel : BaseViewModel
    {
        #region Declaration
        SettingsChangePINModel objSettingsChangePINModel;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsConfirmChangePinViewModel()
        {
            _isPinValidatorVisible = Visibility.Collapsed;
            HitVisibility = true;

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
        /// Property to set Pin
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
        /// Property for change pin
        /// </summary>
        private RelayCommand _confirmPin;
        public RelayCommand ConfirmPin
        {

            get
            {
                if (_confirmPin == null)
                {
                    _confirmPin = new RelayCommand(SaveNewPin);
                    _confirmPin.Enabled = true;
                }

                return _confirmPin;
            }
            set { _confirmPin = value; }
        }
        //
        /// <summary>
        /// Property for navigation to ToSettingsChangePINPage
        /// </summary>
        private RelayCommand _toSettingsChangePINPage;
        public RelayCommand ToSettingsChangePINPage
        {

            get
            {
                if (_toSettingsChangePINPage == null)
                {
                    _toSettingsChangePINPage = new RelayCommand(NavigateToSettingsChangePINPage);
                    _toSettingsChangePINPage.Enabled = true;
                }

                return _toSettingsChangePINPage;
            }
            set { _toSettingsChangePINPage = value; }
        }
        /// <summary>
        /// To Open/close the popup
        /// </summary>
        private bool _isPinMismatchPopupOpen;
        public bool IsPinMismatchPopupOpen
        {
            get { return _isPinMismatchPopupOpen; }
            set
            {
                _isPinMismatchPopupOpen = value;
                OnPropertyChanged("IsPinMismatchPopupOpen");
            }
        }
        /// <summary>
        /// Property for PinMismatchOk Command
        /// </summary>
        private RelayCommand _pinMismatchOkCommand;
        public RelayCommand PinMismatchOkCommand
        {

            get
            {
                if (_pinMismatchOkCommand == null)
                {
                    _pinMismatchOkCommand = new RelayCommand(PinMismatch);
                    _pinMismatchOkCommand.Enabled = true;
                }

                return _pinMismatchOkCommand;
            }
            set { _pinMismatchOkCommand = value; }
        }

        /// <summary>
        /// Set popup text
        /// </summary>
        private string _popuptext;
        public string PopupText
        {
            get { return _popuptext; }
            set { _popuptext = value; OnPropertyChanged("PopupText"); }
        }

        /// <summary>
        /// To Open/close the popup
        /// </summary>
        private bool _isSuccessPopupOpen;
        public bool IsSuccessPopupOpen
        {
            get { return _isSuccessPopupOpen; }
            set
            {
                _isSuccessPopupOpen = value;
                OnPropertyChanged("IsSuccessPopupOpen");
            }
        }
        //
        /// <summary>
        /// Property for SuccessCommand
        /// </summary>
        private RelayCommand _successCommand;
        public RelayCommand SuccessCommand
        {

            get
            {
                if (_successCommand == null)
                {
                    _successCommand = new RelayCommand(SuccessUrl);
                    _successCommand.Enabled = true;
                }

                return _successCommand;
            }
            set { _successCommand = value; }
        }

        /// <summary>
        /// Set SuccessPopup Text
        /// </summary>
        private string _successPopupText;
        public string SuccessPopupText
        {
            get { return _successPopupText; }
            set { _successPopupText = value; OnPropertyChanged("SuccessPopupText"); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Navigate to Settings page
        /// </summary>
        private void SuccessUrl()
        {
            IsSuccessPopupOpen = false;
            HitVisibility = true;
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToSettingPageURL);
        }
        /// <summary>
        /// Navigate to settings change PIN page on cancel click
        /// </summary>
        private void NavigateToSettingsChangePINPage()
        {
            App.NewPin = string.Empty;
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToSettingsChangePinURL);
        }
        /// <summary>
        /// Close the popup.
        /// </summary>
        private void PinMismatch()
        {
            IsPinMismatchPopupOpen = false;
            HitVisibility = true;
        }
        /// <summary>
        /// Method to navigate to signup screen
        /// </summary>
        private void SaveNewPin()
        {
            if (!string.IsNullOrEmpty(Pin) || !string.IsNullOrWhiteSpace(Pin))
            {
                if (Pin.Length < 4)
                {
                    IsPinValidatorVisible = Visibility.Visible;
                    PinLengthMessage = "PIN should be 4 digits";
                    Pin = string.Empty;
                }
                else if (Pin != App.NewPin)
                {
                    IsPinValidatorVisible = Visibility.Visible;
                    PinLengthMessage = "\t\tPIN mismatch";
                    Pin = string.Empty;
                }
                else
                {
                    if (Utilities.IsConnectedToNetwork())
                    {
                        objSettingsChangePINModel = new SettingsChangePINModel(this);
                    }
                    else
                    {
                        IsPinMismatchPopupOpen = true;
                        HitVisibility = false;
                        PopupText = "No connectivity.";
                    } 
                }
            }
            else
            {
                IsPinValidatorVisible = Visibility.Visible;
                PinLengthMessage = "Please enter a PIN";
            }
        }
    }
        #endregion
        
}
