using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Model
{
    [DataContract]
    public class PillsDetails
    {
        #region Data Members
        private string _title;
        [DataMember]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _weblink;
        [DataMember]
        public string WebLink
        {
            get { return _weblink; }
            set { _weblink = value; }
        }
        private string _uri;
        [DataMember]
        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
        } 
        #endregion            
    }
}
