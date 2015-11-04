using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    [DataContract]
    public class UpdatePushRequest
    {
        #region Data Members
        [DataMember(Name = "pharmacyid")]
        public string pharmacyid { get; set; }

        [DataMember(Name = "deviceid")]
        public string deviceid { get; set; }
        
        [DataMember(Name = "os")]
        public string os { get; set; }

        [DataMember(Name = "pushid")]
        public string pushid { get; set; }
        
        [DataMember(Name = "mail")]
        public string mail { get; set; }
        
        [DataMember(Name = "pin")]
        public string pin { get; set; }
        [DataMember(Name = "system_version")]
        public string system_version { get; set; }
        [DataMember(Name = "app_version")]
        public string app_version { get; set; }
        [DataMember(Name = "branding_version")]
        public string branding_version { get; set; }

        #endregion
    }
    
    
}
