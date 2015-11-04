using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    public class Surgery
    {
        #region Data Members
        public string name { get; set; }
        public string address { get; set; } 
        #endregion
    }
    
    public class Payload
    {
        #region Data Members
        public string status { get; set; }
        public int sms_confirmed { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public int mail_confirmed { get; set; }
        public string birthdate { get; set; }
        public int sex { get; set; }
        public string phone { get; set; }
        public string postcode { get; set; }
        public string nhs { get; set; }
        public string country { get; set; }
        public string mail { get; set; }
        public string pharmacyid { get; set; }
        public List<string> devices { get; set; }
        public string verifyby { get; set; }
        public Surgery surgery { get; set; }
        public string pharmacyname { get; set; } 
        #endregion
    }

    public class LoginResponse
    {
        #region Data Members
        public int status { get; set; }
        public string message { get; set; }
        public Payload payload { get; set; } 
        #endregion
    }    
}
