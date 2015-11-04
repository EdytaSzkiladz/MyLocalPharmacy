using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    public class ConfirmRepeatViewModel : BaseViewModel
    {
        #region Declarations
        ConfirmRepeatModel ConfirmRepeatModel; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constuctor
        /// </summary>
        public ConfirmRepeatViewModel()
        {
            IsPopupErrorOpen = false;
            HitVisibility = true;
            if (App.ObjBrandingResponse != null)
            {
                PrimaryColour =  Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);
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
        /// Property for Send Button click
        /// </summary>
        private RelayCommand _sendCommand;
        public RelayCommand NextCommand
        {

            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new RelayCommand(SendTapped);
                    _sendCommand.Enabled = true;
                }

                return _sendCommand;
            }
            set { _sendCommand = value; }
        }



        /// <summary>
        /// Property for Error Popup Ok Button click
        /// </summary>
        private RelayCommand _popupErrorOkCommand;
        public RelayCommand PopupErrorOkCommand
        {

            get
            {
                if (_popupErrorOkCommand == null)
                {
                    _popupErrorOkCommand = new RelayCommand(PopupErrorOkTapped);
                    _popupErrorOkCommand.Enabled = true;
                }

                return _popupErrorOkCommand;
            }
            set { _popupErrorOkCommand = value; }
        }

        /// <summary>
        /// Property for orders Sent Popup Ok Button click
        /// </summary>
        private RelayCommand _popupSentOkCommand;
        public RelayCommand PopupSentOkCommand
        {

            get
            {
                if (_popupSentOkCommand == null)
                {
                    _popupSentOkCommand = new RelayCommand(PopupSentOkTapped);
                    _popupSentOkCommand.Enabled = true;
                }

                return _popupSentOkCommand;
            }
            set { _popupSentOkCommand = value; }
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
        /// Property to set Error popup text
        /// </summary>
        private string _popupErrorText;

        public string PopupErrorText
        {
            get { return _popupErrorText; }
            set { _popupErrorText = value; OnPropertyChanged("PopupErrorText"); }
        }

        /// <summary>
        /// Property to show and hide Error popup 
        /// </summary>
        private bool _isPopupErrorOpen;

        public bool IsPopupErrorOpen
        {
            get { return _isPopupErrorOpen; }
            set { _isPopupErrorOpen = value; OnPropertyChanged("IsPopupErrorOpen"); }
        }

        /// <summary>
        /// Property to show and hide Sent popup 
        /// </summary>
        private bool _isPopupSentOpen;

        public bool IsPopupSentOpen
        {
            get { return _isPopupSentOpen; }
            set { _isPopupSentOpen = value; OnPropertyChanged("IsPopupSentOpen"); }
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
        /// Property to show and hide Confirm popup 
        /// </summary>
        private bool _isConfirmPopupOpen;

        public bool IsConfirmPopupOpen
        {
            get { return _isConfirmPopupOpen; }
            set { _isConfirmPopupOpen = value; OnPropertyChanged("IsConfirmPopupOpen"); }
        }

        /// <summary>
        /// Property for confirm Popup Cancel Button click
        /// </summary>
        private RelayCommand _cancelCommandPopup;
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
        #endregion

        #region Methods
        /// <summary>
        /// Method invoked on tapping send button
        /// </summary>
        private void SendTapped()
        {
            IsConfirmPopupOpen = true;
            HitVisibility = false;
        }

        /// <summary>
        /// Method invoked on tapping OK button of popupError
        /// </summary>
        private void PopupErrorOkTapped()
        {
            IsPopupErrorOpen = false;
            HitVisibility = true;

        }

        /// <summary>
        /// Method invoked on tapping OK button of popupSent
        /// </summary>
        private void PopupSentOkTapped()
        {
            IsPopupSentOpen = false;

            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToHomePanoramaURL + "?goto=2");
            HitVisibility = true;
        }

        /// <summary>
        /// Method invoked on tapping cancel button of confirm popup
        /// </summary>
        private void CancelPopupTapped()
        {
            IsConfirmPopupOpen = false;
            HitVisibility = true;

        }


        /// <summary>
        /// Method invoked on tapping OK button of confirm popup
        /// </summary>
        private void OkPopupTapped()
        {
            IsConfirmPopupOpen = false;
            HitVisibility = true;
            if (Utilities.IsConnectedToNetwork())
            {
                ConfirmRepeatModel = new ConfirmRepeatModel(this);
            }
            else
            {
                IsPopupErrorOpen = true;
                PopupErrorText = "No Internet Connectivity.";
                HitVisibility = false;
            }

        } 
        #endregion
    }
}
