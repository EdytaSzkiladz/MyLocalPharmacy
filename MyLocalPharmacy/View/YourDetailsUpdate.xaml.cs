using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyLocalPharmacy.ViewModel;
using MyLocalPharmacy.Utils;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace MyLocalPharmacy.View
{
    public partial class YourDetails : PhoneApplicationPage
    {
        #region Declarations
        private YourDetailsUpdateViewModel yourDetailsUpdateVM;
        private string _yourDetailsUpdateVMKey = "YourDetailsUpdateVM";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public YourDetails()
        {
            InitializeComponent();

            (App.Current.Resources["PhoneRadioCheckBoxCheckBrush"] as SolidColorBrush).Color = Colors.Black;
            LayoutRoot.IsHitTestVisible = true;
        }
        #endregion

        #region Events               
        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            LayoutRoot.IsHitTestVisible = true;
            yourDetailsUpdateVM = new StateHelper().RestorePageLevelData<YourDetailsUpdateViewModel>(_yourDetailsUpdateVMKey);
            if ((App.IsToombStoned) || (!string.IsNullOrEmpty(App.SurgeonSaved)))
            {
      
                if (yourDetailsUpdateVM != null)
                {
                    yourDetailsUpdateVM.PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);

                    yourDetailsUpdateVM.SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);

                    yourDetailsUpdateVM.FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
                    yourDetailsUpdateVM.ButtonValueOnupdate = App.SurgeonSaved;
                }
                else
                {
                    yourDetailsUpdateVM = new YourDetailsUpdateViewModel(App.ObjLgResponse);
                }
            }
            else
            {
                if (yourDetailsUpdateVM == null)
                {
                    yourDetailsUpdateVM = new YourDetailsUpdateViewModel(App.ObjLgResponse);
                }
                else if (string.IsNullOrEmpty(App.SurgeonSaved) || string.IsNullOrWhiteSpace(App.SurgeonSaved))
                {
                    yourDetailsUpdateVM.ButtonValueOnupdate = "Choose Doctor for surgery (Optional)";
                }
            }

            this.DataContext = yourDetailsUpdateVM;

            new StateHelper().ClearPageLevelData<YourDetailsUpdateViewModel>(_yourDetailsUpdateVMKey, yourDetailsUpdateVM);

            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToYourDetailsUpdateURL;

            if (!e.IsNavigationInitiator)
            {
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }

        }
        /// <summary>
        /// OnNavigatedFrom event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if(popupSuccess.IsOpen)
            {
                popupSuccess.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
               
            }
            if (!(e.NavigationMode == NavigationMode.Back))
            {
                new StateHelper().SavePageLevelData<YourDetailsUpdateViewModel>(_yourDetailsUpdateVMKey, yourDetailsUpdateVM);
            }
        }

        /// <summary>
        /// Overriding backkeypress
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {

            if (popup.IsOpen)
            {
                popup.IsOpen = false;
                LayoutRoot.IsHitTestVisible = true;
                e.Cancel = true;
            }
          
            else if (popupSuccess.IsOpen)
            {
                LayoutRoot.IsHitTestVisible = true;
                if (App.IsFromRejected)
                {
                    App.IsFromRejected = false;
                    
                    NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.Relative));
                }
                else
                {
                    
                    NavigationService.Navigate(new Uri(PageURL.navigateToSettingPageURL, UriKind.Relative));
                }
            }
            else
            {
                if (App.IsFromRejected)
                {
                    App.IsFromRejected = true;

                    NavigationService.Navigate(new Uri(PageURL.navigateToHomePanoramaURL, UriKind.Relative));
                }
                else
                {

                    NavigationService.Navigate(new Uri(PageURL.navigateToSettingPageURL, UriKind.Relative));
                }
            }

        }
        #endregion
    }
}