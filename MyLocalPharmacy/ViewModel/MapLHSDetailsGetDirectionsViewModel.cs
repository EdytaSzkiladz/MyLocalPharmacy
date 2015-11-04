using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    public class MapLHSDetailsGetDirectionsViewModel:BaseViewModel
    {
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MapLHSDetailsGetDirectionsViewModel()
        {
            if (App.ObjBrandingResponse != null)
            {
                PrimaryColour = App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour;
                SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
                FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
            }
            else
            {
                PrimaryColour = RxConstants.PrimaryColourCode;
                SecondaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);
                FontColor = Utilities.GetColorFromHexa(RxConstants.FontColourCode);
            }
        } 
        #endregion

         #region Properties
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
        /// For Progress Bar
        /// </summary>
        private Visibility progressBarVisibilty = Visibility.Visible;
        [DataMember]
        public Visibility ProgressBarVisibilty
        {
            get
            {
                return progressBarVisibilty;
            }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    progressBarVisibilty = value;
                    OnPropertyChanged("ProgressBarVisibilty");
                });

            }
        } 
        #endregion
    }
}
