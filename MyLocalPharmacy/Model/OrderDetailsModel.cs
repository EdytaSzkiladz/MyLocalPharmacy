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
    public class OrderDetailsModel
    {
        #region Declarations
        OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel(); 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_objOrderDetailsViewModel"></param>
        /// <param name="commandStatus"></param>
        public OrderDetailsModel(OrderDetailsViewModel _objOrderDetailsViewModel, string commandStatus)
        {
            orderDetailsViewModel = _objOrderDetailsViewModel;
            if (commandStatus.Equals("cancel"))
            {
                CallWebserviceCancel();
            }
            else
            {
                CallWebserviceReorder();
            }

        } 
        #endregion

        #region Methods
        /// <summary>
        /// Webservice call to cancel order
        /// </summary>
        private void CallWebserviceCancel()
        {
            string apiUrl = RxConstants.SetOrderStatus;

            SetOrderStatusRequest objInputParam = new SetOrderStatusRequest
            {

                mail = App.LoginEmailId,

                pin = App.HashPIN,
                orderid = App.selectedOrder.id,
                status = RxConstants.SetOrderStatusCancelled,
                system_version = "android",
                app_version = "1.6",
                branding_version = "MLP"
            };

            WebClient cancelOrderWebserviceCall = new WebClient();
            var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);
            var json = JsonHelper.Serialize(objInputParam);
            cancelOrderWebserviceCall.Headers["Content-type"] = "application/json";
            cancelOrderWebserviceCall.UploadStringCompleted += CancelOrderwebservicecall_UploadStringCompleted;
            cancelOrderWebserviceCall.UploadStringAsync(uri, "POST", json);
        }

        /// <summary>
        /// Webservice call to reorder
        /// </summary>
        private void CallWebserviceReorder()
        {
            string apiUrl = RxConstants.sendOrder;
            App.prescriptionCollection = new PrescriptionCollection();
            Prescription prescription = new Prescription();

            prescription.drugname = App.selectedOrder.drugname;
            prescription.reason = orderDetailsViewModel.ReasonForOrdering;
            prescription.quantity = App.selectedOrder.quantity;
            prescription.amp = App.selectedOrder.amp;
            prescription.ampp = App.selectedOrder.ampp;
            prescription.vmp = App.selectedOrder.vmp;
            prescription.vmpp = App.selectedOrder.vmpp;

            App.prescriptionCollection.Add(prescription);

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
        /// Response of cancel-webservice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelOrderwebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            SetOrderStatusResponse objcancelOrderResponse = new SetOrderStatusResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objcancelOrderResponse = Utils.JsonHelper.Deserialize<SetOrderStatusResponse>(response);
                    if (objcancelOrderResponse.status == 0)
                    {
                        orderDetailsViewModel.IsPopupCancelledOpen = true;

                        orderDetailsViewModel.PopupCancelText = "Order Cancelled";
                        orderDetailsViewModel.HitVisibility = false;
                    }
                }
            }
            catch (Exception)
            {
                orderDetailsViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
               
            }
           
        }

        /// <summary>
        /// Response of reorder
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
                        orderDetailsViewModel.IsPopupCancelledOpen = true;
                        orderDetailsViewModel.PopupCancelText = "Re-Ordered";
                        App.prescriptionCollection.Clear();
                        orderDetailsViewModel.HitVisibility = false;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No Internet Connectivity.");
            }
            
        } 
        #endregion
    
    }
}
