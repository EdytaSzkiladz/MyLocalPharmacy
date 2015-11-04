using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    [DataContract]
    public class SelectSurgenFeedData : IComparable<SelectSurgenFeedData>
    {
        #region Data Members

        public string _name;
        [DataMember(Name = "Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [DataMember(Name = "OdsCode")]
        public string _odsCode;

        public string OdsCode
        {
            get { return _odsCode; }
            set { _odsCode = value; }
        }
        public string _addressLine1;
        [DataMember(Name = "AddressLine1")]
        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; }
        }
        public string _addressLine2;
        [DataMember(Name = "AddressLine2")]
        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; }
        }
        public string _addressLine3;
        [DataMember(Name = "AddressLine3")]
        public string AddressLine3
        {
            get { return _addressLine3; }
            set { _addressLine3 = value; }
        }
        public string _addressLine4;
        [DataMember(Name = "AddressLine4")]
        public string AddressLine4
        {
            get { return _addressLine4; }
            set { _addressLine4 = value; }
        }
        #endregion

        public int CompareTo(SelectSurgenFeedData b)
        {
            // Alphabetic sort name[A to Z]
            return this.Name.CompareTo(b.Name);
        }
    }
}
