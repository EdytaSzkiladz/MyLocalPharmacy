using Microsoft.Phone.Scheduler;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Model;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;


namespace MyLocalPharmacy.Utils
{
    public static class Utilities
    {
             
        #region NetworkConnectivity
        public static bool IsConnectedToNetwork()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
        #endregion


        #region SHA256 Encoder
        public static string GetSHA256(string text)
        {
            //  UnicodeEncoding UE = new UnicodeEncoding();
            Encoding enc = Encoding.UTF8;
            byte[] hashValue;
            byte[] message = enc.GetBytes(text);

            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        } 
        #endregion

        #region String to Color Code
        public static SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            //hexaColor = "#FF" + hexaColor;
            //hexaColor = "#FF1BA1E2";
            //hexaColor = "#FFadbbff";
            //hexaColor = "#adbbff"; 
            hexaColor = "#" + "FF" + hexaColor.Substring(1, hexaColor.Length - 1);
            return new SolidColorBrush(
                Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16),
                    Convert.ToByte(hexaColor.Substring(7, 2), 16)
                )
            );
        }
        #endregion

        #region Search Criteria

        public static bool SearchCriteria(string searchData)
        {
            bool isPostCodeValid = Regex.IsMatch(searchData, @RxConstants.UK_POSTCODE_REGEX, RegexOptions.IgnoreCase);

            return isPostCodeValid;
        }
        #endregion

        #region Unix to DateTime Converter

        public static DateTime UnixToDateTime(double unixTimeStamp)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            double MaxUnixSeconds = (DateTime.MaxValue - UnixEpoch).TotalSeconds;
            System.DateTime dtDateTime = unixTimeStamp > MaxUnixSeconds
                                          ? UnixEpoch.AddMilliseconds(unixTimeStamp)
                                          : UnixEpoch.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        #endregion

        #region Clear AppData
        public static void ClearAllAppVariables()
        {
            new StateHelper().ClearAllPersistentData();
            new StateHelper().ClearAllTransientData();

            App.ApId = string.Empty;

            App.IsPageHomePanorama = false;
            App.LoginPharId = string.Empty;
            App.LoginEmailId = string.Empty;
            App.PIN = string.Empty;
            App.resetLoginPIN = string.Empty;
            App.resetLoginAuthCode = string.Empty;

            App.SignUpPharId = string.Empty;
            App.YourDetailsLoginEmail = string.Empty;
            App.HashPIN = string.Empty;

            App.BrandingHash = string.Empty;
            App.AdvtHash = string.Empty;

            App.LoginPharId = string.Empty;
            App.LoginEmailId = string.Empty;
            App.LoginPharmacyname = string.Empty;
            App.LoginPharmacyAddress1 = string.Empty;
            App.LoginPharmacyAddress2 = string.Empty;
            App.LoginPharmacyAddress3 = string.Empty;
            App.PostCode = string.Empty;
            App.PharmacyPhoneNo = string.Empty;
            App.PharmacyBranchName = string.Empty;
            App.DrugsData = string.Empty;
            App.ObjLgResponse = new LoginResponse();
            App.ObjBrandingResponse = new GetPharmacyInformationResponse();

            App.LeafletWebLink = string.Empty;
            App.ConditionSearchName = string.Empty;
            App.ConditionSearchUrl = string.Empty;
          
            App.IsToombStoned = false;

            if (App.AdImages!=null)
            App.AdImages.Clear();
            App.IsUserRegistered = false;

            App.IsPageHomePanorama = false;
            App.IsPageUpdateYourDetailsafterLogin = false;
            App.IsPageYourDetailsafterLoginSaved = false;
            App.TombStonedPageURL = string.Empty;
            App.StaticSplashURL = string.Empty;

            App.UseGps = true;
            App.GPSCoordinatesAvailable = false;
            App.LastGPSPingedTime = DateTime.MinValue;
            App.GPSDistance = 60;

            App.SelectedSurgen = string.Empty;
            App.IsSelectedSurgen = false;
            App.IsDisableSearchsurgen = false;
            App.IsNavigatedFromYourDetailsLogin = false;
            App.IsNavigatedFromYourDetailsLoginwithTC = false;
            App.IsNavigatedFromYourDetailsUpdate = false;
            App.IsDisplaySelectedSurgenOnSearchBox = false;
            if (App.SelectSurgenCollectionGlobalvar != null)
                App.SelectSurgenCollectionGlobalvar.Clear();

            ClearReminderData();

            App.LocalServiceDistance = 30;

            App.FindServiceTiltle = "GP Surgeries";
            App.NoofTriesLeft =9;
            App.PinResetFromSettingsPage = false;
            App.NewPin = string.Empty;
            App.IsChangePharmacy = false;
            App.DrugDBHash = string.Empty;

            App.IsFromLoginScreen = false;

            App.SurgeonSaved = string.Empty;
            App.SurgeonAddress = string.Empty;

            if (App.prescriptionCollection != null)
                App.prescriptionCollection.Clear();
            App.selectedDrugIndex = 0;
            if (App.autoCompleteListDrug != null)
                App.autoCompleteListDrug.Clear();
            if (App.autoCompleteEditListDrug != null)
                App.autoCompleteEditListDrug.Clear();

            if (App.OrderedPillDetailsCollection != null)
                App.OrderedPillDetailsCollection.Clear();
            if (App.selectedOrder != null)
            App.selectedOrder = new OrderedPillDetails();
            App.SelectedIndexToEdit = 0;
        
        }

        public static void ClearReminderData()
        {
            App.HeaderPillsReminder = string.Empty;
            if (App.DailyMorningPillsCollection != null)
                App.DailyMorningPillsCollection.Clear();
            if (App.DailyAfternoonPillsCollection != null)
                App.DailyAfternoonPillsCollection.Clear();
            if (App.DailyEveningPillsCollection != null)
                App.DailyEveningPillsCollection.Clear();
            if (App.DailyNightPillsCollection != null)
                App.DailyNightPillsCollection.Clear();
            if (App.WeeklyPillsCollection != null)
                App.WeeklyPillsCollection.Clear();
            if (App.MonthlyPillsCollection != null)
                App.MonthlyPillsCollection.Clear();
            if (App.Every28DaysPillsCollection != null)
                App.Every28DaysPillsCollection.Clear();

            ScheduledAction oldReminderToClearDM = ScheduledActionService.Find("daily morning reminder");
            if (oldReminderToClearDM != null)
                ScheduledActionService.Remove(oldReminderToClearDM.Name);

            ScheduledAction oldReminderToClearDA = ScheduledActionService.Find("daily afternoon reminder");
            if (oldReminderToClearDA != null)
                ScheduledActionService.Remove(oldReminderToClearDA.Name);

            ScheduledAction oldReminderToClearDE = ScheduledActionService.Find("daily evening reminder");
            if (oldReminderToClearDE != null)
                ScheduledActionService.Remove(oldReminderToClearDE.Name);

            ScheduledAction oldReminderToClearDN = ScheduledActionService.Find("daily night reminder");
            if (oldReminderToClearDN != null)
                ScheduledActionService.Remove(oldReminderToClearDN.Name);

            ScheduledAction oldReminderToClearW = ScheduledActionService.Find("weekly reminder");
            if (oldReminderToClearW != null)
                ScheduledActionService.Remove(oldReminderToClearW.Name);

            ScheduledAction oldReminderToClearM = ScheduledActionService.Find("monthly reminder");
            if (oldReminderToClearM != null)
                ScheduledActionService.Remove(oldReminderToClearM.Name);

            ScheduledAction oldReminderToClearE28 = ScheduledActionService.Find("every 28 days reminder");
            if (oldReminderToClearE28 != null)
                ScheduledActionService.Remove(oldReminderToClearE28.Name);

            App.IsReminderToSet = false;
            App.ReminderToDisplay = string.Empty;
            App.IsChecked = false;
            App.Bgcolor = string.Empty;

            App.DailyMorningReminderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            App.DailyMorningReminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            App.DailyMorningReminderOnOff = "Off";

            App.DailyEveningReminderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            App.DailyEveningReminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            App.DailyEveningReminderOnOff = "Off";

            App.DailyAfternoonReminderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            App.DailyAfternoonReminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            App.DailyAfternoonReminderOnOff = "Off";

            App.DailyNightReminderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            App.DailyNightReminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            App.DailyNightReminderOnOff = "Off";

            App.WeeklyReminderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            App.WeeklyReminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            App.WeeklyReminderOnOff = "Off";
            App.MonthlyReminderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            App.MonthlyReminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            App.MonthlyReminderOnOff = "Off";

            App.Every28DaysReminderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            App.Every28DaysReminderTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            App.Every28DaysReminderOnOff = "Off";
        }
        #endregion
    }
}
