using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Model
{
    [DataContract]
    public class PillsReminderModel
    {
        #region Data Members
        [DataMember(Name = "PillName")]
        public string PillName { get; set; }

        [DataMember(Name = "NumberOfPills")]
        public string NumberOfPills { get; set; } 
        #endregion
    }
}
