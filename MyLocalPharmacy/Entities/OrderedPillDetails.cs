using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyLocalPharmacy.Entities
{
    public class OrderedPillDetails
    {
        #region Properties
            public string status { get; set; }

            public DateTime updated { get; set; }

            public string vmpp { get; set; }

            public DateTime created { get; set; }

            public string reason { get; set; }

            public string drugname { get; set; }

            public string amp { get; set; }

            public string ampp { get; set; }

            public int id { get; set; }

            public string vmp { get; set; }

            public string quantity { get; set; }

            public string timeRange { get; set; }

            public string time { get; set; }

            public string orderDate { get; set; }

            public DateTime orderTime { get; set; }

            public string StatusFontColour { get; set; }
        #endregion
        
    }
}
