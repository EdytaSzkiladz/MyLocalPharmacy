using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.ViewModel
{
    public class ConditionLeafletViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ConditionLeafletViewModel()
        {
            if (App.ObjBrandingResponse != null)
            {
                AppBarPrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;

            }
            else
            {
                AppBarPrimaryColour = RxConstants.PrimaryColourCodeAppbar;

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
        #endregion
       
    }
}
