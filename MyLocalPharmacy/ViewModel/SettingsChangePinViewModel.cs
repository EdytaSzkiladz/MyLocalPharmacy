using System;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.Common;
using System.Windows;
using Microsoft.Phone.Controls;
using MyLocalPharmacy.Contract;
using System.Runtime.Serialization;

namespace MyLocalPharmacy.ViewModel
{
   public class SettingsChangePinViewModel: BaseViewModel
   {
        #region Constructor
       public SettingsChangePinViewModel()
       {
           _isPinValidatorVisible = Visibility.Collapsed;
           HitVisibility = true;
       }
       #endregion
       
        #region Properties

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
       /// Property for navigation to ToSettingsPage
       /// </summary>
       private RelayCommand _toSettingsPage;
       public RelayCommand ToSettingsPage
       {

           get
           {
               if (_toSettingsPage == null)
               {
                   _toSettingsPage = new RelayCommand(NavigateToSettingsPage);
                   _toSettingsPage.Enabled = true;
               }

               return _toSettingsPage;
           }
           set { _toSettingsPage = value; }
       }

      
        /// <summary>
        /// To set the Pin Message
        /// </summary>
        private string _pinLengthMessage;
        public string PinLengthMessage
        {
            get { return _pinLengthMessage; }
            set
            {
                _pinLengthMessage = value;
                OnPropertyChanged("PinLengthMessage");
            }
        }
        /// <summary>
        /// To set the Pin Message Visibilty
        /// </summary>
        private Visibility _isPinValidatorVisible;
        public Visibility IsPinValidatorVisible
        {
            get { return _isPinValidatorVisible; }
            set
            {
                _isPinValidatorVisible = value;
                OnPropertyChanged("IsPinValidatorVisible");
            }
        }
        /// <summary>
        /// To set the Pin
        /// </summary>
        private string _pin;
        public string Pin
        {
            get { return _pin; }
            set
            {
                _pin = value;
                OnPropertyChanged("Pin");
            }
        }
        /// <summary>
        /// Property for navigation to Confirm New Pin Page
        /// </summary>
        private RelayCommand _settingsConfirmPinChange;
        public RelayCommand SettingsConfirmPinChange
        {

            get
            {
                if (_settingsConfirmPinChange == null)
                {
                    _settingsConfirmPinChange = new RelayCommand(SettingsConfirmPinChangeUrl);
                    _settingsConfirmPinChange.Enabled = true;
                }

                return _settingsConfirmPinChange;
            }
            set { _settingsConfirmPinChange = value; }
        }

        /// <summary>
        /// Property for SameOldNewPinOk Command
        /// </summary>
        private RelayCommand _sameOldNewPinOkCommand;
        public RelayCommand SameOldNewPinOkCommand
        {

            get
            {
                if (_sameOldNewPinOkCommand == null)
                {
                    _sameOldNewPinOkCommand = new RelayCommand(SameOldNewPinOkButton);
                    _sameOldNewPinOkCommand.Enabled = true;
                }

                return _sameOldNewPinOkCommand;
            }
            set { _sameOldNewPinOkCommand = value; }
        }
       
        /// <summary>
        /// To Open/close the popup
        /// </summary>
        private bool _isSameOldNewPinPopupOpen;
        public bool IsSameOldNewPinPopupOpen
        {
            get { return _isSameOldNewPinPopupOpen; }
            set
            {
                _isSameOldNewPinPopupOpen = value;
                OnPropertyChanged("IsSameOldNewPinPopupOpen");
            }
        }
        /// <summary>
        /// To show message if old and new pins match
        /// </summary>
        private string _sameOldNewPinMessage;
        public string SameOldNewPinMessage
        {
            get { return _sameOldNewPinMessage; }
            set
            {
                _sameOldNewPinMessage = value;
                OnPropertyChanged("SameOldNewPinMessage");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to navigate to settings page on cancel click
        /// </summary>
        private void NavigateToSettingsPage()
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToSettingPageURL);
        }
       /// <summary>
       /// Validate and Navigate to confirm new pin page
       /// </summary>
        private void SettingsConfirmPinChangeUrl()
        {
            if (!string.IsNullOrEmpty(Pin) || !string.IsNullOrWhiteSpace(Pin))
            {
                if (Pin.Length < 4)
                {
                    IsPinValidatorVisible = Visibility.Visible;
                    PinLengthMessage = "PIN should be 4 digits";
                    Pin = string.Empty;
                }
                else if (Pin == App.PIN)
                {
                    IsSameOldNewPinPopupOpen = true;
                    HitVisibility = false;
                    SameOldNewPinMessage = "New PIN must be different from old PIN.\nPlease Type the new PIN again.";
                    Pin = string.Empty;
                }
                else
                {
                    App.NewPin = Pin;
                    INavigationService navigationService = this.GetService<INavigationService>();
                    navigationService.Navigate(PageURL.navigateToSettingConfirmChangePINURL);
                }
            }
        }
        /// <summary>
        /// Close the  popupOldNewPin
        /// </summary>
        private void SameOldNewPinOkButton()
        {
            IsSameOldNewPinPopupOpen = false;
            HitVisibility = true;
        }
        #endregion        
    }
}
