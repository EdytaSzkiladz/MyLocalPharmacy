using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
    public class DrugDetails
    {
        #region Properties
            public int _id { get; set; }
            public string amp { get; set; }
            public string ampp { get; set; }
            public string drug_code { get; set; }
            public string drugform { get; set; }
            public string drugname { get; set; }
            public string size { get; set; }
            public string strenght { get; set; }
            public string vmp { get; set; }
            public string vmpp { get; set; }
        #endregion

    }
}
