using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        #region Declarations
        public SignUpModel SignUpModel;
        public SignUpViewModel objSignUpViewModel; 
        bool IsNewUserTab = true; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>     
        public SignUpViewModel()
        {
            _isLogInVisible = Visibility.Collapsed;
            _isPharmacyDetailsVisible = Visibility.Collapsed;
            _isPharmacyTextPanelVisible = Visibility.Visible;

            NewUserButtonColor = new SolidColorBrush(Colors.LightGray);
            LogInButtonColor = new SolidColorBrush(Colors.White);           

            _isLoginPasswordBoxVisible = Visibility.Collapsed;
            _isSignUpPasswordBoxPinVisible = Visibility.Collapsed;
            _progressBarVisibilty = Visibility.Collapsed;
           
            HideAllValidators();
            _isPopupOpen = false;
            HitVisibility = true;
            IsGetDetailsEnabled = true;
            if (App.IsClearLoginMailInnewInstance)
            {
               
                App.IsClearLoginMailInnewInstance = false;
                App.NoofTriesLeft = 9;
                HitVisibility = true;
            }
            
           
        } 
        #endregion

        #region Initialization
        /// <summary>
        /// Method to retrieve the parameters passed in query string
        /// </summary>
        /// <param name="parameters"></param>
        public override void Initialize(IDictionary<string, string> parameters)
        {

            base.Initialize(parameters);
            if ((parameters != null) && (parameters.Count > 0))
            {
                if (parameters["ToPanel"].Equals("SignUp"))
                {
                    ShowSignUpSelectedValues();
                }
                else if (parameters["ToPanel"].Equals("WithoutPINDisplay"))
                {
                    DisplayLoginWithoutPIN();
                }
                else
                {
                    ShowLoginSelectedValues();
                }

            }
        } 
        #endregion

        #region Properties Common to Login and SignUp Screen

        /// <summary>
        /// Property to set progressbar rowno
        /// </summary>
        private string _progressBarRowNo;
        public string ProgressBarRowNo
        {
            get { return _progressBarRowNo; }
            set { _progressBarRowNo = value; OnPropertyChanged("ProgressBarRowNo"); }
        }
        /// <summary>
        /// Property to identify the navigation page on NewUser/Login button
        /// </summary>
        private string _setContinueFlag;
        public string SetContinueFlag
        {
            get { return _setContinueFlag; }
            set { _setContinueFlag = value; }
        }


        /// <summary>
        /// Property to show/hide PharmacyText 
        /// </summary>
        private Visibility _isPharmacyTextPanelVisible;
        public Visibility IsPharmacyTextPanelVisible
        {
            get { return _isPharmacyTextPanelVisible; }
            set
            {
                _isPharmacyTextPanelVisible = value;
                OnPropertyChanged("IsPharmacyTextPanelVisible");
            }
        }

        /// <summary>
        /// Property to set Pharmacy details content
        /// </summary>
        private Visibility _isPharmacyDetailsVisible;
        public Visibility IsPharmacyDetailsVisible
        {
            get { return _isPharmacyDetailsVisible; }
            set
            {
                _isPharmacyDetailsVisible = value;
                OnPropertyChanged("IsPharmacyDetailsVisible");
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
        /// Property to Navigate to respective Pages when "Continue" button is clicked
        /// </summary>
        private RelayCommand _continueTo;
        public RelayCommand ContinueTo
        {

            get
            {
                if (_continueTo == null)
                {
                    _continueTo = new RelayCommand(ContinueToPage);
                    _continueTo.Enabled = true;
                }

                return _continueTo;
            }
            set { _continueTo = value; }
        }

        /// <summary>
        /// Property for navigation to Terms and Condition Page
        /// </summary>
        private RelayCommand _navigateToTC;
        public RelayCommand NavigateToTC
        {

            get
            {
                if (_navigateToTC == null)
                {
                    _navigateToTC = new RelayCommand(NavigateToTermsandCondition);
                    _navigateToTC.Enabled = true;
                }

                return _navigateToTC;
            }
            set { _navigateToTC = value; }
        }

        /// <summary>
        /// Property for navigation to ResetPin Page
        /// </summary>
        private RelayCommand _navigateToResetPin;
        public RelayCommand NavigateToResetPin
        {

            get
            {
                if (_navigateToResetPin == null)
                {
                    _navigateToResetPin = new RelayCommand(NavigateToResetPinPage);
                    _navigateToResetPin.Enabled = true;
                }

                return _navigateToResetPin;
            }
            set { _navigateToResetPin = value; }
        }



        #endregion

        #region Methods Common to SignUp and Login screen

        /// <summary>
        /// Hide All the Validators
        /// </summary>
        private void HideAllValidators()
        {
            _isSignupPidValidatorVisible = Visibility.Collapsed;
            _isSignupPinValidatorVisible = Visibility.Collapsed;

            _isLoginEmailValidatorVisible = Visibility.Collapsed;
            _isLoginPinValidatorVisible = Visibility.Collapsed;
            _isLoginPIdValidatorVisible = Visibility.Collapsed;
        }

        /// <summary>
        /// Method to Navigate to Pages when "Continue" button is clicked
        /// </summary>
        private void ContinueToPage()
        {
           
            if (IsNewUserTab)
            {
                if (IsSignUpDataValid())
                {
                    if (Utilities.IsConnectedToNetwork())
                    {
                        HideAllValidators();
                        ProgressBarVisibilty = Visibility.Visible;
                       
                        ProgressBarRowNo = "2";
                        App.HashPIN = Utilities.GetSHA256(DisplaySignUpPin);
                        CheckForPharmacy();
                        HitVisibility = false;
                    }
                    else
                    {
                        IsPopupOpen = true;
                        PopupText = "No connectivity.";
                        HitVisibility = false;
                    }
                }
            }
            else
            {
                if (IsLoginDataValid())
                {
                    ProgressBarVisibilty = Visibility.Visible;
                    HitVisibility = false;
                    ProgressBarRowNo = "4";
                    App.LoginPharId = LoginPharmacyID;
                    App.HashPIN = Utilities.GetSHA256(DisplayLoginPIN);
                    SignUpModel = new SignUpModel(this, "Loginscreen");
                }
            }
        }
        /// <summary>
        /// Check for valid pharmacy id and proceed to your details screen
        /// </summary>
        private void CheckForPharmacy()
        {
            SignUpModel = new SignUpModel(this, "CheckPharmacyId");
        }

        /// <summary>
        /// Method to Naviagte to Terms and Conditions Page
        /// </summary>
        private void NavigateToTermsandCondition()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            bool success = false;
            if (Utilities.IsConnectedToNetwork())
            {
                if (IsNewUserVisible == Visibility.Visible)
                    App.IsFromsignUp = true;
                success = navigationService.Navigate(PageURL.navigateToTermsandConditionURL);
            }
            else
            {
                IsPopupOpen = true; ;
                PopupText = "No connectivity.";
                HitVisibility = false;
            }
        }
        #endregion

        #region  Properties for SignUp Screen

        /// <summary>
        /// Property to display validation message
        /// </summary>
        private string _signUpPharmacyIdValidationMessage;
        public string SignUpPharmacyIdValidationMessage
        {
            get { return _signUpPharmacyIdValidationMessage; }
            set
            {
                _signUpPharmacyIdValidationMessage = value;
                OnPropertyChanged("SignUpPharmacyIdValidationMessage");
            }
        }

        /// <summary>
        /// Property to highlight the SignUpPharmacyPin Border
        /// </summary>
        private Thickness _signUpPinBorder;
        public Thickness SignUpPinBorder
        {
            get { return _signUpPinBorder; }
            set
            {
                _signUpPinBorder = value;
                OnPropertyChanged("SignUpPinBorder");
            }
        }

        /// <summary>
        /// Property to highlight the SignUpPharmacyID Border
        /// </summary>
        private Thickness _signUpPharmacyIDBorder;
        public Thickness SignUpPharmacyIDBorder
        {
            get { return _signUpPharmacyIDBorder; }
            set
            {
                _signUpPharmacyIDBorder = value;
                OnPropertyChanged("SignUpPharmacyIDBorder");
            }
        }
        /// <summary>
        /// Property to highlight the NewUser Button 
        /// </summary>
        private SolidColorBrush _newUserButtonColor;
        public SolidColorBrush NewUserButtonColor
        {
            get { return _newUserButtonColor; }
            set
            {
                _newUserButtonColor = value;
                OnPropertyChanged("NewUserButtonColor");
            }
        }

        /// <summary>
        /// Property to set NewUser Content
        /// </summary>
        private Visibility _isNewUserVisible;
        public Visibility IsNewUserVisible
        {
            get { return _isNewUserVisible; }
            set { _isNewUserVisible = value; OnPropertyChanged("IsNewUserVisible"); }
        }

        /// <summary>
        /// Property to show and hide wait popup 
        /// </summary>
        private bool _isGetDetailsEnabled;

        public bool IsGetDetailsEnabled
        {
            get { return _isGetDetailsEnabled; }
            set { _isGetDetailsEnabled = value; OnPropertyChanged("IsGetDetailsEnabled"); }
        }

        /// <summary>
        /// Property to get Pharmacy Details
        /// </summary>
        private RelayCommand _getPharmacyDetails;
        public RelayCommand GetPharmacyDetails
        {
            get
            {
                if (_getPharmacyDetails == null)
                {
                    _getPharmacyDetails = new RelayCommand(GetPharmacyData);
                    _getPharmacyDetails.Enabled = true;
                }
                return _getPharmacyDetails;
            }
            set
            {
                _getPharmacyDetails = value;
            }
        }

       
        /// <summary>
        /// Property for NewUser Click
        /// </summary>
        private RelayCommand _newUserCommand;
        public RelayCommand NewUserCommand
        {
            get
            {
                if (_newUserCommand == null)
                {
                    _newUserCommand = new RelayCommand(SubmitNewUser);
                    _newUserCommand.Enabled = true;
                }
                return _newUserCommand;
            }
            set
            {
                _newUserCommand = value;
            }
        }

        /// <summary>
        /// Property to set Visibility of Signup pharmacy id validator
        /// </summary>
        private Visibility _isSignupPinValidatorVisible;
        public Visibility IsSignupPinValidatorVisible
        {
            get { return _isSignupPinValidatorVisible; }
            set { _isSignupPinValidatorVisible = value; OnPropertyChanged("IsSignupPinValidatorVisible"); }
        }

        /// <summary>
        /// Property to set Visibility of Signup pharmacy id validator
        /// </summary>
        private Visibility _isSignupPidValidatorVisible;
        public Visibility IsSignupPidValidatorVisible
        {
            get { return _isSignupPidValidatorVisible; }
            set { _isSignupPidValidatorVisible = value; OnPropertyChanged("IsSignupPidValidatorVisible"); }
        }

        /// <summary>
        /// Property to set Visibility of Signup password box
        /// </summary>
        private Visibility _isSignUpPasswordBoxPinVisible;
        public Visibility IsSignUpPasswordBoxPinVisible
        {
            get { return _isSignUpPasswordBoxPinVisible; }
            set { _isSignUpPasswordBoxPinVisible = value; OnPropertyChanged("IsSignUpPasswordBoxPinVisible"); }
        }

        /// <summary>
        /// Property to set Visibility of Signup Pin text box
        /// </summary>
        private Visibility _isSignUpTextBoxPinVisible;
        public Visibility IsSignUpTextBoxPinVisible
        {
            get { return _isSignUpTextBoxPinVisible; }
            set { _isSignUpTextBoxPinVisible = value; OnPropertyChanged("IsSignUpTextBoxPinVisible"); }
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
        /// Property to set the SignUp PharmacyID
        /// </summary>
        private string _signUpPharmacyID;
        public string SignUpPharmacyID
        {
            get { return _signUpPharmacyID; }
            set { _signUpPharmacyID = value; OnPropertyChanged("SignUpPharmacyID");  }
        }


        #endregion

        #region Methods for SignUp Screen

        /// <summary>
        /// Method to show the panel for new user screen
        /// </summary>
        private void SubmitNewUser()
        {
            IsNewUserTab = true;
            IsNewUserVisible = Visibility.Visible;
            IsLogInVisible = Visibility.Collapsed;

            IsPharmacyTextPanelVisible = Visibility.Visible;
            

            IsSignUpTextBoxPinVisible = Visibility.Visible;
            IsSignUpPasswordBoxPinVisible = Visibility.Collapsed;

            NewUserButtonColor = new SolidColorBrush(Colors.LightGray);
            LogInButtonColor = new SolidColorBrush(Colors.White);

            App.PIN = "";
            DisplaySignUpPin = App.PIN;
            App.PIN = "";
            DisplayLoginPIN = App.PIN;

        }

        /// <summary>
        /// Method to display the entered values in SignUp Screen
        /// </summary>
        private void ShowSignUpSelectedValues()
        {
            IsNewUserTab = true;
            IsLogInVisible = Visibility.Collapsed;
            IsNewUserVisible = Visibility.Visible;

            if (string.IsNullOrEmpty(PharmacyName) || (string.IsNullOrWhiteSpace(PharmacyName)))
                IsPharmacyDetailsVisible = Visibility.Collapsed;
            else
            {
                IsPharmacyDetailsVisible = Visibility.Visible;
            }
            IsPharmacyTextPanelVisible = Visibility.Visible;

            if (string.IsNullOrEmpty(App.PIN) || (string.IsNullOrWhiteSpace(App.PIN)))
            {
                IsSignUpTextBoxPinVisible = Visibility.Visible;
                IsSignUpPasswordBoxPinVisible = Visibility.Collapsed;
            }
            else
            {
                IsSignUpTextBoxPinVisible = Visibility.Collapsed;
                IsSignUpPasswordBoxPinVisible = Visibility.Visible;
            }
            NewUserButtonColor = new SolidColorBrush(Colors.LightGray);
            LogInButtonColor = new SolidColorBrush(Colors.White);

            DisplaySignUpPin = App.PIN;
            SignUpPharmacyID = App.SignUpPharId;
        }

        /// <summary>
        /// Validate SignUp Data
        /// </summary>
        /// <returns></returns>
        private bool IsSignUpDataValid()
        {
            if ((string.IsNullOrEmpty(SignUpPharmacyID) || string.IsNullOrWhiteSpace(SignUpPharmacyID)) && (string.IsNullOrEmpty(DisplaySignUpPin) || string.IsNullOrWhiteSpace(DisplaySignUpPin)))
            {
                SignUpPharmacyIDBorder = new Thickness(1.5);
                SignUpPinBorder = new Thickness(1.5);
                SignUpPharmacyIdValidationMessage = "Pharmacy ID required.";
                IsSignupPidValidatorVisible = Visibility.Visible;
                IsSignupPinValidatorVisible = Visibility.Visible;
                return false;
            }
            else if ((string.IsNullOrEmpty(SignUpPharmacyID) || string.IsNullOrWhiteSpace(SignUpPharmacyID)))
            {
                SignUpPinBorder = new Thickness(0);
                SignUpPharmacyIDBorder = new Thickness(1.5);
                SignUpPharmacyIdValidationMessage = "Pharmacy ID required.";
                IsSignupPidValidatorVisible = Visibility.Visible;
                IsSignupPinValidatorVisible = Visibility.Collapsed;
                return false;
            }
            else if (SignUpPharmacyID.Length < 6)
            {
               
                SignUpPinBorder = new Thickness(0);
                SignUpPharmacyIDBorder = new Thickness(1.5);
                SignUpPharmacyIdValidationMessage = "Pharmacy ID should be 6 characters.";
                IsSignupPidValidatorVisible = Visibility.Visible;
                return false;
            }
            else if ((string.IsNullOrEmpty(DisplaySignUpPin) || string.IsNullOrWhiteSpace(DisplaySignUpPin)))
            {
                SignUpPharmacyIDBorder = new Thickness(0);
                SignUpPinBorder = new Thickness(1.5);
                IsSignupPidValidatorVisible = Visibility.Collapsed;
                IsSignupPinValidatorVisible = Visibility.Visible;
                return false;
            }

            else
            {
                return true;
            }
        }


        #endregion

        #region Pharmacy Details

        #region Properties
        /// <summary>
        /// Set progress bar visibility
        /// </summary>
        private Visibility _progressBarVisibilty = Visibility.Collapsed;
        public Visibility ProgressBarVisibilty
        {
            get
            {
                return _progressBarVisibilty;
            }
            set
            {
                _progressBarVisibilty = value;
                OnPropertyChanged("ProgressBarVisibilty");
            }
        }

        

        /// <summary>
        /// Set progress bar visibility of get Pharmacy details
        /// </summary>
        private Visibility _progressBarVisibiltyGetDetails = Visibility.Collapsed;
        public Visibility ProgressBarVisibiltyGetDetails
        {
            get
            {
                return _progressBarVisibiltyGetDetails;
            }
            set
            {
                _progressBarVisibiltyGetDetails = value;
                OnPropertyChanged("ProgressBarVisibiltyGetDetails");
            }
        }

        /// <summary>
        ///For Pharmacy Name in Pharmacy Details Section
        /// </summary>
        private string _pharmacyName;
        public string PharmacyName
        {
            get { return _pharmacyName; }
            set { _pharmacyName = value; OnPropertyChanged("PharmacyName"); }
        }
        /// <summary>
        /// Property to set address line1 in Pharmacy Details Section
        /// </summary>
        private string _addressLine1;
        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; OnPropertyChanged("AddressLine1"); }
        }
        /// <summary>
        /// Property to set address line1 in Pharmacy Details Section
        /// </summary>
        private string _addressLine2;
        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; OnPropertyChanged("AddressLine2"); }
        }
        /// <summary>
        /// Property to set address line1 in Pharmacy Details Section
        /// </summary>
        private string _addressLine3;
        public string AddressLine3
        {
            get { return _addressLine3; }
            set { _addressLine3 = value; OnPropertyChanged("AddressLine3"); }
        }
        /// <summary>
        /// Property to set PinCode in Pharmacy Details Section
        /// </summary>
        private string _pinCode;
        public string PinCode
        {
            get { return _pinCode; }
            set { _pinCode = value; OnPropertyChanged("PinCode"); }
        }

        #endregion

        #region Method

        /// <summary>
        /// Get Pharmacy Details
        /// </summary>
        private void GetPharmacyData()
        {
            
                if (string.IsNullOrWhiteSpace(SignUpPharmacyID) || string.IsNullOrEmpty(SignUpPharmacyID))
                {
                    IsSignupPidValidatorVisible = Visibility.Visible;
                    SignUpPharmacyIDBorder = new Thickness(1.5);
                    SignUpPharmacyIdValidationMessage = "Pharmacy ID required.";

                }
                else
                {
                    IsSignupPidValidatorVisible = Visibility.Collapsed;
                    SignUpPharmacyIDBorder = new Thickness(0);
                    if (Utilities.IsConnectedToNetwork())
                    {
                        IsPharmacyDetailsVisible = Visibility.Collapsed;
                        ProgressBarVisibiltyGetDetails = Visibility.Visible;
                        ProgressBarRowNo = "2";
                        IsGetDetailsEnabled = false;
                        PharmacyDetailsWebservicecall();
                    }
                    else
                    {
                        IsPopupOpen = true;
                        PopupText = "No connectivity.";
                        HitVisibility = false;
                    }
                }           
        }


        /// <summary>
        /// method to get pharmacy details
        /// </summary>
        private void PharmacyDetailsWebservicecall()
        {
            SignUpModel = new SignUpModel(this);

        }


        #endregion

        #endregion

        # region Properties for Login Screen

        /// <summary>
        /// Property to show and hide popup 
        /// </summary>
        private bool _isPopupOpen;

        public bool IsPopupOpen
        {
            get { return _isPopupOpen; }
            set { _isPopupOpen = value; OnPropertyChanged("IsPopupOpen"); }
        }

        /// <summary>
        /// Property to show and hide popup 
        /// </summary>
        private bool _isIncorrectPinPopupOpen;

        public bool IsIncorrectPinPopupOpen
        {
            get { return _isIncorrectPinPopupOpen; }
            set { _isIncorrectPinPopupOpen = value; OnPropertyChanged("IsIncorrectPinPopupOpen"); }
        }

        /// <summary>
        /// Property to show and hide Confirm popup 
        /// </summary>
        private bool _isConfirmPopupOpen;

        public bool IsConfirmPopupOpen
        {
            get { return _isConfirmPopupOpen; }
            set { _isConfirmPopupOpen = value; OnPropertyChanged("IsConfirmPopupOpen"); }
        }


        /// <summary>
        /// Property to show and hide wait popup 
        /// </summary>
        private bool _isWaitPopupOpen;

        public bool IsWaitPopupOpen
        {
            get { return _isWaitPopupOpen; }
            set { _isWaitPopupOpen = value; OnPropertyChanged("IsWaitPopupOpen"); }
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
        /// Set popup text for incorrect pin
        /// </summary>
        private string _incorrectPinPopupText;

        public string IncorrectPinPopupText
        {
            get { return _incorrectPinPopupText; }
            set { _incorrectPinPopupText = value; OnPropertyChanged("IncorrectPinPopupText"); }
        }
        /// <summary>
        /// Set text for popup with ok and cancel 
        /// </summary>
        private string _popupTextDisplay;

        public string PopupTextDisplay
        {
            get { return _popupTextDisplay; }
            set { _popupTextDisplay = value; OnPropertyChanged("PopupTextDisplay"); }
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

                    _OkCommand = new RelayCommand(PopupOk);
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
        /// Property for OK button of popup incorrect pin
        /// </summary>
        private RelayCommand _incorrectPinOkCommand;

        public RelayCommand IncorrectPinOkCommand
        {
            get
            {
                if (_incorrectPinOkCommand == null)
                {

                    _incorrectPinOkCommand = new RelayCommand(IncorrectPinPopupOk);
                    _incorrectPinOkCommand.Enabled = true;


                }
                return _incorrectPinOkCommand;
            }
            set
            {
                _incorrectPinOkCommand = value;
            }
        }

        /// <summary>
        /// Property for OK button to confirm reset 
        /// </summary>
        private RelayCommand _OkCommandPopup;

        public RelayCommand OkCommandPopup
        {
            get
            {
                if (_OkCommandPopup == null)
                {

                    _OkCommandPopup = new RelayCommand(ConfirmPopupOk);
                    _OkCommandPopup.Enabled = true;


                }
                return _OkCommandPopup;
            }
            set
            {
                _OkCommandPopup = value;
            }
        }

        /// <summary>
        /// Property for cancel button of confirm popup 
        /// </summary>
        private RelayCommand _CancelCommandPopup;

        public RelayCommand CancelCommandPopup
        {
            get
            {
                if (_CancelCommandPopup == null)
                {

                    _CancelCommandPopup = new RelayCommand(ConfirmPopupCancel);
                    _CancelCommandPopup.Enabled = true;


                }
                return _CancelCommandPopup;
            }
            set
            {
                _CancelCommandPopup = value;
            }
        }

        /// <summary>
        /// Property for ok button of wait popup 
        /// </summary>
        private RelayCommand _WaitCommandPopup;

        public RelayCommand WaitCommandPopup
        {
            get
            {
                if (_WaitCommandPopup == null)
                {
                    _WaitCommandPopup = new RelayCommand(WaitPopupOk);
                    _WaitCommandPopup.Enabled = true;
                }
                return _WaitCommandPopup;
            }
            set
            {
                _WaitCommandPopup = value;
            }
        }

        /// <summary>
        /// Property to display validation message
        /// </summary>
        private string _loginPharmacyIdValidationMessage;
        public string LoginPharmacyIdValidationMessage
        {
            get { return _loginPharmacyIdValidationMessage; }
            set
            {
                _loginPharmacyIdValidationMessage = value;
                OnPropertyChanged("LoginPharmacyIdValidationMessage");
            }
        }


        /// <summary>
        /// Property to set Thickness
        /// </summary>
        private Thickness _loginPharmacyIDBorder;
        public Thickness LoginPharmacyIDBorder
        {
            get { return _loginPharmacyIDBorder; }
            set
            {
                _loginPharmacyIDBorder = value;
                OnPropertyChanged("LoginPharmacyIDBorder");

            }
        }

        /// <summary>
        /// Property to set Thickness
        /// </summary>
        private Thickness _loginPinBorder;
        public Thickness LoginPinBorder
        {
            get { return _loginPinBorder; }
            set
            {
                _loginPinBorder = value;
                OnPropertyChanged("LoginPinBorder");

            }
        }

        /// <summary>
        /// Property to set Thickness
        /// </summary>
        private Thickness _loginEmailBorder;
        public Thickness LoginEmailBorder
        {
            get { return _loginEmailBorder; }
            set
            {
                _loginEmailBorder = value;
                OnPropertyChanged("LoginEmailBorder");

            }
        }
        /// <summary>
        /// Property to set visibility of validator
        /// </summary>
        private Visibility _isLoginEmailValidatorVisible;
        public Visibility IsLoginEmailValidatorVisible
        {
            get { return _isLoginEmailValidatorVisible; }
            set
            {
                _isLoginEmailValidatorVisible = value;
                OnPropertyChanged("IsLoginEmailValidatorVisible");

            }
        }

        /// <summary>
        /// Property to set visibility of validator
        /// </summary>
        private Visibility _isLoginPinValidatorVisible;
        public Visibility IsLoginPinValidatorVisible
        {
            get { return _isLoginPinValidatorVisible; }
            set
            {
                _isLoginPinValidatorVisible = value;
                OnPropertyChanged("IsLoginPinValidatorVisible");

            }
        }

        /// <summary>
        /// Property to set visibility of validator
        /// </summary>
        private Visibility _isLoginPIdValidatorVisible;
        public Visibility IsLoginPIdValidatorVisible
        {
            get { return _isLoginPIdValidatorVisible; }
            set
            {
                _isLoginPIdValidatorVisible = value;
                OnPropertyChanged("IsLoginPIdValidatorVisible");

            }
        }

        /// <summary>
        /// Property to set Login content
        /// </summary>
        private Visibility _isLogInVisible;
        public Visibility IsLogInVisible
        {
            get { return _isLogInVisible; }
            set
            {
                _isLogInVisible = value;
                OnPropertyChanged("IsLogInVisible");

            }
        }

        /// <summary>
        /// Property for Login click
        /// </summary>
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {

                    _loginCommand = new RelayCommand(SubmitLogin);
                    _loginCommand.Enabled = true;
                }
                return _loginCommand;
            }
            set
            {
                _loginCommand = value;
            }
        }

        /// <summary>
        /// Property to set the Login PharmacyID
        /// </summary>
        private string _loginPharmacyID;
        public string LoginPharmacyID
        {
            get { return _loginPharmacyID; }
            set { _loginPharmacyID = value; OnPropertyChanged("LoginPharmacyID"); }
        }
        /// <summary>
        /// To display PIN
        /// </summary>
        private string _displayLoginPIN;
        public string DisplayLoginPIN
        {
            get { return _displayLoginPIN; }
            set { _displayLoginPIN = value; OnPropertyChanged("DisplayLoginPIN"); }
        }

        /// <summary>
        /// To display Email
        /// </summary>
        private string _loginEmail;
        public string LoginEmail
        {
            get { return _loginEmail; }
            set { _loginEmail = value; OnPropertyChanged("LoginEmail"); }
        }

        /// <summary>
        /// To display Email Validation Message
        /// </summary>
        private string _emailValidationMessage;
        public string EmailValidationMessage
        {
            get { return _emailValidationMessage; }
            set { _emailValidationMessage = value; OnPropertyChanged("EmailValidationMessage"); }
        }

        /// <summary>
        /// Property to set PIN PasswordBox Visibility in Login screen
        /// </summary>
        private Visibility _isLoginPasswordBoxVisible;
        public Visibility IsLoginPasswordBoxVisible
        {
            get { return _isLoginPasswordBoxVisible; }
            set { _isLoginPasswordBoxVisible = value; OnPropertyChanged("IsLoginPasswordBoxVisible"); }
        }

        /// <summary>
        /// Property to set PIN TextBox Visibility in Login screen
        /// </summary>
        private Visibility _isLoginPinTextBoxVisible;
        public Visibility IsLoginPinTextBoxVisible
        {
            get { return _isLoginPinTextBoxVisible; }
            set { _isLoginPinTextBoxVisible = value; OnPropertyChanged("IsLoginPinTextBoxVisible"); }
        }

        /// <summary>
        /// Property to highlight the Login Button 
        /// </summary>
        private SolidColorBrush _logInButtonColor;
        public SolidColorBrush LogInButtonColor
        {
            get { return _logInButtonColor; }
            set
            {
                _logInButtonColor = value;
                OnPropertyChanged("LogInButtonColor");
            }
        }

        /// <summary>
        /// Property for Tap the Pin control
        /// </summary>
        private RelayCommand _goToEnterPinPage;
        public RelayCommand GoToEnterPinPage
        {
            get
            {
                if (_goToEnterPinPage == null)
                {

                    _goToEnterPinPage = new RelayCommand(NavigateToEnterPinPage);
                    _goToEnterPinPage.Enabled = true;


                }
                return _goToEnterPinPage;
            }
            set
            {
                _goToEnterPinPage = value;
            }
        }

        #endregion

        #region Methods for Login Screen

        /// <summary>
        /// Click ok button of Popup
        /// </summary>
        private void PopupOk()
        {
            IsPopupOpen = false;
            HitVisibility = true;
        }
        

             /// <summary>
        /// Click ok button of Popup
        /// </summary>
        private void IncorrectPinPopupOk()
        {
            IsIncorrectPinPopupOpen = false;
            HitVisibility = true;
           
        }
        /// <summary>
        /// Click cancel button of Popup
        /// </summary>
        private void ConfirmPopupCancel()
        {
            IsConfirmPopupOpen = false;
            HitVisibility = true;
        }

        /// <summary>
        /// Click ok button of Popup Wait
        /// </summary>
        private void WaitPopupOk()
        {
            IsWaitPopupOpen = false;
            HitVisibility = true;
            INavigationService navigationService = this.GetService<INavigationService>();

            if (Utilities.IsConnectedToNetwork())
            {
                navigationService.Navigate(PageURL.navigateToResetPinLoginURL);
            }
            else
            {
                IsPopupOpen = true;
                PopupText = "No connectivity.";
                HitVisibility = false;
            }
        }

        /// <summary>
        /// Click reset button of Popup
        /// </summary>
        private void ConfirmPopupOk()
        {
            if (ValidateEmail())
            {
                IsConfirmPopupOpen = true;
                PopupTextDisplay = "Are you sure you want to reset your PIN?";
                HitVisibility = false;
            }
        }

        /// <summary>
        /// Method to show the panel for Login screen
        /// </summary>
        private void SubmitLogin()
        {
            IsNewUserTab = false;
            IsLogInVisible = Visibility.Visible;
            IsNewUserVisible = Visibility.Collapsed;


            if (ProgressBarVisibiltyGetDetails == Visibility.Collapsed && !(string.IsNullOrEmpty(PharmacyName) || string.IsNullOrWhiteSpace(PharmacyName)))
            {
                IsPharmacyDetailsVisible = Visibility.Visible;
            }
            else
            {
                IsPharmacyDetailsVisible = Visibility.Collapsed;
            }
            IsPharmacyTextPanelVisible = Visibility.Collapsed;

            IsLoginPinTextBoxVisible = Visibility.Visible;
            IsLoginPasswordBoxVisible = Visibility.Collapsed;

            //ProgressBarVisibilty = Visibility.Collapsed;

            NewUserButtonColor = new SolidColorBrush(Colors.White);
            LogInButtonColor = new SolidColorBrush(Colors.LightGray);


            App.PIN = string.Empty;
            DisplaySignUpPin = App.PIN;
            App.PIN = string.Empty;
            DisplayLoginPIN = App.PIN;
        }
        /// <summary>
        ///On redirect from "Cancel" of Enter PIN page
        /// </summary>
        private void DisplayLoginWithoutPIN()
        {
            IsNewUserTab = false;
            IsLogInVisible = Visibility.Visible;
            IsNewUserVisible = Visibility.Collapsed;
           

            IsPharmacyDetailsVisible = Visibility.Collapsed;
            IsPharmacyTextPanelVisible = Visibility.Collapsed;

            IsLoginPinTextBoxVisible = Visibility.Visible;
            IsLoginPasswordBoxVisible = Visibility.Collapsed;

            NewUserButtonColor = new SolidColorBrush(Colors.White);
            LogInButtonColor = new SolidColorBrush(Colors.LightGray);

            DisplayLoginPIN = App.PIN = string.Empty;
            LoginPharmacyID = App.LoginPharId;
           LoginEmail = App.LoginEmailId;
        }

        /// <summary>
        /// Method to display the entered values in Login screen
        /// </summary>
        private void ShowLoginSelectedValues()
        {
            IsNewUserTab = false;
            IsLogInVisible = Visibility.Visible;
            IsNewUserVisible = Visibility.Collapsed;
           

            IsPharmacyDetailsVisible = Visibility.Collapsed;
            IsPharmacyTextPanelVisible = Visibility.Collapsed;

            IsLoginPinTextBoxVisible = Visibility.Collapsed;
            IsLoginPasswordBoxVisible = Visibility.Visible;

            NewUserButtonColor = new SolidColorBrush(Colors.White);
            LogInButtonColor = new SolidColorBrush(Colors.LightGray);

            DisplayLoginPIN = App.PIN;
            if (string.IsNullOrEmpty(App.PIN) || (string.IsNullOrWhiteSpace(App.PIN)))
            {
                IsLoginPinTextBoxVisible = Visibility.Visible;
                IsLoginPasswordBoxVisible = Visibility.Collapsed;
            }
            LoginPharmacyID = App.LoginPharId;
            if (App.IsClearLoginMailInnewInstance || App.IsUserNotExist)
            {
                LoginEmail = string.Empty;
                App.IsClearLoginMailInnewInstance = false;
                App.NoofTriesLeft = 9;
                App.IsUserNotExist = false;
            }
            else
                LoginEmail = App.MailIdToFillAfterPin;
        }

        /// <summary>
        /// Navigate to EnterPin Page
        /// </summary>
        private void NavigateToEnterPinPage()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            if (Utilities.IsConnectedToNetwork())
            {
               navigationService.Navigate(PageURL.navigateToEnterPinURL);
            }
            else
            {
                IsPopupOpen = true;
                PopupText = "No connectivity.";
                HitVisibility = false;
            }
        }

        /// <summary>
        /// Validate email for reset pin
        /// </summary>
        /// <returns></returns>
        private bool ValidateEmail()
        {
            bool LoginEmailHasValue;
            if (string.IsNullOrEmpty(LoginEmail) || string.IsNullOrWhiteSpace(LoginEmail))
            {

                IsLoginEmailValidatorVisible = Visibility.Visible;
                LoginEmailBorder = new Thickness(1.5);
                EmailValidationMessage = "Email ID required.";
                LoginEmailHasValue = false;
               
            }
            else
            {
                bool isEmail = Regex.IsMatch(LoginEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    IsLoginEmailValidatorVisible = Visibility.Visible;
                    LoginEmailBorder = new Thickness(1.5);
                    EmailValidationMessage = "Email ID format is incorrect.";
                    LoginEmailHasValue = false;
                }
                else
                {
                    IsLoginEmailValidatorVisible = Visibility.Collapsed;
                    LoginEmailBorder = new Thickness(0);
                    LoginEmailHasValue = true;
                }

            }
            return LoginEmailHasValue;
        }
        /// <summary>
        /// Navigate to Reset Pin Page
        /// </summary>
        private void NavigateToResetPinPage()
        {
            if (Utilities.IsConnectedToNetwork())
            {
                App.transientMailId = LoginEmail;
                if(App.IsChangePharmacy)
                    SignUpModel = new SignUpModel(this, "Changepharmacy");
                else
                    SignUpModel = new SignUpModel(this, "ResetPin");
            }
            else
            {
                IsPopupOpen = true;
                PopupText = "No connectivity.";
                HitVisibility = false;
            }

        }


        /// <summary>
        /// Validate Login Data
        /// </summary>
        /// <returns></returns>
        private bool IsLoginDataValid()
        {
            bool LoginPharmacyIDHasValue;
            bool DisplayLoginPINHasValue;
            bool LoginEmailHasValue;
            if (string.IsNullOrEmpty(LoginPharmacyID) || string.IsNullOrWhiteSpace(LoginPharmacyID))
            {
                LoginPharmacyIdValidationMessage = "Pharmacy ID required.";
                IsLoginPIdValidatorVisible = Visibility.Visible;
                LoginPharmacyIDBorder = new Thickness(1.5);
                LoginPharmacyIDHasValue = false;
            }
            else
            {
                if (LoginPharmacyID.Length < 6)
                {
                    LoginPharmacyIdValidationMessage = "Pharmacy ID should be 6 characters.";
                    IsLoginPIdValidatorVisible = Visibility.Visible;
                    LoginPharmacyIDBorder = new Thickness(1.5);
                    LoginPharmacyIDHasValue = false;
                }
                else
                {
                    IsLoginPIdValidatorVisible = Visibility.Collapsed;
                    LoginPharmacyIDBorder = new Thickness(0);
                    LoginPharmacyIDHasValue = true;
                }

            }

            if ((string.IsNullOrEmpty(DisplayLoginPIN) || string.IsNullOrWhiteSpace(DisplayLoginPIN)))
            {
                IsLoginPinValidatorVisible = Visibility.Visible;
                LoginPinBorder = new Thickness(1.5);
                DisplayLoginPINHasValue = false;
            }
            else
            {
                IsLoginPinValidatorVisible = Visibility.Collapsed;
                LoginPinBorder = new Thickness(0);
                DisplayLoginPINHasValue = true;
            }
            if (string.IsNullOrEmpty(LoginEmail) || string.IsNullOrWhiteSpace(LoginEmail))
            {

                IsLoginEmailValidatorVisible = Visibility.Visible;
                LoginEmailBorder = new Thickness(1.5);
                EmailValidationMessage = "Email ID required.";
                LoginEmailHasValue = false;
            }
            else
            {
                bool isEmail = Regex.IsMatch(LoginEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    IsLoginEmailValidatorVisible = Visibility.Visible;
                    LoginEmailBorder = new Thickness(1.5);
                    EmailValidationMessage = "Email ID format is incorrect.";
                    LoginEmailHasValue = false;
                }
                else
                {
                    IsLoginEmailValidatorVisible = Visibility.Collapsed;
                    LoginEmailBorder = new Thickness(0);
                    LoginEmailHasValue = true;
                }
            }

            if ((LoginPharmacyIDHasValue) && (DisplayLoginPINHasValue) && (LoginEmailHasValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }


}
