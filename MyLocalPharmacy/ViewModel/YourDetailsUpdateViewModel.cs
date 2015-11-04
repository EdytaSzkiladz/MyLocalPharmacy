using Microsoft.Phone.Controls;
using MyLocalPharmacy.Common;
using System;
using System.Windows;
using MyLocalPharmacy.Utils;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Entities;
using System.Runtime.Serialization;

namespace MyLocalPharmacy.ViewModel
{
    [DataContract]
    public class YourDetailsUpdateViewModel : BaseViewModel
    {
        #region Declarations
        public YourDetailsUpdateModel objYourDetUpdateModel;
        ObservableCollection<string> countries = new ObservableCollection<string>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loginResponse"></param>
        public YourDetailsUpdateViewModel(LoginResponse loginResponse)
        {
            _isFemaleSelected = true;
            PopulateCountry();
            HitVisibility = true;
            GetUserDetails();
            IsSuccessUpdatePopupOpen = false;
            IsPopupOpen = false;
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
            if (App.ObjLgResponse != null)
            {
                NominationStatus = App.ObjLgResponse.payload.status + " in " + App.ObjLgResponse.payload.pharmacyid;
                string fullname = App.ObjLgResponse.payload.name;
                string[] splitfullname = fullname.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                FirstName = splitfullname[0];
                LastName = splitfullname[1];
                AddressLine1 = App.ObjLgResponse.payload.address1;
                AddressLine2 = App.ObjLgResponse.payload.address2;
                if (countries != null)
                {
                    int position = countries.IndexOf(App.ObjLgResponse.payload.country);
                    PickerSelectedIndex = position;
                }
                PostCode = App.ObjLgResponse.payload.postcode;
                DOB = App.ObjLgResponse.payload.birthdate;
                NHS = App.ObjLgResponse.payload.nhs;
                MobileNo = App.ObjLgResponse.payload.phone;
                EmailId = App.ObjLgResponse.payload.mail;
                ButtonValueOnupdate = App.ObjLgResponse.payload.surgery.name;

                if (string.IsNullOrEmpty(ButtonValueOnupdate) || string.IsNullOrWhiteSpace(ButtonValueOnupdate))
                {
                    ButtonValueOnupdate = "Choose Doctor for surgery (Optional)";
                    App.SurgeonAddress = string.Empty;
                }
                else
                {
                    App.SurgeonAddress = App.ObjLgResponse.payload.surgery.address;
                }
                App.SurgeonSaved = ButtonValueOnupdate;

                if (App.ObjLgResponse.payload.verifyby == "mail")
                {

                }

                if (App.ObjLgResponse.payload.sex == 1)
                {
                    IsMaleSelected = true;
                    SelectedForegroundColor = "Black";
                    SelectedBackgroundColor = "White";
                }
                else
                {
                    IsFemaleSelected = true;
                    SelectedForegroundColor = "Black";
                    SelectedBackgroundColor = "White";
                }
            }

        }
        #endregion
     
        #region Properties
       /// <summary>
       /// for radio button
       /// </summary>
        private string _selectedbackgroundColor;
        [DataMember]
        public string SelectedBackgroundColor
        {
            get { return _selectedbackgroundColor; }
            set { _selectedbackgroundColor = value; OnPropertyChanged("SelectedBackgroundColor"); }
        }
        /// <summary>
        /// For Radio button
        /// </summary>
        private string _selectedforegroundColor;
        [DataMember]
        public string SelectedForegroundColor
        {
            get { return _selectedforegroundColor; }
            set { _selectedforegroundColor = value; OnPropertyChanged("SelectedForegroundColor"); }
        }
        /// <summary>
        /// Set Nomination Status
        /// </summary>
        private string _nominationStatus;
        [DataMember]
        public string NominationStatus
        {
            get { return _nominationStatus; }
            set { _nominationStatus = value; OnPropertyChanged("NominationStatus"); }
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
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    _progressBarVisibilty = value;
                    OnPropertyChanged("ProgressBarVisibilty");
                });

            }
        }
        /// <summary>
        /// set selected country
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
        /// set the selected index
        /// </summary>
        private int _pickerSelectedIndex = 0;
        [DataMember]
        public int PickerSelectedIndex
        {
            get
            {
                return this._pickerSelectedIndex;
            }
            set
            {
                this._pickerSelectedIndex = value; OnPropertyChanged("PickerSelectedIndex");
            }
        }
        /// <summary>
        /// Property to populate country
        /// </summary>
        private ObservableCollection<string> _listitems;
        [DataMember]
        public ObservableCollection<string> Listitems
        {
            get { return _listitems; }
            set { _listitems = value; OnPropertyChanged("Listitems"); this.PickerSelectedIndex = 0; }
        }
        /// <summary>
        /// For Primary Color
        /// </summary>
        private SolidColorBrush _fontColor;
        [IgnoreDataMember]
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
        [IgnoreDataMember]
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
        [IgnoreDataMember]
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
        /// For Splash url
        /// </summary>
        private string _splashurl;
        [DataMember]
        public string SplashUrl
        {
            get { return _splashurl; }
            set
            {
                _splashurl = value;
                OnPropertyChanged("SplashUrl");
            }
        }
        /// <summary>
        /// For First name
        /// </summary>
        private string _firstName;
        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged("FirstName"); }
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
        /// Show Address Line1 message
        /// </summary>
        private string _addressLine1Message;
        [DataMember]
        public string AddressLine1Message
        {
            get { return _addressLine1Message; }
            set { _addressLine1Message = value; OnPropertyChanged("AddressLine1Message"); }
        }
        /// <summary>
        /// Show Address  Line1 validator
        /// </summary>
        private Visibility _isAddressLine1ValidatorVisible;
        [DataMember]
        public Visibility IsAddressLine1ValidatorVisible
        {
            get { return _isAddressLine1ValidatorVisible; }
            set { _isAddressLine1ValidatorVisible = value; OnPropertyChanged("IsAddressLine1ValidatorVisible"); }
        }
        /// <summary>
        /// Show validation border over Address Line1 control
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
        /// Show Address Line2 message
        /// </summary>
        private string _addressLine2Message;
        [DataMember]
        public string AddressLine2Message
        {
            get { return _addressLine2Message; }
            set { _addressLine2Message = value; OnPropertyChanged("AddressLine2Message"); }
        }
        /// <summary>
        /// Show Address  Line2 validator
        /// </summary>
        private Visibility _isAddressLine2ValidatorVisible;
        [DataMember]
        public Visibility IsAddressLine2ValidatorVisible
        {
            get { return _isAddressLine2ValidatorVisible; }
            set { _isAddressLine2ValidatorVisible = value; OnPropertyChanged("IsAddressLine2ValidatorVisible"); }
        }
        /// <summary>
        /// Show validation border over Address Line2 control
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
        /// Show Address Line3 message
        /// </summary>
        private string _addressLine3Message;
        [DataMember]
        public string AddressLine3Message
        {
            get { return _addressLine3Message; }
            set { _addressLine3Message = value; OnPropertyChanged("AddressLine3Message"); }
        }
        /// <summary>
        /// Show Address  Line3 validator
        /// </summary>
        private Visibility _isAddressLine3ValidatorVisible;
        [DataMember]
        public Visibility IsAddressLine3ValidatorVisible
        {
            get { return _isAddressLine3ValidatorVisible; }
            set { _isAddressLine3ValidatorVisible = value; OnPropertyChanged("IsAddressLine3ValidatorVisible"); }
        }
        /// <summary>
        /// Show validation border over Address Line3 control
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
        /// For Post Code validation message
        /// </summary>
        private string _postCodeMessage;
        [DataMember]
        public string PostCodeMessage
        {
            get { return _postCodeMessage; }
            set { _postCodeMessage = value; OnPropertyChanged("PostCodeMessage"); }
        }
        /// <summary>
        /// Show Post code validator
        /// </summary>
        private Visibility _isPostCodeValidatorVisible;
        [DataMember]
        public Visibility IsPostCodeValidatorVisible
        {
            get { return _isPostCodeValidatorVisible; }
            set { _isPostCodeValidatorVisible = value; OnPropertyChanged("IsPostCodeValidatorVisible"); }
        }
        /// <summary>
        /// Show validation border over post code control
        /// </summary>
        private Thickness _postCodeBorder;
        [DataMember]
        public Thickness PostCodeBorder
        {
            get { return _postCodeBorder; }
            set { _postCodeBorder = value; OnPropertyChanged("PostCodeBorder"); }
        }
        /// <summary>
        /// For DOB
        /// </summary>
        private string _dob;
        [DataMember]
        public string DOB
        {
            get { return _dob; }
            set { _dob = value; OnPropertyChanged("DOB"); }
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
        /// For Female select option
        /// </summary>
        private bool _isFemaleSelected;
        [DataMember]
         public bool IsFemaleSelected
        {
            get { return _isFemaleSelected; }
            set { _isFemaleSelected = value; OnPropertyChanged("IsFemaleSelected"); }
        }
        /// <summary>
        /// For Male select option
        /// </summary>
        private bool _isMaleSelected;
        [DataMember]
        public bool IsMaleSelected
        {
            get { return _isMaleSelected; }
            set { _isMaleSelected = value; OnPropertyChanged("IsMaleSelected"); }
        }
        /// <summary>
        /// For Mobile No
        /// </summary>
        private string _mobileNo;
        [DataMember]
        public string MobileNo
        {
            get { return _mobileNo; }
            set { _mobileNo = value; OnPropertyChanged("MobileNo"); }
        }
        /// <summary>
        /// Show validation message for mobile Phone control
        /// </summary>
        private string _mobilePhoneMessage;
        [DataMember]
        public string MobilePhoneMessage
        {
            get { return _mobilePhoneMessage; }
            set { _mobilePhoneMessage = value; OnPropertyChanged("MobilePhoneMessage"); }
        }
        /// <summary>
        /// Show the Mobile Phone validator
        /// </summary>
        private Visibility _isMobilePhoneValidatorVisible;
        [DataMember]
        public Visibility IsMobilePhoneValidatorVisible
        {
            get { return _isMobilePhoneValidatorVisible; }
            set { _isMobilePhoneValidatorVisible = value; OnPropertyChanged("IsMobilePhoneValidatorVisible"); }
        }
        /// <summary>
        /// Show validation border over Mobile Phone control
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
        /// For Select Surgery
        /// </summary>
        private string _buttonValueOnupdate;
        [DataMember]
        public string ButtonValueOnupdate
        {
            get { return _buttonValueOnupdate; }
            set { _buttonValueOnupdate = value; OnPropertyChanged("ButtonValueOnupdate"); }
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
        /// <summary>
        /// Property to set the Update Command
        /// </summary>
        private RelayCommand _updateChanges;
        [IgnoreDataMember]
        public RelayCommand UpdateChanges
        {
            get
            {
                if (_updateChanges == null)
                {
                    _updateChanges = new RelayCommand(SaveChanges);
                    _updateChanges.Enabled = true;
                }

                return _updateChanges;
            }
            set { _updateChanges = value; }
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
        /// Property to show and hide successpopup 
        /// </summary>
        private bool _isSuccessUpdatePopupOpen;
        [DataMember]
        public bool IsSuccessUpdatePopupOpen
        {
            get { return _isSuccessUpdatePopupOpen; }
            set { _isSuccessUpdatePopupOpen = value; OnPropertyChanged("IsSuccessUpdatePopupOpen"); }
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
        /// Set successpopup text
        /// </summary>
        private string _successPopupText;
        [DataMember]
        public string SuccessPopupText
        {
            get { return _successPopupText; }
            set { _successPopupText = value; OnPropertyChanged("SuccessPopupText"); }
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
        #endregion

        #region Methods
        /// <summary>
        /// Method to get the user details for update
        /// </summary>
        private void GetUserDetails()
        {
            objYourDetUpdateModel = new YourDetailsUpdateModel(this, "GetData");
        }

        /// <summary>
        /// Method to populate countries
        /// </summary>
        private void PopulateCountry()
        {
            countries.Add("Please Select a Country *");
            countries.Add("England");
            countries.Add("Wales");
            countries.Add("Scotland");
            countries.Add("Northern Ireland");
            Listitems = countries;
        }
        /// <summary>
        /// Click ok button of success popup
        /// </summary>
        private void SuccessOk()
        {
            
            
                IsSuccessUpdatePopupOpen = false;
                HitVisibility = true;
                INavigationService navigationService = this.GetService<INavigationService>();
                if (App.IsFromRejected)
                {
                    App.IsFromRejected = false;
                    navigationService.Navigate(PageURL.navigateToHomePanoramaURL);
                }
                else
                {
                    navigationService.Navigate(PageURL.navigateToSettingPageURL);
                }
                
           
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
            if (Utilities.IsConnectedToNetwork())
            {
                App.IsNavigatedFromYourDetailsUpdate = true;
                App.IsNavigatedFromYourDetailsLoginwithTC = false;
                App.IsNavigatedFromYourDetailsLogin = false;
                INavigationService navigationService = this.GetService<INavigationService>();
                navigationService.Navigate(PageURL.navigateToSelectSurgeryURL);
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

            IsAddressLine1ValidatorVisible = Visibility.Collapsed;
            IsAddressLine2ValidatorVisible = Visibility.Collapsed;
            IsAddressLine3ValidatorVisible = Visibility.Collapsed;
            IsPostCodeValidatorVisible = Visibility.Collapsed;

            IsMobilePhoneValidatorVisible = Visibility.Collapsed;

        }
        /// <summary>
        /// Save the data
        /// </summary>
        private void SaveChanges()
        {
            
            bool IsDataValid = false;
            IsDataValid = IsValidData();

            if (Utilities.IsConnectedToNetwork())
            {
                if (IsDataValid)
                {
                    HitVisibility = false;
                    IsSuccessUpdatePopupOpen = false;
                    IsPopupOpen = false;
                    objYourDetUpdateModel = new YourDetailsUpdateModel(this,"UpdateData");
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
        /// Validate Controls
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            bool AddressLine1HasValue;
            bool AddressLine2HasValue;
            bool AddressLine3HasValue;
            bool PostCodeHasValue;
            bool MobileNoHasValue;           
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
            if (PickerSelectedIndex == 0)
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
           

            if ((AddressLine1HasValue) && (AddressLine2HasValue) && (AddressLine3HasValue) && (PostCodeHasValue) && (MobileNoHasValue))
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

    }
}
