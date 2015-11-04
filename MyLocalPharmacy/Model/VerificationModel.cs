using Microsoft.Phone.Reactive;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MyLocalPharmacy.Model
{
    public class VerificationModel
    {
        #region Declarations
        public VerificationViewModel objVerificationViewModel; 
        BackgroundWorker bw;


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verificationViewModel"></param>
        /// <param name="serviceKey"></param>
        public VerificationModel(VerificationViewModel verificationViewModel, string serviceKey)
        {
            objVerificationViewModel = verificationViewModel;

            if (serviceKey.Equals("Resend"))
            {
                VerificationWebService();
            }
            else
            {
                GetUserDetailsWebService();
            }            
        } 
        #endregion

        #region Methods

        /// <summary>
        /// Webservice call to get user details.
        /// </summary>
        private void GetUserDetailsWebService()
        {
            string apiUrl = RxConstants.getUserDetails;
            try
            {
                LoginParameters objLoginparameters = new LoginParameters
                {
                    Mail = App.LoginEmailId,
                    Pharmacyid = App.LoginPharId.ToUpper(),
                    Pin = App.HashPIN,
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
            catch (Exception)
            {

            }
           
        }

        /// <summary>
        /// Verification By Email WebService Method
        /// </summary>
        private void VerificationWebService()
        {
            string apiUrl = RxConstants.resendConfirmationCodes;
            try
            {
                string mailId;
                string pharmacyId;
                if (string.IsNullOrEmpty(  App.YourDetailsLoginEmail) || string.IsNullOrWhiteSpace(  App.YourDetailsLoginEmail) )
                {
                    mailId = App.LoginEmailId;
                    pharmacyId = App.LoginPharId;
                }
                else
                {
                    mailId = App.YourDetailsLoginEmail;
                    pharmacyId = App.SignUpPharId;
                }

                ResendConfirmationCodesRequest objInputParam = new ResendConfirmationCodesRequest
                {
                    mail = mailId,
                    pin = App.HashPIN,
                    pharmacyid = pharmacyId.ToUpper(),
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
            catch (Exception)
            {

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
                    if (objlgresponse != null)
                    {
                        if (objlgresponse.status == 0)
                        {
                            if (objlgresponse.payload.status.Equals(RxConstants.userStatusConfirmed))
                            {
                                objVerificationViewModel.IsPopupOpen = true;
                                objVerificationViewModel.HitVisibility = false;
                            }
                            else
                            {
                                GetUserDetailsWebService();
                                
                            }
                            
                        }
                        else
                        {
                            GetUserDetailsWebService();
                        }
                        
                    }

                }
            }
            catch (Exception)
            {
                objVerificationViewModel.HitVisibility = true;
            }


        }

        

        /// <summary>
        /// Method to get response of web service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetpinswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            ResetPinResponse objResetPinResponse = new ResetPinResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objResetPinResponse = Utils.JsonHelper.Deserialize<ResetPinResponse>(response);

                    if (objResetPinResponse.status == 0)
                    {
                        objVerificationViewModel.IsResendPopupOpen = true;
                        objVerificationViewModel.HitVisibility = false;
                    }
                    else if (objResetPinResponse.status == 324)
                    {
                        objVerificationViewModel.IsConfirmationPopupOpen = true;
                        objVerificationViewModel.HitVisibility = false;
                    }
                }
            }
            catch (Exception)
            {
                objVerificationViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
              
            }
           
        }        
        #endregion
    }
}
