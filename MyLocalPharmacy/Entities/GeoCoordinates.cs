using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    public class GeoCoordinates
    {
        #region Data Members
        private double latitude;
        public double XCoordinate
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
            }
        }

        private double longitude;
        public double YCoordinate
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
            }
        }

        private string data;
        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        } 
        #endregion
    }
}
