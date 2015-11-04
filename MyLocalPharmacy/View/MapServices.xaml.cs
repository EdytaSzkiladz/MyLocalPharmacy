using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.ViewModel;
using System.Windows.Input;
using System.Xml.Linq;
using MyLocalPharmacy.Utils;
using System.ComponentModel;
using System.Windows.Media;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Tasks;
using Windows.Devices.Geolocation;
using System.Windows.Media.Imaging;

namespace MyLocalPharmacy.View
{
    public partial class MapServices : PhoneApplicationPage
    {
        #region Declarations

        GPSurgeriesCollection newFeedList = new GPSurgeriesCollection();
        List<string> ListOfLinks = new List<string>();
        Dictionary<string, int> locationDictionary;
        int[] arrayOfpages;
        string[] arrayOfUrls;
        string[] details;
        int totalPages;
        bool IsPlotted;
        bool IsPostCodeSearch;
        int count = 0;

        bool IsFromSaved;

        Map MyMap; 

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MapServices()
        {
            InitializeComponent();
            tbxSearch.Visibility = Visibility.Collapsed;
            tbkTitle.Text = App.FindServiceTiltle;
            MyMap = new Map();
            MyMap.IsHitTestVisible = true;
            MyMap.Center = App.LocalServiceCentreCoordinates;
            MyMap.ZoomLevel = App.LocalServiceZoomLevel;
            ContentPanel.Children.Add(MyMap);
            this.DataContext = new MapServicesViewModel();
        } 

        #endregion

        #region Methods
        /// <summary>
        /// Method to set Map
        /// </summary>
        /// <param name="coordinateCollectionSub"></param>
        /// <param name="count"></param>
        private void SetMap(GPSurgeriesCollection coordinateCollectionSub, int count)
        {
            if (count == 0)
            {
                GPSurgeriesFeedData coordinate = coordinateCollectionSub.First();
                if (!IsFromSaved)
                {
                    MyMap.Center = new GeoCoordinate(Convert.ToDouble(coordinate.Latitude), Convert.ToDouble(coordinate.Longitude));

                }
                else
                {
                    MyMap.Center = App.LocalServiceCentreCoordinates;
                    MyMap.ZoomLevel = App.LocalServiceZoomLevel;
                    IsFromSaved = false;
                }

            }

            foreach (GPSurgeriesFeedData coordinate in coordinateCollectionSub)
            {
                IsPlotted = true;

                Pushpin pushpin = new Pushpin();

                pushpin.Content = " +";
                pushpin.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(PushpinTap);

                MapLayer layer = new MapLayer();
                MapOverlay overlay = new MapOverlay();

                pushpin.Tag = coordinate.Name + "_" + coordinate.PostCode + "_" + coordinate.Addressline1 + "_" + coordinate.Addressline2 + "_" + coordinate.Addressline3 + "_" + coordinate.Addressline4 + "_" + coordinate.Addressline5 + "_" + coordinate.Telephone + "_" + coordinate.Latitude + "_" + coordinate.Longitude;

                overlay.Content = pushpin;

                overlay.GeoCoordinate = new GeoCoordinate(Convert.ToDouble(coordinate.Latitude), Convert.ToDouble(coordinate.Longitude));
                overlay.PositionOrigin = new Point(0.0, 1.0);
                layer.Add(overlay);
                MyMap.Layers.Add(layer);
            }
        }

        /// <summary>
        /// Webservice Call for both postcode and place
        /// </summary>
        private void CallWebService()
        {
            tbkNoResult.Visibility = Visibility.Collapsed;

            if (MyMap.Layers != null)
            {
                MyMap.Layers.Clear();
                ListOfLinks.Clear();
                count = 0;
                MyMap.ZoomLevel = RxConstants.ZoomLevel;
            }

            string searchCriteria;
            string url;
            double DistanceMileToKm = Convert.ToDouble(App.LocalServiceDistance) * RxConstants.MileToKm;
            int DistanceForWebCall = (int)Math.Round(DistanceMileToKm);

            App.SearchTerm = tbxSearch.Text;
            if ((IsPostCodeSearch = Utilities.SearchCriteria(tbxSearch.Text)) == true)
            {
                searchCriteria = RxConstants.FindServiceByPostcode;
                if (App.FindServiceTiltle.Equals("GP Surgeries"))
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeGP + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;
                else if (App.FindServiceTiltle.Equals("Dentists"))
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeDentist + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;
                else if (App.FindServiceTiltle.Equals("Hospitals"))
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeHospitals + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;
                else if (App.FindServiceTiltle.Equals("opticians"))
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeOpticians + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;
                else
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeGP + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;

                LoadNHSData(url);
            }
            else
            {
                searchCriteria = RxConstants.FindServiceByPlace;


                if (App.FindServiceTiltle.Equals("GP Surgeries"))
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeGP + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;
                else if (App.FindServiceTiltle.Equals("Dentists"))
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeDentist + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;
                else if (App.FindServiceTiltle.Equals("Hospitals"))
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeHospitals + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;
                else if (App.FindServiceTiltle.Equals("opticians"))
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeOpticians + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;
                else
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeGP + searchCriteria + tbxSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;

                CallWebservicePlace(url);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        private void CallWebservicePlace(string url)
        {
            progressBarMap.Visibility = Visibility.Visible;
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompletedPlace;
            webClient.DownloadStringAsync(new Uri(url));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="page"></param>
        private void CallWebserviceForCoordinates(string url, int page)
        {
            progressBarMap.Visibility = Visibility.Visible;
            WebClient webClient = new WebClient();
            string apiuri = url + "&page=" + page;
            var uri = new Uri(apiuri, UriKind.RelativeOrAbsolute);
            webClient.DownloadStringCompleted += webClient_DownloadStringCompletedPostCode;
            webClient.DownloadStringAsync(new Uri(apiuri));
        }

        /// <summary>
        /// Method to load nhs data
        /// </summary>
        /// <param name="url"></param>
        public void LoadNHSData(string url)
        {
            progressBarMap.Visibility = Visibility.Visible;
            WebClient service = new WebClient();
            string apiuri = url + "&page=1";
            var uri = new Uri(apiuri, UriKind.RelativeOrAbsolute);
            service.DownloadStringCompleted += service_DownloadStringCompleted;
            service.DownloadStringAsync(uri);
        }

        /// <summary>
        /// method to find total pages
        /// </summary>
        /// <param name="document"></param>
        /// <param name="val"></param>
        void ParseXML(XElement document, int val)
        {
            try
            {

                XNamespace ns = "http://www.w3.org/2005/Atom";
                XNamespace s = "http://syndication.nhschoices.nhs.uk/services";
                List<XElement> pages = document.Elements().Where(x => x.Name.Equals(ns + "link")).ToList();
                XElement xe = pages.FirstOrDefault(x => x.Attribute("rel").Value.Equals("last"));
                if (xe != null)
                {
                    string href = xe.Attribute("href").Value;
                    Console.WriteLine(href);
                    int index = href.LastIndexOf("&page=");
                    index = index + 6;
                    string NoOfPages = href.Substring(index, href.Length - index);
                    if (!IsPostCodeSearch)
                    {
                        arrayOfpages[val] = Convert.ToInt32(NoOfPages);
                    }
                    else
                    {
                        totalPages = Convert.ToInt32(NoOfPages);
                    }
                }
            }
            catch (Exception ex)
            {
                progressBarMap.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Method to parse xml data of place search
        /// </summary>
        /// <param name="xmlFeedPlace"></param>
        private void ParseSubMenuListPlaces(XElement xmlFeedPlace)
        {

            string link;
            try
            {
                foreach (XElement curElement in xmlFeedPlace.Descendants("Link"))
                {
                    try
                    {
                        link = (curElement.Element("Uri")).Value.ToString();
                        link = link.Replace("?api", ".xml?api");

                        ListOfLinks.Add(link);
                    }


                    catch (FormatException fe)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(fe);
#endif
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(e);
#endif
                    }
                }
            }
            catch (Exception ex)
            {
                progressBarMap.Visibility = Visibility.Collapsed;
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex);
#endif

            }
        }

        /// <summary>
        /// Method to parse xml data of organisations
        /// </summary>
        /// <param name="xmlFeed"></param>
        void ParseSubMenuList(XElement xmlFeed)
        {

            GPSurgeriesCollection coordinateCollectionSub = new GPSurgeriesCollection();
            try
            {
                XNamespace s = "http://syndication.nhschoices.nhs.uk/services";
                foreach (XElement curElement in xmlFeed.Descendants("entry").Descendants("content").Descendants(s + "organisationSummary"))
                {
                    try
                    {
                        GPSurgeriesFeedData gp = new GPSurgeriesFeedData();

                        var newData = new organisationSummary();
                        var newDataCoordinates = new organisationSummaryGeographicCoordinates();
                        newData.name = (curElement.Element(s + "name")).Value.ToString();

                        string[] addresslines = new string[5];
                        string postcode = string.Empty;
                        int i = 0;
                        foreach (XElement curElementCoordinates in curElement.Descendants(s + "address"))
                        {
                            foreach (XElement SubcurElementCoordinates in curElementCoordinates.Descendants(s + "addressLine"))
                            {
                                addresslines[i] = SubcurElementCoordinates.Value;
                                i++;
                            }
                            postcode = curElementCoordinates.Element(s + "postcode").Value.ToString();
                        }

                        string[] contact = new string[3];

                        i = 0;
                        foreach (XElement curElementCoordinates in curElement.Descendants(s + "contact"))
                        {
                            foreach (XElement SubcurElementCoordinates in curElementCoordinates.Descendants(s + "telephone"))
                            {
                                contact[i] = SubcurElementCoordinates.Value;
                                i++;
                            }

                        }



                        foreach (XElement curElementCoordinates in curElement.Descendants(s + "geographicCoordinates"))
                        {

                            newDataCoordinates.latitude = Convert.ToDecimal((curElementCoordinates.Element(s + "latitude")).Value);
                            newDataCoordinates.longitude = Convert.ToDecimal((curElementCoordinates.Element(s + "longitude")).Value);

                        }


                        gp.Name = newData.name;
                        gp.Addressline1 = addresslines[0] == null ? "?" : addresslines[0];
                        gp.Addressline2 = addresslines[1] == null ? "?" : addresslines[1];
                        gp.Addressline3 = addresslines[2] == null ? "?" : addresslines[2];
                        gp.Addressline4 = addresslines[3] == null ? "?" : addresslines[3];
                        gp.Addressline5 = addresslines[4] == null ? "?" : addresslines[4];
                        gp.PostCode = postcode == null ? "?" : postcode;
                        gp.Telephone = contact[0];
                        gp.Latitude = newDataCoordinates.latitude;
                        gp.Longitude = newDataCoordinates.longitude;

                        coordinateCollectionSub.Add(gp);
                        newFeedList.Add(gp);
                    }


                    catch (FormatException fe)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(fe);
#endif
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(e);
#endif
                    }
                }
                //GPSurgeriesCollection colln = newFeedList;

                SetMap(coordinateCollectionSub, count);
                count++;

            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex);
#endif

            }
        }
        
        #endregion

        #region Events
        /// <summary>
        /// Method to show Details Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPopup(object sender, System.Windows.Input.GestureEventArgs e)
        {
            popupDetails.IsOpen = true;
            MyMap.IsHitTestVisible = false;

            tbkName.Text = details[0].Equals("?") ? " " : details[0];
            tbkAddressLine1.Text = details[2].Equals("?") ? " " : details[2];
            tbkAddressLine2.Text = details[3].Equals("?") ? " " : details[3];
            tbkAddressLine3.Text = details[4].Equals("?") ? " " : details[4];
            tbkAddressLine4.Text = details[5].Equals("?") ? " " : details[5];
            tbkAddressLine5.Text = details[6].Equals("?") ? " " : details[6];
            tbkAddressLine6.Text = details[1].Equals("?") ? " " : details[1];
            tbkphonenumber.Text = details[7].Equals("?") ? " " : details[7];
            App.LocalServiceLatitude = details[8];
            App.LocalServiceLongitude = details[9];

        }

        /// <summary>
        /// Tap Event of Pushpin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PushpinTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string pushpinTag = (sender as Pushpin).Tag.ToString();

            details = pushpinTag.Split('_');
            var f = details;

            ToastPrompt toast = new ToastPrompt();
            toast.Title = details[0];
            toast.Message = details[1];

            toast.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Assets/Icons/flag_64.png", UriKind.RelativeOrAbsolute));
            toast.ImageHeight = 50;
            toast.ImageWidth = 50;

            toast.Background = new SolidColorBrush(Colors.Black);
            toast.Opacity = .9;

            toast.MillisecondsUntilHidden = 3000;
            toast.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(ShowPopup);

            toast.Show();

        }

        /// <summary>
        /// Click action of application bar button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (popupDetails.IsOpen)
            {
                popupDetails.IsOpen = false;
                MyMap.IsHitTestVisible = true;
            }

            tbxSearch.Visibility = Visibility.Visible;
            tbkHint.Visibility = Visibility.Visible;
            tbkNoResult.Visibility = Visibility.Collapsed;
            tbxSearch.Text = string.Empty;
            tbxSearch.Focus();
        }

        /// <summary>
        /// Event of Enter key tap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSearch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {

                if (!string.IsNullOrEmpty(tbxSearch.Text) || !string.IsNullOrWhiteSpace(tbxSearch.Text))
                {
                    this.Focus();
                    tbkHint.Visibility = Visibility.Collapsed;
                    progressBarMap.Visibility = Visibility.Visible;

                    if (Utilities.IsConnectedToNetwork())
                    {
                        CallWebService();
                    }
                    else
                    {
                        popupNoInternet.IsOpen = true;
                        progressBarMap.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(tbxSearch.Text) || !string.IsNullOrWhiteSpace(tbxSearch.Text))
                {
                    tbkHint.Visibility = Visibility.Collapsed;
                }
                else
                {
                    tbkHint.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Event of search icon tap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSearch_ActionIconTapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxSearch.Text) || !string.IsNullOrWhiteSpace(tbxSearch.Text))
            {
                this.Focus();
                tbkHint.Visibility = Visibility.Collapsed;
                progressBarMap.Visibility = Visibility.Visible;
                if (Utilities.IsConnectedToNetwork())
                {
                    CallWebService();
                }
                else
                {
                    popupNoInternet.IsOpen = true;
                    progressBarMap.Visibility = Visibility.Collapsed;
                }

            }
        }

        /// <summary>
        /// On lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            tbxSearch.Visibility = Visibility.Collapsed;
            tbkHint.Visibility = Visibility.Collapsed;
            tbxSearch.Text = string.Empty;
        }

        /// <summary>
        /// completed event for place
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webClient_DownloadStringCompletedPlace(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null && e.Result != null)
                {
                    if (!string.IsNullOrEmpty(e.Result))
                    {

                        string v = e.Result.Replace("xmlns=\"http://schemas.datacontract.org/2004/07/NHSChoices.Syndication.Resources\"", "");
                        XElement element = XElement.Parse(v);

                        ParseSubMenuListPlaces(element);
                    }

                    arrayOfpages = new int[ListOfLinks.Count];
                    arrayOfUrls = new string[ListOfLinks.Count];
                    int i = 0;
                    locationDictionary = new Dictionary<string, int>();
                    foreach (string url in ListOfLinks)
                    {
                        locationDictionary.Add(url, i);
                        LoadNHSData(url);
                        i++;
                    }

                }
                else
                {
                    progressBarMap.Visibility = Visibility.Collapsed;
                    tbkNoResult.Visibility = Visibility.Visible;
                    MessageBox.Show("No Results Found.");
                }
            }
            catch (Exception)
            {
                progressBarMap.Visibility = Visibility.Collapsed;
                tbkNoResult.Visibility = Visibility.Visible;
                MessageBox.Show("No Results Found.");
            }
            //


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void service_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    XElement element = XElement.Parse(response);
                    string hrefSelf = null;
                    int val = 0;

                    XNamespace ns = "http://www.w3.org/2005/Atom";
                    List<XElement> pages = element.Elements().Where(x => x.Name.Equals(ns + "link")).ToList();

                    XElement xeSelf = pages.FirstOrDefault(x => x.Attribute("rel").Value.Equals("self"));
                    if (xeSelf != null)
                    {
                        hrefSelf = xeSelf.Attribute("href").Value;
                    }
                    hrefSelf = hrefSelf.Replace("?api", ".xml?api");

                    if (!IsPostCodeSearch)
                    {
                        if (locationDictionary.ContainsKey(hrefSelf))
                        {
                            val = locationDictionary[hrefSelf];
                        }
                    }
                    else
                    {
                        val = -1;
                    }

                    ParseXML(element, val);

                    string v = e.Result.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", "");
                    v = v.Replace("xmlns=\"http://www.w3.org/2005/Atom\"", "");
                    element = XElement.Parse(v);

                    ParseSubMenuList(element);

                    int limit;

                    if (!IsPostCodeSearch)
                    {
                        limit = arrayOfpages[val];
                    }

                    else
                    {
                        limit = totalPages;
                    }
                    for (int i = 2; i <= limit; i++)
                    {
                        CallWebserviceForCoordinates(hrefSelf, i);
                    }

                }
                //
            }
            catch (Exception ex)
            {
                progressBarMap.Visibility = Visibility.Collapsed;
                if (!IsPlotted)
                {
                    tbkNoResult.Visibility = Visibility.Visible;
                    MessageBox.Show("No Results Found.");
                }
                //tbkNoResult.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Post code completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webClient_DownloadStringCompletedPostCode(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null)
            {
                if (!string.IsNullOrEmpty(e.Result))
                {
                    string v = e.Result.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", "");
                    v = v.Replace("xmlns=\"http://www.w3.org/2005/Atom\"", "");
                    XElement element = XElement.Parse(v);

                    ParseSubMenuList(element);
                }
            }
            progressBarMap.Visibility = Visibility.Collapsed;

        }

        /// <summary>
        /// Focus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush();

            brush.Color = Colors.Black;
            tbxSearch.Foreground = brush;
        }

        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.TombStonedPageURL = PageURL.navigateToMapServicesURL;

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
            else
            {
                string strAction;
                if (NavigationContext.QueryString.TryGetValue("action", out strAction))
                {
                    if (strAction.Equals("drawPushpin"))
                    {


                        if (!string.IsNullOrEmpty(App.SearchTerm) || !string.IsNullOrWhiteSpace(App.SearchTerm))
                        {
                            IsFromSaved = true;

                            tbxSearch.Text = App.SearchTerm;

                            this.Focus();
                            tbkHint.Visibility = Visibility.Collapsed;
                            progressBarMap.Visibility = Visibility.Visible;

                            if (Utilities.IsConnectedToNetwork())
                            {
                                CallWebService();
                            }
                            else
                            {
                                popupNoInternet.IsOpen = true;
                            }
                        }
                    }
                }
                else
                {
                    Geolocator MyGeolocator = new Geolocator();
                    MyGeolocator.DesiredAccuracyInMeters = 20;
                    if (MyGeolocator.LocationStatus == PositionStatus.Disabled)
                    {
                        //
                        MessageBoxResult result = MessageBox.Show("Would you like to Enable GPS?", "Enable GPS", MessageBoxButton.OKCancel);

                        if (result == MessageBoxResult.OK)
                        {

                            var res = Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
                        }
                        if (result == MessageBoxResult.Cancel)
                        {
                            //this.Loaded += new RoutedEventHandler(NavigateToHomePanorama);

                        }
                    }

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToHomePanorama(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL + "?goto=1", UriKind.Relative));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            App.LocalServiceCentreCoordinates = MyMap.Center;
            App.LocalServiceZoomLevel = MyMap.ZoomLevel;
        }

        /// <summary>
        /// On window back key press
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (popupNoInternet.IsOpen)
            {
                popupNoInternet.IsOpen = false;
                e.Cancel = true;
            }
            else if (popupDetails.IsOpen)
            {
                MyMap.IsHitTestVisible = true;
                popupDetails.IsOpen = false;
                e.Cancel = true;
            }
            else
            {

                NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL + "?goto=1", UriKind.Relative));
            }

        }

        /// <summary>
        /// Event on tapping the call button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void callStack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string phoneNumber = tbkphonenumber.Text;
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();
                phoneCallTask.PhoneNumber = phoneNumber;
                phoneCallTask.Show();
            }
        }

        /// <summary>
        /// Event on tapping get direction button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void directionStack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToLHSDetailsMapURL, UriKind.Relative));
        }         
        #endregion
    }
}