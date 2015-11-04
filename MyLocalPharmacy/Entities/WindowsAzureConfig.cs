using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLocalPharmacy.Entities
{
    public  class WindowsAzureConfig
    {
        private string _devHubName;

        public string DevHubName
        {
            get { return _devHubName; }
            set { _devHubName = value; }
        }

        private string _devListenAccessConnectionString;

        public string DevListenAccessConnectionString
        {
            get { return _devListenAccessConnectionString; }
            set { _devListenAccessConnectionString = value; }
        }

        private string _productionHubName;

        public string ProductionHubName
        {
            get { return _productionHubName; }
            set { _productionHubName = value; }
        }

        private string _productionListenAccessConnectionString;

        public string ProductionListenAccessConnectionString
        {
            get { return _productionListenAccessConnectionString; }
            set { _productionListenAccessConnectionString = value; }
        }
        

    }
}
