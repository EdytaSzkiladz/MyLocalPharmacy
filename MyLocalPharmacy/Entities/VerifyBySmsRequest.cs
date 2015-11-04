using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    [DataContract]
    public class VerifyBySmsRequest
    {
        #region DataMembers
            [DataMember]
            public string mail { get; set; }
            [DataMember]
            public string pin { get; set; }
            [DataMember]
            public string pharmacyid { get; set; }
            [DataMember]
            public string code { get; set; }
        [DataMember(Name = "system_version")]
        public string system_version { get; set; }
        [DataMember(Name = "app_version")]
        public string app_version { get; set; }
        [DataMember(Name = "branding_version")]
        public string branding_version { get; set; }
        #endregion

    }
}
