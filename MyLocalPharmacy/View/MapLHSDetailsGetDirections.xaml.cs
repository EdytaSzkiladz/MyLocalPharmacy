using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyLocalPharmacy.ViewModel;
using MyLocalPharmacy.Utils;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;
using MyLocalPharmacy.Entities;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyLocalPharmacy.View
{
    public partial class MapLHSDetailsGetDirections : PhoneApplicationPage
    {
        #region Declarations

        List<GeoCoordinate> myRouteCoordinates = new List<GeoCoordinate>();
        RouteQuery myRouteQuery;
        Geolocator MyGeolocator;
        Geoposition MyGeoPosition;
        double latitude = 0;
        double longitude = 0;
        PersistentDataStorage<GPSPosition> GpsData = new PersistentDataStorage<GPSPosition>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MapLHSDetailsGetDirections()
        {
            InitializeComponent();
            this.DataContext = new MapLHSDetailsGetDirectionsViewModel();
            progress.Visibility = Visibility.Visible;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Get the current position of device
        /// </summary>
        private async void GetCurrentLocation()
        {

            MyGeolocator = new Geolocator();

            MyGeolocator.DesiredAccuracyInMeters = 50;

            if (MyGeolocator.LocationStatus == PositionStatus.Disabled)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to Enable GPS?", "Enable GPS", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {

                    var res = Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
                }
                if (result == MessageBoxResult.Cancel)
                {
                    MessageBox.Show("Unable to track your location. Make sure your location service is turned ON.");
                    progress.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                try
                {
                    MyGeoPosition = await MyGeolocator.GetGeopositionAsync(
                        maximumAge: TimeSpan.FromMinutes(5),
                        timeout: TimeSpan.FromSeconds(10)
                        );

                    latitude = MyGeoPosition.Coordinate.Latitude;
                    longitude = MyGeoPosition.Coordinate.Longitude;

                    myRouteCoordinates.Add(new GeoCoordinate(latitude, longitude));
                    myMap.Center = new GeoCoordinate(latitude, longitude);
                    myMap.ZoomLevel = 13;
                    GetDirections();
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry. Unable to retrieve your current location.");
                    progress.Visibility = Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// Geo Code Query completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void ShowRoute()
        {
            try
            {

                myRouteQuery = new RouteQuery();
                myRouteCoordinates.Add(new GeoCoordinate(Convert.ToDouble(App.LocalServiceLatitude), Convert.ToDouble(App.LocalServiceLongitude)));
                myRouteQuery.Waypoints = myRouteCoordinates;
                myRouteQuery.TravelMode = TravelMode.Driving;
                myRouteQuery.QueryCompleted += myRouteQuery_QueryCompleted;
                myRouteQuery.QueryAsync();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Route Query completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        void myRouteQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            try
            {
                if (e.Error == null && e.Result != null)
                {
                    Route MyRoute = e.Result;
                    MapRoute MyMapRoute = new MapRoute(MyRoute);
                    MyMapRoute.Color = (Colors.Blue);
                    myMap.AddRoute(MyMapRoute);
                    #region Draw source location ellipse
                    Ellipse myCircle = new Ellipse();
                    myCircle.Fill = new SolidColorBrush(Colors.Blue);
                    myCircle.Height = 20;
                    myCircle.Width = 20;
                    myCircle.Opacity = 50;
                    MapOverlay myLocationOverlay = new MapOverlay();
                    myLocationOverlay.Content = myCircle;
                    myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
                    myLocationOverlay.GeoCoordinate = myRouteCoordinates[0];
                    MapLayer MylocationLayer = new MapLayer();
                    MylocationLayer.Add(myLocationOverlay);
                    myMap.Layers.Add(MylocationLayer);
                    #endregion
                    #region Draw target location ellipse
                    Ellipse CarCircle = new Ellipse();
                    CarCircle.Fill = new SolidColorBrush(Colors.Red);
                    CarCircle.Height = 20;
                    CarCircle.Width = 20;
                    CarCircle.Opacity = 50;
                    MapOverlay CarLocationOverlay = new MapOverlay();
                    CarLocationOverlay.Content = CarCircle;
                    CarLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
                    CarLocationOverlay.GeoCoordinate = myRouteCoordinates[1];
                    MapLayer CarlocationLayer = new MapLayer();
                    CarlocationLayer.Add(CarLocationOverlay);
                    myMap.Layers.Add(CarlocationLayer);
                    #endregion

                    double distanceInKm = (double)MyRoute.LengthInMeters / 1000;
                    textBoxDirections.Text = "Distance: " + distanceInKm.ToString("0.0") + " km, Time:"
                                                     + MyRoute.EstimatedDuration.Hours + " hrs "
                                                     + MyRoute.EstimatedDuration.Minutes + " mins." + Environment.NewLine;
                    List<string> routeInstructions = new List<string>();
                    foreach (RouteLeg leg in MyRoute.Legs)
                    {
                        for (int i = 0; i < leg.Maneuvers.Count; i++)
                        {
                            RouteManeuver maneuver = leg.Maneuvers[i];
                            string instructionText = maneuver.InstructionText;
                            distanceInKm = 0;

                            if (i > 0)
                            {
                                distanceInKm = (double)leg.Maneuvers[i - 1].LengthInMeters / 1000;
                                instructionText += " (" + distanceInKm.ToString("0.0") + " km)";
                            }
                            routeInstructions.Add(instructionText);
                        }
                    }
                    foreach (string str in routeInstructions)
                    {
                        textBoxDirections.Text += str + Environment.NewLine;
                    }

                    myRouteQuery.Dispose();
                    progress.Visibility = Visibility.Collapsed;

                }
                else
                {
                    MessageBox.Show("Unable to generate route.");
                    progress.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to generate route.");
                progress.Visibility = Visibility.Collapsed;
            }

        }

        /// <summary>
        /// Method to get directions
        /// </summary>
        private void GetDirections()
        {
            if (Utilities.IsConnectedToNetwork())
            {
                ShowRoute();
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
        private void NavigateToServices(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(PageURL.navigateToMapServicesURL + "?action=drawPushpin", UriKind.Relative));
        }

        /// <summary>
        /// OnNavigatedFrom event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App.TombStonedPageURL = PageURL.navigateToLHSDetailsMapURL;

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
            NavigationService.Navigate(new Uri(PageURL.navigateToMapServicesURL+"?action=drawPushpin", UriKind.Relative));
        }

        #endregion
    }
}

