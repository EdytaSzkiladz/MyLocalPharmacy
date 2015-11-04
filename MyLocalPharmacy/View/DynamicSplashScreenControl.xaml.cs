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

namespace MyLocalPharmacy.View
{
    public partial class DynamicSplashScreenControl : UserControl
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DynamicSplashScreenControl()
        {
            InitializeComponent();
            this.progressBarDynamicSplash.IsIndeterminate = true;
            this.DataContext = new DynamicSplashViewModel();

        }
        #endregion

        #region Method
        /// <summary>
        /// Method to call the "Enter PIN" page
        /// </summary>
        public static void UpdationComplete()
        {
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.Relative));
        }
        #endregion
        
    }
}
