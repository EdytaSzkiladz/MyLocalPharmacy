using Microsoft.Phone.Controls;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLocalPharmacy.Model
{
    public class EnterPinModel
    {
        #region Declarations
        EnterPinViewModel enterPinViewModel; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_objEnterPinViewModel"></param>
        public EnterPinModel(EnterPinViewModel _objEnterPinViewModel,string _source)
        {
            enterPinViewModel = _objEnterPinViewModel;

            if (_source.Equals("reset"))
            {
                ResetPinWebService();
            }
            else if (_source.Equals("invalidPin"))
            {
                GetUserDetailsWebService();
            }
            
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Webservice to reset pin
        /// </summary>
        private void ResetPinWebService()
        {
            string apiUrl = RxConstants.sendResetPinCode;
            SendResetPinCodeRequest objInputParam = new SendResetPinCodeRequest
            {
                mail = App.LoginEmailId,
                system_version = "android",
                app_version = "1.6",
                branding_version = "MLP"
            };

            WebClient resetpinswebservicecall = new WebClient();
            var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);
            var json = JsonHelper.Serialize(objInputParam);

            resetpinswebservicecall.Headers["Content-type"] = "application/json";
            resetpinswebservicecall.UploadStringCompleted += resetpinswebservicecall_UploadStringCompleted;

            resetpinswebservicecall.UploadStringAsync(uri, "POST", json);
        }

        /// <summary>
        /// Get User Details web service call
        /// </summary>
        private void GetUserDetailsWebService()
        {
            string apiUrl = RxConstants.getUserDetails;
            LoginParameters objLoginparameters = new LoginParameters
            {
                Mail = App.LoginEmailId,
                Pharmacyid = App.LoginPharId.ToUpper(),
                Pin = Utilities.GetSHA256( enterPinViewModel.Pin),
                system_version = "android",
                app_version = "1.6",
                branding_version = "MLP"

            };

            WebClient userdetailswebservicecall = new WebClient();
            var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

            var json = JsonHelper.Serialize(objLoginparameters);
            userdetailswebservicecall.Headers["Content-type"] = "application/json";
            userdetailswebservicecall.UploadStringCompleted += userdetailswebservicecall_UploadStringCompleted;

            userdetailswebservicecall.UploadStringAsync(uri, "POST", json);
        }

        /// <summary>
        /// Check the response for  resetpin webservice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetpinswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            SendResetPinCodeResponse objresetpincoderesponse = new SendResetPinCodeResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objresetpincoderesponse = Utils.JsonHelper.Deserialize<SendResetPinCodeResponse>(response);
                    if (objresetpincoderesponse.status == 0)
                    {
                        enterPinViewModel.HitVisibility = true;
                        PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                        frame.Navigate(new Uri(PageURL.navigateToResetPinLoginURL, UriKind.Relative));
                    }
                    else
                    {
                        enterPinViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        MessageBox.Show("Please Contact your pharmacy.");
                    }

                }
            }
            catch (Exception)
            {
                enterPinViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                enterPinViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
            }
        }

        /// <summary>
        /// Get the response of user details web service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userdetailswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            LoginResponse objlgresponse = new LoginResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objlgresponse = Utils.JsonHelper.Deserialize<LoginResponse>(response);

                    if (objlgresponse.status == 0)
                    {
                        App.PIN = enterPinViewModel.Pin;
                        App.HashPIN = Utilities.GetSHA256(enterPinViewModel.Pin);
                        PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                        App.IsToombStoned = false;
                        App.IsApplicationInstancePreserved = false;

                        if ((!string.IsNullOrEmpty(App.TombStonedPageURL)) && (!string.IsNullOrWhiteSpace(App.TombStonedPageURL)))
                        {
                            frame.Navigate(new Uri(App.TombStonedPageURL, UriKind.Relative));
                        }
                        else if (!App.IsUserRegistered)
                        {
                            frame.Navigate(new Uri(PageURL.navigateToYourDetailsLoginURL, UriKind.Relative));

                        }
                        else if (App.IsPageHomePanorama)
                        {
                            frame.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.Relative));

                        }
                        else if (App.IsPageUpdateYourDetailsafterLogin)
                        {
                            frame.Navigate(new Uri(PageURL.navigateToYourDetailswithTCURL, UriKind.Relative));

                        }
                        App.NoofTriesLeft = 9;
                    }
                    else
                    {
                        enterPinViewModel.IsIncorrectPinPopupOpen = true;
                        enterPinViewModel.IncorrectPinMessage = "Incorrect PIN entered please try again.\nYou have " + App.NoofTriesLeft + " tries left before your data is wiped.";
                        enterPinViewModel.HitVisibility = false;
                        App.NoofTriesLeft = App.NoofTriesLeft - 1;
                        enterPinViewModel.Pin = string.Empty;
                    }

                }
            }
            catch (Exception)
            {
                enterPinViewModel.IsIncorrectPinPopupOpen = true;
                enterPinViewModel.IncorrectPinMessage = "Incorrect PIN entered please try again.\nYou have " + App.NoofTriesLeft + " tries left before your data is wiped.";
                enterPinViewModel.HitVisibility = false;
                App.NoofTriesLeft = App.NoofTriesLeft - 1;
                enterPinViewModel.Pin = string.Empty;

            }


        }

        #endregion
    }
}
