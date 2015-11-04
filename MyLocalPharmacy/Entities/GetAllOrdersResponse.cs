using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{

       [DataContract]
        public class PrescriptionGetAllOrders
        {
            #region DataMembers
                [DataMember(Name = "status")]
                public string status { get; set; }
                [DataMember(Name = "updated")]
                public double updated { get; set; }
                [DataMember(Name = "vmpp")]
                public string vmpp { get; set; }
                [DataMember(Name = "created")]
                public double created { get; set; }
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

        [DataContract]
        public class PrescriptionGetEachOrders
        {
            #region DataMembers
                [DataMember(Name = "pharmacyid")]
                public string pharmacyid { get; set; }
                [DataMember(Name = "pres")]
                public List<PrescriptionGetAllOrders> pres { get; set; }
                [DataMember(Name = "pharmacyname")]
                public string pharmacyname { get; set; }
            #endregion
           
        }

        [DataContract]
        public class PayloadGetAllOrdersResponse
        {
            #region DataMembers
                [DataMember(Name = "prescriptions")]
                public List<PrescriptionGetEachOrders> prescriptions { get; set; }
            #endregion
           
        }

        [DataContract]
        public class GetAllOrdersResponse
        {
            #region DataMembers
                [DataMember(Name = "status")]
                public int status { get; set; }
                [DataMember(Name = "message")]
                public string message { get; set; }
                [DataMember(Name = "payload")]
                public PayloadGetAllOrdersResponse payload { get; set; }
            #endregion
            
        }

}
