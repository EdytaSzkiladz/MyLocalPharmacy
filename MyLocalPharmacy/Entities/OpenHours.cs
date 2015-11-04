using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    public class OpenHours
    {
        #region Properties
            private string _dayname;

            public string DayName
            {
                get { return _dayname; }
                set { _dayname = value; }
            }
            private string _timings;

            public string Timings
            {
                get { return _timings; }
                set { _timings = value; }
            }
        #endregion
    }
}
