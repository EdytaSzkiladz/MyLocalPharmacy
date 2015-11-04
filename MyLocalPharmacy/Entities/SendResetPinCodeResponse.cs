using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{

    [DataContract]
    public class SendResetPinCodeResponsePayload
    {
    }

    [DataContract]
    public class SendResetPinCodeResponse
    {
        #region Data Members
        [DataMember(Name = "status")]
        public int status { get; set; }
        [DataMember(Name = "message")]
        public string message { get; set; }
        [DataMember(Name = "payload")]
        public Payload payload { get; set; } 
        #endregion
    }

}
