namespace MyLocalPharmacy.Utils
{
   public sealed class RxConstants
    {

        #region Webservices
        public const string server = "https://rxmobile-test.rxsystems.org.uk/2.0/";
        //public const string getPharmacyInformations = "https://rxmobile.rxsystems.org.uk/1.0/getPharmacyInformations/";
        public const string getPharmacyInformations = server + "getPharmacyInformations/";
        public const string getAdvert = server + "getAdvert/";
        public const string getUserDetails = server + "getUserDetails/";
        public const string sendNomination = server + "sendNomination/";
        public const string changePin = server + "changePin/";
        public const string getAllOrders = server + "getAllOrders/";
        public const string sendOrder = server + "sendOrder/";
        public const string sendResetPinCode = server + "sendResetPinCode/";
        public const string resetPin = server + "resetPin/";
        public const string removeOrder = server + "removeOrder/";
        public const string updatePushNotificationsId = server + "updatePushNotificationsId/";
        public const string sendUserDetails = server + "sendUserDetails/";
        public const string resendConfirmationCodes = server + "resendConfirmationCodes/";
        public const string verifyBySms = server + "confirmPhoneNumber/";
        public const string SetOrderStatus = server + "setOrderStatus/";
        public const string updatePushId = server + "updatePushNotificationsId/";

       //Rxconstants for push notifications

  //public const string getPharmacyInformations = "http://poland.qburst.com:8800/1.0/getPharmacyInformations/";
  //     public const string getAdvert = "http://poland.qburst.com:8800/1.0/getAdvert/";
  //     public const string getUserDetails = "http://poland.qburst.com:8800/1.0/getUserDetails/";
      // public const string sendNomination = "http://poland.qburst.com:8800/1.0/sendNomination/";
  //     public const string changePin = "http://poland.qburst.com:8800/1.0/changePin/";
  //     public const string getAllOrders = "http://poland.qburst.com:8800/1.0/getAllOrders/";
  //     public const string sendOrder = "http://poland.qburst.com:8800/1.0/sendOrder/";
  //     public const string sendResetPinCode = "http://poland.qburst.com:8800/1.0/sendResetPinCode/";
  //     public const string resetPin = "http://poland.qburst.com:8800/1.0/resetPin/";
  //     public const string removeOrder = "http://poland.qburst.com:8800/1.0/removeOrder/";
  //     public const string updatePushNotificationsId = "http://poland.qburst.com:8800/1.0/updatePushNotificationsId/";
  //     public const string sendUserDetails = "http://poland.qburst.com:8800/1.0/sendUserDetails/";
  //     public const string resendConfirmationCodes = "http://poland.qburst.com:8800/1.0/resendConfirmationCodes/";
  //     public const string verifyBySms = "http://poland.qburst.com:8800/1.0/confirmPhoneNumber/";
       // public const string SetOrderStatus = "http://poland.qburst.com:8800/1.0/setOrderStatus/";

        #endregion

        #region Setup Screen Navigations
        public const string termsandConditionslink = "http://securepharm.co.uk/content/documentation/rxs-mpa-customer-licence_final_6-january-2014_blms_v-1-4/";
        public const string myLocalPharmacySupport = "http://www.mylocalpharmacyapp.co.uk"; 
        #endregion

        #region NHS Services
        public const string nhsWales = "http://www.wales.nhs.uk/ourservices/directory";
        public const string nhsScotland = "http://www.nhs24.com/FindLocal";
        public const string nhsNorthernIreland = "http://servicefinder.hscni.net/"; 
        #endregion

        public const string PrimaryColourCode = "#C0E2E1";
        public const string PrimaryColourCodeAppbar = "#FFC0E2E1";
        public const string FontColourCode = "#000000";

        public const string ConditionLeafletsUrl = "http://api.patient.co.uk/search/pil/all?apikey=7a7b35ey-043k-9123ad90";
        public const string ConditionSearchUrl = "http://api.patient.co.uk/search/pil/";
        public const string ApiKey = "?apikey=7a7b35ey-043k-9123ad90";
        public const string FindServiceBaseUrl = "http://v1.syndication.nhschoices.nhs.uk/organisations/";
        public const string FindServiceTypeGP = "gppractices/";
        public const string FindServiceTypeDentist = "dentists/";
        public const string FindServiceTypeHospitals = "hospitals/";
        public const string FindServiceTypeOpticians = "opticians/";
        public const string FindServiceByPostcode = "postcode/";
        public const string FindServiceByPlace = "place/";
        public const string FindServiceAPIKey = ".xml?apikey=DERJRLPW";
        public const string FindServiceRange = "&range=";
        public const string FindServicePageNumber = "&page=";
        //public const double MileToKm = 1.609344;
        public const double MileToKm = 1.609344;
        public const double ZoomLevel = 14;

        public const string FindServiceByName = "name/";

        public const string UK_POSTCODE_REGEX = "^((([A-PR-UWYZ][0-9][0-9]?)|(([A-PR-UWYZ][A-HK-Y][0-9][0-9]?)|(([A-PR-UWYZ][0-9][A-HJKSTUW])|([A-PR-UWYZ][A-HK-Y][0-9][ABEHMNPRV-Y])))) ?[0-9][ABD-HJLNP-UW-Z]{2})";

        public const string SetOrderStatusCancelled = "cancelled";
        public const string ButtonContentCancel = "Cancel Order";
        public const string ButtonContentReorder = "Re-order Now";

        public const string OrderStatusSubmitted = "Submitted";
        public const string OrderStatusCancelled = "Cancelled";
        public const string OrderStatusAcknowledged = "Acknowledged";
        public const string OrderStatusCollected = "Collected";
        public const string OrderStatusDelivered = "Delivered";

        public const string OrderStatusToSubmitted = "Ordered";
        public const string OrderStatusToCancelled = "Declined";
        public const string OrderStatusToAcknowledged = "Order Received";
        public const string OrderStatusToCollected = "Ready For Collection";
        public const string OrderStatusToDelivered = "Ready For Delivery";

        public const double UKLatitude = 51.5286416;
        public const double UKLongitude = -0.1015987;

        public const string userStatusApproved = "Approved";
        public const string userStatusVerified = "Verified";
        public const string userStatusConfirmed = "Confirmed";
        public const string userStatusRejected = "Rejected";
        public const string userStatusRemoved = "Removed";
       
        

        #region PillsReminder
        public const string HeaderPillsReminderDM = "daily morning";
        public const string HeaderPillsReminderDA = "daily afternoon";
        public const string HeaderPillsReminderDE = "daily evening";
        public const string HeaderPillsReminderDN = "daily night";
        public const string HeaderPillsReminderWeekly = "weekly";
        public const string HeaderPillsReminderMonthly = "monthly";
        public const string HeaderPillsReminderEvery28Days = "every 28 days";

        public const string ReminderToDisplayDM = "daily morning reminder";
        public const string ReminderToDisplayDA = "daily afternoon reminder";
        public const string ReminderToDisplayDE = "daily evening reminder";
        public const string ReminderToDisplayDN = "daily night reminder";
        public const string ReminderToDisplayWeekly = "weekly reminder";
        public const string ReminderToDisplayMonthly = "monthly reminder";
        public const string ReminderToDisplayEvery28Days = "every 28 days reminder";
        #endregion
    }
}
