using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyLocalPharmacy.Resources;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Threading;
using MyLocalPharmacy.View;
using MyLocalPharmacy.Utils;

namespace MyLocalPharmacy
{
    public partial class MainPage : PhoneApplicationPage
    {

        private Popup popup;
        private BackgroundWorker backroungWorker;
        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            App.StaticSplashURL = @"/Assets/Images/icon_splash.png";
            if (App.IsUserRegistered && (!string.IsNullOrEmpty(App.PIN) || (!string.IsNullOrWhiteSpace(App.PIN))))
                ShowDynamicSplash();
            else
                ShowSplash();
          
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

       
        /// <summary>
        /// Method to show Dynamic Splash screen.
        /// </summary>
        private void ShowDynamicSplash()
        {
            this.popup = new Popup();
            this.popup.Child = new DynamicSplashScreenControl();
            this.popup.IsOpen = true;
            StartLoadingData();
        }
       
        /// <summary>
        /// Method to load the data in the background
        /// </summary>
        private void StartLoadingData()
        {
            backroungWorker = new BackgroundWorker();
            backroungWorker.DoWork += new DoWorkEventHandler(backroungWorker_DoWork);
            backroungWorker.RunWorkerCompleted +=
          new RunWorkerCompletedEventHandler(backroungWorker_RunWorkerCompleted);
            backroungWorker.RunWorkerAsync();
        }

        public void backroungWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(4000);
        }
        public void backroungWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                this.popup.IsOpen = false;
            });
        }

        /// <summary>
        /// Method to show the Splash screen
        /// </summary>
        private void ShowSplash()
        {
            this.popup = new Popup();
            this.popup.Child = new SplashScreenControl();
            this.popup.IsOpen = true;
            StartLoadingData();
        }
        /// <summary>
        /// After the page gets loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
          
            if (!App.IsUserRegistered || string.IsNullOrEmpty(App.PIN) || string.IsNullOrWhiteSpace(App.PIN))
            NavigationService.Navigate(new Uri(PageURL.navigateToSignUpURL, UriKind.Relative));
        }       
    }
}