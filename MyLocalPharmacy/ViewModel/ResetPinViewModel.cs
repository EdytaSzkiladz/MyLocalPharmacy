using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Utils;
using System.Windows;

namespace MyLocalPharmacy.ViewModel
{
    public class ResetPinViewModel : BaseViewModel
    {

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ResetPinViewModel()
        {
            _isPinValidatorVisible = Visibility.Collapsed;
        } 
        #endregion

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
                    _enterPin = new RelayCommand(NavigateBack);
                    _enterPin.Enabled = true;
                }

                return _enterPin;
            }
            set { _enterPin = value; }
        }
        /// <summary>
        /// Property to navigate back
        /// </summary>
        private RelayCommand _toResetPinLogin;
        public RelayCommand ToResetPinLogin
        {

            get
            {
                if (_toResetPinLogin == null)
                {
                    _toResetPinLogin = new RelayCommand(BackToResetPinLogin);
                    _toResetPinLogin.Enabled = true;
                }

                return _toResetPinLogin;
            }
            set { _toResetPinLogin = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        ///  Method to navigate to Reset Login Pin screen
        /// </summary>
        private void BackToResetPinLogin()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToResetPinLoginURL);
        }
       
        /// <summary>
        /// Method to navigate to signup screen
        /// </summary>
        private void NavigateBack()
        {
          
            INavigationService navigationService = this.GetService<INavigationService>();           
            if (!string.IsNullOrEmpty(Pin) || !string.IsNullOrWhiteSpace(Pin))
            {
                if (Pin.Length < 4)
                {
                    IsPinValidatorVisible = Visibility.Visible;
                    PinLengthMessage = "PIN should be 4 digits";
                    Pin = string.Empty;
                }
                else
                {
                    App.PIN = Pin;
                    navigationService.Navigate(PageURL.navigateToConfirmResetlURL);
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
