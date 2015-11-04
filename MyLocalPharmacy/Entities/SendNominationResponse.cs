using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{   
        [DataContract]
        public class SendNominationResponseSurgery
        {
            #region Data Members
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "address")]
            public string address { get; set; } 
            #endregion
        }

        [DataContract]
        public class SendNominationResponsePayload
        {
            #region Data Members
            [DataMember(Name = "status")]
            public string status { get; set; }
            [DataMember(Name = "sms_confirmed")]
            public int sms_confirmed { get; set; }
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "address1")]
            public string address1 { get; set; }
            [DataMember(Name = "address2")]
            public string address2 { get; set; }
            [DataMember(Name = "mail_confirmed")]
            public int mail_confirmed { get; set; }
            [DataMember(Name = "birthdate")]
            public string birthdate { get; set; }
            [DataMember(Name = "sex")]
            public int sex { get; set; }
            [DataMember(Name = "phone")]
            public string phone { get; set; }
            [DataMember(Name = "postcode")]
            public string postcode { get; set; }
            [DataMember(Name = "nhs")]
            public string nhs { get; set; }
            [DataMember(Name = "country")]
            public string country { get; set; }
            [DataMember(Name = "mail")]
            public string mail { get; set; }
            [DataMember(Name = "pharmacyid")]
            public string pharmacyid { get; set; }
            [DataMember(Name = "devices")]
            public List<string> devices { get; set; }
            [DataMember(Name = "verifyby")]
            public string verifyby { get; set; }
            [DataMember(Name = "surgery")]
            public SendNominationResponseSurgery surgery { get; set; }
            [DataMember(Name = "pharmacyname")]
            public string pharmacyname { get; set; } 
            #endregion
        }

        [DataContract]
        public class SendNominationResponse
        {
            #region Data Members
            [DataMember(Name = "status")]
            public int status { get; set; }
            [DataMember(Name = "message")]
            public string message { get; set; }
            [DataMember(Name = "payload")]
            public SendNominationResponsePayload payload { get; set; } 
            #endregion
        }    
}
