using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLocalPharmacy.Model
{
    [DataContract]
    public class YourDetailswithTCModel
    {
        #region Declarations
        public YourDetailswithTCViewModel objYourDetTCViewModel;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objYourDetTCVM"></param>
        public YourDetailswithTCModel(YourDetailswithTCViewModel objYourDetTCVM, string todo)
        {
            objYourDetTCViewModel = objYourDetTCVM;

            if (todo.Equals("update"))
            {
                CallSendUserDetailsWebService();
            }
            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to get device information
        /// </summary>
        public static string DeviceModel
        {
            get
            {
                string manufacturer = DeviceStatus.DeviceManufacturer;
                string model = string.Empty;
                if (manufacturer.Equals("NOKIA") || manufacturer.Equals("MICROSOFT"))
                {
                    model = manufacturer + " ";
                    string name = DeviceStatus.DeviceName.Substring(0, 6);
                    switch (name)
                    {
                        case "RM-846":
                            model += "Lumia 620";
                            break;
                        case "RM-878":
                            model += "Lumia 810";
                            break;
                        case "RM-824":
                        case "RM-825":
                        case "RM-826":
                            model += "Lumia 820";
                            break;
                        case "RM-845":
                            model += "Lumia 822";
                            break;
                        case "RM-820":
                        case "RM-821":
                        case "RM-822":
                            model += "Lumia 920";
                            break;
                        case "RM-867":
                            model += "Lumia 920T";
                            break;
                        case "RM-994":
                            model += "Lumia 1320";
                            break;
                    }
                }
                else if (manufacturer.Equals("HTC"))
                {
                    string[] partModel = DeviceStatus.DeviceName.Split(' ');
                    model = manufacturer + " " + partModel[2];
                }
                else if (manufacturer.Equals("Huawei"))
                {
                    model = DeviceStatus.DeviceName;
                }
                else if (manufacturer.Equals("Samsung"))
                {
                    model = DeviceStatus.DeviceName;
                }
                return model;
            }
        }


        /// <summary>
        /// Call web service for senduserdetails
        /// </summary>
        private void CallUpdatePushWebService()
        {
            objYourDetTCViewModel.ProgressBarVisibilty = Visibility.Visible;
            string apiUrl = RxConstants.updatePushId;
            
            try
            {
                string deviceUniqueID = string.Empty;
                object deviceID;

                if (DeviceExtendedProperties.TryGetValue("DeviceUniqueId", out deviceID))
                {
                    byte[] myDeviceID = (byte[])deviceID;
                    deviceUniqueID = Convert.ToBase64String(myDeviceID);
                }

                UpdatePushRequest objInputParameters = new UpdatePushRequest
                {
                    pharmacyid = App.LoginPharId.ToUpper(),                    
                    os = "Windows Phone",                    
                    mail = objYourDetTCViewModel.EmailId,                    
                    pin = App.HashPIN,
                    deviceid = deviceUniqueID,
                    pushid = App.ApId,
                    system_version = "android",
                    app_version = "1.6",
                    branding_version = "MLP"
                };
                WebClient updatePushwebservicecall = new WebClient();
                var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

                var json = JsonHelper.Serialize(objInputParameters);
                updatePushwebservicecall.Headers["Content-type"] = "application/json";
                updatePushwebservicecall.UploadStringCompleted += updatePushwebservicecall_UploadStringCompleted;

                updatePushwebservicecall.UploadStringAsync(uri, "POST", json);
            }
            catch (Exception)
            {
                objYourDetTCViewModel.HitVisibility = true;
                objYourDetTCViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                MessageBox.Show("Registering for notification failed.");
            }

        }

        /// <summary>
        /// Get Response from senduserdetails web service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updatePushwebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            UpdatePushResponse objUpdatePushResponse = null;
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objUpdatePushResponse = Utils.JsonHelper.Deserialize<UpdatePushResponse>(response);
                    if (objUpdatePushResponse.status != 0)
                    {                        
                        MessageBox.Show("Registering for notification failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registering for notification failed.");
            }

        }
        

        /// <summary>
        /// Call web service for senduserdetails
        /// </summary>
        private void CallSendUserDetailsWebService()
        {
            objYourDetTCViewModel.ProgressBarVisibilty = Visibility.Visible;
            string apiUrl = RxConstants.sendUserDetails;
            string gender = "0";
            try
            {
                if (objYourDetTCViewModel.IsMaleSelected)
                {
                    gender = "1";
                }
                else
                {
                    gender = "0";
                }
                string deviceUniqueID = string.Empty;
                object deviceID;

                if (DeviceExtendedProperties.TryGetValue("DeviceUniqueId", out deviceID))
                {
                    deviceUniqueID = deviceID.ToString();
                }

                string deviceName = string.Empty;
                object devicename;

                if (DeviceExtendedProperties.TryGetValue("DeviceName", out devicename))
                {
                    deviceName = devicename.ToString();
                }
                string emptyStringValue = "Choose Doctor for surgery (Optional)";
                SendUserDetailsRequest objInputParameters = new SendUserDetailsRequest
                {
                    pharmacyid = App.LoginPharId.ToUpper(),
                    model = DeviceModel,
                    os = "Windows Phone",
                    fullname = objYourDetTCViewModel.FirstName + " " + objYourDetTCViewModel.LastName,
                    nhs = objYourDetTCViewModel.NHS,
                    birthdate = objYourDetTCViewModel.DOB,
                    address1 = objYourDetTCViewModel.AddressLine1,
                    address2 = objYourDetTCViewModel.AddressLine2,
                    postcode = objYourDetTCViewModel.PostCode,
                    phone = objYourDetTCViewModel.MobileNo,
                    mail = objYourDetTCViewModel.EmailId,
                    sex = gender,
                    pin = App.HashPIN,
                    country = objYourDetTCViewModel.SelectedCountry,
                    surgery = new SendUserDetailsRequestSurgery
                    {
                        name = ((!string.IsNullOrEmpty(App.SurgeonSaved)) && (!string.IsNullOrWhiteSpace(App.SurgeonSaved)) && (App.SurgeonSaved != emptyStringValue)) ? App.SurgeonSaved : string.Empty,
                        address = ((!string.IsNullOrEmpty(App.SurgeonAddress)) && (!string.IsNullOrWhiteSpace(App.SurgeonAddress)) && (App.SurgeonSaved != emptyStringValue)) ? App.SurgeonAddress : string.Empty
                    },
                    system_version = "android",
                    app_version = "1.6",
                    branding_version = "MLP"
                };
                WebClient sendUserDetailswebservicecall = new WebClient();
                var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

                var json = JsonHelper.Serialize(objInputParameters);
                sendUserDetailswebservicecall.Headers["Content-type"] = "application/json";
                sendUserDetailswebservicecall.UploadStringCompleted += sendUserDetailswebservicecall_UploadStringCompleted;

                sendUserDetailswebservicecall.UploadStringAsync(uri, "POST", json);
            }
            catch (Exception)
            {
                objYourDetTCViewModel.HitVisibility = true;
                objYourDetTCViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                MessageBox.Show("Sorry, Unable to process your request.");
            }
            
        }

        /// <summary>
        /// Get Response from senduserdetails web service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendUserDetailswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            SendUserDetailsResponse objSendUserDetailsResponse = null;
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objSendUserDetailsResponse = Utils.JsonHelper.Deserialize<SendUserDetailsResponse>(response);
                    if (objSendUserDetailsResponse.status == 0)
                    {
                        CallUpdatePushWebService();

                        App.ObjLgResponse.payload.address1 = objSendUserDetailsResponse.payload.address1;
                        App.ObjLgResponse.payload.address2 = objSendUserDetailsResponse.payload.address2;
                        App.ObjLgResponse.payload.birthdate = objSendUserDetailsResponse.payload.birthdate;
                        App.ObjLgResponse.payload.country = objSendUserDetailsResponse.payload.country;
                        App.ObjLgResponse.payload.devices = objSendUserDetailsResponse.payload.devices;
                        App.ObjLgResponse.payload.mail = objSendUserDetailsResponse.payload.mail;
                        App.ObjLgResponse.payload.mail_confirmed = objSendUserDetailsResponse.payload.mail_confirmed;
                        App.ObjLgResponse.payload.name = objSendUserDetailsResponse.payload.name;
                        App.ObjLgResponse.payload.nhs = objSendUserDetailsResponse.payload.nhs;
                        App.ObjLgResponse.payload.pharmacyid = objSendUserDetailsResponse.payload.pharmacyid;
                        App.ObjLgResponse.payload.pharmacyname = objSendUserDetailsResponse.payload.pharmacyname;
                        App.ObjLgResponse.payload.phone = objSendUserDetailsResponse.payload.phone;
                        App.ObjLgResponse.payload.postcode = objSendUserDetailsResponse.payload.postcode;
                        App.ObjLgResponse.payload.sex = objSendUserDetailsResponse.payload.sex;
                        App.ObjLgResponse.payload.sms_confirmed = objSendUserDetailsResponse.payload.sms_confirmed;
                        App.ObjLgResponse.payload.status = objSendUserDetailsResponse.payload.status;
                        App.ObjLgResponse.payload.surgery.address = objSendUserDetailsResponse.payload.surgery.address;
                        App.ObjLgResponse.payload.surgery.name = objSendUserDetailsResponse.payload.surgery.name;
                        App.ObjLgResponse.payload.verifyby = objSendUserDetailsResponse.payload.verifyby;

                       
                        objYourDetTCViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        if (objYourDetTCViewModel.IsSuccessPopupOpen==false)
                        objYourDetTCViewModel.IsSuccessPopupOpen = true;
                        App.TombStonedPageURL = PageURL.navigateToHomePanoramaURL;
                        objYourDetTCViewModel.HitVisibility = false;
                        objYourDetTCViewModel.SuccessPopupText = objSendUserDetailsResponse.message;
                    }
                   
                    else 
                    {
                        objYourDetTCViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objYourDetTCViewModel.IsPopupOpen = true;
                       
                        objYourDetTCViewModel.HitVisibility = false;
                        objYourDetTCViewModel.PopupText = objSendUserDetailsResponse.message;
                    }
                }
            }
            catch (Exception ex)
            {

                objYourDetTCViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                objYourDetTCViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
            }

        }
        
        
        #endregion
    }
}
