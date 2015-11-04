using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{
        [DataContract]
        public class Appearance
        {
            #region Data Members
            [DataMember(Name = "splash_url")]
            public string splash_url { get; set; }
            [DataMember(Name = "font_colour")]
            public string font_colour { get; set; }
            [DataMember(Name = "secondary_colour")]
            public string secondary_colour { get; set; }
            [DataMember(Name = "primary_colour")]
            public string primary_colour { get; set; } 
            #endregion
        }

        [DataContract]
        public class OpeningHour
        {
            #region Data Members
            [DataMember(Name = "open")]
            public string open { get; set; }
            [DataMember(Name = "dayname")]
            public string dayname { get; set; }
            [DataMember(Name = "is_closed")]
            public bool is_closed { get; set; }
            [DataMember(Name = "close")]
            public string close { get; set; } 
            #endregion
        }

        [DataContract]
        public class BrandingData
        {
            #region Data Members
            [DataMember(Name = "website")]
            public string website { get; set; }
            [DataMember(Name = "city")]
            public string city { get; set; }
            [DataMember(Name = "twitter_link")]
            public string twitter_link { get; set; }
            [DataMember(Name = "branch_name")]
            public string branch_name { get; set; }
            [DataMember(Name = "address1")]
            public string address1 { get; set; }
            [DataMember(Name = "address2")]
            public string address2 { get; set; }
            [DataMember(Name = "bank_holidays")]
            public List<object> bank_holidays { get; set; }
            [DataMember(Name = "appearance")]
            public Appearance appearance { get; set; }
            [DataMember(Name = "phone")]
            public string phone { get; set; }
            [DataMember(Name = "pharmacy_name")]
            public string pharmacy_name { get; set; }
            [DataMember(Name = "services")]
            public List<object> services { get; set; }
            [DataMember(Name = "postcode")]
            public string postcode { get; set; }
            [DataMember(Name = "opening_hours")]
            public List<OpeningHour> opening_hours { get; set; }
            [DataMember(Name = "pharmacist1")]
            public string pharmacist1 { get; set; }
            [DataMember(Name = "facebook_link")]
            public string facebook_link { get; set; }
            [DataMember(Name = "pharmacist2")]
            public string pharmacist2 { get; set; }
            [DataMember(Name = "additional_info")]
            public string additional_info { get; set; } 
            #endregion
        }

        [DataContract]
        public class AdvertData
        {
            #region Data Members
            [DataMember(Name = "is_image_url")]
            public bool is_image_url { get; set; }
            [DataMember(Name = "name")]
            public string name { get; set; }
            [DataMember(Name = "created")]
            public object created { get; set; }
            [DataMember(Name = "url")]
            public string url { get; set; }
            [DataMember(Name = "image_builder")]
            public ImageBuilder image_builder { get; set; }
            [DataMember(Name = "image_url")]
            public string image_url { get; set; } 
            #endregion
        }

    public class ImageBuilder
    {
        #region Data Members
        [DataMember(Name = "line1")]
        public string line1 { get; set; }
        [DataMember(Name = "line2")]
        public string line2 { get; set; }
        [DataMember(Name = "background_color")]
        public string background_color { get; set; }
        [DataMember(Name = "font_color")]
        public string font_color { get; set; }
        #endregion
    }

    [DataContract]
        public class GetPharmacyInformationResponsePayload
        {
            #region Data Members
            [DataMember(Name = "branding_hash")]
            public string branding_hash { get; set; }
            [DataMember(Name = "drugs_hash")]
            public string drugs_hash { get; set; }
            [DataMember(Name = "advert_hash")]
            public string advert_hash { get; set; }
            [DataMember(Name = "branding_data")]
            public BrandingData branding_data { get; set; }
            [DataMember(Name = "drugs_data")]
            public string drugs_data { get; set; }
            [DataMember(Name = "advert_data")]
            public List<AdvertData> advert_data { get; set; } 
            #endregion
        }

        [DataContract]
        public class GetPharmacyInformationResponse
        {
            #region Data Members
            [DataMember(Name = "status")]
            public int status { get; set; }
            [DataMember(Name = "message")]
            public string message { get; set; }
            [DataMember(Name = "payload")]
            public GetPharmacyInformationResponsePayload payload { get; set; } 
            #endregion
        }    
}
