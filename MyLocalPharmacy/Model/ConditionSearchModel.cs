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
    public class ConditionSearchModel
    {
        #region Declarations
        ConditionSearchViewModel objConditionSearchViewModel;
        ConditionSearchCollection conditionLeaflets; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_objConditionSearchViewModel"></param>
        public ConditionSearchModel(ConditionSearchViewModel _objConditionSearchViewModel)
        {
            this.objConditionSearchViewModel = _objConditionSearchViewModel;
            RequestData();
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method to call webservice
        /// </summary>
        public void RequestData()
        {
            string url = RxConstants.ConditionSearchUrl + App.ConditionSearchName + RxConstants.ApiKey;
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri(url));
        }

        /// <summary>
        /// Web service completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null && e.Result != null && e.Result.Count() != 2)
                {
                    conditionLeaflets = JsonHelper.Deserialize<ConditionSearchCollection>(e.Result);

                    objConditionSearchViewModel.SearchCollection = conditionLeaflets;
                    objConditionSearchViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                    objConditionSearchViewModel.NoLeafletTextVisibility = Visibility.Collapsed;
                }
                else
                {
                    objConditionSearchViewModel.NoLeafletTextVisibility = Visibility.Visible;
                    objConditionSearchViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                objConditionSearchViewModel.ProgressBarVisibilty = Visibility.Collapsed;
                
                MessageBox.Show("Sorry, Unable to process your request.");

            }

        } 
        #endregion
    }
}
