using Microsoft.Phone.Controls;
using MyLocalPharmacy.Contract;
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
using System.Windows.Media;

namespace MyLocalPharmacy.Model
{
    public class GetAllOrdersModel
    {
        #region Declarations
        HomePanoramaViewModel homePanoramaViewModel;
        AccountDisabledViewModel accountDisabledViewModel;
        string statusText;
        string StatusFontColour;
       
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_objHomePanoramaViewModel"></param>
        public GetAllOrdersModel(HomePanoramaViewModel _objHomePanoramaViewModel)
        {
            homePanoramaViewModel = _objHomePanoramaViewModel;
           
            GetUserDetailsWebService();
            
        }
        /// <summary>
        /// Constructor Overload
        /// </summary>
        /// <param name="_objHomePanoramaViewModel"></param>
        /// <param name="IdsToDelete"></param>
        public GetAllOrdersModel(HomePanoramaViewModel _objHomePanoramaViewModel,List<int> IdsToDelete)
        {
            homePanoramaViewModel = _objHomePanoramaViewModel;
            CallWebserviceDelete(IdsToDelete);
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method to call user details service
        /// </summary>
        private void GetUserDetailsWebService()
        {
            string apiUrl = RxConstants.getUserDetails;
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



        /// <summary>
        /// Web service call for getting all orders.
        /// </summary>
        private void CallWebservice()
        {
            string apiUrl = RxConstants.getAllOrders;

            GetAllOrdersRequest objInputParam = new GetAllOrdersRequest
            {
                mail = App.LoginEmailId,
                pin = App.HashPIN,
                system_version = "android",
                app_version = "1.6",
                branding_version = "MLP"
            };

            WebClient getAllOrdersWebserviceCall = new WebClient();
            var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);
            var json = JsonHelper.Serialize(objInputParam);
            getAllOrdersWebserviceCall.Headers["Content-type"] = "application/json";
            getAllOrdersWebserviceCall.UploadStringCompleted += GetAllOrderswebservicecall_UploadStringCompleted;
            getAllOrdersWebserviceCall.UploadStringAsync(uri, "POST", json);
        }




        /// <summary>
        /// Web service call for delete order(s).
        /// </summary>
        private void CallWebserviceDelete(List<int> IdsToDelete)
        {
            string apiUrl = RxConstants.removeOrder;

            RemoveOrderRequest objInputParam = new RemoveOrderRequest
            {
                mail = App.LoginEmailId,
                pin = App.HashPIN,
                orderid = IdsToDelete,
                system_version = "android",
                app_version = "1.6",
                branding_version = "MLP"
            };

            WebClient DeleteWebserviceCall = new WebClient();
            var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);
            var json = JsonHelper.Serialize(objInputParam);
            DeleteWebserviceCall.Headers["Content-type"] = "application/json";
            DeleteWebserviceCall.UploadStringCompleted += DeleteWebserviceCallwebservicecall_UploadStringCompleted;
            DeleteWebserviceCall.UploadStringAsync(uri, "POST", json);
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
                    App.ObjLgResponse = objlgresponse;
                    CheckForDisabledAccount();                    
                    
                }
            }
            catch (Exception)
            {
                CheckForDisabledAccount();             
                
            }


        }
        
        /// <summary>
        /// Response of get all orders webservice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetAllOrderswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            GetAllOrdersResponse objgetAllOrdersResponse = new GetAllOrdersResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objgetAllOrdersResponse = Utils.JsonHelper.Deserialize<GetAllOrdersResponse>(response);
                    if (objgetAllOrdersResponse.status == 0)
                    {
                        FillList(objgetAllOrdersResponse);
                    }
                }
            }
            catch (Exception)
            {
                homePanoramaViewModel.ProgressBarVisibiltyOrderRepeat = System.Windows.Visibility.Collapsed;
            }
            finally
            {
                homePanoramaViewModel.HitVisibilityRepeat = true;
            }
            
        }

        /// <summary>
        /// Method to check for disabled account
        /// </summary>
        private void CheckForDisabledAccount()
        {
            if (App.ObjLgResponse != null)
            {
                if (App.ObjLgResponse.status == 0)
                {
                    if (App.ObjLgResponse.payload.status.Equals(RxConstants.userStatusApproved))
                    {
                        homePanoramaViewModel.StplWaitingApprovalVisibility = Visibility.Collapsed;
                        homePanoramaViewModel.StplApprovedVisibility = Visibility.Visible;
                        CallWebservice();
                    }

                    else if (App.ObjLgResponse.payload.status.Equals(RxConstants.userStatusRejected))
                    {
                        App.IsFromRejected = true;
                        homePanoramaViewModel.IsRejectedPopupOpen = true;
                        homePanoramaViewModel.PopupRejectedText = "Request has been rejected by pharmacy";
                        homePanoramaViewModel.HitVisibility = false;
                        homePanoramaViewModel.ProgressBarVisibiltyOrderRepeat = Visibility.Collapsed;
                    }

                    else if (App.ObjLgResponse.payload.status.Equals(RxConstants.userStatusRemoved))
                    {
                        
                        PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                        frame.Navigate(new Uri(PageURL.navigateToAccountDisabledURL + "?message=" + App.ObjLgResponse.message + "&title=account removed", UriKind.Relative));
                        
                    }

                    else
                    {
                        homePanoramaViewModel.StplWaitingApprovalVisibility = Visibility.Visible;
                        homePanoramaViewModel.StplApprovedVisibility = Visibility.Collapsed;
                        homePanoramaViewModel.ProgressBarVisibiltyOrderRepeat = Visibility.Collapsed;
                    }
                }

                else if (App.ObjLgResponse.status == 301)
                {
                    homePanoramaViewModel.IsPopupOpen = true;
                    homePanoramaViewModel.PopupText = "Your PIN has been changed.";
                    homePanoramaViewModel.HitVisibility = false;
                }
                else
                {
                    PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                    frame.Navigate(new Uri(PageURL.navigateToAccountDisabledURL + "?message=" + App.ObjLgResponse.message + "&title=account disabled", UriKind.Relative));
                }
                homePanoramaViewModel.HitVisibilityRepeat = true;
            }
        }

        /// <summary>
        /// Method to Fill ListBox Itemsource
        /// </summary>
        /// <param name="objgetAllOrdersResponse"></param>
        private void FillList(GetAllOrdersResponse objgetAllOrdersResponse)
        {
            OrderedPillDetails orderedPillDetails;
            App.OrderedPillDetailsCollection = new OrderedPillDetailsCollection();

            foreach (var item in objgetAllOrdersResponse.payload.prescriptions)
            {
                foreach (var pres in item.pres)
                {
                    DateTime dateTimeUpdated = Utilities.UnixToDateTime(pres.updated);
                    dateTimeUpdated = dateTimeUpdated.ToLocalTime();
                    DateTime dateTimeCreated = Utilities.UnixToDateTime(pres.created);
                    var kind = dateTimeCreated.Kind;
                    dateTimeCreated = dateTimeCreated.ToLocalTime();
                    ChangeStatusText(pres.status);

                    string orderDate = dateTimeCreated.DayOfWeek + ", " + dateTimeCreated.Day + " " + dateTimeCreated.ToString("MMMM") + " " + dateTimeCreated.Year;

                    orderedPillDetails = new OrderedPillDetails();
                    orderedPillDetails.amp = pres.amp;
                    orderedPillDetails.ampp = pres.ampp;
                    orderedPillDetails.created = dateTimeCreated;
                    orderedPillDetails.drugname = pres.drugname;
                    orderedPillDetails.id = pres.id;
                    orderedPillDetails.quantity = pres.quantity;
                    orderedPillDetails.reason = pres.reason;
                    orderedPillDetails.status = statusText;
                    orderedPillDetails.updated = dateTimeUpdated;
                    orderedPillDetails.vmp = pres.vmp;
                    orderedPillDetails.vmpp = pres.vmpp;
                    orderedPillDetails.timeRange = string.Empty;
                    orderedPillDetails.time = dateTimeUpdated.ToString("h:mm tt");
                    orderedPillDetails.orderDate = orderDate;
                    orderedPillDetails.orderTime = dateTimeCreated;
                    orderedPillDetails.StatusFontColour = StatusFontColour;

                    App.OrderedPillDetailsCollection.Add(orderedPillDetails);
                }

            }

            homePanoramaViewModel.OrderedPillCollection = App.OrderedPillDetailsCollection;
            homePanoramaViewModel.ProgressBarVisibiltyOrderRepeat = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Response of delete orders webservice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteWebserviceCallwebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            GetAllOrdersResponse objRemoveOrdersResponse = new GetAllOrdersResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objRemoveOrdersResponse = Utils.JsonHelper.Deserialize<GetAllOrdersResponse>(response);
                    if (objRemoveOrdersResponse.status == 0)
                    {
                        FillList(objRemoveOrdersResponse);
                    }
                    homePanoramaViewModel.IsRemovePopupOpen = true;
                    homePanoramaViewModel.HitVisibility = false;
                    homePanoramaViewModel.PopupRemoveText = "Order(s) removed.";
                }
            }
            catch (Exception)
            {
                homePanoramaViewModel.ProgressBarVisibiltyOrderRepeat = System.Windows.Visibility.Collapsed;
            }
           
        }

        /// <summary>
        /// Method to change the status message text
        /// </summary>
        /// <param name="_statusText"></param>
        /// <returns></returns>
        public void ChangeStatusText(string _statusText)
        {
            switch (_statusText)
            {
                case RxConstants.OrderStatusSubmitted:
                    {
                        statusText = RxConstants.OrderStatusToSubmitted;
                        StatusFontColour = "#FFF3B200";
                    }
                    break;
                case RxConstants.OrderStatusCancelled:
                    {
                        statusText = RxConstants.OrderStatusToCancelled;
                        StatusFontColour = "#FFFF0000";
                    }
                    break;
                case RxConstants.OrderStatusAcknowledged:
                    {
                        statusText = RxConstants.OrderStatusToAcknowledged;
                        StatusFontColour = "#FF77B900";
                    }
                    break;
                case RxConstants.OrderStatusCollected:
                    {
                        statusText = RxConstants.OrderStatusToCollected;
                        StatusFontColour = "#FF77B900";
                    }
                    break;
                case RxConstants.OrderStatusDelivered:
                    {
                        statusText = RxConstants.OrderStatusToDelivered;
                        StatusFontColour = "#FF77B900";
                    }
                    break;


            }
        }
        #endregion
    }
}
