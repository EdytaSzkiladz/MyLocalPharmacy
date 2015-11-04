using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Utils;
using System.Windows;

namespace MyLocalPharmacy.ViewModel
{
   public class SetPinViewModel : BaseViewModel
   {
       #region Properties
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
       /// To set Pin
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
       //
       /// <summary>
       /// On "Cancel" click to signup
       /// </summary>
       private RelayCommand _toSignUp;
       public RelayCommand ToSignUp
       {

           get
           {
               if (_toSignUp == null)
               {
                   _toSignUp = new RelayCommand(ToSignUpUrl);
                   _toSignUp.Enabled = true;
               }
               return _toSignUp;
           }
           set { _toSignUp = value; }
       }
       
       /// <summary>
       /// Property for navigation to ConfirmPin Page
       /// </summary>
       private RelayCommand _toConfirmPin;
       public RelayCommand ToConfirmPin
       {

           get
           {
               if (_toConfirmPin == null)
               {
                   _toConfirmPin = new RelayCommand(NavigateToConfirmPin);
                   _toConfirmPin.Enabled = true;
               }

               return _toConfirmPin;
           }
           set { _toConfirmPin = value; }
       }
       #endregion

       #region Methods
       /// <summary>
       /// Navigate to "Signup" on "Cancel" click
       /// </summary>
       private void ToSignUpUrl()
       {
           INavigationService navigationService = this.GetService<INavigationService>();
           navigationService.Navigate(PageURL.navigateToSignUpPanelURL);
       }
       /// <summary>
       /// Method to navigate to confirm pin
       /// </summary>
       private void NavigateToConfirmPin()
       {
           INavigationService navigationService = this.GetService<INavigationService>();

           if (!string.IsNullOrEmpty(Pin) || !string.IsNullOrWhiteSpace(Pin))
           {
               if (Pin.Length < 4)
               {
                   IsPinValidatorVisible = Visibility.Visible;
                   PinLengthMessage = "Pin should be 4 digits";
                   Pin = string.Empty;
               }
               else
               {
                   App.PIN = Pin;
                   navigationService.Navigate(PageURL.navigateToConfirmPinURL);
               }
           }
           else
           {
               IsPinValidatorVisible = Visibility.Visible;
               PinLengthMessage = "Please enter a PIN";
           }
       }
       #endregion 
    }
}
