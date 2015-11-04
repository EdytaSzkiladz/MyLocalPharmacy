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
    public class VerifyBySmsModel
    {
        #region Declarations
        public VerifyBySmsViewModel objVerifyBySmsViewModel; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="verifyBySmsViewModel"></param>
        /// <param name="verify"></param>
        public VerifyBySmsModel(VerifyBySmsViewModel verifyBySmsViewModel, string verify)
        {
            objVerifyBySmsViewModel = verifyBySmsViewModel;
            if (verify.Equals("ResentPin"))
            {
                ResendBySmsWebService();
            }
            else if (verify.Equals("Verify"))
            {
                VerifyBySmsWebService();
            }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// VerifyBySmsWebService Method
        /// </summary>
        private void ResendBySmsWebService()
        {
            string apiUrl = RxConstants.resendConfirmationCodes;
            try
            {
                ResendConfirmationCodesRequest objInputParam = new ResendConfirmationCodesRequest
                {
                    mail = App.YourDetailsLoginEmail,
                    pin = App.HashPIN,
                    pharmacyid = App.SignUpPharId.ToUpper(),
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

                MessageBox.Show("Sorry, Unable to process your request.");
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
                        objVerifyBySmsViewModel.IsResentPopupOpen = true;
                        objVerifyBySmsViewModel.HitVisibility = false;

                    }
                    else if (objResetPinResponse.status == 324)
                    {
                        objVerifyBySmsViewModel.IsRequestPopupOpen = true;
                        objVerifyBySmsViewModel.HitVisibility = false;
                        objVerifyBySmsViewModel.PopupText = objResetPinResponse.message;
                    }
                }
            }
            catch (Exception)
            {
                objVerifyBySmsViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
               
            }
            
        }

        /// <summary>
        /// VerifyBySmsWebService Method
        /// </summary>
        private void VerifyBySmsWebService()
        {
            string apiUrl = RxConstants.verifyBySms;
            try
            {
                VerifyBySmsRequest objInputParamSms = new VerifyBySmsRequest
                {
                    mail = App.YourDetailsLoginEmail,
                    pin = App.HashPIN,
                    pharmacyid = App.SignUpPharId.ToUpper(),
                    code = objVerifyBySmsViewModel.AuthCode,
                    system_version = "android",
                    app_version = "1.6",
                    branding_version = "MLP"

                };
                WebClient verifywebservicecall = new WebClient();
                var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);
                var json = JsonHelper.Serialize(objInputParamSms);

                verifywebservicecall.Headers["Content-type"] = "application/json";
                verifywebservicecall.UploadStringCompleted += verifywebservicecall_UploadStringCompleted;

                verifywebservicecall.UploadStringAsync(uri, "POST", json);       
            }
            catch (Exception)
            {

                MessageBox.Show("Sorry, Unable to process your request.");
            }
               
        }

        /// <summary>
        /// Method to get response of web service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void verifywebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            VerifyBySmsResponse objVerifyResponse = new VerifyBySmsResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objVerifyResponse = Utils.JsonHelper.Deserialize<VerifyBySmsResponse>(response);

                    if (objVerifyResponse.status != 0)
                    {
                        objVerifyBySmsViewModel.IsRequestPopupOpen = true;
                        objVerifyBySmsViewModel.HitVisibility = false;
                        objVerifyBySmsViewModel.PopupText = objVerifyResponse.message;
                    }
                    else
                    {
                        objVerifyBySmsViewModel.IsVerifiedPopupOpen = true;
                        objVerifyBySmsViewModel.HitVisibility = false;
                    }
                }
            }
            catch (Exception)
            {
                objVerifyBySmsViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
              
            }
            
        }
        
        #endregion
    }
}
