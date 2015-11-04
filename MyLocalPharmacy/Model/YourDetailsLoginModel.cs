using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MyLocalPharmacy.ViewModel;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.Entities;
using System.Net;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using System.Runtime.Serialization;
using System.Globalization;
namespace MyLocalPharmacy.Model
{
    [DataContract]
    public  class YourDetailsLoginModel
    {
        #region Declarations
        public YourDetailsLoginViewModel objYourDetlginViewModel;
        string verifyBy = "mail"; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objYourDetloginVM"></param>
        public YourDetailsLoginModel(YourDetailsLoginViewModel objYourDetloginVM)
        {
            objYourDetlginViewModel = objYourDetloginVM;
            CallSendNominationWebService();
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Call web service for send nomination
        /// </summary>
        private void CallSendNominationWebService()
        {
            
            objYourDetlginViewModel.ProgressBarVisibilty = Visibility.Visible;
            string apiUrl = RxConstants.sendNomination;
            try
            {
                string gender = "0";
                if (objYourDetlginViewModel.IsMaleSelected)
                {
                    gender = "1";
                }
                else
                {
                    gender = "0";
                }


                if (objYourDetlginViewModel.IsMailSelected)
                {
                    verifyBy = "mail";
                }
                else
                {
                    verifyBy = "sms";
                }

                string deviceUniqueID = string.Empty;
                object deviceID;

                if (DeviceExtendedProperties.TryGetValue("DeviceUniqueId", out deviceID))
                {
                    byte[] myDeviceID = (byte[])deviceID;
                    deviceUniqueID = Convert.ToBase64String(myDeviceID);
                }

                string deviceName = string.Empty;
                object devicename;

                if (DeviceExtendedProperties.TryGetValue("DeviceName", out devicename))
                {
                    deviceName = devicename.ToString();
                }
                string emptyStringValue = "Choose Doctor for surgery (Optional)";
                SendNominationRequest objInputParameters = new SendNominationRequest
                {
                    pharmacyid = App.SignUpPharId.ToUpper(),
                    deviceid = deviceUniqueID,
                    model = DeviceModel,
                    os = "Windows Phone",
                    pushid = App.ApId,
                    fullname = objYourDetlginViewModel.FirstName + " " + objYourDetlginViewModel.LastName,
                    nhs = objYourDetlginViewModel.NHS,
                    birthdate = objYourDetlginViewModel.DOB.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    address1 = objYourDetlginViewModel.AddressLine1,
                    address2 = objYourDetlginViewModel.AddressLine2,
                    postcode = objYourDetlginViewModel.PostCode,
                    phone = objYourDetlginViewModel.MobileNo,
                    mail = objYourDetlginViewModel.EmailId,
                    sex = gender,
                    pin = App.HashPIN,
                    country = objYourDetlginViewModel.SelectedCountry,
                    mode = "new",
                    verifyby = verifyBy,
                    surgery = new SendNominationRequestSurgery
                    {
                        name = ((!string.IsNullOrEmpty(App.SelectedSurgen)) && (!string.IsNullOrWhiteSpace(App.SelectedSurgen)) && (App.SelectedSurgen != emptyStringValue)) ? App.SelectedSurgen : string.Empty,

                        address = ((!string.IsNullOrEmpty(App.SurgeonAddress)) && (!string.IsNullOrWhiteSpace(App.SurgeonAddress)) && (App.SelectedSurgen != emptyStringValue)) ? App.SurgeonAddress : string.Empty
                    },
                    system_version = "android",
                    app_version = "1.6",
                    branding_version = "MLP"
                };
                App.SelectedSurgen = string.Empty;
                App.SurgeonAddress = string.Empty;
                WebClient sendNominationswebservicecall = new WebClient();
                var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

                var json = JsonHelper.Serialize(objInputParameters);
                sendNominationswebservicecall.Headers["Content-type"] = "application/json";
                sendNominationswebservicecall.UploadStringCompleted += sendNominationswebservicecall_UploadStringCompleted;

                sendNominationswebservicecall.UploadStringAsync(uri, "POST", json);
            }
            catch (Exception)
            {

                MessageBox.Show("Sorry, Unable to process your request.");
            }
           
        }

        /// <summary>
        /// Method to get device information
        /// </summary>
        public static string DeviceModel
        {
            get
            {
                string manufacturer = DeviceStatus.DeviceManufacturer;
                string model = string.Empty;
                if (manufacturer.Equals("NOKIA")||manufacturer.Equals("MICROSOFT"))
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
        /// Get response from the web service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendNominationswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            SendNominationResponse objSendNominationResponse = null;
           
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objSendNominationResponse = Utils.JsonHelper.Deserialize<SendNominationResponse>(response);

                    if (objSendNominationResponse.status == 0)
                    {
                        App.LoginPharId = App.SignUpPharId.ToUpper();
                        App.YourDetailsLoginEmail = objYourDetlginViewModel.EmailId;
                        App.LoginEmailId = objYourDetlginViewModel.EmailId;
                        App.IsUserRegistered = true;
                        if (App.ObjLgResponse == null)
                        { App.ObjLgResponse = new LoginResponse(); }
                        if (App.ObjLgResponse.payload == null)
                        { App.ObjLgResponse.payload = new Payload(); }
                        if (App.ObjLgResponse.payload.surgery == null)
                        { App.ObjLgResponse.payload.surgery = new Surgery(); }
                       
                            App.ObjLgResponse.payload.address1 = objSendNominationResponse.payload.address1;
                            App.ObjLgResponse.payload.address2 = objSendNominationResponse.payload.address2;
                            App.ObjLgResponse.payload.birthdate = objSendNominationResponse.payload.birthdate;
                            App.ObjLgResponse.payload.country = objSendNominationResponse.payload.country;
                            App.ObjLgResponse.payload.devices = objSendNominationResponse.payload.devices;
                            App.ObjLgResponse.payload.mail = objSendNominationResponse.payload.mail;
                            App.ObjLgResponse.payload.mail_confirmed = objSendNominationResponse.payload.mail_confirmed;
                            App.ObjLgResponse.payload.name = objSendNominationResponse.payload.name;
                            App.ObjLgResponse.payload.nhs = objSendNominationResponse.payload.nhs;
                            App.ObjLgResponse.payload.pharmacyid = objSendNominationResponse.payload.pharmacyid;
                            App.ObjLgResponse.payload.pharmacyname = objSendNominationResponse.payload.pharmacyname;
                            App.ObjLgResponse.payload.phone = objSendNominationResponse.payload.phone;
                            App.ObjLgResponse.payload.postcode = objSendNominationResponse.payload.postcode;
                            App.ObjLgResponse.payload.sex = objSendNominationResponse.payload.sex;
                            App.ObjLgResponse.payload.sms_confirmed = objSendNominationResponse.payload.sms_confirmed;
                            App.ObjLgResponse.payload.status = objSendNominationResponse.payload.status;
                            App.ObjLgResponse.payload.surgery.address = objSendNominationResponse.payload.surgery.address;
                            App.ObjLgResponse.payload.surgery.name = objSendNominationResponse.payload.surgery.name;
                            App.ObjLgResponse.payload.verifyby = objSendNominationResponse.payload.verifyby;
                       
                        
                        
                        GetAdvtImages();
                    }
                    
                    else
                    {
                        objYourDetlginViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objYourDetlginViewModel.IsPopupOpen = true;
                        objYourDetlginViewModel.HitVisibility = false;
                        objYourDetlginViewModel.PopupText = objSendNominationResponse.message;
                    }
                }
            }
            catch (Exception ex)
            {
                objYourDetlginViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                objYourDetlginViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
               
            }
           
        }

        /// <summary>
        /// Method to call the web service for advertisememnt images
        /// </summary>
        private void GetAdvtImages()
        {
            string apiUrl = RxConstants.getPharmacyInformations;
            GetPharmacyInformationRequest objInputParameters = new GetPharmacyInformationRequest
            {
                Pharmacyid = App.LoginPharId.ToUpper(),
                Deviceid = string.Empty,
                Model = string.Empty,
                Os = string.Empty,
                Branding_hash = string.Empty,
                Advert_hash = string.Empty,
                Drugs_hash = string.Empty,
                system_version = "android",
                app_version = "1.6",
                branding_version = "MLP"
            };

            WebClient pharmacyinfoadvtimageswebservicecall = new WebClient();
            var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

            var json = JsonHelper.Serialize(objInputParameters);
            pharmacyinfoadvtimageswebservicecall.Headers["Content-type"] = "application/json";
            pharmacyinfoadvtimageswebservicecall.UploadStringCompleted += pharmacyinfoadvtimageswebservicecall_UploadStringCompleted;

            pharmacyinfoadvtimageswebservicecall.UploadStringAsync(uri, "POST", json);
        }

        /// <summary>
        /// Method to get the response for advertisement images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pharmacyinfoadvtimageswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            GetPharmacyInformationResponse objPhBrandingInfoResponse = null;
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objPhBrandingInfoResponse = Utils.JsonHelper.Deserialize<GetPharmacyInformationResponse>(response);
                    if ((objPhBrandingInfoResponse.payload != null) && (objPhBrandingInfoResponse.status == 0))
                    {
                        App.ObjBrandingResponse = objPhBrandingInfoResponse;
                        App.DrugDBHash = objPhBrandingInfoResponse.payload.drugs_hash;
                        App.AdImages = null;
                        foreach (var item in objPhBrandingInfoResponse.payload.advert_data)
                        {
                            if (App.AdImages == null)
                            {
                                App.AdImages = new List<string>();
                                App.AdImages.Add(item.image_url.Replace("https", "http"));
                            }
                            else
                            {
                                App.AdImages.Add(item.image_url.Replace("https", "http"));
                            }
                        }

                        objYourDetlginViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objYourDetlginViewModel.IsSuccessPopupOpen = true;
                        objYourDetlginViewModel.HitVisibility = false;
                        objYourDetlginViewModel.SuccessPopupText = "Patient has been created.";// "Patient created.";
                       
                    }
                }
            }
            catch (Exception)
            {
                objYourDetlginViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                objYourDetlginViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
            }
            
        }
        #endregion
    }
}
