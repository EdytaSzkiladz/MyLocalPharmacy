using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    public class AccountDisabledViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountDisabledViewModel()
        {
            if (App.ObjBrandingResponse != null)
            {
                GetPharmacyInformationResponse objbrandinfo = new GetPharmacyInformationResponse();
                objbrandinfo = App.ObjBrandingResponse;
                PrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;
                AppBarPrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;
                SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
                FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
                PharmacyName = App.ObjBrandingResponse.payload.branding_data.pharmacy_name;
                AddressLine1 = objbrandinfo.payload.branding_data.address1;
                AddressLine2 = objbrandinfo.payload.branding_data.address2;
                AddressLine3 = objbrandinfo.payload.branding_data.city;
                PinCode = objbrandinfo.payload.branding_data.postcode;
                PhoneNumber = objbrandinfo.payload.branding_data.phone;
                
            }
        } 
        #endregion

        #region Properties
        /// <summary>
        /// For AppBar PrimaryColor
        /// </summary>
        private string _appBarPrimaryColour;
        [DataMember]
        public string AppBarPrimaryColour
        {
            get { return _appBarPrimaryColour; }
            set
            {
                _appBarPrimaryColour = value;
                OnPropertyChanged("AppBarPrimaryColour");
            }
        }

        /// <summary>
        /// Property for PharmacyName
        /// </summary>
        private string _pharmacyName;
        [DataMember]
        public string PharmacyName
        {
            get { return _pharmacyName; }
            set { _pharmacyName = value; OnPropertyChanged("PharmacyName"); }
        }

        /// <summary>
        /// Property for AddressLine1
        /// </summary>
        private string _addressLine1;
        [DataMember]
        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; OnPropertyChanged("AddressLine1"); }
        }
        /// <summary>
        /// Property for AddressLine2
        /// </summary>
        private string _addressLine2;
        [DataMember]
        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; OnPropertyChanged("AddressLine2"); }
        }
        /// <summary>
        /// Property for AddressLine3
        /// </summary>
        private string _addressLine3;
        [DataMember]
        public string AddressLine3
        {
            get { return _addressLine3; }
            set { _addressLine3 = value; OnPropertyChanged("AddressLine3"); }
        }
        /// <summary>
        /// Property for PinCode
        /// </summary>
        private string _pinCode;
        [DataMember]
        public string PinCode
        {
            get { return _pinCode; }
            set { _pinCode = value; OnPropertyChanged("PinCode"); }
        }

        /// <summary>
        /// Property for PhoneNumber
        /// </summary>
        private string _phoneNumber;
        [DataMember]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }


        /// <summary>
        /// For Primary Color
        /// </summary>
        private string _primaryColour;
        [DataMember]
        public string PrimaryColour
        {
            get { return _primaryColour; }
            set
            {
                _primaryColour = value;
                OnPropertyChanged("PrimaryColour");
            }
        }
        /// <summary>
        /// For Secondary Color
        /// </summary>
        private SolidColorBrush _secondaryColour;
        [IgnoreDataMember]
        public SolidColorBrush SecondaryColour
        {
            get { return _secondaryColour; }
            set
            {
                _secondaryColour = value;
                OnPropertyChanged("SecondaryColour");
            }
        }

        /// <summary>
        /// For Font Color
        /// </summary>
        private SolidColorBrush _fontColor;
        [IgnoreDataMember]
        public SolidColorBrush FontColor
        {
            get { return _fontColor; }
            set
            {
                _fontColor = value;
                OnPropertyChanged("FontColor");
            }
        } 
        #endregion
    }
}
