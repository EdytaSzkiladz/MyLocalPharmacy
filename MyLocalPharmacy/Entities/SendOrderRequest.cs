using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{

    [DataContract]
    public class Prescription
    {
        #region Data Members
        [DataMember(Name = "drugname")]
        public string drugname { get; set; }
        [DataMember(Name = "reason")]
        public string reason { get; set; }
        [DataMember(Name = "quantity")]
        public object quantity { get; set; }
        [DataMember(Name = "vmp")]
        public string vmp { get; set; }
        [DataMember(Name = "vmpp")]
        public string vmpp { get; set; }
        [DataMember(Name = "amp")]
        public string amp { get; set; }
        [DataMember(Name = "ampp")]
        public string ampp { get; set; }
        #endregion
    }
    [DataContract]
    public class SendOrderRequest
    {
        #region Data Members
        [DataMember(Name = "pharmacyid")]
        public string pharmacyid { get; set; }
        [DataMember(Name = "mail")]
        public string mail { get; set; }
        [DataMember(Name = "pin")]
        public string pin { get; set; }
        [DataMember(Name = "prescriptions")]
        public List<Prescription> prescriptions { get; set; }
        [DataMember(Name = "system_version")]
        public string system_version { get; set; }
        [DataMember(Name = "app_version")]
        public string app_version { get; set; }
        [DataMember(Name = "branding_version")]
        public string branding_version { get; set; }
        [DataMember(Name = "drugs_hash")]
        public string drugs_hash { get; set; }
        #endregion
    }

}
