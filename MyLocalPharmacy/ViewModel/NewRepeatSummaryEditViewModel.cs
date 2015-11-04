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
    public class NewRepeatSummaryEditViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public NewRepeatSummaryEditViewModel()
        {
            DrugName = (App.prescriptionCollection.ElementAt(App.selectedDrugIndex).drugname).ToString();
            DrugQty = Convert.ToInt32((App.prescriptionCollection.ElementAt(App.selectedDrugIndex)).quantity);
            OrderReason = (App.prescriptionCollection.ElementAt(App.selectedDrugIndex)).reason.ToString();

            if (App.ObjBrandingResponse != null)
            {
                PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);
                SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
                FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
            }
            else
            {
                PrimaryColour = SecondaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);
                FontColor = Utilities.GetColorFromHexa(RxConstants.FontColourCode);
            }

        } 
        #endregion

        #region Properties

        /// <summary>
        /// For Primary Color
        /// </summary>
        private SolidColorBrush _primaryColour;
        [IgnoreDataMember]
        public SolidColorBrush PrimaryColour
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

        /// <summary>
        /// Propert for drug name
        /// </summary>
        private string _drugName;
        [DataMember]
        public string DrugName
        {
            get { return _drugName; }
            set
            {
                _drugName = value;
                OnPropertyChanged("DrugName");
            }
        }
        /// <summary>
        /// Property for pill order reason
        /// </summary>
        private string _orderReason;
        [DataMember]
        public string OrderReason
        {
            get { return _orderReason; }
            set
            {
                _orderReason = value;
                OnPropertyChanged("OrderReason");
            }
        }
        /// <summary>
        /// Property For Quantity
        /// </summary>
        private int _drugqty;
        [DataMember]
        public int DrugQty
        {
            get { return _drugqty; }
            set
            {
                _drugqty = value;
                OnPropertyChanged("DrugQty");
            }
        } 
        #endregion
    }
}
