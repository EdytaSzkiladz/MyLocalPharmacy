using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    public class GPSPosition
    {
        #region Declarations
        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }

        public GPSPosition()
        {
            Latitude = 0.0;
            Longitude = 0.0;
        }

        public GPSPosition(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        #endregion
    }
}
