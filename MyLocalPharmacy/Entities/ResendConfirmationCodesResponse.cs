using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{

    [DataContract]
    public class PayloadResendConfirmationCodesResponse
    {
    }

    [DataContract]
    public class ResendConfirmationCodesResponse
    {
        #region Data Members
        [DataMember(Name = "status")]
        public int status { get; set; }
        [DataMember(Name = "message")]
        public string message { get; set; }
        [DataMember(Name = "payload")]
        public PayloadResendConfirmationCodesResponse payload { get; set; } 
        #endregion
    }
}
