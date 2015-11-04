using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Utils;
using System.Windows;

namespace MyLocalPharmacy.ViewModel
{
    public class ConfirmPinViewModel : BaseViewModel
    {
        #region Properties
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
        /// To set the confirmed PIN
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
        private RelayCommand _confirmPin;
        public RelayCommand ConfirmPin
        {

            get
            {
                if (_confirmPin == null)
                {
                    _confirmPin = new RelayCommand(NavigateFromConfirmPin);
                    _confirmPin.Enabled = true;
                }

                return _confirmPin;
            }
            set { _confirmPin = value; }
        }

        /// <summary>
        /// Property for navigation to ConfirmPin Page
        /// </summary>
        private RelayCommand _toSetPIN;
        public RelayCommand ToSetPIN
        {

            get
            {
                if (_toSetPIN == null)
                {
                    _toSetPIN = new RelayCommand(ToSetPINUrl);
                    _toSetPIN.Enabled = true;
                }

                return _toSetPIN;
            }
            set { _toSetPIN = value; }
        }
        
        #endregion

        #region Methods
        /// <summary>
        /// On "Cancel" click move to SetPIN Url
        /// </summary>
        private void ToSetPINUrl()
        {
            App.PIN = string.Empty;
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToSetPinURL);
        } 
        /// <summary>
        /// Method to navigate to signup screen
        /// </summary>
        private void NavigateFromConfirmPin()
        {
            if (!string.IsNullOrEmpty(Pin) || !string.IsNullOrWhiteSpace(Pin))
            {
                if (Pin.Length < 4)
                {
                    IsPinValidatorVisible = Visibility.Visible;
                    PinLengthMessage = "PIN should be 4 digits";
                    Pin = string.Empty;
                }
                else if (Pin == App.PIN)
                {
                    App.PIN = Pin;
                    INavigationService navigationService = this.GetService<INavigationService>();
                    navigationService.Navigate(PageURL.navigateToSignUpPanelURL);
                }
                else
                {
                    IsPinValidatorVisible = Visibility.Visible;
                    PinLengthMessage = "\t\tPIN mismatch";
                    Pin = string.Empty;
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
