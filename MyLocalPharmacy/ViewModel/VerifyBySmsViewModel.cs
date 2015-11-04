using System;
using MyLocalPharmacy.Common;
using Microsoft.Phone.Controls;
using System.Windows;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.Model;
namespace MyLocalPharmacy.ViewModel
{
    public class VerifyBySmsViewModel : BaseViewModel
    {
        #region Declaration
        public VerifyBySmsModel VerifyBySmsModel;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public VerifyBySmsViewModel()
        {
            IsAuthCodeValidatorVisible = Visibility.Collapsed;
            HitVisibility = true;
            App.IsFromLoginScreen = false;
            App.IsVerifiedBySms = true;
        } 
        #endregion

        #region Properties
        /// <summary>
        /// Property to show and hide popup "Code already sent" 
        /// </summary>
        private bool _isRequestPopupOpen;
        
        public bool IsRequestPopupOpen
        {
            get { return _isRequestPopupOpen; }
            set { _isRequestPopupOpen = value; OnPropertyChanged("IsRequestPopupOpen"); }
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
        /// Property to get and set Authrisation code
        /// </summary>
        private string _authCode;
        public string AuthCode
        {
            get { return _authCode; }
            set { _authCode = value; OnPropertyChanged("AuthCode"); }
        }

        /// <summary>
        /// Property to show and hide popup "Code Resent" 
        /// </summary>
        private bool _isResentPopupOpen;

        public bool IsResentPopupOpen
        {
            get { return _isResentPopupOpen; }
            set { _isResentPopupOpen = value; OnPropertyChanged("IsResentPopupOpen"); }
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
        /// Property to set text to popup 
        /// </summary>
        private string _popupText;

        public string PopupText
        {
            get { return _popupText; }
            set { _popupText = value; OnPropertyChanged("PopupText"); }
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
        /// Property to show and hide popup "Verified" 
        /// </summary>
        private bool _isVerifiedPopupOpen;

        public bool IsVerifiedPopupOpen
        {
            get { return _isVerifiedPopupOpen; }
            set { _isVerifiedPopupOpen = value; OnPropertyChanged("IsVerifiedPopupOpen"); }
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
        /// Property for Resend sms Button
        /// </summary>
        private RelayCommand _ResendCommand;
        public RelayCommand ResendCommand
        {
            get
            {
                if (_ResendCommand == null)
                {

                    _ResendCommand = new RelayCommand(ResendCode);
                    _ResendCommand.Enabled = true;


                }
                return _ResendCommand;
            }
            set
            {
                _ResendCommand = value;
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
        /// Property for Ok button in popup "Wait for few minutes"
        /// </summary>
        private RelayCommand _WaitOkCommand;

        public RelayCommand WaitOkCommand
        {
            get
            {
                if (_WaitOkCommand == null)
                {

                    _WaitOkCommand = new RelayCommand(WaitOk);
                    _WaitOkCommand.Enabled = true;


                }
                return _WaitOkCommand;
            }
            set
            {
                _WaitOkCommand = value;
            }
        }

        /// <summary>
        /// Property for Ok button in popup "Code Sent"
        /// </summary>
        private RelayCommand _ResentOkCommand;

        public RelayCommand ResentOkCommand
        {
            get
            {
                if (_ResentOkCommand == null)
                {

                    _ResentOkCommand = new RelayCommand(ResentOk);
                    _ResentOkCommand.Enabled = true;


                }
                return _ResentOkCommand;
            }
            set
            {
                _ResentOkCommand = value;
            }
        }

        /// <summary>
        /// Property for Ok button in popup "Incorrect SMS code"
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

        /// <summary>
        /// Property for Ok button in popup "Verified"
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


        /// <summary>
        /// Property for Verify Button
        /// </summary>
        private RelayCommand _VerifyCommand;

        public RelayCommand VerifyCommand
        {
            get
            {
                if (_VerifyCommand == null)
                {

                    _VerifyCommand = new RelayCommand(Verify);
                    _VerifyCommand.Enabled = true;
                }
                return _VerifyCommand;
            }
            set
            {
                _VerifyCommand = value;
            }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method of Relay Command ResendCommand
        /// </summary>
        private void ResendCode()
        {
            if (Utilities.IsConnectedToNetwork())
            {
                VerifyBySmsModel = new VerifyBySmsModel(this, "ResentPin");
            }

            else
            {
                IsNoInternetPopupOpen = true;
                HitVisibility = false;
            }
        }

        /// <summary>
        /// Method of Relay Command ResendCommand
        /// </summary>
        private void WaitOk()
        {
            IsRequestPopupOpen = false;
            HitVisibility = true;
        }

        /// <summary>
        /// Method of Relay Command ResentOkCommand
        /// </summary>
        private void ResentOk()
        {
            IsResentPopupOpen = false;
            HitVisibility = true;
         
        }

        /// <summary>
        /// Method of Relay Command IncorrectOkCommand
        /// </summary>
        private void IncorrectOk()
        {
            IsIncorrectPopupOpen = false;
            HitVisibility = true;
          
        }

        /// <summary>
        /// Method of Relay Command VerifiedOkCommand
        /// </summary>
        private void VerifiedOk()
        {
            IsVerifiedPopupOpen = false;
            HitVisibility = true;
            App.IsApplicationInstancePreserved = false;
            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            frame.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.Relative));

        }

        /// <summary>
        /// Method of Relay Command VerifyCommand
        /// </summary>
        private void Verify()
        {
            if (string.IsNullOrEmpty(AuthCode) || string.IsNullOrWhiteSpace(AuthCode))
            {
                AuthCodeBorder = new Thickness(1.5);
                IsAuthCodeValidatorVisible = Visibility.Visible;                
            }
            else
            {
                AuthCodeBorder = new Thickness(0);
                IsAuthCodeValidatorVisible = Visibility.Collapsed;
                if (Utilities.IsConnectedToNetwork())
                {
                    VerifyBySmsModel = new VerifyBySmsModel(this, "Verify");
                }
                else
                {
                    IsNoInternetPopupOpen = true;
                    HitVisibility = false;
                }
            }

        }

        /// <summary>
        /// Method of No Internet Ok Command
        /// </summary>
        private void NoInternetOk()
        {
            IsNoInternetPopupOpen = false;
            HitVisibility = true;
        } 
        #endregion
    }
}
        
    
    
    

