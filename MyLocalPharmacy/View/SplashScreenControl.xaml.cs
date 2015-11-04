using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace MyLocalPharmacy.View
{
    public partial class SplashScreenControl : UserControl
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SplashScreenControl()
        {
            InitializeComponent();
            this.progBarSplash.IsIndeterminate = true;
        }
        #endregion
      
    }
}
