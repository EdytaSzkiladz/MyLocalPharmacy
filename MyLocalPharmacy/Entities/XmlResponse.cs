using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Entities
{


    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class feed
    {

        private feedEntry entryField;

        /// <remarks/>
        public feedEntry entry
        {
            get
            {
                return this.entryField;
            }
            set
            {
                this.entryField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class feedEntry
    {

        private string idField;

        private feedEntryTitle titleField;

        private System.DateTime updatedField;

        private feedEntryLink[] linkField;

        private feedEntryContent contentField;

        /// <remarks/>
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public feedEntryTitle title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public System.DateTime updated
        {
            get
            {
                return this.updatedField;
            }
            set
            {
                this.updatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("link")]
        public feedEntryLink[] link
        {
            get
            {
                return this.linkField;
            }
            set
            {
                this.linkField = value;
            }
        }

        /// <remarks/>
        public feedEntryContent content
        {
            get
            {
                return this.contentField;
            }
            set
            {
                this.contentField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class feedEntryTitle
    {

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class feedEntryLink
    {

        private string relField;

        private string titleField;

        private string hrefField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string rel
        {
            get
            {
                return this.relField;
            }
            set
            {
                this.relField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string href
        {
            get
            {
                return this.hrefField;
            }
            set
            {
                this.hrefField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class feedEntryContent
    {

        private organisationSummary organisationSummaryField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://syndication.nhschoices.nhs.uk/services")]
        public organisationSummary organisationSummary
        {
            get
            {
                return this.organisationSummaryField;
            }
            set
            {
                this.organisationSummaryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://syndication.nhschoices.nhs.uk/services")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://syndication.nhschoices.nhs.uk/services", IsNullable = false)]
    public partial class organisationSummary
    {

        private string nameField;

        private string odsCodeField;

        private organisationSummaryAddress addressField;

        private organisationSummaryContact contactField;

        private organisationSummaryGeographicCoordinates geographicCoordinatesField;

        private decimal distanceField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string odsCode
        {
            get
            {
                return this.odsCodeField;
            }
            set
            {
                this.odsCodeField = value;
            }
        }

        /// <remarks/>
        public organisationSummaryAddress address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public organisationSummaryContact contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }

        /// <remarks/>
        public organisationSummaryGeographicCoordinates geographicCoordinates
        {
            get
            {
                return this.geographicCoordinatesField;
            }
            set
            {
                this.geographicCoordinatesField = value;
            }
        }

        /// <remarks/>
        public decimal Distance
        {
            get
            {
                return this.distanceField;
            }
            set
            {
                this.distanceField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://syndication.nhschoices.nhs.uk/services")]
    public partial class organisationSummaryAddress
    {

        private string[] addressLineField;

        private string postcodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("addressLine")]
        public string[] addressLine
        {
            get
            {
                return this.addressLineField;
            }
            set
            {
                this.addressLineField = value;
            }
        }

        /// <remarks/>
        public string postcode
        {
            get
            {
                return this.postcodeField;
            }
            set
            {
                this.postcodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://syndication.nhschoices.nhs.uk/services")]
    public partial class organisationSummaryContact
    {

        private string telephoneField;

        private string typeField;

        /// <remarks/>
        public string telephone
        {
            get
            {
                return this.telephoneField;
            }
            set
            {
                this.telephoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://syndication.nhschoices.nhs.uk/services")]
    public partial class organisationSummaryGeographicCoordinates
    {

        private decimal longitudeField;

        private decimal latitudeField;

        /// <remarks/>
        public decimal longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }

        /// <remarks/>
        public decimal latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }
    }

}

