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
using MyLocalPharmacy.Utils;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using SQLite;
namespace MyLocalPharmacy.View
{
    public partial class YourDetailsLogin : PhoneApplicationPage
    {
        #region Declarations
        private YourDetailsLoginViewModel yourDetailsLoginVM;
        private string _yourDetailsLoginVMKey = "YourDetailsLoginVM";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public YourDetailsLogin()
        {
            InitializeComponent();

            (App.Current.Resources["PhoneRadioCheckBoxCheckBrush"] as SolidColorBrush).Color = Colors.Black;
            App.IsFromLoginScreen = false;
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
            popupSuccess.IsOpen = false;
            popup.IsOpen = false;
            yourDetailsLoginVM = new StateHelper().RestorePageLevelData<YourDetailsLoginViewModel>(_yourDetailsLoginVMKey);
            if ((App.IsToombStoned) || (!string.IsNullOrEmpty(App.SelectedSurgen)) )
            {
                
                if (yourDetailsLoginVM != null)
                {
                    if ((string.IsNullOrEmpty(App.SelectedSurgen) || string.IsNullOrWhiteSpace(App.SelectedSurgen)))
                    {
                        yourDetailsLoginVM.ButtonValue = "Choose Doctor for surgery (Optional)";
                    }
                    else
                    yourDetailsLoginVM.ButtonValue = App.SelectedSurgen;
                }
                else
                {
                    yourDetailsLoginVM = new YourDetailsLoginViewModel();
                }
            }
            else
            {
               
                if (yourDetailsLoginVM == null)
                {
                    
                    yourDetailsLoginVM = new YourDetailsLoginViewModel();
                }
                else if (string.IsNullOrEmpty(App.SelectedSurgen) || string.IsNullOrWhiteSpace(App.SelectedSurgen))
                {
                    yourDetailsLoginVM.ButtonValue = "Choose Doctor for surgery (Optional)";
                }
                else
                {
                    yourDetailsLoginVM.ButtonValue = App.SelectedSurgen;
                }
            }

            yourDetailsLoginVM.HitVisibility = true;
            yourDetailsLoginVM.ProgressBarVisibilty = Visibility.Collapsed;
            

            this.DataContext = yourDetailsLoginVM;
            new StateHelper().ClearPageLevelData<YourDetailsLoginViewModel>(_yourDetailsLoginVMKey, yourDetailsLoginVM);
           
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToYourDetailsLoginURL;
            if (!e.IsNavigationInitiator)
            {
                popupSuccess.IsOpen = false;
                popup.IsOpen = false;
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

            if (!(e.NavigationMode == NavigationMode.Back))
            {
                new StateHelper().SavePageLevelData<YourDetailsLoginViewModel>(_yourDetailsLoginVMKey, yourDetailsLoginVM);
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
                
            }
            else
            {
                Exit();
            }
        }
        /// <summary>
        /// Method to call WriteToFile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            await WriteToFile();
            if (scrollVw != null)
                this.scrollVw.ScrollToVerticalOffset(0);
        }
        #endregion

        #region Methods
        /// <summary>
        /// For writing db data
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
        private void Exit()
        {
            while (NavigationService.BackStack.Any())
            {
                NavigationService.RemoveBackEntry();

            }
        }
        #endregion
    }
}