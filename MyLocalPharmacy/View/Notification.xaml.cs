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
namespace MyLocalPharmacy.View
{
    public partial class Notification : PhoneApplicationPage
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Notification()
        {
            InitializeComponent();
            this.DataContext = new NotificationViewModel();
        }
        #endregion

        #region Events
        /// <summary>
        /// OnNAvigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string strContent;
            if (NavigationContext.QueryString.TryGetValue("contentVal", out strContent))
            {
                string[] contentArr = strContent.Split(new string[] { "~~" }, StringSplitOptions.None);
                tbkHeaderName.Text = contentArr[0].ToString();
                tbkContent.Text = contentArr[1].ToString();
                
            }
        }

        #endregion
    }
    
}