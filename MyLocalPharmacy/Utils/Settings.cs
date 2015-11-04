using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Utils
{
    public class Settings
    {
    
        #region GPS Location Service
        public static void startGPSLocationService()
        {

            GPSLocationService gpsService = new GPSLocationService();
            gpsService.StartLocationService();
        }
        public static void stopGPSLocationService()
        {
            GPSLocationService gpsService = new GPSLocationService();
            gpsService.StopLocationService();
        }
        #endregion
    }
}
