using MyLocalPharmacy.Common;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    public class MapServicesViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MapServicesViewModel()
        {
            IsPopupOpen = false;
            IsNoInternetPopupOpen = false;
            ProgressBarVisibilty = Visibility.Collapsed;
            if (App.ObjBrandingResponse != null)
            {
                PrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;
                SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
                FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
            }
            else
            {
                PrimaryColour = RxConstants.PrimaryColourCode;
                SecondaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);
                FontColor = Utilities.GetColorFromHexa(RxConstants.FontColourCode);
            }

        } 
        #endregion

        #region Properties
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
        private string _primaryColour;
        [DataMember]
        public string PrimaryColour
        {
            get { return _primaryColour; }
            set
            {
                _primaryColour = value;
                OnPropertyChanged("PrimaryColour");
            }
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
                _progressBarVisibilty = value;
                OnPropertyChanged("ProgressBarVisibilty");
            }
        }

        /// <summary>
        /// Property to show and hide popup "No Internet Connectivity" 
        /// </summary>
        private bool _isNoInternetPopupOpen;
        [DataMember]
        public bool IsNoInternetPopupOpen
        {
            get { return _isNoInternetPopupOpen; }
            set { _isNoInternetPopupOpen = value; OnPropertyChanged("IsNoInternetPopupOpen"); }
        }


        /// <summary>
        /// Property for Ok button in popup "No Internet Connectivity"
        /// </summary>
        private RelayCommand _NoInternetOkCommand;
        [IgnoreDataMember]
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

        #endregion

        #region Methods
        /// <summary>
        /// Method to close popup
        /// </summary>
        private void NoInternetOk()
        {
            IsNoInternetPopupOpen = false;
        }  
        #endregion

    }
}
