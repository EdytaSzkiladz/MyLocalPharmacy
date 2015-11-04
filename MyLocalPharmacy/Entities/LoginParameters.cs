using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    [DataContract]
    public  class LoginParameters
    {
        #region Data Members
        [DataMember(Name = "pin")]
        public string Pin { get; set; }

        [DataMember(Name = "mail")]
        public string Mail { get; set; }


        [DataMember(Name = "pharmacyid")]
        public string Pharmacyid { get; set; }
        [DataMember(Name = "system_version")]
        public string system_version { get; set; }
        [DataMember(Name = "app_version")]
        public string app_version { get; set; }
        [DataMember(Name = "branding_version")]
        public string branding_version { get; set; }
        #endregion            
    }
}
