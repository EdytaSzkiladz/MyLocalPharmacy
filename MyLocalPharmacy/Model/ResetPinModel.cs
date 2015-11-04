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
    public class ResetPinModel
    {
        #region Declarations
        public ResetPinLoginViewModel objResetPinLoginViewModel; 
        #endregion

        #region Constructor
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="resetPinLoginViewModel"></param>
        public ResetPinModel(ResetPinLoginViewModel resetPinLoginViewModel)
        {
            objResetPinLoginViewModel = resetPinLoginViewModel;
            ResetPinWebService();
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method to call Reset Pin Webservice
        /// </summary>
        private void ResetPinWebService()
        {
            string apiUrl = RxConstants.resetPin;
            var pinHashed = Utilities.GetSHA256(objResetPinLoginViewModel.DisplaySignUpPin);
            try
            {
                ResetPinRequest objInputParam = new ResetPinRequest
                {
                    mail = objResetPinLoginViewModel.LoginEmail,
                    code = objResetPinLoginViewModel.AuthCode,
                    pin = pinHashed,
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
        /// Response of webservice
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
                        objResetPinLoginViewModel.IsResetPopupOpen = true;
                        objResetPinLoginViewModel.HitVisibility = false;
                        App.resetLoginAuthCode = null;
                        App.resetLoginPIN = null;
                        App.PIN = null;
                        objResetPinLoginViewModel.AuthCode = null;
                        objResetPinLoginViewModel.DisplaySignUpPin = null;
                    }
                    else if (objResetPinResponse.status == 315)
                    {

                        objResetPinLoginViewModel.IsIncorrectPopupOpen = true;
                        objResetPinLoginViewModel.HitVisibility = false;
                    }
                    else if (objResetPinResponse.status == 310)
                    {
                        objResetPinLoginViewModel.IsLoginPinTextBoxVisible = Visibility.Collapsed;
                        objResetPinLoginViewModel.IsNoUserPopupOpen = true;
                        objResetPinLoginViewModel.HitVisibility = false;
                    }

                }
            }
            catch (Exception)
            {
                objResetPinLoginViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
                
            }
        } 
        #endregion
    }
}
