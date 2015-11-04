using System;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Storage.Streams;
using Windows.Storage;
using System.IO;
using SQLite;
using Microsoft.Phone.Info;
namespace MyLocalPharmacy.Model
{
    public class HomePanoramaPharmacyDetailsModel
    {

        #region Declarations
        HomePanoramaViewModel objHomePanoramaVM; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objhomePanoramaViewModel"></param>
        public HomePanoramaPharmacyDetailsModel(HomePanoramaViewModel objhomePanoramaViewModel)
        {
            objHomePanoramaVM = objhomePanoramaViewModel;
            PharmacyDetailsWebService();
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
        /// Web service to get the Pharmacy Details
        /// </summary>
        private void PharmacyDetailsWebService()
        {
            string apiUrl = RxConstants.getPharmacyInformations;
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
            GetPharmacyInformationRequest objInputParameters = new GetPharmacyInformationRequest
            {
                Pharmacyid = App.LoginPharId.ToUpper(),
                Deviceid = ((!string.IsNullOrWhiteSpace(deviceUniqueID)) && (!string.IsNullOrEmpty(deviceUniqueID))) ? deviceUniqueID : string.Empty,
                Model = ((!string.IsNullOrWhiteSpace(DeviceModel)) && (!string.IsNullOrEmpty(DeviceModel))) ? DeviceModel : string.Empty,
                Os = "Windows Phone",
                Branding_hash = string.Empty,
                Advert_hash = ((!string.IsNullOrWhiteSpace(App.AdvtHash)) && (!string.IsNullOrEmpty(App.AdvtHash))) ? App.AdvtHash : string.Empty,
                Drugs_hash = ((!string.IsNullOrWhiteSpace(App.DrugDBHash)) && (!string.IsNullOrEmpty(App.DrugDBHash))) ? App.DrugDBHash : string.Empty,
                system_version = "android",
                app_version = "1.6",
                branding_version = "MLP"
            };

            WebClient pharmacydetailswebservicecall = new WebClient();
            var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

            var json = JsonHelper.Serialize(objInputParameters);
            pharmacydetailswebservicecall.Headers["Content-type"] = "application/json";
            pharmacydetailswebservicecall.UploadStringCompleted += pharmacydetailswebservicecall_UploadStringCompleted;

            pharmacydetailswebservicecall.UploadStringAsync(uri, "POST", json);
        }

        /// <summary>
        /// Response for Pharmacy Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pharmacydetailswebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            GetPharmacyInformationResponse objPhDetResponse = null;
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    objPhDetResponse = Utils.JsonHelper.Deserialize<GetPharmacyInformationResponse>(response);
                    if ((objPhDetResponse.payload != null) && (objPhDetResponse.status == 0))
                    {
                        App.ObjBrandingResponse = objPhDetResponse;


                        objHomePanoramaVM.AppBarPrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;
                        objHomePanoramaVM.PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);
                        objHomePanoramaVM.SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
                        objHomePanoramaVM.FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);


                        objHomePanoramaVM.PharmacyName = objPhDetResponse.payload.branding_data.pharmacy_name;
                        objHomePanoramaVM.PharmacyBranchName = objPhDetResponse.payload.branding_data.branch_name + @"               ";
                        App.PharmacyBranchName = objHomePanoramaVM.PharmacyBranchName;
                        objHomePanoramaVM.AddressLine1 = objPhDetResponse.payload.branding_data.address1;
                        App.LoginPharmacyAddress1 = objPhDetResponse.payload.branding_data.address1;
                        objHomePanoramaVM.AddressLine2 = objPhDetResponse.payload.branding_data.address2;
                        App.LoginPharmacyAddress2 = objPhDetResponse.payload.branding_data.address2;
                        objHomePanoramaVM.AddressLine3 = objPhDetResponse.payload.branding_data.city;
                        objHomePanoramaVM.PinCode = objPhDetResponse.payload.branding_data.postcode;
                        App.PostCode = objPhDetResponse.payload.branding_data.postcode;
                        objHomePanoramaVM.PharmacistName1 = objPhDetResponse.payload.branding_data.pharmacist1;
                        objHomePanoramaVM.PharmacistName2 = objPhDetResponse.payload.branding_data.pharmacist2;
                        objHomePanoramaVM.WebsiteLink = objPhDetResponse.payload.branding_data.website;

                        App.PharmacyPhoneNo = objPhDetResponse.payload.branding_data.phone;
                        App.DrugsData = objPhDetResponse.payload.drugs_data;
                        if (App.DrugDBHash != objPhDetResponse.payload.drugs_hash)
                        {
                            UpdateDBFile();
                        }

                        if (!string.IsNullOrEmpty(objPhDetResponse.payload.branding_data.twitter_link) && !string.IsNullOrWhiteSpace(objPhDetResponse.payload.branding_data.twitter_link))
                        {
                            objHomePanoramaVM.IsTwitterLinkVisible = Visibility.Visible;
                            objHomePanoramaVM.TwitterLink = objPhDetResponse.payload.branding_data.twitter_link;
                        }
                        if (!string.IsNullOrEmpty(objPhDetResponse.payload.branding_data.facebook_link) && !string.IsNullOrWhiteSpace(objPhDetResponse.payload.branding_data.facebook_link))
                        {
                            objHomePanoramaVM.IsFacebookLinkVisible = Visibility.Visible;
                            objHomePanoramaVM.FacebookLink = objPhDetResponse.payload.branding_data.facebook_link;
                        }

                        if (objPhDetResponse.payload.branding_data.opening_hours != null)
                        {
                            bool isClosedToday = objPhDetResponse.payload.branding_data.opening_hours.SingleOrDefault(s => s.dayname == Convert.ToString(System.DateTime.Today.DayOfWeek)).is_closed;
                            string openingTime = Convert.ToString(objPhDetResponse.payload.branding_data.opening_hours.SingleOrDefault(s => s.dayname == Convert.ToString(System.DateTime.Today.DayOfWeek)).open);
                            string closingTime = Convert.ToString(objPhDetResponse.payload.branding_data.opening_hours.SingleOrDefault(s => s.dayname == Convert.ToString(System.DateTime.Today.DayOfWeek)).close);
                            string todayOpenTime = !isClosedToday ? openingTime + "-" + closingTime : "Closed";

                            objHomePanoramaVM.Opentodaytime = todayOpenTime;

                            List<OpenHours> lstOpenHours = new List<OpenHours>();
                            OpenHours objOpenHours;
                            foreach (var item in objPhDetResponse.payload.branding_data.opening_hours)
                            {
                                objOpenHours = new OpenHours { DayName = item.dayname, Timings = !item.is_closed ? item.open + "-" + item.close : "Closed" };
                                lstOpenHours.Add(objOpenHours);
                            }
                            objHomePanoramaVM.OpeningHours = lstOpenHours;
                        }

                        if (objPhDetResponse.payload.advert_data != null)
                        {
                            objHomePanoramaVM.AdvertisementData = new ObservableCollection<AdvertData>(objPhDetResponse.payload.advert_data);
                            App.AdImages = null;
                            foreach (var item in objPhDetResponse.payload.advert_data)
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
                        }
                        objHomePanoramaVM.ProgressBarVisibilty = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception)
            {
                objHomePanoramaVM.ProgressBarVisibilty = Visibility.Collapsed;
            }

        }
        /// <summary>
        /// Write Data to Sqlite file
        /// </summary>
        /// <returns></returns>
        public async Task WriteToFile()
        {
            string data = App.DrugsData;

            byte[] decoded = System.Convert.FromBase64String(data);

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            var file = await local.CreateFileAsync("DataFile.sqlite", CreationCollisionOption.ReplaceExisting);

            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(decoded, 0, decoded.Length);
            }
            StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("DataFile.sqlite");
        }
        /// <summary>
        /// Method to call the writetofile task
        /// </summary>
        private async void UpdateDBFile()
        {
            await WriteToFile();
        } 
        #endregion
        
    }
}
