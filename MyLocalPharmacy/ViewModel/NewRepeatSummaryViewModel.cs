using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Entities;
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
    public class NewRepeatSummaryViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public NewRepeatSummaryViewModel()
        {
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
            if (App.prescriptionCollection != null || DrugSearchCollection != null)
            {
                DrugSearchCollection = App.prescriptionCollection;

                IsNoItemsVisibility = Visibility.Collapsed;
                ListBoxHeaderVisibility = Visibility.Visible;

            }
            else
            {
                IsNoItemsVisibility = Visibility.Visible;
                ListBoxHeaderVisibility = Visibility.Collapsed;
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
        /// Property to set visibility of "No items."
        /// </summary>
        private Visibility _isNoItemsVisibility;
        [DataMember]
        public Visibility IsNoItemsVisibility
        {
            get { return _isNoItemsVisibility; }
            set { _isNoItemsVisibility = value; OnPropertyChanged("IsNoItemsVisibility"); }
        }

        /// <summary>
        /// Property to set visibility of "ListBox title"
        /// </summary>
        private Visibility _listBoxHeaderVisibility;
        [DataMember]
        public Visibility ListBoxHeaderVisibility
        {
            get { return _listBoxHeaderVisibility; }
            set { _listBoxHeaderVisibility = value; OnPropertyChanged("ListBoxHeaderVisibility"); }
        }

        /// <summary>
        /// Drugs Collection
        /// </summary>
        private PrescriptionCollection _drugSearchCollection;
        [DataMember]
        public PrescriptionCollection DrugSearchCollection
        {
            get { return _drugSearchCollection; }
            set
            {
                _drugSearchCollection = value;
                OnPropertyChanged("DrugSearchCollection");
            }
        }


       
        /// <summary>
        /// Property to set hit visibility
        /// </summary>
        private bool _hitVisibility;
        [DataMember]
        public bool HitVisibility
        {
            get { return _hitVisibility; }
            set { _hitVisibility = value; OnPropertyChanged("HitVisibility"); }
        }



        /// <summary>
        /// Property for navigation to Confirm medicines
        /// </summary>
        private RelayCommand _nextCommand;
        [IgnoreDataMember]
        public RelayCommand NextCommand
        {

            get
            {
                if (_nextCommand == null)
                {
                    _nextCommand = new RelayCommand(NextTapped);
                    _nextCommand.Enabled = true;
                }

                return _nextCommand;
            }
            set { _nextCommand = value; }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method invoked on tapping next button
        /// </summary>
        private void NextTapped()
        {
            if (App.prescriptionCollection != null)
            {
                if (App.prescriptionCollection.Count > 0)
                {

                    INavigationService navigationService = this.GetService<INavigationService>();
                    navigationService.Navigate(PageURL.navigateToConfirmRepeatURL);
                }
                else
                {
                    MessageBox.Show("Please add atleast one drug to continue.");
                }
            }
            else
            {
                MessageBox.Show("Please add atleast one drug to continue.");
            }
        }

        #endregion

    }
}
