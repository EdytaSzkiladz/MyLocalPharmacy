using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Services;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using MyLocalPharmacy.Entities;

namespace MyLocalPharmacy.View
{
    public partial class Page1 : PhoneApplicationPage
    {
        #region Declarations
        List<GeoCoordinate> myRouteCoordinates = new List<GeoCoordinate>();

        RouteQuery myRouteQuery;

        GeocodeQuery mygeocodeQuery;

        Geolocator MyGeolocator;

        Geoposition MyGeoPosition;
        double latitude = 0;
        double longitude = 0;
        PersistentDataStorage<GPSPosition> GpsData = new PersistentDataStorage<GPSPosition>();
        #endregion

        #region Constructor
        public Page1()
        {
            InitializeComponent();
            this.DataContext = new MapPharmacyViewModel();
            progress.Visibility = Visibility.Visible;
        }
        #endregion

        #region Method

        /// <summary>
        /// Get the current position of device
        /// </summary>
        private async void GetCurrentLocation()
        {
         
            MyGeolocator = new Geolocator();

            MyGeolocator.DesiredAccuracyInMeters = 20;

            if (MyGeolocator.LocationStatus == PositionStatus.Disabled)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to Enable GPS?", "Enable GPS", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    var res = Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
                }
                if (result == MessageBoxResult.Cancel)
                {
                    this.Loaded += new RoutedEventHandler(NavigateToHomePanorama);

                }
            }
            else
            {
                

                MyGeoPosition = await MyGeolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                latitude = MyGeoPosition.Coordinate.Latitude;
                longitude = MyGeoPosition.Coordinate.Longitude;

                myRouteCoordinates.Add(new GeoCoordinate(latitude, longitude)); 

                myMap.Center = new GeoCoordinate(latitude, longitude);
                myMap.ZoomLevel = 17;
                GetDirections();
                progress.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Geo Code Query completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mygeocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {

            if (e.Error == null)
            {
                try
                {
                    myRouteQuery = new RouteQuery();

                    myRouteCoordinates.Add(e.Result[0].GeoCoordinate);

                    myRouteQuery.Waypoints = myRouteCoordinates;

                    myRouteQuery.QueryCompleted += myRouteQuery_QueryCompleted;

                    myRouteQuery.QueryAsync();

                    mygeocodeQuery.Dispose();
                }
                catch (Exception)
                {

                }

            }

        }

        /// <summary>
        /// Route Query completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myRouteQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {

            if (e.Error == null)
            {

                Route MyRoute = e.Result;

                MapRoute MyMapRoute = new MapRoute(MyRoute);

                myMap.AddRoute(MyMapRoute);

                foreach (RouteLeg leg in MyRoute.Legs)
                {

                    foreach (RouteManeuver maneuver in leg.Maneuvers)
                    {
                        textBoxDirections.Text += maneuver.InstructionText + Environment.NewLine;
                    }
                }

                myRouteQuery.Dispose();
            }
            else
            {
                MessageBox.Show("Unable to generate route.");
            }
        }

        /// <summary>
        /// Method to get directions
        /// </summary>
        private void GetDirections()
        {
            string searchTerm = string.Concat(App.LoginPharmacyAddress1 + ", " + App.LoginPharmacyAddress2 + ", " + App.PostCode + ", " + App.LoginPharmacyAddress3);

            if (Utilities.IsConnectedToNetwork())
            {
                mygeocodeQuery = new GeocodeQuery { GeoCoordinate = new GeoCoordinate(0, 0), SearchTerm = searchTerm };
                mygeocodeQuery.QueryCompleted += mygeocodeQuery_QueryCompleted;

                mygeocodeQuery.QueryAsync();
            }
            else
            {
                MessageBox.Show("No network found");
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Event to navigate to Enter pin Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelNavigationPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.IsToombStoned = false;
            App.IsApplicationInstancePreserved = false;
            this.NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Event to navigate to Panorama
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToHomePanorama(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.Relative));
        }


        /// <summary>
        /// OnNavigatedFrom event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (!(e.NavigationMode == NavigationMode.Back))
            {            
                
            }
        }

        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.TombStonedPageURL = PageURL.navigateToPharmacyDetailsMapURL;
            
            if (App.IsApplicationInstancePreserved)
            {
                App.IsApplicationInstancePreserved = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));                
            }

            else if (App.IsToombStoned)
            {
                App.IsToombStoned = false;
                App.IsApplicationInstancePreserved = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));                
            }
            else
            {
                GetCurrentLocation();
            }
        }
        /// <summary>
        /// Method for back key
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.Relative));
        }

        #endregion
    }
}