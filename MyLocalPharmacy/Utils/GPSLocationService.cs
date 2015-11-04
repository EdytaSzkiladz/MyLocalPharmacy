using MyLocalPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLocalPharmacy.Utils
{
    public class GPSLocationService
    {
        #region Declarations
        GeoCoordinateWatcher _watcher;
        PersistentDataStorage<GPSPosition> objData = new PersistentDataStorage<GPSPosition>();

        public string StatusMsg
        {
            get;
            set;
        }
        #endregion

        #region GPS location service
        /// <summary>
        ///Method to start location service
        /// </summary>
        /// <returns></returns>
        public void StartLocationService()
        {


            if (null == _watcher)
            {
                _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default); // using default accuracy
                _watcher.MovementThreshold = 20; // use MovementThreshold to ignore noise in the signal

                //declare event handlers for statuschanged and positionchanged
                _watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
                _watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
                _watcher.Start();
            }
        }

        /// <summary>
        ///Method to stop location service
        /// </summary>
        /// <returns></returns>
        public void StopLocationService()
        {
            if (null != _watcher)
                _watcher.Stop();
        }

        /// <summary>
        ///Method to get Gps location 
        /// </summary>
        /// <returns></returns>
        public void GetGpsLocation()
        {
            if (_watcher == null)
                _watcher = new GeoCoordinateWatcher();
            if (!_watcher.Position.Location.IsUnknown && _watcher.Status == GeoPositionStatus.Ready)
            {
                objData.Backup(StateHelper._gpsPOSITIONKey, new MyLocalPharmacy.Entities.GPSPosition(_watcher.Position.Location.Latitude, _watcher.Position.Location.Longitude));
                App.GPSCoordinatesAvailable = true;
                App.LastGPSPingedTime = DateTime.Now;
            }

            else
            {
                if (objData.Restore<GPSPosition>(StateHelper._gpsPOSITIONKey) == null)
                {
                    App.GPSCoordinatesAvailable = false;
                    //SimulatorTesting();
                }
                else
                    App.GPSCoordinatesAvailable = true;
            }
        }

        /// <summary>
        ///Method to get location service status
        /// </summary>
        /// <returns></returns>
        public GeoPositionStatus GetLocationServiceStatus()
        {
            return _watcher.Status;
        }
        #endregion

        //Remove this block before deploying it on Windows mobile.
        void SimulatorTesting()
        {

            //GeoCoordinate gc = new GeoCoordinate(9.98246, 76.35987);
            ////GeoCoordinate gc = new GeoCoordinate(34.05223420, -118.24368490);
            //objData.Backup(Settings.GPSPOSITION, new SaveWorks.Entities.GPSPosition(gc.Latitude, gc.Longitude));
            //App.appVariables.GPSCoordinatesAvailable = true;
            //App.appVariables.LastGPSPingedTime = DateTime.Now;

        }
        /// <summary>
        /// Event handler for the GeoCoordinateWatcher.StatusChanged event.
        /// </summary>
        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {

            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    // The Location Service is disabled or unsupported.
                    // Check to see whether the user has disabled the Location Service.
                    App.GPSCoordinatesAvailable = false;
                    objData.Remove(StateHelper._gpsPOSITIONKey);
                    if (_watcher.Permission == GeoPositionPermission.Denied)
                    {
                        // The user has disabled the Location Service on the device.
                        StatusMsg = @"GPS Location Service is disabled on this device.Please enable GPS. ";
                        MessageBox.Show(StatusMsg);
                    }
                    else
                    {
                        StatusMsg = "Location is not functioning on this device.";
                        MessageBox.Show(StatusMsg);
                    }

                    break;

                case GeoPositionStatus.Initializing:
                    StatusMsg = "The Location Service is initializing.";
                  //  MessageBox.Show(StatusMsg);
                    break;

                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data.
                    break;

                case GeoPositionStatus.Ready:
                    // The Location Service is working and is receiving location data.
                    App.GPSCoordinatesAvailable = true;
                    objData.Backup(StateHelper._gpsPOSITIONKey, new MyLocalPharmacy.Entities.GPSPosition(_watcher.Position.Location.Latitude, _watcher.Position.Location.Longitude));
                    App.LastGPSPingedTime = DateTime.Now;
                    break;
            }
        }

        /// <summary>
        /// Event handler for the GeoCoordinateWatcher.PositionChanged event.
        /// </summary>
        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            //objData.Remove(Settings.GPSPOSITION);
            if (!_watcher.Position.Location.IsUnknown && _watcher.Status == GeoPositionStatus.Ready)
            {
                objData.Backup(StateHelper._gpsPOSITIONKey, new MyLocalPharmacy.Entities.GPSPosition(e.Position.Location.Latitude, e.Position.Location.Longitude));
                App.GPSCoordinatesAvailable = true;
                App.LastGPSPingedTime = DateTime.Now;
            }
            else
            {
                //App.appVariables.GPSCoordinatesAvailable = false;
                if (objData.Restore<GPSPosition>(StateHelper._gpsPOSITIONKey) == null)
                    App.GPSCoordinatesAvailable = false;
                else
                    App.GPSCoordinatesAvailable = true;
            }


        }
    }
}
