using System;
using System.Diagnostics;
using System.Resources;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyLocalPharmacy.Resources;
using System.Windows.Controls.Primitives;
using Windows.ApplicationModel.Activation;
using System.Threading;
using System.ComponentModel;
using MyLocalPharmacy.View;
using MyLocalPharmacy.Entities;
using System.Collections.Generic;
using MyLocalPharmacy.Utils;
using MyLocalPharmacy.Model;
using System.IO;
using MyLocalPharmacy.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Device.Location;
using Microsoft.Phone.Info;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MyLocalPharmacy
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        PushNotificationHandler _pushNotificationHandler; 

        #region AppLevelVariables
        public static string PIN;

        public static string ApId; 

        public static string resetLoginPIN;
        public static string resetLoginAuthCode;
      
        public static string SignUpPharId;
        public static string YourDetailsLoginEmail;
        public static string HashPIN;
        
        public static string BrandingHash;
        public static string AdvtHash;

        public static string LoginPharId;
        public static string LoginEmailId;
        public static string LoginPharmacyname;
        public static string LoginPharmacyAddress1;
        public static string LoginPharmacyAddress2;
        public static string LoginPharmacyAddress3;
        public static string PostCode;
        public static string PharmacyPhoneNo;
        public static string PharmacyBranchName;
        public static string DrugsData;
        public static LoginResponse ObjLgResponse;
        public static GetPharmacyInformationResponse ObjBrandingResponse;

        public static string LeafletWebLink;
        public static string ConditionSearchName;
        public static string ConditionSearchUrl;


        //Indicates whether the Application is Launching from Toombstoned State.
        public static bool IsApplicationInstancePreserved;
        public static bool IsToombStoned;

        public static List<string> AdImages;
        public static List<BitmapImage> AddImages;
        public static List<string> ImagesUrl;
        public static List<string> ImagesName;
        public static bool IsUserRegistered;

        public static bool IsPageHomePanorama;
        public static bool IsPageUpdateYourDetailsafterLogin;
        public static bool IsPageYourDetailsafterLoginSaved;
        public static string TombStonedPageURL;
        public static string StaticSplashURL;

        public static bool UseGps = true;
        public static bool GPSCoordinatesAvailable = false;
        public static DateTime LastGPSPingedTime = DateTime.MinValue;
        public static int GPSDistance = 60;

        public static string SelectedSurgen;
        public static bool IsSelectedSurgen;
        public static bool IsDisableSearchsurgen;
        public static bool IsNavigatedFromYourDetailsLogin;
        public static bool IsNavigatedFromYourDetailsLoginwithTC;
        public static bool IsNavigatedFromYourDetailsUpdate;
        public static bool IsDisplaySelectedSurgenOnSearchBox;
        public static SelectSurgenFeedDataCollection SelectSurgenCollectionGlobalvar;

        public static string HeaderPillsReminder = string.Empty;
        public static PillsReminderModelCol DailyMorningPillsCollection;
        public static PillsReminderModelCol DailyAfternoonPillsCollection;
        public static PillsReminderModelCol DailyEveningPillsCollection;
        public static PillsReminderModelCol DailyNightPillsCollection;
        public static PillsReminderModelCol WeeklyPillsCollection;
        public static PillsReminderModelCol MonthlyPillsCollection;
        public static PillsReminderModelCol Every28DaysPillsCollection;
        public static List<PillsReminderModel> PillsReminderModelColLocalStorage;
        public static PillsReminderModel objPillsReminderModel=new PillsReminderModel();


        public static string LocalServiceLatitude;
        public static string LocalServiceLongitude;
        public static string SearchTerm;
        public static GeoCoordinate LocalServiceCentreCoordinates = new GeoCoordinate(51.5286416, -0.1015987);
        public static double LocalServiceZoomLevel = RxConstants.ZoomLevel;

        public static bool IsReminderToombstoned;
        public static bool IsReminderToSet;
        public static string ReminderToDisplay;
        public static bool IsChecked;
        public static string Bgcolor;

        public static bool IsCheckedTempStore;
        public static DateTime ReminderDateTemp;
        public static DateTime ReminderTimeTemp;

        public static bool IsClearLoginMailInnewInstance;

        public static DateTime DailyMorningReminderDate;
        public static DateTime DailyMorningReminderTime;
        public static string DailyMorningReminderOnOff;

        public static DateTime DailyEveningReminderDate;
        public static DateTime DailyEveningReminderTime;
        public static string DailyEveningReminderOnOff;

        public static DateTime DailyAfternoonReminderDate;
        public static DateTime DailyAfternoonReminderTime;
        public static string DailyAfternoonReminderOnOff;

        public static DateTime DailyNightReminderDate;
        public static DateTime DailyNightReminderTime;
        public static string DailyNightReminderOnOff;

        public static DateTime WeeklyReminderDate;
        public static DateTime WeeklyReminderTime;
        public static string WeeklyReminderOnOff;

        public static DateTime MonthlyReminderDate;
        public static DateTime MonthlyReminderTime;
        public static string MonthlyReminderOnOff;

        public static DateTime Every28DaysReminderDate;
        public static DateTime Every28DaysReminderTime;
        public static string Every28DaysReminderOnOff;

        public static int LocalServiceDistance=30;

        public static string FindServiceTiltle = "GP Surgeries";
        public static int NoofTriesLeft;
        public static bool PinResetFromSettingsPage;
        public static string NewPin;
        public static bool IsChangePharmacy;
        public static string DrugDBHash;
     
        public static bool IsFromLoginScreen;
        public static bool IsFromRejected;

        public static string SurgeonSaved;
        public static string SurgeonAddress;

        public static PrescriptionCollection prescriptionCollection;
        public static int selectedDrugIndex;
        public static List<string> autoCompleteListDrug = new List<string>();
        public static List<string> autoCompleteEditListDrug = new List<string>();

        public static OrderedPillDetailsCollection OrderedPillDetailsCollection = new OrderedPillDetailsCollection();
        public static OrderedPillDetails selectedOrder = new OrderedPillDetails();
        public static int SelectedIndexToEdit;

        public static bool IsPillUpdated;
        public static bool IsEditCancelled;

        public static bool IsVerifiedBySms;
        public static bool IsVerifiedByEmail;

        public static string transientMailId;

        public static bool IsUserNotExist;

        public static bool IsFromsignUp;
        public static string MailIdToFillAfterPin;


       
        #endregion

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard XAML initialization
            InitializeComponent();


            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
            
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            
            new Utils.StateHelper().LoadAppLevelPersistantData();
            IsApplicationInstancePreserved = false;
            IsClearLoginMailInnewInstance = true;

            SuscribePush();
        }

        /// <summary>
        /// Method to subscribe push using Windows Azure
        /// </summary>
        public async void SuscribePush()
        {
            if (string.IsNullOrWhiteSpace(ApId) || string.IsNullOrEmpty(ApId))
            {
                ApId = Convert.ToString(Guid.NewGuid());
                Regex rgx = new Regex("[^a-zA-Z0-9]");
                ApId = rgx.Replace(ApId, "");               
            }

            string[] UniqueID = { "ApId" };
            UniqueID[0] = ApId;
            _pushNotificationHandler = new PushNotificationHandler();
            await _pushNotificationHandler.SubscribeToPushes(UniqueID);  
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            IsToombStoned = false;
            if (e.IsApplicationInstancePreserved)
            {
                IsApplicationInstancePreserved = true;
            }
            else
            {
                IsToombStoned = true;
                IsApplicationInstancePreserved = false;
                new Utils.StateHelper().LoadAppLevelPersistantData();
                new Utils.StateHelper().LoadAppLevelTransientData();
            }

        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            IsToombStoned = true;
            new Utils.StateHelper().SaveAppLevelPersistantData();
            new Utils.StateHelper().SaveAppLevelTransientData();           
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            App.UseGps = false;
            Settings.stopGPSLocationService();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }



        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;
        

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
                throw;
            }
        }
    }
}