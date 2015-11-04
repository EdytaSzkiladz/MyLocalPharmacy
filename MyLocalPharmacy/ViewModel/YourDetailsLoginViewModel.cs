using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Text.RegularExpressions;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.Model;
using System.Collections.ObjectModel;
using MyLocalPharmacy.Common;
using System.Runtime.Serialization;

namespace MyLocalPharmacy.ViewModel
{
    public class YourDetailsLoginViewModel : BaseViewModel
    {
        #region Declarations
        public YourDetailsLoginModel objydloginModel; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public YourDetailsLoginViewModel()
        {
            _isFemaleSelected = true;
            _isMailSelected = true;
            HideAllValidators();
            _isPopupOpen = false;
            _dob = System.DateTime.Now;
            HitVisibility = true;
            PopulateCountry();
            if ((!string.IsNullOrEmpty(App.SelectedSurgen)) && (!string.IsNullOrWhiteSpace(App.SelectedSurgen)))
            {
                _buttonValue = App.SelectedSurgen;
            }
            else
                _buttonValue = "Choose Doctor for surgery (Optional)"; 
        } 
        #endregion        

        #region Properties
        /// <summary>
        /// Property to set progressbar rowno
        /// </summary>
        private string _progressBarRowNo;
        [DataMember]
        public string ProgressBarRowNo
        {
            get { return _progressBarRowNo; }
            set { _progressBarRowNo = value; OnPropertyChanged("ProgressBarRowNo"); }
        }

        /// <summary>
        /// Set progress bar visibility
        /// </summary>
        private Visibility _progressBarVisibilty = Visibility.Collapsed;
        [DataMember]
        public Visibility ProgressBarVisibilty
        {
            get
            {
                return _progressBarVisibilty;
            }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                _progressBarVisibilty = value;
                OnPropertyChanged("ProgressBarVisibilty");
                });

            }
        }

        /// <summary>
        /// To set selected country
        /// </summary>
        private string _selectedCountry;
        [DataMember]
        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                if (value != _selectedCountry)
                {
                    _selectedCountry = value;
                    OnPropertyChanged("SelectedCountry");
                }
            }
        }

        /// <summary>
        /// set picker index
        /// </summary>
        private int pickerSelectedIndex = 0;
        [DataMember]
        public int PickerSelectedIndex
        {
            get
            {
                return this.pickerSelectedIndex;
            }
            set
            {
                this.pickerSelectedIndex = value; OnPropertyChanged("PickerSelectedIndex"); 
            }
        }

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
        /// Property to show and hide popup 
        /// </summary>
        private ObservableCollection<string> _listitems;
        [DataMember]
        public ObservableCollection<string> Listitems
        {
            get { return _listitems; }
            set { _listitems = value; OnPropertyChanged("Listitems"); this.PickerSelectedIndex = 0; }
        }

        /// <summary>
        /// Property to show and hide popup 
        /// </summary>
        private bool _isPopupOpen;
        [DataMember]
        public bool IsPopupOpen
        {
            get { return _isPopupOpen; }
            set { _isPopupOpen = value; OnPropertyChanged("IsPopupOpen"); }
        }

        /// <summary>
        /// Set popup text
        /// </summary>
        private string _popuptext;
        [DataMember]
        public string PopupText
        {
            get { return _popuptext; }
            set { _popuptext = value; OnPropertyChanged("PopupText"); }
        }

        /// <summary>
        /// Property for OK button of popup 
        /// </summary>
        private RelayCommand _OkCommand;
        [IgnoreDataMember]
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
        /// Property for OK button of successpopup 
        /// </summary>
        private RelayCommand _successokCommand;
         [IgnoreDataMember]
        public RelayCommand SuccessCommand
        {
            get
            {
                if (_successokCommand == null)
                {

                    _successokCommand = new RelayCommand(SuccessOk);
                    _successokCommand.Enabled = true;


                }
                return _successokCommand;
            }
            set
            {
                _successokCommand = value;
            }
        }

        /// <summary>
        /// to set success popup text
        /// </summary>
        private string _successPopupText;
        [DataMember]
        public string SuccessPopupText
        {
            get { return _successPopupText; }
            set { _successPopupText = value; OnPropertyChanged("SuccessPopupText"); }
        }

        /// <summary>
        /// Property to show and hide successpopup 
        /// </summary>
        private bool _isSuccessPopupOpen;
        [DataMember]
        public bool IsSuccessPopupOpen
        {
            get { return _isSuccessPopupOpen; }
            set { _isSuccessPopupOpen = value; OnPropertyChanged("IsSuccessPopupOpen"); }
        }

        /// <summary>
        /// For NHS Validator message
        /// </summary>
        private string _nhsLengthMessage;
        [DataMember]
        public string NHSLengthMessage
        {
            get { return _nhsLengthMessage; }
            set { _nhsLengthMessage = value; OnPropertyChanged("NHSLengthMessage"); }
        }

        /// <summary>
        /// Show the the NHS validator
        /// </summary>
        private Visibility _isNHSLengthValidatorVisible;
        [DataMember]
        public Visibility IsNHSLengthValidatorVisible
        {
            get { return _isNHSLengthValidatorVisible; }
            set { _isNHSLengthValidatorVisible = value; OnPropertyChanged("IsNHSLengthValidatorVisible"); }
        }

        /// <summary>
        /// for NHS
        /// </summary>
        private Thickness _nhsLengthBorder;
        [DataMember]
        public Thickness NHSLengthBorder
        {
            get { return _nhsLengthBorder; }
            set { _nhsLengthBorder = value; OnPropertyChanged("NHSLengthBorder"); }
        }

        /// <summary>
        /// For First Name
        /// </summary>
        private string _firstName;
        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged("FirstName"); }
        }

        /// <summary>
        /// For First Name Validation message
        /// </summary>
        private string _firstNameMessage;
        [DataMember]
        public string FirstNameMessage
        {
            get { return _firstNameMessage; }
            set { _firstNameMessage = value; OnPropertyChanged("FirstNameMessage"); }
        }

        /// <summary>
        /// Show the the First name validator
        /// </summary>
        private Visibility _isFirstNameValidatorVisible;
        [DataMember]
        public Visibility IsFirstNameValidatorVisible
        {
            get { return _isFirstNameValidatorVisible; }
            set { _isFirstNameValidatorVisible = value; OnPropertyChanged("IsFirstNameValidatorVisible"); }
        }

        /// <summary>
        /// Show validation border over first name control
        /// </summary>
        private Thickness _firstnameBorder;
        [DataMember]
        public Thickness FirstnameBorder
        {
            get { return _firstnameBorder; }
            set { _firstnameBorder = value; OnPropertyChanged("FirstnameBorder"); }
        }

        /// <summary>
        /// For Last name
        /// </summary>
        private string _lastName;
        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged("LastName"); }
        }

        /// <summary>
        /// For Last name validation message
        /// </summary>
        private string _lastNameMessage;
        [DataMember]
        public string LastNameMessage
        {
            get { return _lastNameMessage; }
            set { _lastNameMessage = value; OnPropertyChanged("LastNameMessage"); }
        }

        /// <summary>
        /// Show the Last name validator
        /// </summary>
        private Visibility _isLastNameValidatorVisible;
        [DataMember]
        public Visibility IsLastNameValidatorVisible
        {
            get { return _isLastNameValidatorVisible; }
            set { _isLastNameValidatorVisible = value; OnPropertyChanged("IsLastNameValidatorVisible"); }
        }

        /// <summary>
        /// Show validation border over last name control
        /// </summary>
        private Thickness _lastnameBorder;
        [DataMember]
        public Thickness LastnameBorder
        {
            get { return _lastnameBorder; }
            set { _lastnameBorder = value; OnPropertyChanged("LastnameBorder"); }
        }

        /// <summary>
        /// For Address Line1
        /// </summary>
        private string _addressLine1;
        [DataMember]
        public string AddressLine1
        {
            get
            {
                return _addressLine1;
            }
            set
            {
                _addressLine1 = value;
                OnPropertyChanged("AddressLine1");
            }
        }

        /// <summary>
        /// Show the validation message for Address Line1
        /// </summary>
        private string _addressLine1Message;
        [DataMember]
        public string AddressLine1Message
        {
            get { return _addressLine1Message; }
            set { _addressLine1Message = value; OnPropertyChanged("AddressLine1Message"); }
        }

        /// <summary>
        /// Show the Address Line1 validator
        /// </summary>
        private Visibility _isAddressLine1ValidatorVisible;
        [DataMember]
        public Visibility IsAddressLine1ValidatorVisible
        {
            get { return _isAddressLine1ValidatorVisible; }
            set { _isAddressLine1ValidatorVisible = value; OnPropertyChanged("IsAddressLine1ValidatorVisible"); }
        }

        /// <summary>
        /// Show the validation border over Address Line1 control
        /// </summary>
        private Thickness _addresLine1Border;
        [DataMember]
        public Thickness AddresLine1Border
        {
            get { return _addresLine1Border; }
            set { _addresLine1Border = value; OnPropertyChanged("AddresLine1Border"); }
        }

        /// <summary>
        /// For Address Line2
        /// </summary>
        private string _addressLine2;
        [DataMember]
        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; OnPropertyChanged("AddressLine2"); }
        }

        /// <summary>
        /// Show the validation message for Address Line2
        /// </summary>
        private string _addressLine2Message;
        [DataMember]
        public string AddressLine2Message
        {
            get { return _addressLine2Message; }
            set { _addressLine2Message = value; OnPropertyChanged("AddressLine2Message"); }
        }

        /// <summary>
        /// Show the Address Line2 validator
        /// </summary>
        private Visibility _isAddressLine2ValidatorVisible;
        [DataMember]
        public Visibility IsAddressLine2ValidatorVisible
        {
            get { return _isAddressLine2ValidatorVisible; }
            set { _isAddressLine2ValidatorVisible = value; OnPropertyChanged("IsAddressLine2ValidatorVisible"); }
        }

        /// <summary>
        /// Show the validation border over Address Line2 control
        /// </summary>
        private Thickness _addresLine2Border;
        [DataMember]
        public Thickness AddresLine2Border
        {
            get { return _addresLine2Border; }
            set { _addresLine2Border = value; OnPropertyChanged("AddresLine2Border"); }
        }

        /// <summary>
        /// For Address Line3
        /// </summary>
        private string _addressLine3;
        [DataMember]
        public string AddressLine3
        {
            get { return _addressLine3; }
            set { _addressLine3 = value; OnPropertyChanged("AddressLine3"); }
        }

        /// <summary>
        /// Show the validation message for Address Line3
        /// </summary>
        private string _addressLine3Message;
        [DataMember]
        public string AddressLine3Message
        {
            get { return _addressLine3Message; }
            set { _addressLine3Message = value; OnPropertyChanged("AddressLine3Message"); }
        }

        /// <summary>
        /// Show the Address Line3 validator
        /// </summary>
        private Visibility _isAddressLine3ValidatorVisible;
        [DataMember]
        public Visibility IsAddressLine3ValidatorVisible
        {
            get { return _isAddressLine3ValidatorVisible; }
            set { _isAddressLine3ValidatorVisible = value; OnPropertyChanged("IsAddressLine3ValidatorVisible"); }
        }

        /// <summary>
        /// Show the validation border over Address Line3 control
        /// </summary>
        private Thickness _addresLine3Border;
        [DataMember]
        public Thickness AddresLine3Border
        {
            get { return _addresLine3Border; }
            set { _addresLine3Border = value; OnPropertyChanged("AddresLine3Border"); }
        }

        /// <summary>
        /// For Post Code
        /// </summary>
        private string _postCode;
        [DataMember]
        public string PostCode
        {
            get { return _postCode; }
            set { _postCode = value; OnPropertyChanged("PostCode"); }
        }

        /// <summary>
        /// Show the validation message for Post Code
        /// </summary>
        private string _postCodeMessage;
        [DataMember]
        public string PostCodeMessage
        {
            get { return _postCodeMessage; }
            set { _postCodeMessage = value; OnPropertyChanged("PostCodeMessage"); }
        }

        /// <summary>
        /// Show the Post Code validator
        /// </summary>
        private Visibility _isPostCodeValidatorVisible;
        [DataMember]
        public Visibility IsPostCodeValidatorVisible
        {
            get { return _isPostCodeValidatorVisible; }
            set { _isPostCodeValidatorVisible = value; OnPropertyChanged("IsPostCodeValidatorVisible"); }
        }

        /// <summary>
        /// Show the validation border over Post Code control
        /// </summary>
        private Thickness _postCodeBorder;
        [DataMember]
        public Thickness PostCodeBorder
        {
            get { return _postCodeBorder; }
            set { _postCodeBorder = value; OnPropertyChanged("PostCodeBorder"); }
        }

        /// <summary>
        /// For Date of Birth
        /// </summary>
        private DateTime _dob;
        [DataMember]
        public DateTime DOB
        {
            get { return _dob; }
            set { _dob = value; OnPropertyChanged("DOB"); }
        }

        /// <summary>
        /// Show the validation message for DOB
        /// </summary>
        private string _dobMessage;
        [DataMember]
        public string DOBMessage
        {
            get { return _dobMessage; }
            set { _dobMessage = value; OnPropertyChanged("DOBMessage"); }
        }

        /// <summary>
        /// Show the DOB validator
        /// </summary>
        private Visibility _isDOBValidatorVisible;
        [DataMember]
        public Visibility IsDOBValidatorVisible
        {
            get { return _isDOBValidatorVisible; }
            set { _isDOBValidatorVisible = value; OnPropertyChanged("IsDOBValidatorVisible"); }
        }

        /// <summary>
        /// Show the validation border over DOB control
        /// </summary>
        private Thickness _dobBorder;
        [DataMember]
        public Thickness DOBBorder
        {
            get { return _dobBorder; }
            set { _dobBorder = value; OnPropertyChanged("DOBBorder"); }
        }

        /// <summary>
        /// For NHS
        /// </summary>
        private string _nhs;
        [DataMember]
        public string NHS
        {
            get { return _nhs; }
            set { _nhs = value; OnPropertyChanged("NHS"); }
        }

        /// <summary>
        /// For Female 
        /// </summary>
        private bool _isFemaleSelected;
        [DataMember]
        public bool IsFemaleSelected
        {
            get { return _isFemaleSelected; }
            set { _isFemaleSelected = value; OnPropertyChanged("IsFemaleSelected"); }
        }

        /// <summary>
        /// For Male
        /// </summary>
        private bool _isMaleSelected;
        [DataMember]
        public bool IsMaleSelected
        {
            get { return _isMaleSelected; }
            set { _isMaleSelected = value; OnPropertyChanged("IsMaleSelected"); }
        }

        /// <summary>
        /// For Mobile Phone
        /// </summary>
        private string _mobileNo;
        [DataMember]
        public string MobileNo
        {
            get { return _mobileNo; }
            set { _mobileNo = value; OnPropertyChanged("MobileNo"); }
        }

        /// <summary>
        /// Show the validation message for Mobile Phone
        /// </summary>
        private string _mobilePhoneMessage;
        [DataMember]
        public string MobilePhoneMessage
        {
            get { return _mobilePhoneMessage; }
            set { _mobilePhoneMessage = value; OnPropertyChanged("MobilePhoneMessage"); }
        }

        /// <summary>
        /// Show the validator for Mobile Phone
        /// </summary>
        private Visibility _isMobilePhoneValidatorVisible;
        [DataMember]
        public Visibility IsMobilePhoneValidatorVisible
        {
            get { return _isMobilePhoneValidatorVisible; }
            set { _isMobilePhoneValidatorVisible = value; OnPropertyChanged("IsMobilePhoneValidatorVisible"); }
        }

        /// <summary>
        /// Show validation border over Mobile phone control
        /// </summary>
        private Thickness _mobilePhoneBorder;
        [DataMember]
        public Thickness MobilePhoneBorder
        {
            get { return _mobilePhoneBorder; }
            set { _mobilePhoneBorder = value; OnPropertyChanged("MobilePhoneBorder"); }
        }

        /// <summary>
        /// For email id
        /// </summary>
        private string _emailId;
        [DataMember]
        public string EmailId
        {
            get { return _emailId; }
            set { _emailId = value; OnPropertyChanged("EmailId"); }
        }

        /// <summary>
        /// Show the validation message for email
        /// </summary>
        private string _emailMessage;
        [DataMember]
        public string EmailMessage
        {
            get { return _emailMessage; }
            set { _emailMessage = value; OnPropertyChanged("EmailMessage"); }
        }

        /// <summary>
        /// Show the email validator
        /// </summary>
        private Visibility _isEmailValidatorVisible;
        [DataMember]
        public Visibility IsEmailValidatorVisible
        {
            get { return _isEmailValidatorVisible; }
            set { _isEmailValidatorVisible = value; OnPropertyChanged("IsEmailValidatorVisible"); }
        }

        /// <summary>
        /// Show validation border over email control
        /// </summary>
        private Thickness _emailBorder;
        [DataMember]
        public Thickness EmailBorder
        {
            get { return _emailBorder; }
            set { _emailBorder = value; OnPropertyChanged("EmailBorder"); }
        }

        /// <summary>
        /// For confirm email
        /// </summary>
        private string _confirmEmail;
        [DataMember]
        public string ConfirmEmail
        {
            get { return _confirmEmail; }
            set { _confirmEmail = value; OnPropertyChanged("ConfirmEmail"); }
        }

        /// <summary>
        /// Show the validation message for confirm email
        /// </summary>
        private string _emailConfirmMessage;
        [DataMember]
        public string EmailConfirmMessage
        {
            get { return _emailConfirmMessage; }
            set { _emailConfirmMessage = value; OnPropertyChanged("EmailConfirmMessage"); }
        }

        /// <summary>
        /// Show the confirm email validator
        /// </summary>
        private Visibility _isEmailConfirmValidatorVisible;
        [DataMember]
        public Visibility IsEmailConfirmValidatorVisible
        {
            get { return _isEmailConfirmValidatorVisible; }
            set { _isEmailConfirmValidatorVisible = value; OnPropertyChanged("IsEmailConfirmValidatorVisible"); }
        }

        /// <summary>
        /// Show validation border over confirm email control
        /// </summary>
        private Thickness _confirmEmailBorder;
        [DataMember]
        public Thickness ConfirmEmailBorder
        {
            get { return _confirmEmailBorder; }
            set { _confirmEmailBorder = value; OnPropertyChanged("ConfirmEmailBorder"); }
        }

        /// <summary>
        /// Select sms option
        /// </summary>
        private bool _isSmsSelected;
        [DataMember]
        public bool IsSmsSelected
        {
            get { return _isSmsSelected; }
            set { _isSmsSelected = value; OnPropertyChanged("IsSmsSelected"); }
        }

        /// <summary>
        /// Select mail option
        /// </summary>
        private bool _isMailSelected;
        [DataMember]
        public bool IsMailSelected
        {
            get { return _isMailSelected; }
            set { _isMailSelected = value; OnPropertyChanged("IsMailSelected"); }
        }

        /// <summary>
        /// Select/Unselect Terms and Conditions
        /// </summary>
        private bool _checkedorNot;
        [DataMember]
        public bool CheckedorNot
        {
            get { return _checkedorNot; }
            set { _checkedorNot = value; OnPropertyChanged("CheckedorNot"); }
        }

        /// <summary>
        /// Show Terms and Conditions validation message
        /// </summary>
        private string _tcMessage;
        [DataMember]
        public string TCMessage
        {
            get { return _tcMessage; }
            set { _tcMessage = value; OnPropertyChanged("TCMessage"); }
        }

        /// <summary>
        /// Show TC validator
        /// </summary>
        private Visibility _isTCValidatorVisible;
        [DataMember]
        public Visibility IsTCValidatorVisible
        {
            get { return _isTCValidatorVisible; }
            set { _isTCValidatorVisible = value; OnPropertyChanged("IsTCValidatorVisible"); }
        }

        /// <summary>
        /// Show validation border over TC control
        /// </summary>
        private Thickness _tcBorder;
        [DataMember]
        public Thickness TCBorder
        {
            get { return _tcBorder; }
            set { _tcBorder = value; OnPropertyChanged("TCBorder"); }
        }

        /// <summary>
        /// For Select Surgery
        /// </summary>
        private string _buttonValue;
        [DataMember]
        public string ButtonValue
        {
            get { return _buttonValue; }
            set { _buttonValue = value; OnPropertyChanged("ButtonValue"); }
        }

        /// <summary>
        /// Property to set Navigation Page
        /// </summary>
        private RelayCommand _navigateTo;
         [IgnoreDataMember]
        public RelayCommand NavigateTo
        {
            get
            {
                if (_navigateTo == null)
                {
                    _navigateTo = new RelayCommand(NavigateToTermsandCondition);
                    _navigateTo.Enabled = true;
                }

                return _navigateTo;
            }
            set { _navigateTo = value; }
        }

        /// <summary>
        /// Property to set Navigation Page
        /// </summary>
        private RelayCommand _saveTo;
         [IgnoreDataMember]
        public RelayCommand SaveTo
        {
            get
            {
                if (_saveTo == null)
                {
                    _saveTo = new RelayCommand(SaveContent);
                    _saveTo.Enabled = true;
                }
                return _saveTo;
            }
            set { _saveTo = value; }
        }

        
        /// <summary>
        /// Property to set Navigation Page
        /// </summary>
        private RelayCommand _surgeryPage;
         [IgnoreDataMember]
        public RelayCommand SurgeryPage
        {
            get
            {
                if (_surgeryPage == null)
                {
                    _surgeryPage = new RelayCommand(NavigateToSurgeryPage);
                    _surgeryPage.Enabled = true;
                }
                return _surgeryPage;
            }
            set { _surgeryPage = value; }
        }  
        #endregion

        #region Methods
        /// <summary>
        /// Method to populate countries
        /// </summary>
        private void PopulateCountry()
        {
            ObservableCollection<string> countries = new ObservableCollection<string>();
            countries.Add("Please Select a Country *");
            countries.Add("England");
            countries.Add("Wales");
            countries.Add("Scotland");
            countries.Add("Northern Ireland");
            Listitems = countries;
        }

        /// <summary>
        /// Click ok button of Popup
        /// </summary>
        private void PopupOk()
        {
            IsPopupOpen = false;
            HitVisibility = true;
        }

        /// <summary>
        /// Method to Navigate to Surgery Page
        /// </summary>
        private void NavigateToSurgeryPage()
        {
            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            bool success = false;
            if (Utilities.IsConnectedToNetwork())
            {
                App.IsNavigatedFromYourDetailsLogin = true;
                App.IsNavigatedFromYourDetailsLoginwithTC = false;
                App.IsNavigatedFromYourDetailsUpdate = false;
                success = frame.Navigate(new Uri(PageURL.navigateToSelectSurgeryURL, UriKind.Relative));
            }
            else
            {
                IsPopupOpen = true;
                PopupText = "No connectivity.";
                HitVisibility = false;
            }
        }

        /// <summary>
        /// Hide  All the Validators when page loads
        /// </summary>
        private void HideAllValidators()
        {
            IsFirstNameValidatorVisible = Visibility.Collapsed;
            IsLastNameValidatorVisible = Visibility.Collapsed;
            IsAddressLine1ValidatorVisible = Visibility.Collapsed;
            IsAddressLine2ValidatorVisible = Visibility.Collapsed;
            IsAddressLine3ValidatorVisible = Visibility.Collapsed;
            IsPostCodeValidatorVisible = Visibility.Collapsed;
            IsDOBValidatorVisible = Visibility.Collapsed;
            IsMobilePhoneValidatorVisible = Visibility.Collapsed;
            IsEmailValidatorVisible = Visibility.Collapsed;
            IsEmailConfirmValidatorVisible = Visibility.Collapsed;
            IsTCValidatorVisible = Visibility.Collapsed;
            IsNHSLengthValidatorVisible = Visibility.Collapsed;
        }

        /// <summary>
        /// navigate on success popup ok
        /// </summary>
        private void SuccessOk()
        {
            IsSuccessPopupOpen = false;
            if (Utilities.IsConnectedToNetwork())
            {
                PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                if (IsMailSelected)
                {
                    frame.Navigate(new Uri(PageURL.navigateToVerificationByEmailURL, UriKind.Relative));
                }
                else
                {
                    frame.Navigate(new Uri(PageURL.navigateToVerificationBySMSURL, UriKind.Relative));
                }
            }
            else
            {
                IsPopupOpen = true;
                PopupText = "No connectivity.";
                HitVisibility = false;
            }
        }

        /// <summary>
        /// Method to Navigate to Terms and Condition Page
        /// </summary>
        private void NavigateToTermsandCondition()
        {
            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;          

            if (Utilities.IsConnectedToNetwork())
            {
                frame.Navigate(new Uri(PageURL.navigateToTermsandConditionURL, UriKind.Relative));
            }
            else
            {
                IsPopupOpen = true;
                PopupText = "No connectivity.";
                HitVisibility = false;
            }
        }

        /// <summary>
        /// Save the content
        /// </summary>
        private void SaveContent()
        {
            
            bool IsDataValid = false;
            IsDataValid = IsValidData();
            if (Utilities.IsConnectedToNetwork())
            {
                if (IsDataValid)
                {
                    HitVisibility = false;
                    objydloginModel = new YourDetailsLoginModel(this);
                }
            }
            else
            {
                IsPopupOpen = true;
                PopupText = "No connectivity.";
                HitVisibility = false;
            }
        }

        #region Validations
       
        /// <summary>
        /// Validate data controls
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            bool FirstNameHasValue;
            bool LastNameHasValue;
            bool AddressLine1HasValue;
            bool AddressLine2HasValue;
            bool AddressLine3HasValue;
            bool PostCodeHasValue;
            bool DOBHasValue;
            bool MobileNoHasValue;
            bool EmailHasValue;
            bool EmailConfirmHasValue;
            bool TCHasValue;
            bool NHSHasValue;
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName))
            {
                IsFirstNameValidatorVisible = Visibility.Visible;
                FirstnameBorder = new Thickness(1.5);
                FirstNameMessage = "First name required.";
                FirstNameHasValue = false;
            }
            else
            {

                bool isAlphabetsOnly = Regex.IsMatch(FirstName, "^[A-Za-z]*$", RegexOptions.IgnorePatternWhitespace);
                if (isAlphabetsOnly)
                {
                    IsFirstNameValidatorVisible = Visibility.Collapsed;
                    FirstnameBorder = new Thickness(0);
                    FirstNameHasValue = true;
                }
                else
                {
                    IsFirstNameValidatorVisible = Visibility.Visible;
                    FirstnameBorder = new Thickness(1.5);
                    FirstNameMessage = "Only alphabets allowed.";
                    FirstNameHasValue = false;
                }
            }

            if (string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName))
            {
                IsLastNameValidatorVisible = Visibility.Visible;
                LastnameBorder = new Thickness(1.5);
                LastNameMessage = "Last name required.";
                LastNameHasValue = false;
            }
            else
            {
                bool isAlphabetsOnly = Regex.IsMatch(LastName, "^[A-Za-z]*$", RegexOptions.IgnorePatternWhitespace);
                if (isAlphabetsOnly)
                {
                    IsLastNameValidatorVisible = Visibility.Collapsed;
                    LastnameBorder = new Thickness(0);
                    LastNameHasValue = true;
                }
                else
                {
                    IsLastNameValidatorVisible = Visibility.Visible;
                    LastnameBorder = new Thickness(1.5);
                    LastNameMessage = "Only alphabets allowed.";
                    LastNameHasValue = false;
                }
            }
            if (string.IsNullOrEmpty(AddressLine1) || string.IsNullOrWhiteSpace(AddressLine1))
            {
                IsAddressLine1ValidatorVisible = Visibility.Visible;
                AddresLine1Border = new Thickness(1.5);
                AddressLine1Message = "Address Line 1 required.";
                AddressLine1HasValue = false;
            }
            else
            {
                IsAddressLine1ValidatorVisible = Visibility.Collapsed;
                AddresLine1Border = new Thickness(0);
                AddressLine1HasValue = true;
            }

            if (string.IsNullOrEmpty(AddressLine2) || string.IsNullOrWhiteSpace(AddressLine2))
            {
                IsAddressLine2ValidatorVisible = Visibility.Visible;
                AddresLine2Border = new Thickness(1.5);
                AddressLine2Message = "Address Line 2 required.";
                AddressLine2HasValue = false;
            }
            else
            {
                IsAddressLine2ValidatorVisible = Visibility.Collapsed;
                AddresLine2Border = new Thickness(0);
                AddressLine2HasValue = true;
            }


            if (NHS != null) 
            {
              if (NHS.Length == 0 || NHS.Length == 10 || (string.IsNullOrEmpty(NHS)))
                {
                    IsNHSLengthValidatorVisible = Visibility.Collapsed;
                    NHSLengthBorder = new Thickness(0);
                    NHSHasValue = true;
                }
                else
                {
                    IsNHSLengthValidatorVisible = Visibility.Visible;
                    NHSLengthBorder = new Thickness(1.5);
                    NHSLengthMessage = "Should be either zero or ten digits.";
                    NHSHasValue = false;
                }
            }
            else
            {
                NHSHasValue = true;
            }
        
            if (PickerSelectedIndex==0)
            {
                IsAddressLine3ValidatorVisible = Visibility.Visible;
                AddresLine3Border = new Thickness(1.5);
                AddressLine3Message = "Please select a country.";
                AddressLine3HasValue = false;
            }
            else
            {
                IsAddressLine3ValidatorVisible = Visibility.Collapsed;
                AddresLine3Border = new Thickness(0);
                AddressLine3HasValue = true;
            }
            if (string.IsNullOrEmpty(PostCode) || string.IsNullOrWhiteSpace(PostCode))
            {
                IsPostCodeValidatorVisible = Visibility.Visible;
                PostCodeBorder = new Thickness(1.5);
                PostCodeMessage = "Post Code required.";
                PostCodeHasValue = false;
            }
            else
            {
                bool isPostCodeValid = Regex.IsMatch(PostCode, @"((([A-PR-UWYZ][0-9][0-9]?)|(([A-PR-UWYZ][A-HK-Y][0-9][0-9]?)|(([A-PR-UWYZ][0-9][A-HJKSTUW])|([A-PR-UWYZ][A-HK-Y][0-9][ABEHMNPRV-Y])))) ?[0-9][ABD-HJLNP-UW-Z]{2})", RegexOptions.IgnoreCase);
                if (isPostCodeValid)
                {
                    IsPostCodeValidatorVisible = Visibility.Collapsed;
                    PostCodeBorder = new Thickness(0);
                    PostCodeHasValue = true;
                }
                else
                {
                    IsPostCodeValidatorVisible = Visibility.Visible;
                    PostCodeBorder = new Thickness(1.5);
                    PostCodeMessage = "Invalid Post Code.";
                    PostCodeHasValue = false;
                }
            }
            if (string.IsNullOrEmpty(DOB.ToString("dd/mm/yy")) || string.IsNullOrWhiteSpace(DOB.ToString("dd/mm/yy")))
            {
                IsDOBValidatorVisible = Visibility.Visible;
                DOBBorder = new Thickness(1.5);
                DOBMessage = "Date of birth required.";
                DOBHasValue = false;
            }
            else
            {
                if (DateTime.Compare(System.DateTime.Now.Date, DOB.Date)<=0)
                {
                    IsDOBValidatorVisible = Visibility.Visible;
                    DOBBorder = new Thickness(1.5);
                    DOBMessage = "Date of birth should be less than the current date.";
                    DOBHasValue = false;
                }
                else
                {
                    IsDOBValidatorVisible = Visibility.Collapsed;
                    DOBBorder = new Thickness(0);
                    DOBHasValue = true;
                }
               
            }
            if (string.IsNullOrEmpty(MobileNo) || string.IsNullOrWhiteSpace(MobileNo))
            {
                IsMobilePhoneValidatorVisible = Visibility.Visible;
                MobilePhoneBorder = new Thickness(1.5);
                MobilePhoneMessage = "Mobile Number required.";
                MobileNoHasValue = false;
            }
            else
            {
                bool isDigitsOnly = Regex.IsMatch(MobileNo, "^[0-9]*$", RegexOptions.IgnorePatternWhitespace);
                if (isDigitsOnly)
                {
                    IsMobilePhoneValidatorVisible = Visibility.Collapsed;
                    MobilePhoneBorder = new Thickness(0);
                    MobileNoHasValue = true;
                }
                else
                {
                    IsMobilePhoneValidatorVisible = Visibility.Visible;
                    MobilePhoneBorder = new Thickness(1.5);
                    MobilePhoneMessage = "Only digits allowed.";
                    MobileNoHasValue = false;
                }             
            }
            if (string.IsNullOrEmpty(EmailId) || string.IsNullOrWhiteSpace(EmailId))
            {
                IsEmailValidatorVisible = Visibility.Visible;
                EmailBorder = new Thickness(1.5);
                EmailMessage = "Email ID required.";
                EmailHasValue = false;
            }
            else
            {
                bool isEmail = Regex.IsMatch(EmailId, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    IsEmailValidatorVisible = Visibility.Visible;
                    EmailBorder = new Thickness(1.5);
                    EmailMessage = "Email ID format is incorrect.";
                    EmailHasValue = false;
                }
                else
                {
                    IsEmailValidatorVisible = Visibility.Collapsed;
                    EmailBorder = new Thickness(0);
                    EmailHasValue = true;
                }
            }
            
            if (string.IsNullOrEmpty(ConfirmEmail) || string.IsNullOrWhiteSpace(ConfirmEmail))
            {
                IsEmailConfirmValidatorVisible = Visibility.Visible;
                ConfirmEmailBorder = new Thickness(1.5);
                EmailConfirmMessage = "Confirm Email ID required.";
                EmailConfirmHasValue = false;
            }
            else
            {
                bool isEmail = Regex.IsMatch(ConfirmEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    IsEmailConfirmValidatorVisible = Visibility.Visible;
                    ConfirmEmailBorder = new Thickness(1.5);
                    EmailConfirmMessage = "Confirm Email ID format is incorrect.";
                    EmailConfirmHasValue = false;
                }
                else
                {
                    if (ConfirmEmail != EmailId)
                    {
                        IsEmailConfirmValidatorVisible = Visibility.Visible;
                        ConfirmEmailBorder = new Thickness(1.5);
                        EmailConfirmMessage = "Email IDs do not match.";
                        EmailConfirmHasValue = false;
                    }
                    else
                    {
                        IsEmailConfirmValidatorVisible = Visibility.Collapsed;
                        ConfirmEmailBorder = new Thickness(0);
                        EmailConfirmHasValue = true;
                    }                 
                }
             
            }

            if (!CheckedorNot)
            {
                IsTCValidatorVisible = Visibility.Visible;
                TCBorder = new Thickness(1.5);
                TCMessage = "Please read and accept terms and conditions.";
                TCHasValue = false;
            }
            else
            {
                IsTCValidatorVisible = Visibility.Collapsed;
                TCBorder = new Thickness(0);
                TCHasValue = true;
            }

            if ((FirstNameHasValue) && (LastNameHasValue) && (AddressLine1HasValue) && (NHSHasValue)&&(AddressLine2HasValue) && (AddressLine3HasValue) && (PostCodeHasValue)
                && (DOBHasValue) && (MobileNoHasValue) && (EmailHasValue) && (EmailConfirmHasValue)
                && (TCHasValue))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please fill all the mandatory fields.");
                return false;
            }
        }

        #endregion

        #endregion
    }
}
