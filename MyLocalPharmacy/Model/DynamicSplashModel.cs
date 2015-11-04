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
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MyLocalPharmacy.Model
{
    public class DynamicSplashModel
    {
        #region Declarations
            public DynamicSplashViewModel objDynaViewModel;
            private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
            string imgUrl = string.Empty;
            bool IsStaticSplash;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objDynaSplashVM"></param>
        public DynamicSplashModel(DynamicSplashViewModel objDynaSplashVM)
        {
            objDynaViewModel = objDynaSplashVM;
            GetDynamicSplash();
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method to call the web service
        /// </summary>
        private void GetDynamicSplash()
        {
            CheckResponseTime();
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

            WebClient pharmacyinfodynamicsplashwebservicecall = new WebClient();
            var uri = new Uri(apiUrl, UriKind.RelativeOrAbsolute);

            var json = JsonHelper.Serialize(objInputParameters);
            pharmacyinfodynamicsplashwebservicecall.Headers["Content-type"] = "application/json";
            pharmacyinfodynamicsplashwebservicecall.UploadStringCompleted += pharmacyinfodynamicsplashwebservicecall_UploadStringCompleted;

            pharmacyinfodynamicsplashwebservicecall.UploadStringAsync(uri, "POST", json);
        }

        /// <summary>
        /// Method to set splash url based on response time
        /// </summary>
        public void CheckResponseTime()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();

        }
        /// <summary>
        /// Method to set splash url and navigate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            IsStaticSplash = true;
            if (string.IsNullOrWhiteSpace(imgUrl) || string.IsNullOrEmpty(imgUrl))
                objDynaViewModel.DynamicSplashImageUrl = new BitmapImage(new Uri(App.StaticSplashURL, UriKind.Relative));

            if (((App)Application.Current).RootVisual != null)
            {
                ((App)Application.Current).RootVisual.Dispatcher.BeginInvoke(View.DynamicSplashScreenControl.UpdationComplete);
            }
            _dispatcherTimer.Stop();
        }
        /// <summary>
        /// Method to get the response
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pharmacyinfodynamicsplashwebservicecall_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
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
                        imgUrl = objPhBrandingInfoResponse.payload.branding_data.appearance.splash_url.Replace("https", "http");
                        objDynaViewModel.DynamicSplashImageUrl = new BitmapImage(new Uri(imgUrl, UriKind.Absolute));

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
                    }
                    if (!IsStaticSplash)
                    {
                        if (((App)Application.Current).RootVisual != null)
                        {
                            ((App)Application.Current).RootVisual.Dispatcher.BeginInvoke(View.DynamicSplashScreenControl.UpdationComplete);
                        }
                    }
                }
            }
            catch (Exception)
            {
               
            }
        } 
        #endregion
    }
}
