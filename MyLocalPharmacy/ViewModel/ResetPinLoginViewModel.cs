using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System.Windows;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    public class ResetPinLoginViewModel : BaseViewModel
    {
        #region Declarations
        private bool _isValidatedAuth = false;
        private bool _isValidatedPin = false;
        public ResetPinModel ResetPinModel; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ResetPinLoginViewModel()
        {
            HitVisibility = true;
            IsAuthCodeValidatorVisible = Visibility.Collapsed;
            IsPinValidatorVisible = Visibility.Collapsed;

            if (App.ObjBrandingResponse != null)
            {
                if (App.ObjBrandingResponse.payload != null)
                {
                    PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);
                    AppBarPrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;
                }
                else
                {
                    PrimaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);

                    AppBarPrimaryColour = RxConstants.PrimaryColourCodeAppbar;
                }
            }
            else
            {
                PrimaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);
                AppBarPrimaryColour = RxConstants.PrimaryColourCodeAppbar;
            }

            if (string.IsNullOrEmpty(App.resetLoginPIN) || string.IsNullOrWhiteSpace(App.resetLoginPIN))
            {
                IsLoginPasswordBoxVisible = Visibility.Collapsed;
                IsLoginPinTextBoxVisible = Visibility.Visible;
            }
            else
            {
                IsLoginPasswordBoxVisible = Visibility.Visible;
                IsLoginPinTextBoxVisible = Visibility.Collapsed;
                DisplaySignUpPin = App.resetLoginPIN;
            }

            if (!string.IsNullOrEmpty(App.resetLoginAuthCode) || !string.IsNullOrWhiteSpace(App.resetLoginAuthCode))
            {
                AuthCode = App.resetLoginAuthCode;
            }
        } 
        #endregion

        #region Properties
        #region Validator Properties
        /// <summary>
        /// Show validation border over Pin control
        /// </summary>
        private Thickness _pinBorder;
        public Thickness PinBorder
        {
            get { return _pinBorder; }
            set { _pinBorder = value; OnPropertyChanged("PinBorder"); }
        }

        /// <summary>
        /// Show validation border over Auth Code control
        /// </summary>
        private Thickness _authCodeBorder;
        public Thickness AuthCodeBorder
        {
            get { return _authCodeBorder; }
            set { _authCodeBorder = value; OnPropertyChanged("AuthCodeBorder"); }
        }

        /// <summary>
        /// Show the AuthCode validator
        /// </summary>
        private Visibility _isAuthCodeValidatorVisible;
        public Visibility IsAuthCodeValidatorVisible
        {
            get { return _isAuthCodeValidatorVisible; }
            set { _isAuthCodeValidatorVisible = value; OnPropertyChanged("IsAuthCodeValidatorVisible"); }
        }

        /// <summary>
        /// Show the Pin validator
        /// </summary>
        private Visibility _isPinValidatorVisible;
        public Visibility IsPinValidatorVisible
        {
            get { return _isPinValidatorVisible; }
            set { _isPinValidatorVisible = value; OnPropertyChanged("IsPinValidatorVisible"); }
        }
        #endregion

        /// <summary>
        /// For AppBarPrimaryColour
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
        /// Property to set hit visibility
        /// </summary>
        private bool _hitVisibility;
        public bool HitVisibility
        {
            get { return _hitVisibility; }
            set { _hitVisibility = value; OnPropertyChanged("HitVisibility"); }
        }

        /// <summary>
        /// Property to set the SignUp PIN
        /// </summary>
        private string _displaySignUpPin;
        public string DisplaySignUpPin
        {
            get { return _displaySignUpPin; }
            set { _displaySignUpPin = value; OnPropertyChanged("DisplaySignUpPin"); }
        }

        /// <summary>
        /// Property to show and hide popup "Incorrect Code" 
        /// </summary>
        private bool _isIncorrectPopupOpen;

        public bool IsIncorrectPopupOpen
        {
            get { return _isIncorrectPopupOpen; }
            set { _isIncorrectPopupOpen = value; OnPropertyChanged("IsIncorrectPopupOpen"); }
        }

        /// <summary>
        /// Property to show and hide popup "Code has been reset" 
        /// </summary>
        private bool _isResetPopupOpen;

        public bool IsResetPopupOpen
        {
            get { return _isResetPopupOpen; }
            set { _isResetPopupOpen = value; OnPropertyChanged("IsResetPopupOpen"); }
        }

        /// <summary>
        /// Property to show and hide popup "no such user" 
        /// </summary>
        private bool _isNoUserPopupOpen;

        public bool IsNoUserPopupOpen
        {
            get { return _isNoUserPopupOpen; }
            set { _isNoUserPopupOpen = value; OnPropertyChanged("IsNoUserPopupOpen"); }
        }

        /// <summary>
        /// Property to show and hide popup "No Internet Connectivity" 
        /// </summary>
        private bool _isNoInternetPopupOpen;

        public bool IsNoInternetPopupOpen
        {
            get { return _isNoInternetPopupOpen; }
            set { _isNoInternetPopupOpen = value; OnPropertyChanged("IsNoInternetPopupOpen"); }
        }

        /// <summary>
        /// Property to set PIN PasswordBox Visibility in ResetPinLogin screen
        /// </summary>
        private Visibility _isLoginPasswordBoxVisible;
        public Visibility IsLoginPasswordBoxVisible
        {
            get { return _isLoginPasswordBoxVisible; }
            set { _isLoginPasswordBoxVisible = value; OnPropertyChanged("IsLoginPasswordBoxVisible"); }
        }

        /// <summary>
        /// Property to set PIN TextBox Visibility in ResetPinLogin screen
        /// </summary>
        private Visibility _isLoginPinTextBoxVisible;
        public Visibility IsLoginPinTextBoxVisible
        {
            get { return _isLoginPinTextBoxVisible; }
            set { _isLoginPinTextBoxVisible = value; OnPropertyChanged("IsLoginPinTextBoxVisible"); }
        }

        /// <summary>
        /// Property to get and set Email
        /// </summary>
        private string _loginEmail;
        public string LoginEmail
        {
            get { return _loginEmail; }
            set { _loginEmail = value; OnPropertyChanged("LoginEmail"); }
        }

        /// <summary>
        /// Property to get and set Authrisation code
        /// </summary>
        private string _authCode;
        public string AuthCode
        {
            get { return _authCode; }
            set { _authCode = value; OnPropertyChanged("AuthCode"); }
        }

        /// <summary>
        /// Property for Ok button in popup "Incorrect Authorisation code"
        /// </summary>
        private RelayCommand _IncorrectOkCommand;

        public RelayCommand IncorrectOkCommand
        {
            get
            {
                if (_IncorrectOkCommand == null)
                {

                    _IncorrectOkCommand = new RelayCommand(IncorrectOk);
                    _IncorrectOkCommand.Enabled = true;


                }
                return _IncorrectOkCommand;
            }
            set
            {
                _IncorrectOkCommand = value;
            }
        }
        //
        /// <summary>
        /// On "Cancel" click
        /// </summary>
        private RelayCommand _toResetPinLogin;

        public RelayCommand ToResetPinLogin
        {
            get
            {
                if (_toResetPinLogin == null)
                {

                    _toResetPinLogin = new RelayCommand(ToResetPinLoginUrl);
                    _toResetPinLogin.Enabled = true;


                }
                return _toResetPinLogin;
            }
            set
            {
                _toResetPinLogin = value;
            }
        }
        
        /// <summary>
        /// Property for Ok button in popup "No such user"
        /// </summary>
        private RelayCommand _NoUserOkCommand;

        public RelayCommand NoUserOkCommand
        {
            get
            {
                if (_NoUserOkCommand == null)
                {

                    _NoUserOkCommand = new RelayCommand(NoUserOk);
                    _NoUserOkCommand.Enabled = true;


                }
                return _NoUserOkCommand;
            }
            set
            {
                _NoUserOkCommand = value;
            }
        }

        /// <summary>
        /// Property for Ok button in popup "No Internet Connectivity"
        /// </summary>
        private RelayCommand _NoInternetOkCommand;

        public RelayCommand NoInternetOkCommand
        {
            get
            {
                if (_NoInternetOkCommand == null)
                {

                    _NoInternetOkCommand = new RelayCommand(NoInternetOk);
                    _NoInternetOkCommand.Enabled = true;


                }
                return _NoInternetOkCommand;
            }
            set
            {
                _NoInternetOkCommand = value;
            }
        }


        /// <summary>
        /// Property for Ok button in popup "Reset Done"
        /// </summary>
        private RelayCommand _resetOkCommand;

        public RelayCommand ResetOkCommand
        {
            get
            {
                if (_resetOkCommand == null)
                {

                    _resetOkCommand = new RelayCommand(ResetOk);
                    _resetOkCommand.Enabled = true;


                }
                return _resetOkCommand;
            }
            set
            {
                _IncorrectOkCommand = value;
            }
        }

        /// <summary>
        /// Property for Ok button in Appbar
        /// </summary>
        private RelayCommand _VerifiedOkCommand;

        public RelayCommand VerifiedOkCommand
        {
            get
            {
                if (_VerifiedOkCommand == null)
                {

                    _VerifiedOkCommand = new RelayCommand(VerifiedOk);
                    _VerifiedOkCommand.Enabled = true;


                }
                return _VerifiedOkCommand;
            }
            set
            {
                _VerifiedOkCommand = value;
            }
        }
        
        #endregion

        #region Methods
        /// <summary>
        /// Navigate to ToResetPinLoginUrl
        /// </summary>
        private void ToResetPinLoginUrl()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToResetPinLoginURL);
        }
        /// <summary>
        ///Method to verify
        /// </summary>
        private void VerifiedOk()
        {
            ValidateControls();
            if (_isValidatedAuth == true && _isValidatedPin == true)
            {
                if (Utilities.IsConnectedToNetwork())
                {
                    DisplaySignUpPin = App.resetLoginPIN;
                    LoginEmail = App.LoginEmailId;
                    ResetPinModel = new ResetPinModel(this);
                }
                else
                {
                    IsNoInternetPopupOpen = true;
                    HitVisibility = false;
                }
            }
        }

        /// <summary>
        /// Method to validate Controls
        /// </summary>
        private void ValidateControls()
        {
            if (string.IsNullOrEmpty(AuthCode) || string.IsNullOrWhiteSpace(AuthCode))
            {
                AuthCodeBorder = new Thickness(1.5);
                IsAuthCodeValidatorVisible = Visibility.Visible;
                _isValidatedAuth = false;
            }
            else
            {
                AuthCodeBorder = new Thickness(0);
                IsAuthCodeValidatorVisible = Visibility.Collapsed;
                _isValidatedAuth = true;
            }
            if (string.IsNullOrEmpty(DisplaySignUpPin) || string.IsNullOrWhiteSpace(DisplaySignUpPin))
            {
                PinBorder = new Thickness(1.5);
                IsPinValidatorVisible = Visibility.Visible;
                _isValidatedPin = false;
            }
            else
            {
                PinBorder = new Thickness(0);
                IsPinValidatorVisible = Visibility.Collapsed;
                _isValidatedPin = true;
            }
        }
        /// <summary>
        /// Method to handle "OK" click
        /// </summary>
        private void ResetOk()
        {
            App.TombStonedPageURL = string.Empty;
            INavigationService navigationService = this.GetService<INavigationService>();
            if (App.PinResetFromSettingsPage)
            {
                App.PinResetFromSettingsPage = false;
                navigationService.Navigate(PageURL.navigateToHomePanoramaURL);
            }
            else
                navigationService.Navigate(PageURL.navigateToLoginPanelURL);
            HitVisibility = true;
        }
        /// <summary>
        /// Method to handle "OK" click of "NoUser" popup
        /// </summary>
        private void NoUserOk()
        {            
            IsNoUserPopupOpen = false;
            HitVisibility = true;
        }

        /// <summary>
        /// Method to handle "OK" click of "Incorrect" popup
        /// </summary>
        private void IncorrectOk()
        {
            IsIncorrectPopupOpen = false;
            HitVisibility = true;
        }

        /// <summary>
        /// Method to handle "OK" click of "NoInternet" popup
        /// </summary>
        private void NoInternetOk()
        {
            IsNoInternetPopupOpen = false;
            HitVisibility = true;
        } 
        #endregion
    }
}
