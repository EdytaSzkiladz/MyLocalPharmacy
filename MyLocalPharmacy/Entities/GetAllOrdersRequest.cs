using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    [DataContract]
    public class GetAllOrdersRequest
    {
        #region Data Members
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
