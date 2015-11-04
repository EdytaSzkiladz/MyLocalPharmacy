using MyLocalPharmacy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.Entities;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media;
using System.IO;

namespace MyLocalPharmacy.Model
{
    [DataContract]
    public class SignUpModel
    {
        #region Declarations
        public SignUpViewModel objSignUpViewModel;
        GetPharmacyInformationRequest objInputParameters;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for login screen
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <param name="forlogin"></param>
        public SignUpModel(SignUpViewModel loginViewModel, string forlogin)
        {
            objSignUpViewModel = loginViewModel;
            switch (forlogin)
            {
                case "Loginscreen":
                    BrandingInfoWebService();
                    break;
                case "CheckPharmacyId":
                    CheckPharmacyIdWebservice();
                    break;
                case "Changepharmacy":
                    CallSendNominationWebService();
                    break;
                default:
                    ResetPinWebService();
                    break;
            }
            
        }
        /// <summary>
        /// For SignUp Screen get details
        /// </summary>
        /// <param name="signUpViewModel"></param>
        public SignUpModel(SignUpViewModel signUpViewModel)
        {
            objSignUpViewModel = signUpViewModel;
            CallGetPharmacyInformationWebService();
        }
        

 
        #endregion

        #region Login Methods
        /// <summary>
        /// Web service to get the branding info
        /// </summary>
        private void BrandingInfoWebService()
        {
            try
            {
                string apiUrl = RxConstants.getPharmacyInformations;
                objInputParameters = new GetPharmacyInformationRequest
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
                WebClient pharmacyinfoforbrandingwebservicecall = new WebClient();
                var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

                var json = JsonHelper.Serialize(objInputParameters);
                pharmacyinfoforbrandingwebservicecall.Headers["Content-type"] = "application/json";
                pharmacyinfoforbrandingwebservicecall.UploadStringCompleted += pharmacyinfoforbrandingwebservicecall_UploadStringCompleted;

                pharmacyinfoforbrandingwebservicecall.UploadStringAsync(uri, "POST", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, Unable to process your request.");  
                
            }
            
        }

        /// <summary>
        /// Response for Branding Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pharmacyinfoforbrandingwebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
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

                        App.BrandingHash = objPhBrandingInfoResponse.payload.branding_hash;
                        App.AdvtHash = objPhBrandingInfoResponse.payload.advert_hash;
                        App.DrugsData = objPhBrandingInfoResponse.payload.drugs_data;
                        App.DrugDBHash = objPhBrandingInfoResponse.payload.drugs_hash;
                        App.AddImages = null;
                        foreach (var item in objPhBrandingInfoResponse.payload.advert_data)
                        {
                            if (App.AddImages == null)
                            {
                                App.AddImages = new List<BitmapImage>();
                                App.ImagesUrl = new List<string>();
                                App.ImagesName = new List<string>();
                            }
                            if (item.image_url != null)
                            {
                                Uri uri = new Uri(item.image_url.Replace("https", "http"));
                                BitmapImage bmi = new BitmapImage(uri);
                                App.ImagesUrl.Add(item.url);
                                App.ImagesName.Add(item.name);
                                App.AddImages.Add(bmi);
                            }
                            else
                            {
                                Color backgroundColor = ConvertStringToColor(item.image_builder.background_color);
                                Color foreroundColor2 = ConvertStringToColor(item.image_builder.font_color);
                                SolidColorBrush backgroundBrush = new SolidColorBrush(backgroundColor);
                                SolidColorBrush foregroundBrush = new SolidColorBrush(foreroundColor2);
                                string text = item.image_builder.line1 + Environment.NewLine +Environment.NewLine + item.image_builder.line2;
                                WriteableBitmap bmpSmall = new WriteableBitmap(200, 120);

                                Grid grid = new Grid();
                                grid.Width = bmpSmall.PixelWidth;
                                grid.Height = bmpSmall.PixelHeight;

                                var background = new Canvas();
                                background.Width = bmpSmall.PixelWidth;
                                background.Height = bmpSmall.PixelHeight;
                                background.Background = backgroundBrush;
                                var textBlock = new TextBlock();
                                textBlock.Width = bmpSmall.PixelWidth;
                                textBlock.FontFamily = new FontFamily("Segoe WP Light");
                                textBlock.Text = text;
                                textBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
                                textBlock.VerticalAlignment = VerticalAlignment.Center;
                                textBlock.FontSize = 12;
                                textBlock.TextWrapping = TextWrapping.Wrap;
                                textBlock.Foreground = foregroundBrush;
                                textBlock.TextAlignment = TextAlignment.Center;
                                grid.Children.Add(background);
                                grid.Children.Add(textBlock);
                                grid.Measure(new Size(bmpSmall.PixelWidth, bmpSmall.PixelHeight));
                                grid.Arrange(new Rect(0, 0, bmpSmall.PixelWidth, bmpSmall.PixelHeight));
                                grid.UpdateLayout();
                                bmpSmall.Render(grid, null);
                                bmpSmall.Invalidate();

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    bmpSmall.SaveJpeg(ms, 200, 120, 0, 100);
                                    BitmapImage bmp = new BitmapImage();
                                    bmp.SetSource(ms);
                                    App.ImagesUrl.Add(item.url);
                                    App.ImagesName.Add(item.name);
                                    App.AddImages.Add(bmp);
                                }  
                            }
                        }
                    }
                    LoginUserDetailsWebService();
                }
            }
            catch (Exception ex)
            {
                objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                objSignUpViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
               
            }
            
        }

        public Color ConvertStringToColor(String hex)
        {
            //remove the # at the front
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// To check user emailid validity for resetpin
        /// </summary>
        private void ResetPinWebService()
        {
            string apiUrl = RxConstants.sendResetPinCode;
            try
            {
                SendResetPinCodeRequest objInputParam = new SendResetPinCodeRequest
                {
                    mail = objSignUpViewModel.LoginEmail,
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
        /// Check the response for the emailid in resetpin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetpinswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            SendResetPinCodeResponse objresetpincoderesponse = new SendResetPinCodeResponse();
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objresetpincoderesponse = Utils.JsonHelper.Deserialize<SendResetPinCodeResponse>(response);
                    if (objresetpincoderesponse.status == 0)
                    {
                        objSignUpViewModel.IsConfirmPopupOpen = false;
                        objSignUpViewModel.HitVisibility = true;
                        PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                        frame.Navigate(new Uri(PageURL.navigateToResetPinLoginURL, UriKind.Relative));
                    }
                    else if (objresetpincoderesponse.status == 310)
                    {
                        objSignUpViewModel.IsConfirmPopupOpen = false;
                        objSignUpViewModel.IsPopupOpen = true;
                        objSignUpViewModel.PopupText = "No such user.";
                    }
                    else if (objresetpincoderesponse.status == 318)
                    {
                        objSignUpViewModel.IsConfirmPopupOpen = false;
                        objSignUpViewModel.IsWaitPopupOpen = true;
                        objSignUpViewModel.HitVisibility = false;
                    }
                    else
                    {
                        objSignUpViewModel.IsConfirmPopupOpen = false;
                        objSignUpViewModel.IsPopupOpen = true;
                        objSignUpViewModel.PopupText = objresetpincoderesponse.message;
                    }
                    objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                objSignUpViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");     
            }
        }

        /// <summary>
        /// To get the user details while login and display it when "continue" is clciked
        /// </summary>
        private void LoginUserDetailsWebService()
        {                       
            string apiUrl = RxConstants.getUserDetails;
            LoginParameters objLoginparameters = new LoginParameters
            {
                Mail = objSignUpViewModel.LoginEmail,
                Pharmacyid = objSignUpViewModel.LoginPharmacyID.ToUpper(),
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
                        objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        App.ObjLgResponse = objlgresponse;

                        App.IsUserRegistered = true;
                        App.IsPageUpdateYourDetailsafterLogin = true;
                        App.YourDetailsLoginEmail = objSignUpViewModel.LoginEmail;
                        App.SignUpPharId = objSignUpViewModel.LoginPharmacyID;
                        PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                        if (objlgresponse.payload.status.Equals("Registered"))
                        {
                            if (objlgresponse.payload.verifyby.Equals("sms"))
                            {
                                frame.Navigate(new Uri(PageURL.navigateToVerificationBySMSURL, UriKind.Relative));
                            }
                            else if (objlgresponse.payload.verifyby.Equals("mail"))
                            {
                                frame.Navigate(new Uri(PageURL.navigateToVerificationByEmailURL, UriKind.Relative));
                            }
                        }
                        else if ((objlgresponse.payload.status != "Rejected") && (App.IsPageYourDetailsafterLoginSaved))
                            frame.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.Relative));
                        else if ((objlgresponse.payload.status != "Rejected") && (App.IsPageUpdateYourDetailsafterLogin))
                            frame.Navigate(new Uri(PageURL.navigateToYourDetailswithTCURL, UriKind.Relative));
                        else
                        {
                            objSignUpViewModel.IsPopupOpen = true;
                            objSignUpViewModel.PopupText = objlgresponse.message;
                            objSignUpViewModel.HitVisibility = false;
                        }
                    }
                   
                    else if (objlgresponse.status == 301)
                    {
                        App.PIN = string.Empty;
                        App.HashPIN = string.Empty;
                        objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objSignUpViewModel.IsIncorrectPinPopupOpen = true;

                        objSignUpViewModel.IncorrectPinPopupText = objlgresponse.message;
                    }
                    
                    else if (objlgresponse.status == 314)
                    {
                        App.ObjLgResponse = objlgresponse;

                        objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objSignUpViewModel.IsPopupOpen = true;
                        objSignUpViewModel.PopupText = objlgresponse.message;
                    }
                    
                    else if (objlgresponse.status == 308)
                    {
                        App.ObjLgResponse = objlgresponse;
                        string displayTextonPopUp = string.Concat("You are about to change pharmacy.\nAll your orders from the previous pharmacy : ", App.ObjLgResponse.payload.pharmacyid, " will be automatically declined.\nAre you sure you want to proceed?");
                        objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objSignUpViewModel.IsConfirmPopupOpen = true;
                        objSignUpViewModel.HitVisibility = false;
                        objSignUpViewModel.PopupTextDisplay = displayTextonPopUp;
                        App.IsChangePharmacy = true;

                    }
                   
                         else if (objlgresponse.status == 310)
                    {
                        objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objSignUpViewModel.IsIncorrectPinPopupOpen = true;
                        objSignUpViewModel.IncorrectPinPopupText = objlgresponse.message;
                        App.IsUserNotExist = true;
                        objSignUpViewModel.LoginEmail = string.Empty;
                       
                    }
                    else 
                    {
                        objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objSignUpViewModel.IsPopupOpen = true;
                        objSignUpViewModel.PopupText = objlgresponse.message;
                    }
                }
            }
            catch (Exception)
            {
                objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                objSignUpViewModel.HitVisibility = true;
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
                if (manufacturer.Equals("NOKIA"))
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
        /// Send Nomination for changing pharmacy
        /// </summary>
        private void CallSendNominationWebService()
        {
            objSignUpViewModel.ProgressBarVisibilty = Visibility.Visible;
            string apiUrl1 = RxConstants.sendNomination;

            string deviceUniqueID = string.Empty;
            try
            {
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
                LoginResponse objResponse = App.ObjLgResponse;
                if (objResponse != null)
                {
                    SendNominationRequest objInputParameters = new SendNominationRequest
                    {
                        pharmacyid = App.LoginPharId.ToUpper(),
                        deviceid = deviceUniqueID,
                        model = DeviceModel,
                        os = "Windows Phone",
                        pushid = App.ApId,
                        fullname = objResponse.payload.name,
                        firstname = string.Empty,
                        lastname = string.Empty,
                        forename = string.Empty,
                        surname = string.Empty,
                        nhs = objResponse.payload.nhs,
                        birthdate = objResponse.payload.birthdate,
                        address1 = objResponse.payload.address1,
                        address2 = objResponse.payload.address2,
                        postcode = objResponse.payload.postcode,
                        phone = objResponse.payload.phone,
                        mail = objResponse.payload.mail,
                        sex = Convert.ToString(objResponse.payload.sex),
                        pin = App.HashPIN,
                        country = objResponse.payload.country,
                        mode = "old",
                        verifyby = objResponse.payload.verifyby,
                        surgery = new SendNominationRequestSurgery { name = objResponse.payload.surgery.name, address = objResponse.payload.surgery.address },
                        system_version = "android",
                        app_version = "1.6",
                        branding_version = "MLP"
                    };


                    WebClient sendNominationswebservicecall = new WebClient();
                    var uri1 = new Uri(apiUrl1, UriKind.RelativeOrAbsolute);

                    var json = JsonHelper.Serialize(objInputParameters);
                    sendNominationswebservicecall.Headers["Content-type"] = "application/json";
                    sendNominationswebservicecall.UploadStringCompleted += sendNominationswebservicecall_UploadStringCompleted;

                    sendNominationswebservicecall.UploadStringAsync(uri1, "POST", json);
                }
                App.IsChangePharmacy = false;
            }
            catch (Exception)
            {

                MessageBox.Show("Sorry, Unable to process your request.");
            }

        }
        /// <summary>
        /// Response from webservice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendNominationswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            SendNominationResponse objNominationResponse = null;
            if (e.Result != null)
            {
                var response = e.Result.ToString();
                objNominationResponse = Utils.JsonHelper.Deserialize<SendNominationResponse>(response);
                if ((objNominationResponse.payload != null) && (objNominationResponse.status == 0))
                {
                    if (objNominationResponse.message.Equals("Device already registered."))
                    {
                        objSignUpViewModel.IsPopupOpen = true;
                        objSignUpViewModel.IsConfirmPopupOpen = false;

                        objSignUpViewModel.PopupText = objNominationResponse.message;
                    }
                    else
                    {
                        objSignUpViewModel.IsConfirmPopupOpen = false;
                        objSignUpViewModel.HitVisibility = true;
                        App.ObjLgResponse.payload.pharmacyid = objNominationResponse.payload.pharmacyid;
                        App.ObjLgResponse.payload.pharmacyname = objNominationResponse.payload.pharmacyname;
                        App.ObjLgResponse.payload.status = objNominationResponse.payload.status + " in " + App.ObjLgResponse.payload.pharmacyid;

                        App.IsUserRegistered = true;
                        PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                        frame.Navigate(new Uri(PageURL.navigateToYourDetailswithTCURL, UriKind.Relative));
                    }
                    objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                }
                else
                {
                    objSignUpViewModel.IsPopupOpen = true;
                    objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;

                }
            }
        }
        #endregion        

        #region SignUp Methods
              
        /// <summary>
        /// Get pharmacy information in signup screen
        /// </summary>
        public void CallGetPharmacyInformationWebService()
        {
           
            string apiUrl = RxConstants.getPharmacyInformations;
            try
            {
                GetPharmacyInformationRequest objInputParameters = new GetPharmacyInformationRequest
                {
                    Pharmacyid = objSignUpViewModel.SignUpPharmacyID.ToUpper(),
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

                WebClient pharmacyinfowebservicecall = new WebClient();
                var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

                var json = JsonHelper.Serialize(objInputParameters);
                pharmacyinfowebservicecall.Headers["Content-type"] = "application/json";
                pharmacyinfowebservicecall.UploadStringCompleted += pharmacyinfowebservicecall_UploadStringCompleted;

                pharmacyinfowebservicecall.UploadStringAsync(uri, "POST", json);
            }
            catch (Exception)
            {

                MessageBox.Show("Sorry, Unable to process your request.");
            }
           
        }

        /// <summary>
        /// Web service response for pharmacy information in signup screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pharmacyinfowebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            GetPharmacyInformationResponse objLoginResponse = null;
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objLoginResponse = Utils.JsonHelper.Deserialize<GetPharmacyInformationResponse>(response);
                    if ((objLoginResponse.payload != null) && (objLoginResponse.status == 0))
                    {
                        App.LoginPharmacyname = objLoginResponse.payload.branding_data.pharmacy_name;
                        App.BrandingHash = objLoginResponse.payload.branding_hash;
                        App.AdvtHash = objLoginResponse.payload.advert_hash;
                        App.DrugsData = objLoginResponse.payload.drugs_data;
                        objSignUpViewModel.PharmacyName = objLoginResponse.payload.branding_data.pharmacy_name;
                        objSignUpViewModel.AddressLine1 = objLoginResponse.payload.branding_data.address1;
                        objSignUpViewModel.AddressLine2 = objLoginResponse.payload.branding_data.address2;
                        objSignUpViewModel.AddressLine3 = objLoginResponse.payload.branding_data.city;
                        objSignUpViewModel.PinCode = objLoginResponse.payload.branding_data.postcode;
                        objSignUpViewModel.IsPharmacyDetailsVisible = Visibility.Visible;
                        objSignUpViewModel.ProgressBarVisibiltyGetDetails = Visibility.Collapsed;
                        objSignUpViewModel.IsGetDetailsEnabled = true;
                    }
                   
                    else 
                    {
                        objSignUpViewModel.ProgressBarVisibiltyGetDetails = Visibility.Collapsed;
                        objSignUpViewModel.IsPopupOpen = true;
                        objSignUpViewModel.PopupText = objLoginResponse.message;
                        objSignUpViewModel.HitVisibility = false;
                    }
                }
            }
            catch (Exception)
            {
                objSignUpViewModel.ProgressBarVisibiltyGetDetails = Visibility.Collapsed;
                objSignUpViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
                objSignUpViewModel.IsGetDetailsEnabled = true;
            }
        }
        #endregion

        #region CheckForPharmacy Methods
        /// <summary>
        /// Get pharmacy information in signup screen
        /// </summary>
        public void CheckPharmacyIdWebservice()
        {
            string apiUrl = RxConstants.getPharmacyInformations;
            try
            {
                GetPharmacyInformationRequest objInputParameters = new GetPharmacyInformationRequest
                {
                    Pharmacyid = objSignUpViewModel.SignUpPharmacyID.ToUpper(),
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

                WebClient checkpharmacyinfowebservicecall = new WebClient();
                var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

                var json = JsonHelper.Serialize(objInputParameters);
                checkpharmacyinfowebservicecall.Headers["Content-type"] = "application/json";
                checkpharmacyinfowebservicecall.UploadStringCompleted += checkpharmacyinfowebservicecall_UploadStringCompleted;

                checkpharmacyinfowebservicecall.UploadStringAsync(uri, "POST", json);
            }
            catch (Exception)
            {

                MessageBox.Show("Sorry, Unable to process your request.");
            }
            
        }

        /// <summary>
        /// Web service response for pharmacy information in signup screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkpharmacyinfowebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            GetPharmacyInformationResponse objLoginResponse = null;
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objLoginResponse = Utils.JsonHelper.Deserialize<GetPharmacyInformationResponse>(response);
                    if (objLoginResponse.status == 0)
                    {
                        App.SignUpPharId = objSignUpViewModel.SignUpPharmacyID;
                        App.BrandingHash = objLoginResponse.payload.branding_hash;
                        App.AdvtHash = objLoginResponse.payload.advert_hash;
                        App.DrugsData = objLoginResponse.payload.drugs_data;
                        App.DrugDBHash = objLoginResponse.payload.drugs_hash;
                        App.ObjBrandingResponse = objLoginResponse;
                        objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objSignUpViewModel.HitVisibility=true;
                        PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                        frame.Navigate(new Uri(PageURL.navigateToYourDetailsLoginURL, UriKind.Relative));
                    }
                    else
                    {
                        objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                        objSignUpViewModel.IsPopupOpen = true;
                        objSignUpViewModel.HitVisibility = false;
                        objSignUpViewModel.PopupText = objLoginResponse.message;
                    }
                }
            }
            catch (Exception)
            {
                objSignUpViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                objSignUpViewModel.HitVisibility = true;
                MessageBox.Show("Sorry, Unable to process your request.");
                 
            }
            
        } 
        #endregion
        
    }
}
