using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{

    [DataContract]
    public class SendUserDetailsRequestSurgery
    {
        #region Data Members
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "address")]
        public string address { get; set; } 
        #endregion
    }

    [DataContract]
    public class SendUserDetailsRequest
    {
        #region Data Members
        [DataMember(Name = "pharmacyid")]
        public string pharmacyid { get; set; }
        [DataMember(Name = "model")]
        public string model { get; set; }
        [DataMember(Name = "os")]
        public string os { get; set; }
        [DataMember(Name = "fullname")]
        public string fullname { get; set; }
        [DataMember(Name = "nhs")]
        public string nhs { get; set; }
        [DataMember(Name = "birthdate")]
        public string birthdate { get; set; }
        [DataMember(Name = "address1")]
        public string address1 { get; set; }
        [DataMember(Name = "address2")]
        public string address2 { get; set; }
        [DataMember(Name = "postcode")]
        public string postcode { get; set; }
        [DataMember(Name = "phone")]
        public string phone { get; set; }
        [DataMember(Name = "mail")]
        public string mail { get; set; }
        [DataMember(Name = "sex")]
        public string sex { get; set; }
        [DataMember(Name = "pin")]
        public string pin { get; set; }
        [DataMember(Name = "country")]
        public string country { get; set; }
        [DataMember(Name = "surgery")]
        public SendUserDetailsRequestSurgery surgery { get; set; }
        [DataMember(Name = "system_version")]
        public string system_version { get; set; }
        [DataMember(Name = "app_version")]
        public string app_version { get; set; }
        [DataMember(Name = "branding_version")]
        public string branding_version { get; set; }
        #endregion
    }

}
