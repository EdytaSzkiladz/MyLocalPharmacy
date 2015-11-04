﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.ViewModel;
using MyLocalPharmacy.Entities;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace MyLocalPharmacy.View
{
    public partial class SelectSurgery : PhoneApplicationPage
    {
        #region Declarations
        SelectSurgenFeedDataCollection selectSurgenCollection = new SelectSurgenFeedDataCollection();
        bool IsPostCodeSearch;
        bool IsAdded;
        int totalPages;
        int NoOfpagesCompleted = 0;
        string url = null;
        private string selectedDistance = "5";
        SelectSurgeryViewModel _selectSurgeryViewModel = null;
        #endregion

        #region Constructor
        public SelectSurgery()
        {
            InitializeComponent();
            _selectSurgeryViewModel = new SelectSurgeryViewModel(this);
            this.DataContext = _selectSurgeryViewModel;
            
            stplDistance.Visibility = Visibility.Collapsed;
            LayoutRoot.IsHitTestVisible = true;
        }
        #endregion

        #region Methods
        /// <summary>
        ///  for selecting search criteria
        /// </summary>
        private void SelectSearchCriteria()
        {
            string searchCriteria;
            selectSurgenCollection.Clear();
          
            if ((IsPostCodeSearch = Utilities.SearchCriteria(tbxSurgenSearch.Text) == true))
            {
                stplDistance.Visibility = Visibility.Visible;
                double DistanceMileToKm = Convert.ToDouble(Convert.ToInt32(selectedDistance)) * RxConstants.MileToKm;
                int DistanceForWebCall = (int)Math.Round(DistanceMileToKm);
                searchCriteria = RxConstants.FindServiceByPostcode;
                if (App.FindServiceTiltle.Equals("GP Surgeries"))
                    url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeGP + searchCriteria + tbxSurgenSearch.Text + RxConstants.FindServiceAPIKey + RxConstants.FindServiceRange + DistanceForWebCall;
                LoadNHSData(url);
            }
            else
            {
                searchCriteria = RxConstants.FindServiceByName;
                url = RxConstants.FindServiceBaseUrl + RxConstants.FindServiceTypeGP + searchCriteria + tbxSurgenSearch.Text + RxConstants.FindServiceAPIKey;
                LoadNHSData(url);
            }
        }

        

        /// <summary>
        /// Calling web Service
        /// </summary>
        /// <param name="url"></param>
        private void LoadNHSData(string url)
        {
            WebClient service = new WebClient();
            string apiuri = url + "&page=1";
            var uri = new Uri(apiuri, UriKind.RelativeOrAbsolute);
            service.DownloadStringCompleted += service_DownloadStringCompletedForSurgen;
            service.DownloadStringAsync(uri);
        }

        private void service_DownloadStringCompletedForSurgen(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    var response = e.Result.ToString();
                    XElement element = XElement.Parse(response);
                    XNamespace ns = "http://www.w3.org/2005/Atom";

                    ParseXML(element);

                    string v = e.Result.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", "");
                    v = v.Replace("xmlns=\"http://www.w3.org/2005/Atom\"", "");
                    element = XElement.Parse(v);
                    ParseSubMenuList(element);
                    int limit = totalPages;
                    for (int i = 2; i <= limit; i++)
                    {
                        CallWebserviceForEntirePages(i);
                    }
                    progressBar.Visibility = Visibility.Collapsed;
                    LayoutRoot.IsHitTestVisible = true;
                 
                }
            }
            catch (Exception ex)
            {
                LayoutRoot.IsHitTestVisible = true;
              
                progressBar.Visibility = Visibility.Collapsed;
                selectSurgenCollection.Clear();
                lstSurgenSearch.Visibility = Visibility.Collapsed;
                tbkNoResult.Visibility = Visibility.Visible;
                
            }

        }

        /// <summary>
        /// calling webService for total pages
        /// </summary>
        /// <param name="page"></param>
        private void CallWebserviceForEntirePages(int page)
        {
            WebClient webClient = new WebClient();
            string apiuri = url + "&page=" + page;
            var uri = new Uri(apiuri, UriKind.RelativeOrAbsolute);
            webClient.DownloadStringCompleted += webClient_DownloadStringCompletedPostCodeForEntirePage;
            webClient.DownloadStringAsync(new Uri(apiuri));
        }

        private void webClient_DownloadStringCompletedPostCodeForEntirePage(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null)
            {
                if (!string.IsNullOrEmpty(e.Result))
                {
                    string v = e.Result.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", "");
                    v = v.Replace("xmlns=\"http://www.w3.org/2005/Atom\"", "");
                    XElement element = XElement.Parse(v);

                    ParseSubMenuList(element);
                }

            }
        }

        /// <summary>
        /// Parse Xml data obtained
        /// </summary>
        /// <param name="xmlFeed"></param>
        private void ParseSubMenuList(XElement xmlFeed)
        {
            try
            {
                XNamespace s = "http://syndication.nhschoices.nhs.uk/services";
                foreach (XElement curElement in xmlFeed.Descendants("entry").Descendants("content").Descendants(s + "organisationSummary"))
                {
                    lstSurgenSearch.ItemsSource = null;
                    try
                    {
                        SelectSurgenFeedData SelectSurgen = new SelectSurgenFeedData();
                        var newData = new organisationSummaryAddress();

                        SelectSurgen.Name = (curElement.Element(s + "name")).Value.ToString();
                        SelectSurgen.OdsCode = (curElement.Element(s + "odsCode")).Value.ToString();
                        string[] addresslines = new string[6];
                        int i = 0;
                        foreach (XElement curElementCoordinates in curElement.Descendants(s + "address"))
                        {
                            foreach (XElement SubcurElementCoordinates in curElementCoordinates.Descendants(s + "addressLine"))
                            {
                                addresslines[i] = SubcurElementCoordinates.Value;
                                i++;
                            }
                            addresslines[5] = curElementCoordinates.Element(s + "postcode").Value.ToString();
                        }
                        if ((!string.IsNullOrEmpty(addresslines[0])) && (!string.IsNullOrWhiteSpace(addresslines[0])))
                        {
                            SelectSurgen.AddressLine1 = addresslines[0];
                        }
                        else
                        {
                            SelectSurgen.AddressLine1 = "-";
                        }
                        if ((!string.IsNullOrEmpty(addresslines[1])) && (!string.IsNullOrWhiteSpace(addresslines[1])))
                        {
                            SelectSurgen.AddressLine2 = addresslines[1];
                        }
                        else
                        {
                            SelectSurgen.AddressLine2 = "-";
                        }
                        if ((!string.IsNullOrEmpty(addresslines[2])) && (!string.IsNullOrWhiteSpace(addresslines[2])))
                        {
                            SelectSurgen.AddressLine3 = addresslines[2];
                        }
                        else
                        {
                            SelectSurgen.AddressLine3 = "-";
                        }
                        if ((!string.IsNullOrEmpty(addresslines[3])) && (!string.IsNullOrWhiteSpace(addresslines[3])))
                        {
                            SelectSurgen.AddressLine4 = addresslines[3];
                        }
                        else
                        {
                            SelectSurgen.AddressLine4 = "-";
                        }
                        if ((!string.IsNullOrEmpty(addresslines[4])) && (!string.IsNullOrWhiteSpace(addresslines[4])))
                        {
                            SelectSurgen.AddressLine4 = string.Concat(SelectSurgen.AddressLine4, ", ", addresslines[4]);
                        }
                        if ((!string.IsNullOrEmpty(addresslines[5])) && (!string.IsNullOrWhiteSpace(addresslines[5])))
                        {
                            SelectSurgen.AddressLine4 = string.Concat(SelectSurgen.AddressLine4, ", ", addresslines[5]);
                        }
                        selectSurgenCollection.Add(SelectSurgen);
                        App.SelectSurgenCollectionGlobalvar = selectSurgenCollection;

                    }
                    catch (FormatException fe)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(fe);
#endif
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(e);
#endif
                    }

                }

                lstSurgenSearch.Items.Clear();
                if (!IsPostCodeSearch)
                selectSurgenCollection.Sort();
                lstSurgenSearch.ItemsSource = selectSurgenCollection;
                progressBar.Visibility = Visibility.Collapsed;
                LayoutRoot.IsHitTestVisible = true;
               
            }
            catch (Exception ex)
            {
                LayoutRoot.IsHitTestVisible = true;
                tbxSurgenSearch.Focus();
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex);
#endif
            }
        }

        private void ParseXML(XElement document)
        {
            XNamespace ns = "http://www.w3.org/2005/Atom";
            XNamespace s = "http://syndication.nhschoices.nhs.uk/services";
            List<XElement> pages = document.Elements().Where(x => x.Name.Equals(ns + "link")).ToList();
            XElement xe = pages.FirstOrDefault(x => x.Attribute("rel").Value.Equals("last"));
            if (xe != null)
            {
                string href = xe.Attribute("href").Value;
                Console.WriteLine(href);
                int index = href.LastIndexOf("&page=");
                index = index + 6;
                string NoOfPages = href.Substring(index, href.Length - index);
                totalPages = Convert.ToInt32(NoOfPages);
            }
        }
        /// <summary>
        /// Method to get data based on name/postcode
        /// </summary>
        /// <param name="distance"></param>
        public void TriggerWebServiceCall(string distance)
        {
            selectedDistance = distance;
            tbkNoResult.Visibility = Visibility.Collapsed;
            lstSurgenSearch.Visibility = Visibility.Visible;


            if (App.IsNavigatedFromYourDetailsLogin)
                App.SelectedSurgen = tbxSurgenSearch.Text;
            else
                App.SurgeonSaved = tbxSurgenSearch.Text;

            if (Utilities.IsConnectedToNetwork())
            {

                if (string.IsNullOrEmpty(tbxSurgenSearch.Text) || string.IsNullOrWhiteSpace(tbxSurgenSearch.Text))
                {
                    App.IsDisableSearchsurgen = false;
                    placeHolder.Visibility = Visibility.Visible;
                    lstSurgenSearch.ItemsSource = null;
                    NoOfpagesCompleted = 0;
                    progressBar.Visibility = Visibility.Collapsed;
                    LayoutRoot.IsHitTestVisible = true;
                    tbxSurgenSearch.Focus();
                }
                else
                    placeHolder.Visibility = Visibility.Collapsed;

                if (!App.IsDisableSearchsurgen)
                {
                    string selectedSurgenName = tbxSurgenSearch.Text;
                    if (!string.IsNullOrEmpty(selectedSurgenName) && !string.IsNullOrWhiteSpace(selectedSurgenName) && selectedSurgenName.Length > 2)
                    {

                        LayoutRoot.IsHitTestVisible = false;
                        placeHolder.Visibility = Visibility.Collapsed;
                        progressBar.Visibility = Visibility.Visible;
                        SelectSearchCriteria();
                    }

                }

            }
            else
            {
                popupNoInternetData.IsOpen = true;
                LayoutRoot.IsHitTestVisible = false;
            }
        }
               

        #endregion

        #region Events

        /// <summary>
        /// Calling webservice on text changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSurgenSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPostCodeSearch = false;
            TriggerWebServiceCall(selectedDistance);
        }
        /// <summary>
        /// Binding selecting surgen to previous pages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSurgenSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            App.IsDisplaySelectedSurgenOnSearchBox = true;
            App.IsDisableSearchsurgen = true;
            if (lstSurgenSearch.SelectedItem != null)
            {
                var data = lstSurgenSearch.SelectedItem as SelectSurgenFeedData;
                if (App.IsNavigatedFromYourDetailsLogin)
                {
                    App.SelectedSurgen = data.Name;
                    tbxSurgenSearch.Text = App.SelectedSurgen;
                }
                else
                {
                    App.SurgeonSaved = data.Name;
                    tbxSurgenSearch.Text = App.SurgeonSaved;
                }

                App.SurgeonAddress = data.AddressLine1 + ", " + data.AddressLine2 + ", " + data.AddressLine3 + ", " + data.AddressLine4;

            }
        }

        /// <summary>
        /// OnNavigatedTo event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.TombStonedPageURL = PageURL.navigateToSelectSurgeryURL;
           
                if (App.SelectSurgenCollectionGlobalvar != null)
                    lstSurgenSearch.ItemsSource = App.SelectSurgenCollectionGlobalvar;
                
                if (App.IsNavigatedFromYourDetailsLogin)
                {
                    if (!string.IsNullOrEmpty(App.SelectedSurgen) && !string.IsNullOrWhiteSpace(App.SelectedSurgen))
                    {
                        if (App.SelectedSurgen.Equals("Choose Doctor for surgery (Optional)"))
                        {
                            placeHolder.Visibility = Visibility.Visible;
                            lstSurgenSearch.ItemsSource = null;
                            
                        }
                        else
                        {
                            tbxSurgenSearch.Text = App.SelectedSurgen;
                            placeHolder.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        lstSurgenSearch.ItemsSource = null;
                        
                        placeHolder.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(App.SurgeonSaved) && !string.IsNullOrWhiteSpace(App.SurgeonSaved))
                    {
                        if (App.SurgeonSaved.Equals("Choose Doctor for surgery (Optional)"))
                        {
                            placeHolder.Visibility = Visibility.Visible;
                            lstSurgenSearch.ItemsSource = null;
                            lstSurgenSearch.SelectedIndex = -1;
                        }
                        else
                        {
                            tbxSurgenSearch.Text = App.SurgeonSaved;
                            placeHolder.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        lstSurgenSearch.ItemsSource = null;
                        lstSurgenSearch.SelectedIndex = -1;
                        placeHolder.Visibility = Visibility.Visible;
                    }
                }

                if (!e.IsNavigationInitiator) 
                {
                    App.IsToombStoned = false;
                    NavigationService.Navigate(new Uri(PageURL.navigateToEnterPinURL, UriKind.RelativeOrAbsolute));
                }
        }


        /// <summary>
        /// For the search textbox to get focused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSurgenSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            placeHolder.Visibility = Visibility.Collapsed;
            if (tbxSurgenSearch.Text.Equals("Choose Doctor for surgery (Optional)") || tbxSurgenSearch.Text.Equals(string.Empty))
            {
                tbxSurgenSearch.Text = string.Empty;
                placeHolder.Visibility = Visibility.Visible; 
            }
        }
        /// <summary>
        /// For the search textbox to lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSurgenSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxSurgenSearch.Text))
            {
                placeHolder.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// For dissappear Search hint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void placeHolder_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            tbxSurgenSearch.Focus();
            placeHolder.Visibility = Visibility.Collapsed; 
        }
        /// <summary>
        /// Go back to Details entering page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (App.IsNavigatedFromYourDetailsLogin)
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToYourDetailsLoginURL, UriKind.Relative));
                App.IsNavigatedFromYourDetailsLogin = false;
                App.IsNavigatedFromYourDetailsLoginwithTC = false;
                App.IsNavigatedFromYourDetailsUpdate = false;
            }
            else if (App.IsNavigatedFromYourDetailsLoginwithTC)
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToYourDetailswithTCURL, UriKind.Relative));
                App.IsNavigatedFromYourDetailsLoginwithTC = false;
                App.IsNavigatedFromYourDetailsLogin = false;
                App.IsNavigatedFromYourDetailsUpdate = false;
            }
            else if (App.IsNavigatedFromYourDetailsUpdate)
            {
                NavigationService.Navigate(new Uri(PageURL.navigateToYourDetailsUpdateURL, UriKind.Relative));
                App.IsNavigatedFromYourDetailsUpdate = false;
                App.IsNavigatedFromYourDetailsLoginwithTC = false;
                App.IsNavigatedFromYourDetailsLogin = false;
            }
        }
        /// <summary>
        /// For clear Search field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxSurgenSearch_ActionIconTapped(object sender, EventArgs e)
        {
            tbxSurgenSearch.Text = string.Empty;
        }
        #endregion

    }
}