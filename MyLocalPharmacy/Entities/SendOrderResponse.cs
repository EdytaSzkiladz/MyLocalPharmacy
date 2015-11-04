using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{


    [DataContract]
    public class SendOrderResponse
    {
        #region DataMembers
            [DataMember(Name = "status")]
            public int status { get; set; }
            [DataMember(Name = "message")]
            public string message { get; set; }
            [DataMember(Name = "payload")]
            public PayloadResponse payload { get; set; }
        #endregion
        
    }

    [DataContract]
    public class PayloadResponse
    {
        #region DataMember
            [DataMember(Name = "prescriptions")]
            public List<PrescriptionResponse> prescriptions { get; set; }
        #endregion
       
    }

    [DataContract]
    public class PrescriptionResponse
    {
        #region DataMembers
            [DataMember(Name = "status")]
            public string status { get; set; }
            [DataMember(Name = "updated")]
            public float updated { get; set; }
            [DataMember(Name = "vmpp")]
            public string vmpp { get; set; }
            [DataMember(Name = "created")]
            public float created { get; set; }
            [DataMember(Name = "reason")]
            public string reason { get; set; }
            [DataMember(Name = "drugname")]
            public string drugname { get; set; }
            [DataMember(Name = "amp")]
            public string amp { get; set; }
            [DataMember(Name = "ampp")]
            public string ampp { get; set; }
            [DataMember(Name = "id")]
            public int id { get; set; }
            [DataMember(Name = "vmp")]
            public string vmp { get; set; }
            [DataMember(Name = "quantity")]
            public string quantity { get; set; }
        #endregion
       
    }
}
