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
using System.Windows.Media;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using SQLite;
using System.Collections.ObjectModel;
namespace MyLocalPharmacy.View
{
    public partial class YourDetailswithTC : PhoneApplicationPage
    {
        #region Declarations
        private YourDetailswithTCViewModel yourDetailsTCVM;
        private string _yourDetailsTCVMKey = "YourDetailsTCVM";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public YourDetailswithTC()
        {
            InitializeComponent();

            (App.Current.Resources["PhoneRadioCheckBoxCheckBrush"] as SolidColorBrush).Color = Colors.Black;
           
            App.IsPageUpdateYourDetailsafterLogin = true;

            App.IsPageHomePanorama = false;

            App.IsFromLoginScreen = false;
            LayoutRoot.IsHitTestVisible = true;
            popupSuccess.IsOpen = false;

        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to write db drug data
        /// </summary>
        /// <returns></returns>
        public async Task WriteToFile()
        {
            string data = App.DrugsData;

            byte[] decoded = System.Convert.FromBase64String(data);

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            var file = await local.CreateFileAsync("DataFile.sqlite",
            CreationCollisionOption.ReplaceExisting);

            //Write the data from the textbox.
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(decoded, 0, decoded.Length);
            }
            StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("DataFile.sqlite");
        }

        /// <summary>
        /// Remove entries from back stack
        /// </summary>
        private void Exit()
        {
            while (NavigationService.BackStack.Any())
            {
                NavigationService.RemoveBackEntry();

            }
        }
        #endregion

        #region Events
       
        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            yourDetailsTCVM = new StateHelper().RestorePageLevelData<YourDetailswithTCViewModel>(_yourDetailsTCVMKey);
            if ((App.IsToombStoned) || (!string.IsNullOrEmpty(App.SurgeonSaved)))
            {
     
                if (yourDetailsTCVM != null)
                {
                    yourDetailsTCVM.PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);

                    yourDetailsTCVM.SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);

                    yourDetailsTCVM.FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
                    yourDetailsTCVM.ButtonValue = App.SurgeonSaved;
                }
                else
                {
                    yourDetailsTCVM = new YourDetailswithTCViewModel(App.ObjLgResponse);
                }
            }
            else
            {
                if (yourDetailsTCVM == null)
                {
                    yourDetailsTCVM = new YourDetailswithTCViewModel(App.ObjLgResponse);
                }
                else if (string.IsNullOrEmpty(App.SurgeonSaved) || string.IsNullOrWhiteSpace(App.SurgeonSaved))
                {
                    yourDetailsTCVM.ButtonValue = "Choose Doctor for surgery (Optional)";
                }
            }
            
            this.DataContext = yourDetailsTCVM;

            new StateHelper().ClearPageLevelData<YourDetailswithTCViewModel>(_yourDetailsTCVMKey, yourDetailsTCVM);

            base.OnNavigatedTo(e);

            App.TombStonedPageURL = PageURL.navigateToYourDetailswithTCURL;

            if (!e.IsNavigationInitiator)
            {
                popupSuccess.IsOpen = false;
                popup.IsOpen = false;
                App.IsToombStoned = false;
                NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
            }
        }
        /// <summary>
        /// Overriding the backkey press
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
                Exit();  
            }
            else
            {
                Exit();
            } 

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (!(e.NavigationMode == NavigationMode.Back))
            {
                new StateHelper().SavePageLevelData<YourDetailswithTCViewModel>(_yourDetailsTCVMKey,yourDetailsTCVM);
            }
        }
        /// <summary>
        /// Asynchronously call update click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnTCUpdate_Click(object sender, RoutedEventArgs e)
        {
            await WriteToFile();
            if (scrollVw != null)
                this.scrollVw.ScrollToVerticalOffset(scrollVw.ScrollableHeight);
        }
        #endregion      
    }
}