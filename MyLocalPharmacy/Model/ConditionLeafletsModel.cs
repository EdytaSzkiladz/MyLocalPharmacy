using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyLocalPharmacy.Model
{
    public class ConditionLeafletsModel
    {
        #region Declarations
        HomePanoramaViewModel objHomePanoramaViewModel;
        ConditionLeafletsCollection conditionLeaflets; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_objHomePanoramaViewModel"></param>
        public ConditionLeafletsModel(HomePanoramaViewModel _objHomePanoramaViewModel)
        {
            this.objHomePanoramaViewModel = _objHomePanoramaViewModel;
            RequestData();
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method to call webservice
        /// </summary>
        public void RequestData()
        {
            string url = RxConstants.ConditionLeafletsUrl;
            objHomePanoramaViewModel.ProgressBarLeafletVisibilty = Visibility.Visible;
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri(url));

        }

        /// <summary>
        /// Webservice download complete event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null && e.Result != null)
                {
                    conditionLeaflets = JsonHelper.Deserialize<ConditionLeafletsCollection>(e.Result);

                    List<LeafletsGroup<ConditionLeafletsResponse>> DataSource = LeafletsGroup<ConditionLeafletsResponse>.CreateGroups(conditionLeaflets,
                    System.Threading.Thread.CurrentThread.CurrentUICulture,
                    (ConditionLeafletsResponse s) => { return s.Title; }, true);

                    objHomePanoramaViewModel.ProgressBarLeafletVisibilty = Visibility.Collapsed;
                    objHomePanoramaViewModel.LeafletsCollection = DataSource;
                }
            }
            catch (Exception)
            {
                objHomePanoramaViewModel.ProgressBarLeafletVisibilty = Visibility.Collapsed;
                objHomePanoramaViewModel.LeafletNoInternetVisibility = Visibility.Visible;
            }

        } 
        #endregion
    }
}
