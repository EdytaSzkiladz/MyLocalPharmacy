using Microsoft.Phone.Controls;
using MyLocalPharmacy.Common;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.View;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;
namespace MyLocalPharmacy.ViewModel
{
    public class SelectSurgeryViewModel : BaseViewModel
    {
        #region Declarations

        SelectSurgery _selectSurgery = null;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="selectSurgery"></param>
        public SelectSurgeryViewModel(SelectSurgery selectSurgery)
        {
            _selectSurgery = selectSurgery;
            _isInternetPopupOpen = false;

            PopulateDistances();

            if (App.IsNavigatedFromYourDetailsLogin)
            {
                PrimaryColour = SecondaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);
                FontColor = Utilities.GetColorFromHexa(RxConstants.FontColourCode);
            }
            else
            {
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
            }

            HitVisibility = true;
        }
        #endregion

        #region Properties


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
        /// For display selected surgen name from list box
        /// </summary>
        private string _displaySelectedSurgen;
        [DataMember]
        public string DisplaySelectedSurgen
        {
            get { return _displaySelectedSurgen; }
            set
            {
                _displaySelectedSurgen = value;
                OnPropertyChanged("DisplaySelectedSurgen");
            }
        }

        /// <summary>
        /// Popup for display no Internet
        /// </summary>
        private bool _isInternetPopupOpen;
        [DataMember]
        public bool IsInternetPopupOpen
        {
            get { return _isInternetPopupOpen; }
            set { _isInternetPopupOpen = value; OnPropertyChanged("IsInternetPopupOpen"); }
        }

        /// <summary>
        /// Command for binding selected surgen to previous page
        /// </summary>
        private RelayCommand _addSurgenCommand;
        [IgnoreDataMember]
        public RelayCommand AddSurgenCommand
        {
            get
            {
                if (_addSurgenCommand == null)
                {
                    _addSurgenCommand = new RelayCommand(AddingToyourDetials);
                    _addSurgenCommand.Enabled = true;
                }
                return _addSurgenCommand;
            }
            set
            {
                _addSurgenCommand = value;
            }
        }

        /// <summary>
        ///  Command for display no internet popup
        /// </summary>
        private RelayCommand _noInternetDataOkCommand;
        [IgnoreDataMember]
        public RelayCommand NoInternetDataOkCommand
        {
            get
            {
                if (_noInternetDataOkCommand == null)
                {
                    _noInternetDataOkCommand = new RelayCommand(NoInternetData);
                    _noInternetDataOkCommand.Enabled = true;
                }
                return _noInternetDataOkCommand;
            }
            set
            {
                _noInternetDataOkCommand = value;
            }
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
        /// To set selected country
        /// </summary>
        private string _selectedDistance;
        [DataMember]
        public string SelectedDistance
        {
            get { return _selectedDistance; }
            set
            {
                if (value != _selectedDistance)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {   
                            _selectedDistance = value;
                            OnPropertyChanged("SelectedDistance");
                            if (_selectSurgery != null)
                                _selectSurgery.TriggerWebServiceCall(SelectedDistance);
                        });
                }
            }
        }

        /// <summary>
        /// Property to show and hide popup 
        /// </summary>
        private ObservableCollection<string> _listitems;
        [DataMember]
        public ObservableCollection<string> Listitems
        {
            get { return _listitems; }
            set {
                _listitems = value; 
                OnPropertyChanged("Listitems");  }
        }
        #endregion

        #region Method
        /// <summary>
        /// Method to clsoe popup
        /// </summary>
        private void NoInternetData()
        {
            IsInternetPopupOpen = false;
            HitVisibility = true;
        }

        /// <summary>
        /// Method to populate Distances
        /// </summary>
        private void PopulateDistances()
        {
            ObservableCollection<string> distances = new ObservableCollection<string>();
            distances.Add("5");
            distances.Add("10");
            distances.Add("25");
            distances.Add("50");
            
            Listitems = distances;
            SelectedDistance = Listitems[0];

        }


        /// <summary>
        /// method for binding selected surgen Name
        /// </summary>
        private void AddingToyourDetials()
        {

          
            App.IsSelectedSurgen = true;
            App.IsDisableSearchsurgen = false;
            App.IsDisplaySelectedSurgenOnSearchBox = false;
            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            bool success = false;
            if (Utilities.IsConnectedToNetwork())
            {

                if (App.IsNavigatedFromYourDetailsLogin)
                {
                    if (DisplaySelectedSurgen != App.SelectedSurgen)
                        App.SurgeonAddress = string.Empty;
                    App.SelectedSurgen = DisplaySelectedSurgen;

                    success = frame.Navigate(new Uri(PageURL.navigateToYourDetailsLoginURL, UriKind.Relative));
                    App.IsNavigatedFromYourDetailsLogin = false;
                    App.IsNavigatedFromYourDetailsLoginwithTC = false;
                    App.IsNavigatedFromYourDetailsUpdate = false;
                }
                else if (App.IsNavigatedFromYourDetailsLoginwithTC)
                {
                    if (DisplaySelectedSurgen != App.SurgeonSaved)
                        App.SurgeonAddress = string.Empty;
                    App.SurgeonSaved = DisplaySelectedSurgen;
                    success = frame.Navigate(new Uri(PageURL.navigateToYourDetailswithTCURL, UriKind.Relative));
                    App.IsNavigatedFromYourDetailsLoginwithTC = false;
                    App.IsNavigatedFromYourDetailsLogin = false;
                    App.IsNavigatedFromYourDetailsUpdate = false;
                }
                else if (App.IsNavigatedFromYourDetailsUpdate)
                {
                    if (DisplaySelectedSurgen != App.SurgeonSaved)
                        App.SurgeonAddress = string.Empty;
                    App.SurgeonSaved = DisplaySelectedSurgen;

                    success = frame.Navigate(new Uri(PageURL.navigateToYourDetailsUpdateURL, UriKind.Relative));
                    App.IsNavigatedFromYourDetailsUpdate = false;
                    App.IsNavigatedFromYourDetailsLoginwithTC = false;
                    App.IsNavigatedFromYourDetailsLogin = false;
                }
            }
            else
            {
                IsInternetPopupOpen = true;
                HitVisibility = false;
            }
        }
        #endregion
    }
}