using Microsoft.Phone.Info;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLocalPharmacy.Model
{
    [DataContract]
    public class YourDetailsUpdateModel
    {
        #region Declarations
        public YourDetailsUpdateViewModel objYourDetUpdateViewModel;
        ObservableCollection<string> countries = new ObservableCollection<string>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objYourDetUpdateVM"></param>
        public YourDetailsUpdateModel(YourDetailsUpdateViewModel yourDetailsUpdateViewModel, string dataFor)
        {
            objYourDetUpdateViewModel = yourDetailsUpdateViewModel;
            if (dataFor.Equals("GetData"))
                GetUserDetailsWebService();
            else
                CallSendUserDetailsWebService();
        } 
        #endregion

        #region GetUserDetails 
        /// <summary>
        /// To get the user details while login and display it when "continue" is clciked
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
                        PopulateCountry();
                        objYourDetUpdateViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        App.ObjLgResponse = objlgresponse;
                        if (App.ObjLgResponse != null)
                        {
                            objYourDetUpdateViewModel.NominationStatus = App.ObjLgResponse.payload.status + " in " + App.ObjLgResponse.payload.pharmacyid;
                            string fullname = App.ObjLgResponse.payload.name;
                            string[] splitfullname = fullname.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            objYourDetUpdateViewModel.FirstName = splitfullname[0];
                            objYourDetUpdateViewModel.LastName = splitfullname[1];
                            objYourDetUpdateViewModel.AddressLine1 = App.ObjLgResponse.payload.address1;
                            objYourDetUpdateViewModel.AddressLine2 = App.ObjLgResponse.payload.address2;
                            if (countries != null)
                            {
                                int position = countries.IndexOf(App.ObjLgResponse.payload.country);
                                objYourDetUpdateViewModel.PickerSelectedIndex = position;
                            }
                            objYourDetUpdateViewModel.PostCode = App.ObjLgResponse.payload.postcode;
                            objYourDetUpdateViewModel.DOB = App.ObjLgResponse.payload.birthdate;
                            objYourDetUpdateViewModel.NHS = App.ObjLgResponse.payload.nhs;
                            objYourDetUpdateViewModel.MobileNo = App.ObjLgResponse.payload.phone;
                            objYourDetUpdateViewModel.EmailId = App.ObjLgResponse.payload.mail;
                            objYourDetUpdateViewModel.ButtonValueOnupdate = App.ObjLgResponse.payload.surgery.name;
                            if (string.IsNullOrEmpty(objYourDetUpdateViewModel.ButtonValueOnupdate) || string.IsNullOrWhiteSpace(objYourDetUpdateViewModel.ButtonValueOnupdate))
                            {
                                objYourDetUpdateViewModel.ButtonValueOnupdate = "Choose Doctor for surgery (Optional)";
                                App.SurgeonAddress = string.Empty;
                            }
                            else
                            {
                                App.SurgeonAddress = App.ObjLgResponse.payload.surgery.address;
                            }
                            App.SurgeonSaved = objYourDetUpdateViewModel.ButtonValueOnupdate;

                            if (App.ObjLgResponse.payload.verifyby == "mail")
                            {

                            }
                            if (App.ObjLgResponse.payload.sex == 1)
                            {
                                objYourDetUpdateViewModel.IsMaleSelected = true;
                                objYourDetUpdateViewModel.SelectedForegroundColor = "Black";
                                objYourDetUpdateViewModel.SelectedBackgroundColor = "White";
                            }
                            else
                            {
                                objYourDetUpdateViewModel.IsFemaleSelected = true;
                                objYourDetUpdateViewModel.SelectedForegroundColor = "Black";
                                objYourDetUpdateViewModel.SelectedBackgroundColor = "White";
                            }
                        }
                        App.IsUserRegistered = true;
                    }
                    else
                    {
                        objYourDetUpdateViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objYourDetUpdateViewModel.IsPopupOpen = true;
                        objYourDetUpdateViewModel.PopupText = objlgresponse.message;
                    }
                }
            }
            catch (Exception)
            {
                objYourDetUpdateViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                objYourDetUpdateViewModel.HitVisibility = true;
                
            }
            

        }

        /// <summary>
        /// Method to populate countries
        /// </summary>
        private void PopulateCountry()
        {
            countries.Add("Please Select a Country *");
            countries.Add("England");
            countries.Add("Wales");
            countries.Add("Scotland");
            countries.Add("Northern Ireland");
            objYourDetUpdateViewModel.Listitems = countries;
        }
        #endregion

        #region UpdateUserDetails
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
        private void CallSendUserDetailsWebService()
        {
            objYourDetUpdateViewModel.ProgressBarVisibilty = Visibility.Visible;
            string apiUrl = RxConstants.sendUserDetails;
            string gender = "0";
            try
            {
                if (objYourDetUpdateViewModel.IsMaleSelected)
                {
                    gender = "1";
                }
                else
                {
                    gender = "0";
                }
                string emptyStringValue = "Choose Doctor for surgery (Optional)";
                SendUserDetailsRequest objInputParameters = new SendUserDetailsRequest
                {
                    pharmacyid = App.LoginPharId.ToUpper(),
                    model = DeviceModel,
                    os = "Windows Phone",
                    fullname = objYourDetUpdateViewModel.FirstName + " " + objYourDetUpdateViewModel.LastName,
                    nhs = objYourDetUpdateViewModel.NHS,
                    birthdate = objYourDetUpdateViewModel.DOB,
                    address1 = objYourDetUpdateViewModel.AddressLine1,
                    address2 = objYourDetUpdateViewModel.AddressLine2,
                    postcode = objYourDetUpdateViewModel.PostCode,
                    phone = objYourDetUpdateViewModel.MobileNo,
                    mail = objYourDetUpdateViewModel.EmailId,
                    sex = gender,
                    pin = App.HashPIN,
                    country = objYourDetUpdateViewModel.SelectedCountry,
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

                        objYourDetUpdateViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objYourDetUpdateViewModel.IsSuccessUpdatePopupOpen = true;
                        objYourDetUpdateViewModel.HitVisibility = false;
                        objYourDetUpdateViewModel.SuccessPopupText = objSendUserDetailsResponse.message;
                    }
                    else
                    {
                        objYourDetUpdateViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objYourDetUpdateViewModel.IsPopupOpen = true;
                        objYourDetUpdateViewModel.HitVisibility = false;
                        objYourDetUpdateViewModel.PopupText = objSendUserDetailsResponse.message;
                    }
                    GetUserDetailsWebService();
                }
            }
            catch (Exception)
            {
                objYourDetUpdateViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                objYourDetUpdateViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
            }
            
        }
        #endregion
    }
}
