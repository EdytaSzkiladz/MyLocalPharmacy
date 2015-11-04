using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLocalPharmacy.Model
{
    public class ConfirmRepeatModel
    {
        #region Declarations
        ConfirmRepeatViewModel confirmRepeatViewModel = new ConfirmRepeatViewModel(); 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_objConfirmRepeatViewModel"></param>
        public ConfirmRepeatModel(ConfirmRepeatViewModel _objConfirmRepeatViewModel)
        {
            confirmRepeatViewModel = _objConfirmRepeatViewModel;
            CallWebservice();
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method to call the service
        /// </summary>
        private void CallWebservice()
        {
            string apiUrl = RxConstants.sendOrder;

            SendOrderRequest objInputParam = new SendOrderRequest
            {
                pharmacyid = App.LoginPharId.ToUpper(),
                mail = App.LoginEmailId,

                pin = App.HashPIN,
                prescriptions = App.prescriptionCollection.ToList(),
                system_version = "android",
                app_version = "1.6",
                branding_version = "MLP",
                drugs_hash = ((!string.IsNullOrWhiteSpace(App.DrugDBHash)) && (!string.IsNullOrEmpty(App.DrugDBHash))) ? App.DrugDBHash : string.Empty
            };

            WebClient sendOrderWebserviceCall = new WebClient();
            var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);
            var json = JsonHelper.Serialize(objInputParam);
            sendOrderWebserviceCall.Headers["Content-type"] = "application/json";
            sendOrderWebserviceCall.UploadStringCompleted += SendOrderwebservicecall_UploadStringCompleted;
            sendOrderWebserviceCall.UploadStringAsync(uri, "POST", json);
        }

        /// <summary>
        /// Response of webservice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendOrderwebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            SendOrderResponse objSendOrderResponse = new SendOrderResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objSendOrderResponse = Utils.JsonHelper.Deserialize<SendOrderResponse>(response);

                    if (objSendOrderResponse.status == 0)
                    {
                        App.TombStonedPageURL ="RequestSent";
                        confirmRepeatViewModel.IsPopupSentOpen = true;
                        confirmRepeatViewModel.HitVisibility = false;
                        App.prescriptionCollection.Clear();
                    }

                }
            }
            catch (Exception)
            {
                confirmRepeatViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
            }
           
        }
        #endregion
    }
}
