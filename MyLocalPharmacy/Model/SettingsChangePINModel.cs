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
    public class SettingsChangePINModel
    {
        #region Declarations
        private SettingsConfirmChangePinViewModel objSettingsConfirmChangePinVM;
        string newPinHashed = string.Empty;
        string oldPinHashed = string.Empty; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objSettingsConfirmChangePinViewModel"></param>
        public SettingsChangePINModel(SettingsConfirmChangePinViewModel objSettingsConfirmChangePinViewModel)
        {
            objSettingsConfirmChangePinVM = objSettingsConfirmChangePinViewModel;
            ChangePinWebService();
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Change Pin webservice
        /// </summary>
        private void ChangePinWebService()
        {
            string apiUrl = RxConstants.changePin;
            newPinHashed = Utilities.GetSHA256(objSettingsConfirmChangePinVM.Pin);
            oldPinHashed = Utilities.GetSHA256(App.PIN);
            try
            {
                ChangePinRequest objInputParam = new ChangePinRequest
                {
                    mail = App.LoginEmailId,
                    pin = oldPinHashed,
                    newpin = newPinHashed,
                    system_version = "android",
                    app_version = "1.6",
                    branding_version = "MLP"
                };

                WebClient changepinswebservicecall = new WebClient();
                var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);
                var json = JsonHelper.Serialize(objInputParam);
                changepinswebservicecall.Headers["Content-type"] = "application/json";
                changepinswebservicecall.UploadStringCompleted += ChangePinWebServiceCall_UploadStringCompleted;
                changepinswebservicecall.UploadStringAsync(uri, "POST", json);
            }
            catch (Exception)
            {

                MessageBox.Show("Sorry, Unable to process your request.");
            }
           
        }
        /// <summary>
        /// Response of web service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePinWebServiceCall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            ChangePinResponse objChangePinResponse = new ChangePinResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objChangePinResponse = Utils.JsonHelper.Deserialize<ChangePinResponse>(response);
                    if (objChangePinResponse.status == 0)
                    {
                        App.PIN = objSettingsConfirmChangePinVM.Pin;
                        App.HashPIN = newPinHashed;
                        objSettingsConfirmChangePinVM.IsSuccessPopupOpen = true;
                        objSettingsConfirmChangePinVM.HitVisibility = false;
                        objSettingsConfirmChangePinVM.SuccessPopupText = objChangePinResponse.message;
                    }
                    else
                    {
                        objSettingsConfirmChangePinVM.IsSuccessPopupOpen = true;
                        objSettingsConfirmChangePinVM.HitVisibility = false;
                        objSettingsConfirmChangePinVM.SuccessPopupText = objChangePinResponse.message;
                    }
                }
            }
            catch (Exception)
            {
                objSettingsConfirmChangePinVM.HitVisibility = true;
            
                MessageBox.Show("Sorry, Unable to process your request.");
            } 

        } 
        #endregion
    }
}
