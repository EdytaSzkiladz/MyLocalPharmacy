using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
   
    public class GPSurgeriesFeedData
    {
        #region Data Members

        private string _name;
       
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _addressline1;

        public string Addressline1
        {
            get { return _addressline1; }
            set { _addressline1 = value; }
        }

        private string _addressline2;

        public string Addressline2
        {
            get { return _addressline2; }
            set { _addressline2 = value; }
        }

        private string _addressline3;

        public string Addressline3
        {
            get { return _addressline3; }
            set { _addressline3 = value; }
        }

        private string _addressline4;

        public string Addressline4
        {
            get { return _addressline4; }
            set { _addressline4 = value; }
        }

        private string _addressline5;

        public string Addressline5
        {
            get { return _addressline5; }
            set { _addressline5 = value; }
        }

        private string _postCode;

        public string PostCode
        {
            get { return _postCode; }
            set { _postCode = value; }
        }

        private string _telephone;

        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        private decimal _latitude;

        public decimal Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        private decimal _longitude;

        public decimal Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }



        #endregion           

    }
}
