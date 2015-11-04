using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.ViewModel;
using System;
using System.Collections.Generic;
using System.Device.Location;
namespace MyLocalPharmacy.Utils
{
    public class StateHelper
    {
        #region DeclarationsAndInitializations

        private const string _apIdKey = "ApIdKey";

        private const string _signUpPharmacyKey = "SignUpPharmacyKey";
        private const string _loginInPharmacyKey = "LogInPharmacyKey";
        private const string _pharmacyNameKey = "PharmacyNameKey";
        private const string _pharmacyAddress1Key = "PharamacyAddress1Key";
        private const string _pharmacyAddress2Key = "PharamacyAddress2Key";
        private const string _pharmacyAddress3Key = "PharamacyAddress3Key";
        private const string _postCodekey = "PostCodeKey";
        private const string _pinKey = "PinKey";
        private const string _hashedPinKey = "HashedPinKey";
        private const string _pillsReminderHeaderKey = "HeaderPillsReminder";
        private const string _drugsDataKey = "DrugsData";
        private const string _drugsDBDataHashKey = "DrugDBHash";
        private const string _tombStonedPageURLKey = "TombStonedPageURL";
        private const string _loginEmailId = "LoginEmailId";
        private const string _isUserRegisteredKey = "IsUserRegisteredKey";
        private const string _isPageHomePanoramaKey = "IsPageHomePanorama";
        private const string _isPageUpdateYourDetailsafterLoginKey = "IsPageUpdateYourDetailsafterLogin";
        private const string _isPageYourDetailsafterLoginSaved = "IsPageYourDetailsafterLoginSaved";
        private const string _isFromLoginScreenKey = "IsFromLoginScreen";
        private const string _objLoginResponseKey = "Objlgresponse";
        private const string _objPharmacyBrandInfoKey = "Objbrandingesponse";
        private const string _pharmacyBranchNameKey = "PharmacyBranchName";

        private const string _isFromRejected = "IsFromRejected";

        private const string _lstAdvtImagesKey = "AdImages";

        private const string _brandHashKey = "BrandHash";
        private const string _advtHashKey = "AdvtHash";

        private const string _staticSplashURLKey = "StaticSplashURL";

        public const string _gpsPOSITIONKey = "GPSPosition";

        private const string _pillsCollectionDMKey = "PillsCollectionDM";
        private const string _pillsCollectionDAKey = "PillsCollectionDA";
        private const string _pillsCollectionDEKey = "PillsCollectionDE";
        private const string _pillsCollectionDNKey = "PillsCollectionDN";

        private const string _pillsCollectionWeeklyKey = "PillsCollectionWeekly";
        private const string _pillsCollectionMonthlyKey = "PillsCollectionMonthly";
        private const string _pillsCollectionE28DaysKey = "PillsCollectionE28Days";

        private const string _dailyMorningreminderDateKey = "DailyMorningReminderDate";
        private const string _dailyMorningreminderTimeKey = "DailyMorningReminderTime";

        private const string _dailyAfternoonreminderDateKey = "DailyAfternoonReminderDate";
        private const string _dailyAfternoonreminderTimeKey = "DailyAfternoonReminderTime";

        private const string _dailyEveningreminderDateKey = "DailyEveningReminderDate";
        private const string _dailyEveningreminderTimeKey = "DailyEveningReminderTime";

        private const string _dailyNightreminderDateKey = "DailyNightReminderDate";
        private const string _dailyNightreminderTimeKey = "DailyNightReminderTime";

        private const string _weeklyreminderDateKey = "WeeklyReminderDate";
        private const string _weeklyreminderTimeKey = "WeeklyReminderTime";

        private const string _monthlyreminderDateKey = "MonthlyReminderDate";
        private const string _monthlyreminderTimeKey = "MonthlyReminderTime";

        private const string _every28DaysreminderDateKey = "Every28DaysReminderDate";
        private const string _every28DaysreminderTimeKey = "Every28DaysReminderTime";

        private const string _objPillsReminderModelKey = "objPillsReminderModel";

        private const string _pillsReminderModelColLocalStorageKey = "PillsReminderModelColLocalStorage";
        private const string _reminderDateTempKey = "ReminderDateTemp";
        private const string _reminderTimeTempKey = "ReminderTimeTemp";

        private const string _isReminderToDisplayKey = "ReminderToDisplay";
        private const string _isReminderToSetKey = "IsReminderToSet";
        private const string _isCheckedKey = "IsChecked";
        private const string _isCheckedTempKey = "IsCheckedTemp";
        
        private const string _bgColorKey = "Bgcolor";

        private const string _selectedSurgenKey = "SelectedSurgen";
        private const string _surgeonSavedKey = "SurgeonSaved";
        private const string _surgeonAddressKey = "SurgeonAddress";

        private const string _isSelectedSurgenIndicatorKey = "IsSelectedSurgen";
        private const string _isDisableSearchsurgenKey = "IsDisableSearchsurgen";
        private const string _isNavigatedFromYourDetailsLoginKey = "IsNavigatedFromYourDetailsLogin";
        private const string _isNavigatedFromYourDetailsLoginwithTCKey = "IsNavigatedFromYourDetailsLoginwithTC";
        private const string _isNavigatedFromYourDetailsUpdateKey = "IsNavigatedFromYourDetailsUpdate";
        private const string _isDisplaySelectedSurgenOnSearchBoxKey = "IsDisplaySelectedSurgenOnSearchBox";
        private const string _selectSurgenCollectionGlobalvarKey = "SelectSurgenCollectionGlobalvar";

        private const string _isReminderDMOnOffKey = "DMReminderOnOff";
        private const string _isReminderDANOnOffKey = "DANReminderOnOff";
        private const string _isReminderDEOnOffKey = "DEReminderOnOff";
        private const string _isReminderDNOnOffKey = "DNReminderOnOff";
        private const string _isReminderWOnOffKey = "WReminderOnOff";
        private const string _isReminderMOnOffKey = "MReminderOnOff";
        private const string _isReminderE28OnOffKey = "E28ReminderOnOff";
        private const string _noofTriesLeftKey = "NoofTriesLeft";
        private const string _pinResetFromSettingsPageKey = "PinResetFromSettingsPage";
        private const string _localServiceDistanceKey = "LocalServiceDistance";
        private const string _isChangePharmacyKey = "IsChangePharmacy";
        private const string _transientMailIdKey = "transientMailId";

        private const string _localServiceLatitudeKey = "LocalServiceLatitude";
        private const string _localServiceLongitudeKey = "LocalServiceLongitude";
        private const string _searchTermKey = "SearchTerm";
        private const string _localServiceCentreCoordinatesKey = "LocalServiceCentreCoordinates";
        private const string _localServiceZoomLevelKey = "LocalServiceZoomLevel";

        private const string _selectedOrderKey = "SelectedOrder";
        private const string _orderedPillDetailsCollectionKey = "OrderedPillDetailsCollection";
        private const string _prescriptionCollectionKey = "PrescriptionCollectionKey";
        private const string _selectedDrugIndexKey = "SelectedDrugIndex";
        private const string _autoCompleteListDrugKey = "AutoCompleteListDrug";
        private const string _autoCompleteEditListDrugKey = "AutoCompleteEditListDrug";
        private const string _leafletWebLinkKey = "LeafletWebLink";

        private const string _ispillUpdatedKey = "IsPillUpdated";
        private const string _isEditCancelledKey = "IsEditCancelled";
       
        

        private const string _selectedIndexToEdit = "SelectedIndexToEdit";

        private string _isVerifiedByEmailKey = "IsVerifiedByEmail";
        private string _isVerifiedBySmsKey = "IsVerifiedBySms";

        private string _isUserNotExistKey = "IsUserNotExist";
        private string _mailIdToFillAfterPin = "MailIdToFillAfterPin";
        private string _isFromsignUpKeyKey = "IsFromsignUp";

        private const int intializeLocalServicesDistance = 30;
        private const int intializeNoofTriesLeft = 10;

        private const string intializeDocForSurgeryButtonText = "Choose your doctor surgery (optional)";
        private const string intializeReminderStatus = "Off";
        private DateTime initializeDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        private DateTime initializeTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        #endregion

        #region Methods

        /// <summary>
        /// Method to save app level Persistent data
        /// </summary>
        public void SaveAppLevelPersistantData()
        {
            PersistentDataStorage<string> persistData = new PersistentDataStorage<string>();

            persistData.Backup(_apIdKey, App.ApId);
            persistData.Backup(_pinKey, App.PIN);
            persistData.Backup(_loginInPharmacyKey, App.LoginPharId);
            persistData.Backup(_brandHashKey, App.BrandingHash);
            persistData.Backup(_advtHashKey, App.AdvtHash);
            persistData.Backup(_hashedPinKey, App.HashPIN);
            persistData.Backup(_staticSplashURLKey, App.StaticSplashURL);
            persistData.Backup(_drugsDataKey, App.DrugsData);
            persistData.Backup(_pillsReminderHeaderKey, App.HeaderPillsReminder);
            persistData.Backup(_isReminderToDisplayKey, App.ReminderToDisplay);
            persistData.Backup(_bgColorKey, App.Bgcolor);
            persistData.Backup(_drugsDBDataHashKey, App.DrugDBHash);

            persistData.Backup(_loginEmailId, App.LoginEmailId);
            persistData.Backup(_isReminderDMOnOffKey, App.DailyMorningReminderOnOff);
            persistData.Backup(_isReminderDANOnOffKey, App.DailyAfternoonReminderOnOff);
            persistData.Backup(_isReminderDEOnOffKey, App.DailyEveningReminderOnOff);
            persistData.Backup(_isReminderDNOnOffKey, App.DailyNightReminderOnOff);
            persistData.Backup(_isReminderWOnOffKey, App.WeeklyReminderOnOff);
            persistData.Backup(_isReminderMOnOffKey, App.MonthlyReminderOnOff);
            persistData.Backup(_isReminderE28OnOffKey, App.Every28DaysReminderOnOff);
            persistData.Backup(_surgeonSavedKey, App.SurgeonSaved);
      


            persistData.Backup(_pharmacyAddress1Key, App.LoginPharmacyAddress1);
            persistData.Backup(_pharmacyAddress2Key, App.LoginPharmacyAddress2);
            persistData.Backup(_pharmacyAddress3Key, App.LoginPharmacyAddress3);

            PersistentDataStorage<int> persistIntData = new PersistentDataStorage<int>();
            persistIntData.Backup(_noofTriesLeftKey, App.NoofTriesLeft);
            persistIntData.Backup(_localServiceDistanceKey, App.LocalServiceDistance);
           

            PersistentDataStorage<bool> persistboolData = new PersistentDataStorage<bool>();
            persistboolData.Backup(_isUserRegisteredKey, App.IsUserRegistered);
            persistboolData.Backup(_isPageHomePanoramaKey, App.IsPageHomePanorama);
            persistboolData.Backup(_isPageUpdateYourDetailsafterLoginKey, App.IsPageUpdateYourDetailsafterLogin);
            persistboolData.Backup(_isPageYourDetailsafterLoginSaved, App.IsPageYourDetailsafterLoginSaved);
            persistboolData.Backup(_isReminderToSetKey, App.IsReminderToSet);
            persistboolData.Backup(_isCheckedKey, App.IsChecked);
            persistboolData.Backup(_isVerifiedBySmsKey, App.IsVerifiedBySms);
            persistboolData.Backup(_isVerifiedByEmailKey, App.IsVerifiedByEmail);
         
            persistboolData.Backup(_isChangePharmacyKey, App.IsChangePharmacy);
            persistboolData.Backup(_isFromLoginScreenKey, App.IsFromLoginScreen);

           
            persistboolData.Backup(_isDisableSearchsurgenKey, App.IsDisableSearchsurgen);
            persistboolData.Backup(_isNavigatedFromYourDetailsLoginKey, App.IsNavigatedFromYourDetailsLogin);
            persistboolData.Backup(_isNavigatedFromYourDetailsLoginwithTCKey, App.IsNavigatedFromYourDetailsLoginwithTC);
            persistboolData.Backup(_isNavigatedFromYourDetailsUpdateKey, App.IsNavigatedFromYourDetailsUpdate);
            persistboolData.Backup(_isDisplaySelectedSurgenOnSearchBoxKey, App.IsDisplaySelectedSurgenOnSearchBox);

            PersistentDataStorage<LoginResponse> persistLoginResponseData = new PersistentDataStorage<LoginResponse>();
            persistLoginResponseData.Backup(_objLoginResponseKey, App.ObjLgResponse);

            PersistentDataStorage<GetPharmacyInformationResponse> persistPharmacyInformationResponseData = new PersistentDataStorage<GetPharmacyInformationResponse>();
            persistPharmacyInformationResponseData.Backup(_objPharmacyBrandInfoKey, App.ObjBrandingResponse);

            PersistentDataStorage<SelectSurgenFeedDataCollection> persistSelectedSurgenColl = new PersistentDataStorage<SelectSurgenFeedDataCollection>();
            persistSelectedSurgenColl.Backup(_selectSurgenCollectionGlobalvarKey, App.SelectSurgenCollectionGlobalvar);


            PersistentDataStorage<List<string>> persistAdvImageData = new PersistentDataStorage<List<string>>();
            persistAdvImageData.Backup(_lstAdvtImagesKey, App.AdImages);

            PersistentDataStorage<PillsReminderModelCol> persistPillsColl = new PersistentDataStorage<PillsReminderModelCol>();
            persistPillsColl.Backup(_pillsCollectionDMKey, App.DailyMorningPillsCollection);
            persistPillsColl.Backup(_pillsCollectionDAKey, App.DailyAfternoonPillsCollection);
            persistPillsColl.Backup(_pillsCollectionDEKey, App.DailyEveningPillsCollection);
            persistPillsColl.Backup(_pillsCollectionDNKey, App.DailyNightPillsCollection);
            persistPillsColl.Backup(_pillsCollectionWeeklyKey, App.WeeklyPillsCollection);
            persistPillsColl.Backup(_pillsCollectionMonthlyKey, App.MonthlyPillsCollection);
            persistPillsColl.Backup(_pillsCollectionE28DaysKey, App.Every28DaysPillsCollection);

            PersistentDataStorage<DateTime> persistDateTimeData = new PersistentDataStorage<DateTime>();
            persistDateTimeData.Backup(_dailyMorningreminderDateKey, App.DailyMorningReminderDate);
            persistDateTimeData.Backup(_dailyMorningreminderTimeKey, App.DailyMorningReminderTime);

            persistDateTimeData.Backup(_dailyAfternoonreminderDateKey, App.DailyAfternoonReminderDate);
            persistDateTimeData.Backup(_dailyAfternoonreminderTimeKey, App.DailyAfternoonReminderTime);

            persistDateTimeData.Backup(_dailyEveningreminderDateKey, App.DailyEveningReminderDate);
            persistDateTimeData.Backup(_dailyEveningreminderTimeKey, App.DailyEveningReminderTime);

            persistDateTimeData.Backup(_dailyNightreminderDateKey, App.DailyNightReminderDate);
            persistDateTimeData.Backup(_dailyNightreminderTimeKey, App.DailyNightReminderTime);

            persistDateTimeData.Backup(_weeklyreminderDateKey, App.WeeklyReminderDate);
            persistDateTimeData.Backup(_weeklyreminderTimeKey, App.WeeklyReminderTime);

            persistDateTimeData.Backup(_monthlyreminderDateKey, App.MonthlyReminderDate);
            persistDateTimeData.Backup(_monthlyreminderTimeKey, App.MonthlyReminderTime);

            persistDateTimeData.Backup(_every28DaysreminderDateKey, App.Every28DaysReminderDate);
            persistDateTimeData.Backup(_every28DaysreminderTimeKey, App.Every28DaysReminderTime);



        }
        /// <summary>
        /// Method to load the app level persistent data
        /// </summary>
        public void LoadAppLevelPersistantData()
        {
            PersistentDataStorage<string> persistData = new PersistentDataStorage<string>();            
            App.ApId = (null == persistData.Restore<string>(_apIdKey)) ? string.Empty : persistData.Restore<string>(_apIdKey);            
            App.PIN = (null == persistData.Restore<string>(_pinKey)) ? string.Empty : persistData.Restore<string>(_pinKey);
            App.LoginPharId = (null == persistData.Restore<string>(_loginInPharmacyKey)) ? string.Empty : persistData.Restore<string>(_loginInPharmacyKey);
            App.BrandingHash = (null == persistData.Restore<string>(_brandHashKey)) ? string.Empty : persistData.Restore<string>(_brandHashKey);
            App.AdvtHash = (null == persistData.Restore<string>(_advtHashKey)) ? string.Empty : persistData.Restore<string>(_advtHashKey);
            App.HashPIN = (null == persistData.Restore<string>(_hashedPinKey)) ? string.Empty : persistData.Restore<string>(_hashedPinKey);
            App.StaticSplashURL = (null == persistData.Restore<string>(_staticSplashURLKey)) ? string.Empty : persistData.Restore<string>(_staticSplashURLKey);
            App.HeaderPillsReminder = (null == persistData.Restore<string>(_pillsReminderHeaderKey)) ? string.Empty : persistData.Restore<string>(_pillsReminderHeaderKey);
            App.LoginPharmacyAddress1 = (null == persistData.Restore<string>(_pharmacyAddress1Key)) ? string.Empty : persistData.Restore<string>(_pharmacyAddress1Key);
            App.LoginPharmacyAddress2 = (null == persistData.Restore<string>(_pharmacyAddress2Key)) ? string.Empty : persistData.Restore<string>(_pharmacyAddress2Key);
            App.LoginPharmacyAddress3 = (null == persistData.Restore<string>(_pharmacyAddress3Key)) ? string.Empty : persistData.Restore<string>(_pharmacyAddress3Key);
            App.DrugsData = (null == persistData.Restore<string>(_drugsDataKey)) ? string.Empty : persistData.Restore<string>(_drugsDataKey);
            App.DrugDBHash = (null == persistData.Restore<string>(_drugsDBDataHashKey)) ? string.Empty : persistData.Restore<string>(_drugsDBDataHashKey);
            App.ReminderToDisplay = (null == persistData.Restore<string>(_isReminderToDisplayKey)) ? string.Empty : persistData.Restore<string>(_isReminderToDisplayKey);
            App.Bgcolor = (null == persistData.Restore<string>(_bgColorKey)) ? string.Empty : persistData.Restore<string>(_bgColorKey);
            App.LoginEmailId = (null == persistData.Restore<string>(_loginEmailId)) ? string.Empty : persistData.Restore<string>(_loginEmailId);

            App.DailyMorningReminderOnOff = (null == persistData.Restore<string>(_isReminderDMOnOffKey)) ? intializeReminderStatus : persistData.Restore<string>(_isReminderDMOnOffKey);
            App.DailyAfternoonReminderOnOff = (null == persistData.Restore<string>(_isReminderDANOnOffKey)) ? intializeReminderStatus : persistData.Restore<string>(_isReminderDANOnOffKey);
            App.DailyEveningReminderOnOff = (null == persistData.Restore<string>(_isReminderDEOnOffKey)) ? intializeReminderStatus : persistData.Restore<string>(_isReminderDEOnOffKey);
            App.DailyNightReminderOnOff = (null == persistData.Restore<string>(_isReminderDNOnOffKey)) ? intializeReminderStatus : persistData.Restore<string>(_isReminderDNOnOffKey);
            App.WeeklyReminderOnOff = (null == persistData.Restore<string>(_isReminderWOnOffKey)) ? intializeReminderStatus : persistData.Restore<string>(_isReminderWOnOffKey);
            App.MonthlyReminderOnOff = (null == persistData.Restore<string>(_isReminderMOnOffKey)) ? intializeReminderStatus : persistData.Restore<string>(_isReminderMOnOffKey);
            App.Every28DaysReminderOnOff = (null == persistData.Restore<string>(_isReminderE28OnOffKey)) ? intializeReminderStatus : persistData.Restore<string>(_isReminderE28OnOffKey);
            App.SurgeonSaved = (null == persistData.Restore<string>(_surgeonSavedKey)) ? string.Empty : persistData.Restore<string>(_surgeonSavedKey);

            PersistentDataStorage<int> persistIntData = new PersistentDataStorage<int>();
            App.NoofTriesLeft = (0 == persistIntData.Restore<int>(_noofTriesLeftKey)) ? intializeNoofTriesLeft : persistIntData.Restore<int>(_noofTriesLeftKey);
            App.LocalServiceDistance = (0 == persistIntData.Restore<int>(_localServiceDistanceKey)) ? intializeLocalServicesDistance : persistIntData.Restore<int>(_localServiceDistanceKey);


            PersistentDataStorage<bool> persistboolData = new PersistentDataStorage<bool>();
            App.IsUserRegistered = (false == persistboolData.Restore<bool>(_isUserRegisteredKey)) ? false : persistboolData.Restore<bool>(_isUserRegisteredKey);
            App.IsPageHomePanorama = (false == persistboolData.Restore<bool>(_isPageHomePanoramaKey)) ? false : persistboolData.Restore<bool>(_isPageHomePanoramaKey);
            App.IsPageUpdateYourDetailsafterLogin = (false == persistboolData.Restore<bool>(_isPageUpdateYourDetailsafterLoginKey)) ? false : persistboolData.Restore<bool>(_isPageUpdateYourDetailsafterLoginKey);
            App.IsPageYourDetailsafterLoginSaved = (false == persistboolData.Restore<bool>(_isPageYourDetailsafterLoginSaved)) ? false : persistboolData.Restore<bool>(_isPageYourDetailsafterLoginSaved);
            App.IsChangePharmacy = (false == persistboolData.Restore<bool>(_isChangePharmacyKey)) ? false : persistboolData.Restore<bool>(_isChangePharmacyKey);

            App.IsVerifiedBySms = (false == persistboolData.Restore<bool>(_isVerifiedBySmsKey)) ? false : persistboolData.Restore<bool>(_isVerifiedBySmsKey);
            App.IsVerifiedByEmail = (false == persistboolData.Restore<bool>(_isVerifiedByEmailKey)) ? false : persistboolData.Restore<bool>(_isVerifiedByEmailKey);

            App.IsReminderToSet = (false == persistboolData.Restore<bool>(_isReminderToSetKey)) ? false : persistboolData.Restore<bool>(_isReminderToSetKey);
            App.IsChecked = (false == persistboolData.Restore<bool>(_isCheckedKey)) ? false : persistboolData.Restore<bool>(_isCheckedKey);
            
            App.IsFromLoginScreen = (false == persistboolData.Restore<bool>(_isFromLoginScreenKey)) ? false : persistboolData.Restore<bool>(_isFromLoginScreenKey);

            
            App.IsDisableSearchsurgen = (false == persistboolData.Restore<bool>(_isDisableSearchsurgenKey)) ? false : persistboolData.Restore<bool>(_isDisableSearchsurgenKey);
            App.IsNavigatedFromYourDetailsLogin = (false == persistboolData.Restore<bool>(_isNavigatedFromYourDetailsLoginKey)) ? false : persistboolData.Restore<bool>(_isNavigatedFromYourDetailsLoginKey);
            App.IsNavigatedFromYourDetailsLoginwithTC = (false == persistboolData.Restore<bool>(_isNavigatedFromYourDetailsLoginwithTCKey)) ? false : persistboolData.Restore<bool>(_isNavigatedFromYourDetailsLoginwithTCKey);
            App.IsNavigatedFromYourDetailsUpdate = (false == persistboolData.Restore<bool>(_isNavigatedFromYourDetailsUpdateKey)) ? false : persistboolData.Restore<bool>(_isNavigatedFromYourDetailsUpdateKey);
            App.IsDisplaySelectedSurgenOnSearchBox = (false == persistboolData.Restore<bool>(_isDisplaySelectedSurgenOnSearchBoxKey)) ? false : persistboolData.Restore<bool>(_isDisplaySelectedSurgenOnSearchBoxKey);

            PersistentDataStorage<SelectSurgenFeedDataCollection> persistSelectedSurgenColl = new PersistentDataStorage<SelectSurgenFeedDataCollection>();
            App.SelectSurgenCollectionGlobalvar = (null == persistSelectedSurgenColl.Restore<SelectSurgenFeedDataCollection>(_selectSurgenCollectionGlobalvarKey)) ? null : persistSelectedSurgenColl.Restore<SelectSurgenFeedDataCollection>(_selectSurgenCollectionGlobalvarKey);


            PersistentDataStorage<LoginResponse> persistLoginResponseData = new PersistentDataStorage<LoginResponse>();
            App.ObjLgResponse = (null == persistLoginResponseData.Restore<LoginResponse>(_objLoginResponseKey)) ? null : persistLoginResponseData.Restore<LoginResponse>(_objLoginResponseKey);

            PersistentDataStorage<GetPharmacyInformationResponse> persistPharmacyInformationResponseData = new PersistentDataStorage<GetPharmacyInformationResponse>();
            App.ObjBrandingResponse = (null == persistPharmacyInformationResponseData.Restore<GetPharmacyInformationResponse>(_objPharmacyBrandInfoKey)) ? null : persistPharmacyInformationResponseData.Restore<GetPharmacyInformationResponse>(_objPharmacyBrandInfoKey);

            PersistentDataStorage<List<string>> persistAdvImageData = new PersistentDataStorage<List<string>>();
            App.AdImages = (null == persistAdvImageData.Restore<List<string>>(_lstAdvtImagesKey)) ? null : persistAdvImageData.Restore<List<string>>(_lstAdvtImagesKey);

            PersistentDataStorage<PillsReminderModelCol> persistPillsColl = new PersistentDataStorage<PillsReminderModelCol>();
            App.DailyMorningPillsCollection = (null == persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionDMKey)) ? null : persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionDMKey);
            App.DailyAfternoonPillsCollection = (null == persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionDAKey)) ? null : persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionDAKey);
            App.DailyEveningPillsCollection = (null == persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionDEKey)) ? null : persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionDEKey);
            App.DailyNightPillsCollection = (null == persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionDNKey)) ? null : persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionDNKey);
            App.WeeklyPillsCollection = (null == persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionWeeklyKey)) ? null : persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionWeeklyKey);
            App.MonthlyPillsCollection = (null == persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionMonthlyKey)) ? null : persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionMonthlyKey);
            App.Every28DaysPillsCollection = (null == persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionE28DaysKey)) ? null : persistPillsColl.Restore<PillsReminderModelCol>(_pillsCollectionE28DaysKey);

           
            
            PersistentDataStorage<DateTime> persistDateTimeData = new PersistentDataStorage<DateTime>();
            App.DailyMorningReminderDate = (new DateTime() == persistDateTimeData.Restore<DateTime>(_dailyMorningreminderDateKey)) ? initializeDate : persistDateTimeData.Restore<DateTime>(_dailyMorningreminderDateKey);
            App.DailyMorningReminderTime = (new DateTime() == persistDateTimeData.Restore<DateTime>(_dailyMorningreminderTimeKey)) ? initializeTime : persistDateTimeData.Restore<DateTime>(_dailyMorningreminderTimeKey);

            App.DailyAfternoonReminderDate = (new DateTime() == persistDateTimeData.Restore<DateTime>(_dailyAfternoonreminderDateKey)) ? initializeDate : persistDateTimeData.Restore<DateTime>(_dailyAfternoonreminderDateKey);
            App.DailyAfternoonReminderTime = (new DateTime() == persistDateTimeData.Restore<DateTime>(_dailyAfternoonreminderTimeKey)) ? initializeTime : persistDateTimeData.Restore<DateTime>(_dailyAfternoonreminderTimeKey);

            App.DailyEveningReminderDate = (new DateTime() == persistDateTimeData.Restore<DateTime>(_dailyEveningreminderDateKey)) ? initializeDate : persistDateTimeData.Restore<DateTime>(_dailyEveningreminderDateKey);
            App.DailyEveningReminderTime = (new DateTime() == persistDateTimeData.Restore<DateTime>(_dailyEveningreminderTimeKey)) ? initializeTime : persistDateTimeData.Restore<DateTime>(_dailyEveningreminderTimeKey);

            App.DailyNightReminderDate = (new DateTime() == persistDateTimeData.Restore<DateTime>(_dailyNightreminderDateKey)) ? initializeDate : persistDateTimeData.Restore<DateTime>(_dailyNightreminderDateKey);
            App.DailyNightReminderTime = (new DateTime() == persistDateTimeData.Restore<DateTime>(_dailyNightreminderTimeKey)) ? initializeTime : persistDateTimeData.Restore<DateTime>(_dailyNightreminderTimeKey);

            App.WeeklyReminderDate = (new DateTime() == persistDateTimeData.Restore<DateTime>(_weeklyreminderDateKey)) ? initializeDate : persistDateTimeData.Restore<DateTime>(_weeklyreminderDateKey);
            App.WeeklyReminderTime = (new DateTime() == persistDateTimeData.Restore<DateTime>(_weeklyreminderTimeKey)) ? initializeTime : persistDateTimeData.Restore<DateTime>(_weeklyreminderTimeKey);

            App.MonthlyReminderDate = (new DateTime() == persistDateTimeData.Restore<DateTime>(_monthlyreminderDateKey)) ? initializeDate : persistDateTimeData.Restore<DateTime>(_monthlyreminderDateKey);
            App.MonthlyReminderTime = (new DateTime() == persistDateTimeData.Restore<DateTime>(_monthlyreminderTimeKey)) ? initializeTime : persistDateTimeData.Restore<DateTime>(_monthlyreminderTimeKey);

            App.Every28DaysReminderDate = (new DateTime() == persistDateTimeData.Restore<DateTime>(_every28DaysreminderDateKey)) ? initializeDate : persistDateTimeData.Restore<DateTime>(_every28DaysreminderTimeKey);
            App.Every28DaysReminderTime = (new DateTime() == persistDateTimeData.Restore<DateTime>(_every28DaysreminderTimeKey)) ? initializeTime : persistDateTimeData.Restore<DateTime>(_every28DaysreminderTimeKey);
        }

        /// <summary>
        /// Method to save app level transient data
        /// </summary>
        public void SaveAppLevelTransientData()
        {
            TransientDataStorage<string> transientData = new TransientDataStorage<string>();
            transientData.Backup(_signUpPharmacyKey, App.SignUpPharId);
            transientData.Backup(_pharmacyNameKey, App.LoginPharmacyname);
            transientData.Backup(_pharmacyAddress1Key, App.LoginPharmacyAddress1);
            transientData.Backup(_pharmacyAddress2Key, App.LoginPharmacyAddress2);
            transientData.Backup(_pharmacyAddress3Key, App.LoginPharmacyAddress3);
            transientData.Backup(_postCodekey, App.PostCode);
            transientData.Backup(_tombStonedPageURLKey, App.TombStonedPageURL);
            transientData.Backup(_pinKey, App.PIN);
            transientData.Backup(_hashedPinKey, App.HashPIN);
            transientData.Backup(_selectedSurgenKey, App.SelectedSurgen);
            transientData.Backup(_pharmacyBranchNameKey, App.PharmacyBranchName);
            transientData.Backup(_leafletWebLinkKey, App.LeafletWebLink);
            transientData.Backup(_surgeonAddressKey, App.SurgeonAddress);

            transientData.Backup(_localServiceLatitudeKey, App.LocalServiceLatitude);
            transientData.Backup(_localServiceLongitudeKey, App.LocalServiceLongitude);
            transientData.Backup(_searchTermKey, App.SearchTerm);

            transientData.Backup(_transientMailIdKey, App.transientMailId);
            transientData.Backup(_mailIdToFillAfterPin, App.MailIdToFillAfterPin);
           

            TransientDataStorage<GeoCoordinate> transientDataGeoCoordinate = new TransientDataStorage<GeoCoordinate>();
            transientDataGeoCoordinate.Backup(_localServiceCentreCoordinatesKey, App.LocalServiceCentreCoordinates);

            TransientDataStorage<double> transientDataZoomLevel = new TransientDataStorage<double>();
            transientDataZoomLevel.Backup(_localServiceZoomLevelKey, App.LocalServiceZoomLevel);

            TransientDataStorage<List<PillsReminderModel>> transientPillsColl = new TransientDataStorage<List<PillsReminderModel>>();
            transientPillsColl.Backup(_pillsReminderModelColLocalStorageKey, App.PillsReminderModelColLocalStorage);

            TransientDataStorage<PillsReminderModel> transientPillModel= new TransientDataStorage<PillsReminderModel>();
            transientPillModel.Backup(_objPillsReminderModelKey, App.objPillsReminderModel);

            TransientDataStorage<bool> boolData = new TransientDataStorage<bool>();
            boolData.Backup(_isCheckedTempKey, App.IsCheckedTempStore);
            boolData.Backup(_isCheckedKey, App.IsChecked);
            boolData.Backup(_pinResetFromSettingsPageKey, App.PinResetFromSettingsPage);
            boolData.Backup(_isFromRejected, App.IsFromRejected);
             boolData.Backup(_ispillUpdatedKey, App.IsPillUpdated);
            boolData.Backup(_isEditCancelledKey, App.IsEditCancelled);
            boolData.Backup(_isUserNotExistKey, App.IsUserNotExist);
            boolData.Backup(_isFromsignUpKeyKey, App.IsFromsignUp);
           

            TransientDataStorage<OrderedPillDetails> orderedPilltransientData = new TransientDataStorage<OrderedPillDetails>();
            orderedPilltransientData.Backup(_selectedOrderKey, App.selectedOrder);

            TransientDataStorage<OrderedPillDetailsCollection> orderedPillsCollectiontransientData = new TransientDataStorage<OrderedPillDetailsCollection>();
            orderedPillsCollectiontransientData.Backup(_orderedPillDetailsCollectionKey, App.OrderedPillDetailsCollection);

            TransientDataStorage<PrescriptionCollection> prescriptionCollectiontransientData = new TransientDataStorage<PrescriptionCollection>();
            prescriptionCollectiontransientData.Backup(_prescriptionCollectionKey, App.prescriptionCollection);

            TransientDataStorage<int> intTransientData = new TransientDataStorage<int>();
            intTransientData.Backup(_selectedDrugIndexKey, App.selectedDrugIndex);
            intTransientData.Backup(_selectedIndexToEdit, App.SelectedIndexToEdit);
           

            TransientDataStorage<DateTime> transientDateTimeData = new TransientDataStorage<DateTime>();
            transientDateTimeData.Backup(_reminderDateTempKey, App.ReminderDateTemp);
            transientDateTimeData.Backup(_reminderTimeTempKey, App.ReminderTimeTemp);


            TransientDataStorage<List<string>> autoCompleteOrderRepeatTransientData = new TransientDataStorage<List<string>>();
            autoCompleteOrderRepeatTransientData.Backup(_autoCompleteListDrugKey, App.autoCompleteListDrug);
            autoCompleteOrderRepeatTransientData.Backup(_autoCompleteListDrugKey, App.autoCompleteEditListDrug);

        }
        /// <summary>
        /// Method to load app level transient data
        /// </summary>
        public void LoadAppLevelTransientData()
        {
            TransientDataStorage<string> transientData = new TransientDataStorage<string>();
            App.SignUpPharId = (null == transientData.Restore<string>(_signUpPharmacyKey)) ? string.Empty : transientData.Restore<string>(_signUpPharmacyKey);
            App.LoginPharmacyname = (null == transientData.Restore<string>(_pharmacyNameKey)) ? string.Empty : transientData.Restore<string>(_pharmacyNameKey);
            App.LoginPharmacyAddress1 = (null == transientData.Restore<string>(_pharmacyAddress1Key)) ? string.Empty : transientData.Restore<string>(_pharmacyAddress1Key);
            App.LoginPharmacyAddress2 = (null == transientData.Restore<string>(_pharmacyAddress2Key)) ? string.Empty : transientData.Restore<string>(_pharmacyAddress2Key);
            App.LoginPharmacyAddress3 = (null == transientData.Restore<string>(_pharmacyAddress3Key)) ? string.Empty : transientData.Restore<string>(_pharmacyAddress3Key);
            App.PostCode = (null == transientData.Restore<string>(_postCodekey)) ? string.Empty : transientData.Restore<string>(_postCodekey);
            App.TombStonedPageURL = (null == transientData.Restore<string>(_tombStonedPageURLKey)) ? string.Empty : transientData.Restore<string>(_tombStonedPageURLKey);
            App.PIN = (null == transientData.Restore<string>(_pinKey)) ? string.Empty : transientData.Restore<string>(_pinKey);
            App.HashPIN = (null == transientData.Restore<string>(_hashedPinKey)) ? string.Empty : transientData.Restore<string>(_hashedPinKey);
            App.SelectedSurgen = (null == transientData.Restore<string>(_selectedSurgenKey)) ? string.Empty : transientData.Restore<string>(_selectedSurgenKey);
            App.PharmacyBranchName = (null == transientData.Restore<string>(_pharmacyBranchNameKey)) ? string.Empty : transientData.Restore<string>(_pharmacyBranchNameKey);
            App.LeafletWebLink = (null == transientData.Restore<string>(_leafletWebLinkKey)) ? string.Empty : transientData.Restore<string>(_leafletWebLinkKey);
            App.SurgeonAddress = (null == transientData.Restore<string>(_surgeonAddressKey)) ? string.Empty : transientData.Restore<string>(_surgeonAddressKey);

            App.LocalServiceLatitude = (null == transientData.Restore<string>(_localServiceLatitudeKey)) ? string.Empty : transientData.Restore<string>(_localServiceLatitudeKey);
            App.LocalServiceLongitude = (null == transientData.Restore<string>(_localServiceLongitudeKey)) ? string.Empty : transientData.Restore<string>(_localServiceLongitudeKey);
            App.SearchTerm = (null == transientData.Restore<string>(_searchTermKey)) ? string.Empty : transientData.Restore<string>(_searchTermKey);

             App.transientMailId = (null == transientData.Restore<string>(_transientMailIdKey)) ? string.Empty : transientData.Restore<string>(_transientMailIdKey);
             App.MailIdToFillAfterPin = (null == transientData.Restore<string>(_mailIdToFillAfterPin)) ? string.Empty : transientData.Restore<string>(_mailIdToFillAfterPin);
            

            TransientDataStorage<GeoCoordinate> transientDataGeoCoordinate = new TransientDataStorage<GeoCoordinate>();
            App.LocalServiceCentreCoordinates = (null == transientDataGeoCoordinate.Restore<GeoCoordinate>(_localServiceCentreCoordinatesKey)) ? null : transientDataGeoCoordinate.Restore<GeoCoordinate>(_localServiceCentreCoordinatesKey);

            TransientDataStorage<double> transientDataZoomLevel = new TransientDataStorage<double>();
            App.LocalServiceZoomLevel = (0 == transientDataZoomLevel.Restore<double>(_localServiceZoomLevelKey)) ? 0 : transientDataZoomLevel.Restore<double>(_localServiceZoomLevelKey);
           
            TransientDataStorage<bool> boolData = new TransientDataStorage<bool>();
            App.IsChecked = (false == boolData.Restore<bool>(_isCheckedKey)) ? false : boolData.Restore<bool>(_isCheckedKey);
            App.IsCheckedTempStore = (false == boolData.Restore<bool>(_isCheckedTempKey)) ? false : boolData.Restore<bool>(_isCheckedTempKey);
            App.PinResetFromSettingsPage = (false == boolData.Restore<bool>(_pinResetFromSettingsPageKey)) ? false : boolData.Restore<bool>(_pinResetFromSettingsPageKey);
            App.IsFromRejected = (false == boolData.Restore<bool>(_isFromRejected)) ? false : boolData.Restore<bool>(_isFromRejected);
            App.IsPillUpdated = (false == boolData.Restore<bool>(_ispillUpdatedKey)) ? false : boolData.Restore<bool>(_ispillUpdatedKey);
            App.IsEditCancelled = (false == boolData.Restore<bool>(_isEditCancelledKey)) ? false : boolData.Restore<bool>(_isEditCancelledKey);
            App.IsUserNotExist = (false == boolData.Restore<bool>(_isUserNotExistKey)) ? false : boolData.Restore<bool>(_isUserNotExistKey);

            App.IsFromsignUp = (false == boolData.Restore<bool>(_isFromsignUpKeyKey)) ? false : boolData.Restore<bool>(_isFromsignUpKeyKey);
           

            TransientDataStorage<OrderedPillDetails> orderedPilltransientData = new TransientDataStorage<OrderedPillDetails>();
            App.selectedOrder = (null == orderedPilltransientData.Restore<OrderedPillDetails>(_selectedOrderKey)) ? null : orderedPilltransientData.Restore<OrderedPillDetails>(_selectedOrderKey);

            TransientDataStorage<List<PillsReminderModel>> transientPillsColl = new TransientDataStorage<List<PillsReminderModel>>();
            App.PillsReminderModelColLocalStorage = (null == transientPillsColl.Restore<List<PillsReminderModel>>(_pillsReminderModelColLocalStorageKey)) ? null : transientPillsColl.Restore<List<PillsReminderModel>>(_pillsReminderModelColLocalStorageKey);

            TransientDataStorage<PillsReminderModel> transientPillModel = new TransientDataStorage<PillsReminderModel>();
            App.objPillsReminderModel = (null == transientPillModel.Restore<PillsReminderModel>(_objPillsReminderModelKey)) ? null : transientPillModel.Restore<PillsReminderModel>(_objPillsReminderModelKey);
          

            TransientDataStorage<OrderedPillDetailsCollection> orderedPillsCollectiontransientData = new TransientDataStorage<OrderedPillDetailsCollection>();
            App.OrderedPillDetailsCollection = (null == orderedPillsCollectiontransientData.Restore<OrderedPillDetailsCollection>(_orderedPillDetailsCollectionKey)) ? null : orderedPillsCollectiontransientData.Restore<OrderedPillDetailsCollection>(_orderedPillDetailsCollectionKey);

            TransientDataStorage<PrescriptionCollection> prescriptionCollectiontransientData = new TransientDataStorage<PrescriptionCollection>();
            App.prescriptionCollection = (null == prescriptionCollectiontransientData.Restore<PrescriptionCollection>(_prescriptionCollectionKey)) ? null : prescriptionCollectiontransientData.Restore<PrescriptionCollection>(_prescriptionCollectionKey);

            TransientDataStorage<int> intTransientData = new TransientDataStorage<int>();
            App.selectedDrugIndex = (0 == intTransientData.Restore<int>(_selectedDrugIndexKey)) ? 0 : intTransientData.Restore<int>(_selectedDrugIndexKey);
            App.SelectedIndexToEdit = (0 == intTransientData.Restore<int>(_selectedIndexToEdit)) ? 0 : intTransientData.Restore<int>(_selectedIndexToEdit);
           
           
            TransientDataStorage<List<string>> autoCompleteOrderRepeatTransientData = new TransientDataStorage<List<string>>();
            App.autoCompleteListDrug = (null == autoCompleteOrderRepeatTransientData.Restore<List<string>>(_autoCompleteListDrugKey)) ? null : autoCompleteOrderRepeatTransientData.Restore<List<string>>(_autoCompleteListDrugKey);
            App.autoCompleteEditListDrug = (null == autoCompleteOrderRepeatTransientData.Restore<List<string>>(_autoCompleteEditListDrugKey)) ? null : autoCompleteOrderRepeatTransientData.Restore<List<string>>(_autoCompleteEditListDrugKey);

            TransientDataStorage<DateTime> transientDateTimeData = new TransientDataStorage<DateTime>();
            App.ReminderDateTemp = (new DateTime() == transientDateTimeData.Restore<DateTime>(_reminderDateTempKey)) ? initializeDate : transientDateTimeData.Restore<DateTime>(_reminderDateTempKey);
            App.ReminderTimeTemp = (new DateTime() == transientDateTimeData.Restore<DateTime>(_reminderTimeTempKey)) ? initializeTime : transientDateTimeData.Restore<DateTime>(_reminderTimeTempKey);

        }
        /// <summary>
        /// For Restoring  page values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T RestorePageLevelData<T>(string key)
        {
            TransientDataStorage<T> store = new TransientDataStorage<T>();

            var data = store.Restore<T>(key);

            return (T)data;
        }
        /// <summary>
        /// For saving page values on tombstoning
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SavePageLevelData<T>(string key, T value)
        {
            TransientDataStorage<T> store = new TransientDataStorage<T>();

            store.Backup(key, value);

        }
        public void ClearPageLevelData<T>(string key, T value)
        {
            TransientDataStorage<T> store = new TransientDataStorage<T>();

            store.Remove(key);
        }
        /// <summary>
        /// ClearAllTransientData
        /// </summary>
        public void ClearAllTransientData()
        {
            TransientDataStorage<string> transientData = new TransientDataStorage<string>();
            transientData.Clear();
            TransientDataStorage<bool> boolData = new TransientDataStorage<bool>();
            boolData.Clear();
            TransientDataStorage<OrderedPillDetails> orderedPilltransientData = new TransientDataStorage<OrderedPillDetails>();
            orderedPilltransientData.Clear();
            TransientDataStorage<OrderedPillDetailsCollection> orderedPillsCollectiontransientData = new TransientDataStorage<OrderedPillDetailsCollection>();
            orderedPillsCollectiontransientData.Clear();
            TransientDataStorage<PrescriptionCollection> prescriptionCollectiontransientData = new TransientDataStorage<PrescriptionCollection>();
            prescriptionCollectiontransientData.Clear();
            TransientDataStorage<int> intTransientData = new TransientDataStorage<int>();
            intTransientData.Clear();
            TransientDataStorage<List<string>> autoCompleteOrderRepeatTransientData = new TransientDataStorage<List<string>>();
            autoCompleteOrderRepeatTransientData.Clear();
        }
        /// <summary>
        /// ClearAllPersistent Data
        /// </summary>
        public void ClearAllPersistentData()
        {
            PersistentDataStorage<string> persistData = new PersistentDataStorage<string>();
            persistData.Clear();
            PersistentDataStorage<int> persistIntData = new PersistentDataStorage<int>();
            persistIntData.Clear();
            PersistentDataStorage<bool> persistboolData = new PersistentDataStorage<bool>();
            persistboolData.Clear();
            PersistentDataStorage<SelectSurgenFeedDataCollection> persistSelectedSurgenColl = new PersistentDataStorage<SelectSurgenFeedDataCollection>();
            persistSelectedSurgenColl.Clear();
            PersistentDataStorage<LoginResponse> persistLoginResponseData = new PersistentDataStorage<LoginResponse>();
            persistLoginResponseData.Clear();
            PersistentDataStorage<GetPharmacyInformationResponse> persistPharmacyInformationResponseData = new PersistentDataStorage<GetPharmacyInformationResponse>();
            persistPharmacyInformationResponseData.Clear();
            PersistentDataStorage<List<string>> persistAdvImageData = new PersistentDataStorage<List<string>>();
            persistAdvImageData.Clear();
            PersistentDataStorage<PillsReminderModelCol> persistPillsColl = new PersistentDataStorage<PillsReminderModelCol>();
            persistPillsColl.Clear();
            PersistentDataStorage<DateTime> persistDateTimeData = new PersistentDataStorage<DateTime>();
            persistDateTimeData.Clear();
        }
        #endregion
    }
}