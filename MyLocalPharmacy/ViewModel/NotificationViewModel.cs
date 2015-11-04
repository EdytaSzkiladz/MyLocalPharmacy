using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    public class NotificationViewModel: BaseViewModel
    {

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationViewModel()
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
        #endregion

        #region Properties
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
        /// For Secondary Color
        /// </summary>
        private SolidColorBrush _secondaryColour;
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
        /// For Font Color
        /// </summary>
        private SolidColorBrush _fontColor;
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
        /// Property to bind Notification header text
        /// </summary>
        private string _notificationHeader;

        public string NotificationHeader
        {
            get
            {
                return _notificationHeader;
            }
            set
            {
                _notificationHeader = value;
                OnPropertyChanged("NotificationHeader");
            }
        }


        /// <summary>
        /// Property to bind Notification Content
        /// </summary>
        private string _notificationContent;

        public string NotificationContent
        {
            get
            {
                return _notificationContent;
            }
            set
            {
                _notificationContent = value;
                OnPropertyChanged("NotificationContent");
            }
        }
        /// <summary>
        /// Property to Navigate to HomePanorama when "Close" button is clicked
        /// </summary>
        private RelayCommand _navigateToHP;
        public RelayCommand NavigateToHP
        {

            get
            {
                if (_navigateToHP == null)
                {
                    _navigateToHP = new RelayCommand(NavigateToHPPage);
                    _navigateToHP.Enabled = true;
                }

                return _navigateToHP;
            }
            set { _navigateToHP = value; }
        }
        /// <summary>
        /// Method to navigate to HomePanorama
        /// </summary>
        private void NavigateToHPPage()
        {
           INavigationService navigationService = this.GetService<INavigationService>();
           navigationService.Navigate(PageURL.navigateToHomePanoramaURL);
                          
        }
        #endregion

    }
}
