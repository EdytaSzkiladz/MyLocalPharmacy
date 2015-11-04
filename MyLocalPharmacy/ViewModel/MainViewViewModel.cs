using System;
using Microsoft.Expression.Interactivity.Core;
using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Entities;
using System.Linq;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using SQLite;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Windows.Navigation;
using System.Device.Location;
using Microsoft.Phone.Controls;


namespace MyLocalPharmacy.ViewModel
{
    public class MainViewViewModel :BaseViewModel
    {
        #region Declarations
        GetAllOrdersModel getAllOrdersModel;
        ConditionLeafletsModel ConditionLeafletsModel;
        HomePanoramaPharmacyDetailsModel objPharmacyDetails;
        AccountDisabledViewModel accountDisabledViewModel;
        List<int> IdsToDelete = new List<int>();
        #endregion
        #region Constructor
        public MainViewViewModel()
        {
            if (App.ObjBrandingResponse != null)
            {
                AppBarPrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;
                PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);
                SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
                FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
                PharmacyBranchName = App.ObjBrandingResponse.payload.branding_data.branch_name;
                PharmacyName = App.ObjBrandingResponse.payload.branding_data.pharmacy_name;
            }
            else
            {
                AppBarPrimaryColour = RxConstants.PrimaryColourCodeAppbar;
                PrimaryColour = SecondaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);
                FontColor = Utilities.GetColorFromHexa(RxConstants.FontColourCode);
            }

            App.IsVerifiedByEmail = false;
            App.IsVerifiedBySms = false;

            HitVisibility = true;
            PharmacyDetailsWebServiceCall();
            CallWebServiceforconditionLeaflets();
            _leafletsCollection = null;

            CallLeafletCommand();
            DisplayGetAllOrders();




            CallSettingsCommand();

            AddOrderCommand = new ActionCommand(
                   () =>
                   {
                       if (StplWaitingApprovalVisibility == Visibility.Visible)
                       {
                           MessageBox.Show("Your account is waiting for approval by the pharmacy. This process may take upto 24 hours.");
                       }
                       else
                       {
                           if (App.prescriptionCollection != null)
                           {
                               App.prescriptionCollection.Clear();
                           }
                           INavigationService navigationService = this.GetService<INavigationService>();
                           navigationService.Navigate(PageURL.navigateToNewRepeatSummaryURL);

                       }

                   });
            RefreshCommand = new ActionCommand(
                   () =>
                   {
                       if (StplWaitingApprovalVisibility == Visibility.Visible)
                       {
                           MessageBox.Show("Your account is waiting for approval by the pharmacy. This process may take upto 24 hours.");
                       }
                       else
                       {
                           ProgressBarVisibiltyOrderRepeat = Visibility.Visible;
                           DisplayGetAllOrders();
                       }

                   });

            _isFacebookLinkVisible = Visibility.Collapsed;
            _isTwitterLinkVisible = Visibility.Collapsed;
            DisplayPills();

        }

#endregion

        #region CommonProperties
        [IgnoreDataMember]
        public ICommand SearchCommand { get; private set; }
        [IgnoreDataMember]
        public ICommand SampleCommand { get; private set; }
        [IgnoreDataMember]
        public ICommand AddOrderCommand { get; private set; }
        [IgnoreDataMember]
        public ICommand RefreshCommand { get; private set; }
        [IgnoreDataMember]
        public ICommand SettingsCommand { get; private set; }
        [IgnoreDataMember]
        public ICommand RefreshLeafletCommand { get; private set; }


        /// <summary>
        /// For AppBar PrimaryColor
        /// </summary>
        private string _appBarPrimaryColour;
        [DataMember]
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
        /// For Font Color
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
        //
        /// <summary>
        /// For Pharmacy Brand
        /// </summary>
        private string _pharmacyBrandName;
        [DataMember]
        public string PharmacyBrandName
        {
            get { return _pharmacyBrandName; }
            set
            {
                _pharmacyBrandName = value;
                OnPropertyChanged("PharmacyBrandName");
            }
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
                //Deployment.Current.Dispatcher.BeginInvoke(() =>
                //{
                _progressBarVisibilty = value;
                OnPropertyChanged("ProgressBarVisibilty");
                //});

            }
        }

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
        private string _popupRejectedText;

        public string PopupRejectedText
        {
            get { return _popupRejectedText; }
            set { _popupRejectedText = value; OnPropertyChanged("PopupRejectedText"); }
        }

        /// <summary>
        /// Property to show and hide popup 
        /// </summary>
        private bool _isRejectedPopupOpen;

        public bool IsRejectedPopupOpen
        {
            get { return _isRejectedPopupOpen; }
            set { _isRejectedPopupOpen = value; OnPropertyChanged("IsRejectedPopupOpen"); }
        }

        /// <summary>
        /// Property to show and hide popup 
        /// </summary>
        private string _popupText;

        public string PopupText
        {
            get { return _popupText; }
            set { _popupText = value; OnPropertyChanged("PopupText"); }
        }


        #endregion

        #region Settings
        /// <summary>
        /// Method to call Settings Command
        /// </summary>
        private void CallSettingsCommand()
        {
            SettingsCommand = new ActionCommand(
                () =>
                {
                    INavigationService navigationService = this.GetService<INavigationService>();
                    navigationService.Navigate(PageURL.navigateToSettingPageURL);
                });
        }
        #endregion

        #region PharmacyDetailsProperties
        /// <summary>
        /// Property for Open time
        /// </summary>
        private string _opentodaytime;
        [DataMember]
        public string Opentodaytime
        {
            get { return _opentodaytime; }
            set { _opentodaytime = value; OnPropertyChanged("Opentodaytime"); }
        }

        /// <summary>
        /// Property for Open time
        /// </summary>
        private string _phone;
        [DataMember]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged("Phone"); }
        }
        /// <summary>
        /// Property for PharmacyName
        /// </summary>
        private string _pharmacyName;
        [DataMember]
        public string PharmacyName
        {
            get { return _pharmacyName; }
            set { _pharmacyName = value; OnPropertyChanged("PharmacyName"); }
        }
        /// <summary>
        /// Property for PharmacyBranchName
        /// </summary>
        private string _pharmacyBranchName;
        [DataMember]
        public string PharmacyBranchName
        {
            get { return _pharmacyBranchName; }
            set
            {
                _pharmacyBranchName = value; OnPropertyChanged("PharmacyBranchName");
            }
        }
        /// <summary>
        /// Property for AddressLine1
        /// </summary>
        private string _addressLine1;
        [DataMember]
        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; OnPropertyChanged("AddressLine1"); }
        }
        /// <summary>
        /// Property for AddressLine2
        /// </summary>
        private string _addressLine2;
        [DataMember]
        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; OnPropertyChanged("AddressLine2"); }
        }
        /// <summary>
        /// Property for AddressLine3
        /// </summary>
        private string _addressLine3;
        [DataMember]
        public string AddressLine3
        {
            get { return _addressLine3; }
            set { _addressLine3 = value; OnPropertyChanged("AddressLine3"); }
        }
        /// <summary>
        /// Property for PinCode
        /// </summary>
        private string _pinCode;
        [DataMember]
        public string PinCode
        {
            get { return _pinCode; }
            set { _pinCode = value; OnPropertyChanged("PinCode"); }
        }
        /// <summary>
        /// Property for PharmacistName1
        /// </summary>
        private string _pharmacistName1;
        [DataMember]
        public string PharmacistName1
        {
            get { return _pharmacistName1; }
            set { _pharmacistName1 = value; OnPropertyChanged("PharmacistName1"); }
        }

        /// <summary>
        /// Property for PharmacistName2
        /// </summary>
        private string _pharmacistName2;
        [DataMember]
        public string PharmacistName2
        {
            get { return _pharmacistName2; }
            set { _pharmacistName2 = value; OnPropertyChanged("PharmacistName2"); }
        }

        /// <summary>
        /// Property for WebsiteLink
        /// </summary>
        private string _websiteLink;
        [DataMember]
        public string WebsiteLink
        {
            get { return _websiteLink; }
            set { _websiteLink = value; OnPropertyChanged("WebsiteLink"); }
        }

        /// <summary>
        /// Property for TwitterLink
        /// </summary>
        private string _twitterLink;
        [DataMember]
        public string TwitterLink
        {
            get { return _twitterLink; }
            set { _twitterLink = value; OnPropertyChanged("TwitterLink"); }
        }

        /// <summary>
        /// Property for Facebook link
        /// </summary>
        private string _facebookLink;
        [DataMember]
        public string FacebookLink
        {
            get { return _facebookLink; }
            set { _facebookLink = value; OnPropertyChanged("FacebookLink"); }
        }
        /// <summary>
        /// Property for opening hours
        /// </summary>
        private List<OpenHours> _openingHours;
        [DataMember]
        public List<OpenHours> OpeningHours
        {
            get { return _openingHours; }
            set { _openingHours = value; OnPropertyChanged("OpeningHours"); }
        }
        /// <summary>
        /// Property for Weblink visibility
        /// </summary>
        private Visibility _isWebLinkVisible;
        [DataMember]
        public Visibility IsWebLinkVisible
        {
            get { return _isWebLinkVisible; }
            set { _isWebLinkVisible = value; OnPropertyChanged("IsWebLinkVisible"); }
        }

        /// <summary>
        /// Property for Twitter link visibility
        /// </summary>
        private Visibility _isTwitterLinkVisible = Visibility.Collapsed;
        [DataMember]
        public Visibility IsTwitterLinkVisible
        {
            get { return _isTwitterLinkVisible; }
            set { _isTwitterLinkVisible = value; OnPropertyChanged("IsTwitterLinkVisible"); }
        }

        /// <summary>
        /// Property for Facebook link visibility
        /// </summary>
        private Visibility _isFacebookLinkVisible = Visibility.Collapsed;
        [DataMember]
        public Visibility IsFacebookLinkVisible
        {
            get { return _isFacebookLinkVisible; }
            set { _isFacebookLinkVisible = value; OnPropertyChanged("IsFacebookLinkVisible"); }
        }

        /// <summary>
        /// For advertisement data
        /// </summary>
        private ObservableCollection<AdvertData> _advertisementData;
        [DataMember]
        public ObservableCollection<AdvertData> AdvertisementData
        {
            get { return _advertisementData; }
            set { _advertisementData = value; OnPropertyChanged("AdvertisementData"); }
        }

        /// <summary>
        /// Property for advertisement image
        /// </summary>
        private List<string> _imageUrls;
        [DataMember]
        public List<string> ImageUrls
        {
            get { return _imageUrls; }
            set { _imageUrls = value; OnPropertyChanged("ImageUrls"); }
        }

        private string _imageUrl;
        [DataMember]
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; OnPropertyChanged("ImageUrl"); }
        }

        //WebLinkPress
        /// <summary>
        /// Property for Weblink click
        /// </summary>
        private RelayCommand _webLinkPress;
        [IgnoreDataMember]
        public RelayCommand WebLinkPress
        {
            get
            {
                if (_webLinkPress == null)
                {

                    _webLinkPress = new RelayCommand(WebLinkPressPage);
                    _webLinkPress.Enabled = true;


                }
                return _webLinkPress;
            }
            set
            {
                _webLinkPress = value;
            }
        }

        #endregion

        #region PharmacyDetailsMethods
        /// <summary>
        /// Method for Pharmacy Details
        /// </summary>
        private void PharmacyDetailsWebServiceCall()
        {
            if (App.ObjBrandingResponse != null)
            {
                FillPharmacyDetailsOffline();
            }
            else
            {
                ProgressBarVisibilty = Visibility.Visible;
            }

            if (Utilities.IsConnectedToNetwork())
            {
                //objPharmacyDetails = new HomePanoramaPharmacyDetailsModel(this);
            }
            else
            {
                ProgressBarVisibilty = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Method to fill pharmacy details if offline
        /// </summary>
        private void FillPharmacyDetailsOffline()
        {
            if (App.IsFromRejected)
            {
                INavigationService navigationService = this.GetService<INavigationService>();
                navigationService.Navigate(PageURL.navigateToYourDetailsUpdateURL);
            }

            AppBarPrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;
            PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);
            SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
            FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);


            PharmacyName = App.ObjBrandingResponse.payload.branding_data.pharmacy_name;
            PharmacyBranchName = App.ObjBrandingResponse.payload.branding_data.branch_name + @"               ";

            AddressLine1 = App.ObjBrandingResponse.payload.branding_data.address1;

            AddressLine2 = App.ObjBrandingResponse.payload.branding_data.address2;

            AddressLine3 = App.ObjBrandingResponse.payload.branding_data.city;
            PinCode = App.ObjBrandingResponse.payload.branding_data.postcode;

            PharmacistName1 = App.ObjBrandingResponse.payload.branding_data.pharmacist1;
            PharmacistName2 = App.ObjBrandingResponse.payload.branding_data.pharmacist2;
            WebsiteLink = App.ObjBrandingResponse.payload.branding_data.website;



            if (!string.IsNullOrEmpty(App.ObjBrandingResponse.payload.branding_data.twitter_link) && !string.IsNullOrWhiteSpace(App.ObjBrandingResponse.payload.branding_data.twitter_link))
            {
                IsTwitterLinkVisible = Visibility.Visible;
                TwitterLink = App.ObjBrandingResponse.payload.branding_data.twitter_link;
            }
            if (!string.IsNullOrEmpty(App.ObjBrandingResponse.payload.branding_data.facebook_link) && !string.IsNullOrWhiteSpace(App.ObjBrandingResponse.payload.branding_data.facebook_link))
            {
                IsFacebookLinkVisible = Visibility.Visible;
                FacebookLink = App.ObjBrandingResponse.payload.branding_data.facebook_link;
            }

            if (App.ObjBrandingResponse.payload.branding_data.opening_hours != null)
            {
                bool isClosedToday = App.ObjBrandingResponse.payload.branding_data.opening_hours.SingleOrDefault(s => s.dayname == Convert.ToString(System.DateTime.Today.DayOfWeek)).is_closed;
                string openingTime = Convert.ToString(App.ObjBrandingResponse.payload.branding_data.opening_hours.SingleOrDefault(s => s.dayname == Convert.ToString(System.DateTime.Today.DayOfWeek)).open);
                string closingTime = Convert.ToString(App.ObjBrandingResponse.payload.branding_data.opening_hours.SingleOrDefault(s => s.dayname == Convert.ToString(System.DateTime.Today.DayOfWeek)).close);
                string todayOpenTime = !isClosedToday ? openingTime + "-" + closingTime : "Closed";

                Opentodaytime = todayOpenTime;
                Phone = App.ObjBrandingResponse.payload.branding_data.phone;

                List<OpenHours> lstOpenHours = new List<OpenHours>();
                OpenHours objOpenHours;
                foreach (var item in App.ObjBrandingResponse.payload.branding_data.opening_hours)
                {
                    objOpenHours = new OpenHours { DayName = item.dayname, Timings = !item.is_closed ? item.open + "-" + item.close : "Closed" };
                    lstOpenHours.Add(objOpenHours);
                }
                OpeningHours = lstOpenHours;
            }


        }

        /// <summary>
        /// Method for Website Link Navigation
        /// </summary>
        private void WebLinkPressPage()
        {
            MessageBoxResult result = MessageBox.Show("Would you like to view this website?", "", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                App.LeafletWebLink = WebsiteLink;
                INavigationService navigationService = this.GetService<INavigationService>();
                navigationService.Navigate(PageURL.navigateToLeafletURL + "?source=pharmacy");
            }
            if (result == MessageBoxResult.Cancel)
            {
                INavigationService navigationService = this.GetService<INavigationService>();
                navigationService.Navigate(PageURL.navigateToHomePanoramaURL);
            }
        }
        #endregion        

        #region Condition Leaflets Properties

        /// <summary>
        /// Set progress bar visibility
        /// </summary>
        private Visibility _progressBarLeafVisibilty = Visibility.Collapsed;
        [DataMember]
        public Visibility ProgressBarLeafletVisibilty
        {
            get
            {
                return _progressBarLeafVisibilty;
            }
            set
            {
                _progressBarLeafVisibilty = value;
                OnPropertyChanged("ProgressBarLeafletVisibilty");

            }
        }



        /// <summary>
        /// Set Leaflet NoInternet Visibility
        /// </summary>
        private Visibility _leafletNoInternetVisibility = Visibility.Collapsed;
        [DataMember]
        public Visibility LeafletNoInternetVisibility
        {
            get
            {
                return _leafletNoInternetVisibility;
            }
            set
            {
                _leafletNoInternetVisibility = value;
                OnPropertyChanged("LeafletNoInternetVisibility");

            }
        }

        /// <summary>
        /// Itemsource property of LongListSelector
        /// </summary>
        private List<LeafletsGroup<ConditionLeafletsResponse>> _leafletsCollection;
        [DataMember]
        public List<LeafletsGroup<ConditionLeafletsResponse>> LeafletsCollection
        {
            get
            {
                return _leafletsCollection;
            }

            set
            {
                if (value != null)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        _leafletsCollection = value;
                        OnPropertyChanged("LeafletsCollection");
                    }
                    );
                }

            }
        }


        #endregion

        #region ConditionLeafletsMethod
        /// <summary>
        /// Method to invoke leaflet Command
        /// </summary>
        private void CallLeafletCommand()
        {
            SearchCommand = new ActionCommand(
                  () =>
                  {

                      INavigationService navigationService = this.GetService<INavigationService>();
                      navigationService.Navigate(PageURL.navigateToLeafletSearchURL);
                  });
            RefreshLeafletCommand = new ActionCommand(
                  () =>
                  {
                      CallWebServiceforconditionLeaflets();
                  });
        }

        /// <summary>
        /// Method for Condition leaflets
        /// </summary>
        private void CallWebServiceforconditionLeaflets()
        {
            if (Utilities.IsConnectedToNetwork())
            {
                LeafletNoInternetVisibility = Visibility.Collapsed;
                //ConditionLeafletsModel = new ConditionLeafletsModel(this);
            }
            else
            {
                LeafletNoInternetVisibility = Visibility.Visible;
            }

        }

        #endregion

        #region OrderRepeat properties
        /// <summary>
        /// Itemsource property of Orders List
        /// </summary>
        private OrderedPillDetailsCollection _orderedPillCollection;
        [DataMember]
        public OrderedPillDetailsCollection OrderedPillCollection
        {
            get
            {
                return _orderedPillCollection;
            }

            set
            {
                if (value != null)
                {
                    _orderedPillCollection = value;
                    OnPropertyChanged("OrderedPillCollection");
                }

            }
        }

        /// <summary>
        /// Property to set hitvisibility to order repeat
        /// </summary>
        private bool _hitVisibilityRepeat;
        [DataMember]
        public bool HitVisibilityRepeat
        {
            get
            {
                return _hitVisibilityRepeat;
            }

            set
            {
                if (value != null)
                {
                    _hitVisibilityRepeat = value;
                    OnPropertyChanged("HitVisibilityRepeat");
                }

            }
        }

        /// <summary>
        /// Set visibility approved stack panel
        /// </summary>
        private Visibility _stplApprovedVisibility = Visibility.Collapsed;
        [DataMember]
        public Visibility StplApprovedVisibility
        {
            get
            {
                return _stplApprovedVisibility;
            }
            set
            {
                _stplApprovedVisibility = value;
                OnPropertyChanged("StplApprovedVisibility");

            }
        }

        /// <summary>
        /// Set visibility approved stack panel
        /// </summary>
        private Visibility _stplWaitingApprovalVisibility = Visibility.Collapsed;
        [DataMember]
        public Visibility StplWaitingApprovalVisibility
        {
            get
            {
                return _stplWaitingApprovalVisibility;
            }
            set
            {
                _stplWaitingApprovalVisibility = value;
                OnPropertyChanged("StplWaitingApprovalVisibility");

            }
        }

        /// <summary>
        /// Set progress bar visibility of order repeat
        /// </summary>
        private Visibility _progressBarVisibiltyOrderRepeat = Visibility.Collapsed;
        [DataMember]
        public Visibility ProgressBarVisibiltyOrderRepeat
        {
            get
            {
                return _progressBarVisibiltyOrderRepeat;
            }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    _progressBarVisibiltyOrderRepeat = value;
                    OnPropertyChanged("ProgressBarVisibiltyOrderRepeat");
                });

            }
        }

        /// <summary>
        /// Property for RepeatHistoryTitle
        /// </summary>
        private string _repeatHistoryTitle;
        [DataMember]
        public string RepeatHistoryTitle
        {
            get { return _repeatHistoryTitle; }
            set { _repeatHistoryTitle = value; OnPropertyChanged("RepeatHistoryTitle"); }
        }

        /// <summary>
        /// Property content of popup Remove
        /// </summary>
        private string _popupRemoveText;
        [DataMember]
        public string PopupRemoveText
        {
            get { return _popupRemoveText; }
            set { _popupRemoveText = value; OnPropertyChanged("PopupRemoveText"); }
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
        /// Property content of popup Confirm
        /// </summary>
        private string _popupTextConfirm;
        [DataMember]
        public string PopupTextConfirm
        {
            get { return _popupTextConfirm; }
            set { _popupTextConfirm = value; OnPropertyChanged("PopupTextConfirm"); }
        }

        /// <summary>
        /// Property to show and hide Remove popup 
        /// </summary>
        private bool _isRemovePopupOpen;
        [DataMember]
        public bool IsRemovePopupOpen
        {
            get { return _isRemovePopupOpen; }
            set { _isRemovePopupOpen = value; OnPropertyChanged("IsRemovePopupOpen"); }
        }

        /// <summary>
        /// Property to show and hide Confirm popup 
        /// </summary>
        private bool _isConfirmPopupOpen;
        [DataMember]
        public bool IsConfirmPopupOpen
        {
            get { return _isConfirmPopupOpen; }
            set { _isConfirmPopupOpen = value; OnPropertyChanged("IsConfirmPopupOpen"); }
        }


        /// <summary>
        /// Property for remove Popup Ok Button click
        /// </summary>
        private RelayCommand _popupRemoveOkCommand;
        [IgnoreDataMember]
        public RelayCommand PopupRemoveOkCommand
        {

            get
            {
                if (_popupRemoveOkCommand == null)
                {
                    _popupRemoveOkCommand = new RelayCommand(PopupRemoveOkTapped);
                    _popupRemoveOkCommand.Enabled = true;
                }

                return _popupRemoveOkCommand;
            }
            set { _popupRemoveOkCommand = value; }
        }



        /// <summary>
        /// Property for confirm Popup Cancel Button click
        /// </summary>
        private RelayCommand _cancelCommandPopup;
        [IgnoreDataMember]
        public RelayCommand CancelCommandPopup
        {

            get
            {
                if (_cancelCommandPopup == null)
                {
                    _cancelCommandPopup = new RelayCommand(CancelPopupTapped);
                    _cancelCommandPopup.Enabled = true;
                }

                return _cancelCommandPopup;
            }
            set { _cancelCommandPopup = value; }
        }



        /// <summary>
        /// Property for confirm Popup OK Button click
        /// </summary>
        private RelayCommand _okComandPopup;
        [IgnoreDataMember]
        public RelayCommand OkComandPopup
        {

            get
            {
                if (_okComandPopup == null)
                {
                    _okComandPopup = new RelayCommand(OkPopupTapped);
                    _okComandPopup.Enabled = true;
                }

                return _okComandPopup;
            }
            set { _okComandPopup = value; }
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
        /// Property for OK button of popup rejected 
        /// </summary>
        private RelayCommand _OkRejectedCommand;

        public RelayCommand OkRejectedCommand
        {
            get
            {
                if (_OkRejectedCommand == null)
                {

                    _OkRejectedCommand = new RelayCommand(PopupRejectedOk);
                    _OkRejectedCommand.Enabled = true;


                }
                return _OkRejectedCommand;
            }
            set
            {
                _OkRejectedCommand = value;
            }
        }


        #endregion

        #region OrderRepeatMethods

        /// <summary>
        /// Method to check for disabled account.
        /// </summary>
        private void CheckForDisabledAccount()
        {
            if (App.ObjLgResponse != null)
            {
                if (App.ObjLgResponse.status == 0)
                {
                    if (App.ObjLgResponse.payload.status.Equals(RxConstants.userStatusApproved))
                    {
                        StplWaitingApprovalVisibility = Visibility.Collapsed;
                        StplApprovedVisibility = Visibility.Visible;

                    }

                    else if (App.ObjLgResponse.payload.status.Equals(RxConstants.userStatusRejected))
                    {
                        App.IsFromRejected = true;
                        IsRejectedPopupOpen = true;
                        PopupRejectedText = "Request has been rejected by pharmacy";
                        HitVisibility = false;
                        ProgressBarVisibiltyOrderRepeat = Visibility.Collapsed;
                    }

                    else
                    {
                        StplWaitingApprovalVisibility = Visibility.Visible;
                        StplApprovedVisibility = Visibility.Collapsed;
                        ProgressBarVisibiltyOrderRepeat = Visibility.Collapsed;

                    }
                }

                else if (App.ObjLgResponse.status == 301)
                {
                    IsPopupOpen = true;
                    PopupText = "Your PIN has been changed.";
                    HitVisibility = false;
                }

            }


        }

        /// <summary>
        /// Method to call get all orders webservice
        /// </summary>
        private void DisplayGetAllOrders()
        {
            if (App.OrderedPillDetailsCollection.Count > 0)
            {
                OrderedPillCollection = App.OrderedPillDetailsCollection;
            }
            else
            {
                ProgressBarVisibiltyOrderRepeat = Visibility.Visible;
            }


            if (Utilities.IsConnectedToNetwork())
            {
                RepeatHistoryTitle = "RepeatHistory";
                HitVisibilityRepeat = false;
                //getAllOrdersModel = new GetAllOrdersModel(this);
            }
            else
            {
                ProgressBarVisibiltyOrderRepeat = Visibility.Collapsed;
                RepeatHistoryTitle = "RepeatHistory(offline)";
                ProgressBarVisibiltyOrderRepeat = Visibility.Collapsed;
                CheckForDisabledAccount();
            }

        }

        /// <summary>
        /// Method for cancel button tap of popup confirm
        /// </summary>
        private void CancelPopupTapped()
        {
            IsConfirmPopupOpen = false;
            HitVisibility = true;
        }

        /// <summary>
        /// Method for ok button of popup confirm
        /// </summary>
        private void OkPopupTapped()
        {
            IsConfirmPopupOpen = false;
            HitVisibility = true;
            ProgressBarVisibiltyOrderRepeat = Visibility.Visible;
           // getAllOrdersModel = new GetAllOrdersModel(this, IdsToDelete);
        }

        /// <summary>
        /// Method for ok button of pop up removed
        /// </summary>
        private void PopupRemoveOkTapped()
        {
            IsRemovePopupOpen = false;
            HitVisibility = true;
        }
        /// <summary>
        /// Methode to navigate to registration page if incorrect pin provided
        /// </summary>
        private void PopupOk()
        {
            HitVisibility = true;
            App.PIN = string.Empty;
            App.HashPIN = string.Empty;
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToLoginPanelURL);
        }


        /// <summary>
        /// Methode to navigate update page if rejected by pharmacy
        /// </summary>
        private void PopupRejectedOk()
        {
            HitVisibility = true;

            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToYourDetailsUpdateURL);
        }

        /// <summary>
        /// Method to call webservice to delete order(s)
        /// </summary>
        /// <param name="_objIdsToDelete"></param>
        public void DeleteOrder(List<int> _objIdsToDelete)
        {
            IsConfirmPopupOpen = true;
            PopupTextConfirm = "Are you sure you want to remove your order?";
            IdsToDelete = _objIdsToDelete;
            HitVisibility = false;

        }


        #endregion


        #region Local Health Services Properties
        /// <summary>
        /// Property to bind surgery button
        /// </summary>
        private RelayCommand _surgeryCommand;
        [IgnoreDataMember]
        public RelayCommand SurgeriesCommand
        {

            get
            {
                if (_surgeryCommand == null)
                {
                    _surgeryCommand = new RelayCommand(SurgeryTap);
                    _surgeryCommand.Enabled = true;
                }

                return _surgeryCommand;
            }
            set { _surgeryCommand = value; }
        }


        /// <summary>
        /// Property to bind dentist button
        /// </summary>
        private RelayCommand _dentistsCommand;
        [IgnoreDataMember]
        public RelayCommand DentistsCommand
        {

            get
            {
                if (_dentistsCommand == null)
                {
                    _dentistsCommand = new RelayCommand(DentistsTap);
                    _dentistsCommand.Enabled = true;
                }

                return _dentistsCommand;
            }
            set { _dentistsCommand = value; }
        }


        /// <summary>
        /// Property to bind hospitals button
        /// </summary>
        private RelayCommand _hospitalsCommand;
        [IgnoreDataMember]
        public RelayCommand HospitalsCommand
        {

            get
            {
                if (_hospitalsCommand == null)
                {
                    _hospitalsCommand = new RelayCommand(HospitalsTap);
                    _hospitalsCommand.Enabled = true;
                }

                return _hospitalsCommand;
            }
            set { _hospitalsCommand = value; }
        }


        /// <summary>
        /// Property to bind optitions button
        /// </summary>
        private RelayCommand _optitionCommand;
        [IgnoreDataMember]
        public RelayCommand OptitionsCommand
        {

            get
            {
                if (_optitionCommand == null)
                {
                    _optitionCommand = new RelayCommand(OpticionsTap);
                    _optitionCommand.Enabled = true;
                }

                return _optitionCommand;
            }
            set { _optitionCommand = value; }
        }
        #endregion

        #region Local Health Services Methods
        /// <summary>
        /// Method for Surgery Click
        /// </summary>
        private void SurgeryTap()
        {
            App.FindServiceTiltle = "GP Surgeries";
            App.LocalServiceCentreCoordinates = new GeoCoordinate(RxConstants.UKLatitude, RxConstants.UKLongitude);
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToMapServicesURL);
        }
        /// <summary>
        /// Method for Dentist Click
        /// </summary>
        private void DentistsTap()
        {
            App.FindServiceTiltle = "Dentists";
            App.LocalServiceCentreCoordinates = new GeoCoordinate(RxConstants.UKLatitude, RxConstants.UKLongitude);
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToMapServicesURL);
        }
        /// <summary>
        /// Method for Hospital Click
        /// </summary>
        private void HospitalsTap()
        {
            App.FindServiceTiltle = "Hospitals";
            App.LocalServiceCentreCoordinates = new GeoCoordinate(RxConstants.UKLatitude, RxConstants.UKLongitude);
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToMapServicesURL);
        }
        /// <summary>
        /// Method for Opticion Tap
        /// </summary>
        private void OpticionsTap()
        {
            App.FindServiceTiltle = "Opticians";
            App.LocalServiceCentreCoordinates = new GeoCoordinate(RxConstants.UKLatitude, RxConstants.UKLongitude);
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToMapServicesURL);
        }
        #endregion


        #region PillsReminderProperties
        /// <summary>
        /// DailyMorning Pill
        /// </summary>
        private string _dmPills;
        [DataMember]
        public string DailyMorningPill
        {
            get { return _dmPills; }
            set { _dmPills = value; OnPropertyChanged("DailyMorningPill"); }
        }
        /// <summary>
        /// DailyMorning Pill RemOnOff
        /// </summary>
        private string _dmRemOnOff;
        [DataMember]
        public string DMRemOnOff
        {
            get { return _dmRemOnOff; }
            set { _dmRemOnOff = value; OnPropertyChanged("DMRemOnOff"); }
        }

        /// <summary>
        /// DailyAfternoon Pill
        /// </summary>
        private string _daPills;
        [DataMember]
        public string DailyAfternoonPill
        {
            get { return _daPills; }
            set { _daPills = value; OnPropertyChanged("DailyAfternoonPill"); }
        }

        /// <summary>
        /// DailyAfternoon Pill RemOffOn
        /// </summary>
        private string _daRemOnOff;
        [DataMember]
        public string DARemOnOff
        {
            get { return _daRemOnOff; }
            set { _daRemOnOff = value; OnPropertyChanged("DARemOnOff"); }
        }

        /// <summary>
        /// DailyEvening Pill
        /// </summary>
        private string _dePills;
        [DataMember]
        public string DailyEveningPill
        {
            get { return _dePills; }
            set { _dePills = value; OnPropertyChanged("DailyEveningPill"); }
        }

        /// <summary>
        /// DailyEvening Pill RemOnOff
        /// </summary>
        private string _deRemOnOff;
        [DataMember]
        public string DERemOnOff
        {
            get { return _deRemOnOff; }
            set { _deRemOnOff = value; OnPropertyChanged("DERemOnOff"); }
        }

        /// <summary>
        /// DailyNight Pill
        /// </summary>
        private string _dnPills;
        [DataMember]
        public string DailyNightPill
        {
            get { return _dnPills; }
            set { _dnPills = value; OnPropertyChanged("DailyNightPill"); }
        }
        /// <summary>
        /// DailyNight Pill RemOnOff
        /// </summary>
        private string _dnRemOnOff;
        [DataMember]
        public string DNRemOnOff
        {
            get { return _dnRemOnOff; }
            set { _dnRemOnOff = value; OnPropertyChanged("DNRemOnOff"); }
        }

        /// <summary>
        /// Weekly Pill
        /// </summary>
        private string _weeklyPills;
        [DataMember]
        public string WeeklyPill
        {
            get { return _weeklyPills; }
            set { _weeklyPills = value; OnPropertyChanged("WeeklyPill"); }
        }
        /// <summary>
        /// Weekly Pill RemOnOff
        /// </summary>
        private string _weeklyRemOnOff;
        [DataMember]
        public string WeeklyRemOnOff
        {
            get { return _weeklyRemOnOff; }
            set { _weeklyRemOnOff = value; OnPropertyChanged("WeeklyRemOnOff"); }
        }
        /// <summary>
        /// Monthly Pill
        /// </summary>
        private string _monthlyPills;
        [DataMember]
        public string MonthlyPill
        {
            get { return _monthlyPills; }
            set { _monthlyPills = value; OnPropertyChanged("MonthlyPill"); }
        }
        /// <summary>
        /// Monthly Pill RemOnOff
        /// </summary>
        private string _monthlyRemOnOff;
        [DataMember]
        public string MonthlyRemOnOff
        {
            get { return _monthlyRemOnOff; }
            set { _monthlyRemOnOff = value; OnPropertyChanged("MonthlyRemOnOff"); }
        }
        /// <summary>
        /// Every 28days Pill
        /// </summary>
        private string _edPills;
        [DataMember]
        public string EDPill
        {
            get { return _edPills; }
            set { _edPills = value; OnPropertyChanged("EDPill"); }
        }
        /// <summary>
        /// Every 28days PillReminderOnOff
        /// </summary>
        private string _edRemOnOff;
        [DataMember]
        public string EDRemOnOff
        {
            get { return _edRemOnOff; }
            set { _edRemOnOff = value; OnPropertyChanged("EDRemOnOff"); }
        }
        #endregion

        #region PillsReminderMethod

        /// <summary>
        /// Display Pills
        /// </summary>
        private void DisplayPills()
        {
            App.IsEditCancelled = false;
            bool IsContentEmpty = true;
            if (App.DailyMorningPillsCollection != null)
            {
                if (App.DailyMorningPillsCollection.Count > 0)
                {
                    IsContentEmpty = false;
                }
            }
            if (!IsContentEmpty)
            {
                string pillNames = ShowPills(App.DailyMorningPillsCollection);
                DailyMorningPill = pillNames;
                DMRemOnOff = App.DailyMorningReminderOnOff;
                IsContentEmpty = true;
            }
            else
            {
                DailyMorningPill = "None";
                DMRemOnOff = App.DailyMorningReminderOnOff;
            }

            if (App.DailyAfternoonPillsCollection != null)
            {
                if (App.DailyAfternoonPillsCollection.Count > 0)
                {
                    IsContentEmpty = false;
                }
            }
            if (!IsContentEmpty)
            {
                IsContentEmpty = true;
                string pillNames = ShowPills(App.DailyAfternoonPillsCollection);
                DailyAfternoonPill = pillNames;
                DARemOnOff = App.DailyAfternoonReminderOnOff;

            }
            else
            {
                DailyAfternoonPill = "None";
                DARemOnOff = App.DailyAfternoonReminderOnOff;
            }
            if (App.DailyEveningPillsCollection != null)
            {
                if (App.DailyEveningPillsCollection.Count > 0)
                {
                    IsContentEmpty = false;
                }
            }
            if (!IsContentEmpty)
            {
                IsContentEmpty = true;
                string pillNames = ShowPills(App.DailyEveningPillsCollection);
                DailyEveningPill = pillNames;
                DERemOnOff = App.DailyEveningReminderOnOff;
            }
            else
            {
                DailyEveningPill = "None";
                DERemOnOff = App.DailyEveningReminderOnOff;
            }
            if (App.DailyNightPillsCollection != null)
            {
                if (App.DailyNightPillsCollection.Count > 0)
                {
                    IsContentEmpty = false;
                }
            }
            if (!IsContentEmpty)
            {
                IsContentEmpty = true;
                string pillNames = ShowPills(App.DailyNightPillsCollection);
                DailyNightPill = pillNames;
                DNRemOnOff = App.DailyNightReminderOnOff;
            }
            else
            {
                DailyNightPill = "None";
                DNRemOnOff = App.DailyNightReminderOnOff;
            }
            if (App.WeeklyPillsCollection != null)
            {
                if (App.WeeklyPillsCollection.Count > 0)
                {
                    IsContentEmpty = false;
                }
            }
            if (!IsContentEmpty)
            {
                IsContentEmpty = true;
                string pillNames = ShowPills(App.WeeklyPillsCollection);
                WeeklyPill = pillNames;
                WeeklyRemOnOff = App.WeeklyReminderOnOff;
            }
            else
            {
                WeeklyPill = "None";
                WeeklyRemOnOff = App.WeeklyReminderOnOff;
            }
            if (App.MonthlyPillsCollection != null)
            {
                if (App.MonthlyPillsCollection.Count > 0)
                {
                    IsContentEmpty = false;
                }
            }
            if (!IsContentEmpty)
            {
                IsContentEmpty = true;
                string pillNames = ShowPills(App.MonthlyPillsCollection);
                MonthlyPill = pillNames;
                MonthlyRemOnOff = App.MonthlyReminderOnOff;
            }
            else
            {
                MonthlyPill = "None";
                MonthlyRemOnOff = App.MonthlyReminderOnOff;
            }
            if (App.Every28DaysPillsCollection != null)
            {
                if (App.Every28DaysPillsCollection.Count > 0)
                {
                    IsContentEmpty = false;
                }
            }
            if (!IsContentEmpty)
            {
                IsContentEmpty = true;
                string pillNames = ShowPills(App.Every28DaysPillsCollection);
                EDPill = pillNames;
                EDRemOnOff = App.Every28DaysReminderOnOff;
            }
            else
            {
                EDPill = "None";
                EDRemOnOff = App.Every28DaysReminderOnOff;
            }
        }


        /// <summary>
        /// Show Pills
        /// </summary>
        /// <param name="pillsCol"></param>
        /// <returns></returns>
        private string ShowPills(PillsReminderModelCol pillsCol)
        {
            string pillNames = string.Empty;
            foreach (var item in pillsCol)
            {
                if (string.IsNullOrEmpty(pillNames) || (string.IsNullOrWhiteSpace(pillNames)))
                {
                    if (item.NumberOfPills.Contains("x"))
                        pillNames = string.Join(", ", string.Concat(item.NumberOfPills.Substring(3), " ", item.PillName));
                    else
                    {
                        item.NumberOfPills = string.Concat(" x ", item.NumberOfPills);
                        pillNames = string.Join(", ", string.Concat(item.NumberOfPills.Substring(3), " ", item.PillName));
                    }
                }

                else
                {
                    if (item.NumberOfPills.Contains("x"))
                        pillNames = string.Join(", ", pillNames, string.Concat(item.NumberOfPills.Substring(3), " ", item.PillName));
                    else
                    {
                        item.NumberOfPills = string.Concat(" x ", item.NumberOfPills);
                        pillNames = string.Join(", ", pillNames, string.Concat(item.NumberOfPills.Substring(3), " ", item.PillName));
                    }
                }

            }
            return pillNames;
        }

        #endregion
    }
}