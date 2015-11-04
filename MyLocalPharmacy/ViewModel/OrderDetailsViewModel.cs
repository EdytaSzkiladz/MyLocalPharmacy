using MyLocalPharmacy.Common;
using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyLocalPharmacy.ViewModel
{
    public class OrderDetailsViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderDetailsViewModel()
        {
            IsPopupCancelledOpen = false;
            HitVisibility = true;

            if (App.ObjBrandingResponse != null)
            {
                PrimaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.primary_colour);
                SecondaryColour = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.secondary_colour);
                FontColor = Utilities.GetColorFromHexa(App.ObjBrandingResponse.payload.branding_data.appearance.font_colour);
            }
            else
            {
                PrimaryColour = SecondaryColour = Utilities.GetColorFromHexa(RxConstants.PrimaryColourCode);
                FontColor = Utilities.GetColorFromHexa(RxConstants.FontColourCode);
            }
            OrderDate = App.selectedOrder.orderDate;
            DrugName = App.selectedOrder.drugname;
            DrugQuantity = App.selectedOrder.quantity;
            DrugStatus = App.selectedOrder.status;

            if (DrugStatus.Equals(RxConstants.OrderStatusToCancelled) || DrugStatus.Equals(RxConstants.OrderStatusToCollected) || DrugStatus.Equals(RxConstants.OrderStatusToDelivered))
            {
                CancelReorder = RxConstants.ButtonContentReorder;
                ReasonHitVisibility = true;
            }
            else
            {
                CancelReorder = RxConstants.ButtonContentCancel;
                ReasonHitVisibility = false;
            }

            TimeRange = ConvertToTimeLapsed();
            Time = App.selectedOrder.time;
            ReasonForOrdering = App.selectedOrder.reason;
            StatusFontColour = App.selectedOrder.StatusFontColour;

        } 
        #endregion
      
        #region Properties

        /// <summary>
        /// For Primary Color
        /// </summary>
        private SolidColorBrush _primaryColour;
        [IgnoreDataMember]
        public SolidColorBrush PrimaryColour
        {
            get { return _primaryColour; }
            set
            {
                _primaryColour = value;
                OnPropertyChanged("PrimaryColour");
            }
        }
        /// <summary>
        /// For Secondary Color
        /// </summary>
        private SolidColorBrush _secondaryColour;
         [IgnoreDataMember]
        public SolidColorBrush SecondaryColour
        {
            get { return _secondaryColour; }
            set
            {
                _secondaryColour = value;
                OnPropertyChanged("SecondaryColour");
            }
        }

        /// <summary>
        /// For Font Color
        /// </summary>
        private SolidColorBrush _fontColor;
         [IgnoreDataMember]
        public SolidColorBrush FontColor
        {
            get { return _fontColor; }
            set
            {
                _fontColor = value;
                OnPropertyChanged("FontColor");
            }
        }

         private string _statusFontColour;
         
         public string StatusFontColour
         {
             get { return _statusFontColour; }
             set
             {
                 _statusFontColour = value;
                 OnPropertyChanged("StatusFontColour");
             }
         }
        
        /// <summary>
        /// Property content of button Re-order/Cancel
        /// </summary>
        private string _cancelReorder;
        [DataMember]
        public string CancelReorder
        {
            get { return _cancelReorder; }
            set { _cancelReorder = value; OnPropertyChanged("CancelReorder"); }
        }

        /// <summary>
        /// Property content of popup Re-ordered/Cancelled
        /// </summary>
        private string _popupCancelText;
        [DataMember]
        public string PopupCancelText
        {
            get { return _popupCancelText; }
            set { _popupCancelText = value; OnPropertyChanged("PopupCancelText"); }
        }

        /// <summary>
        /// Property content of popup Confirm
        /// </summary>
        private string _popupTextConfirm;
        [DataMember]
        public string PopupTextConfirm
        {
            get { return _popupTextConfirm; }
            set { _popupTextConfirm = value; OnPropertyChanged("PopupTextConfirm"); }
        }
        
        
        /// <summary>
        /// Property to set Date of Order
        /// </summary>
        private string _orderDate;
        [DataMember]
        public string OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; OnPropertyChanged("OrderDate"); }
        }

        
        /// <summary>
        /// Property to set drug name
        /// </summary>
        private string _drugName;
        [DataMember]
        public string DrugName
        {
            get { return _drugName; }
            set { _drugName = value; OnPropertyChanged("DrugName"); }
        }

        /// <summary>
        /// Property to set drug quantity
        /// </summary>
        private string _drugQuantity;
        [DataMember]
        public string DrugQuantity
        {
            get { return _drugQuantity; }
            set { _drugQuantity = value; OnPropertyChanged("DrugQuantity"); }
        }

        /// <summary>
        /// Property to set order status
        /// </summary>
        private string _drugStatus;
        [DataMember]
        public string DrugStatus
        {
            get { return _drugStatus; }
            set { _drugStatus = value; OnPropertyChanged("DrugStatus"); }

        }

        /// <summary>
        /// Property to set time from ordered time
        /// </summary>
        private string _timeRange;
        [DataMember]
        public string TimeRange
        {
            get { return _timeRange; }
            set { _timeRange = value; OnPropertyChanged("TimeRange"); }
        }

        /// <summary>
        /// Property to set time of ordering
        /// </summary>
        private string _time;
        [DataMember]
        public string Time
        {
            get { return _time; }
            set { _time = value; OnPropertyChanged("Time"); }
        }

        /// <summary>
        /// Property to set time of ordering
        /// </summary>
        private string _reasonForOrdering;
        [DataMember]
        public string ReasonForOrdering
        {
            get { return _reasonForOrdering; }
            set { _reasonForOrdering = value; OnPropertyChanged("ReasonForOrdering"); }
        }



        /// <summary>
        /// Property to show and hide Cancelled popup 
        /// </summary>
        private bool _isPopupCancelledOpen;
        [DataMember]
        public bool IsPopupCancelledOpen
        {
            get { return _isPopupCancelledOpen; }
            set { _isPopupCancelledOpen = value; OnPropertyChanged("IsPopupCancelledOpen"); }
        }

        /// <summary>
        /// Property to show and hide Confirm popup 
        /// </summary>
        private bool _isConfirmPopupOpen;
        [DataMember]
        public bool IsConfirmPopupOpen
        {
            get { return _isConfirmPopupOpen; }
            set { _isConfirmPopupOpen = value; OnPropertyChanged("IsConfirmPopupOpen"); }
        }

        /// <summary>
        /// Property to set hit visibility
        /// </summary>
        private bool _hitVisibility;
        [DataMember]
        public bool HitVisibility
        {
            get { return _hitVisibility; }
            set { _hitVisibility = value; OnPropertyChanged("HitVisibility"); }
        }

        /// <summary>
        /// Property to set Reason for ordering hit visibility
        /// </summary>
        private bool _reasonHitVisibility;
        [DataMember]
        public bool ReasonHitVisibility
        {
            get { return _reasonHitVisibility; }
            set { _reasonHitVisibility = value; OnPropertyChanged("ReasonHitVisibility"); }
        }


        /// <summary>
        /// Property for Cancel order Button click
        /// </summary>
        private RelayCommand _cancelOrderCommand;
         [IgnoreDataMember]
        public RelayCommand CancelOrderCommand
        {

            get
            {
                if (_cancelOrderCommand == null)
                {
                    _cancelOrderCommand = new RelayCommand(CancelOrderTapped);
                    _cancelOrderCommand.Enabled = true;
                }

                return _cancelOrderCommand;
            }
            set { _cancelOrderCommand = value; }
        }

        /// <summary>
        /// Property for order cancelled Popup Ok Button click
        /// </summary>
        private RelayCommand _popupCancelledOkCommand;
         [IgnoreDataMember]
        public RelayCommand PopupCancelledOkCommand
        {

            get
            {
                if (_popupCancelledOkCommand == null)
                {
                    _popupCancelledOkCommand = new RelayCommand(PopupCancelledOkTapped);
                    _popupCancelledOkCommand.Enabled = true;
                }

                return _popupCancelledOkCommand;
            }
            set { _popupCancelledOkCommand = value; }
        }

        /// <summary>
        /// Property for confirm Popup Cancel Button click
        /// </summary>
        private RelayCommand _cancelCommandPopup;
         [IgnoreDataMember]
        public RelayCommand CancelCommandPopup
        {

            get
            {
                if (_cancelCommandPopup == null)
                {
                    _cancelCommandPopup = new RelayCommand(CancelPopupTapped);
                    _cancelCommandPopup.Enabled = true;
                }

                return _cancelCommandPopup;
            }
            set { _cancelCommandPopup = value; }
        }

        /// <summary>
        /// Property for confirm Popup OK Button click
        /// </summary>
        private RelayCommand _okComandPopup;
         [IgnoreDataMember]
        public RelayCommand OkComandPopup
        {

            get
            {
                if (_okComandPopup == null)
                {
                    _okComandPopup = new RelayCommand(OkPopupTapped);
                    _okComandPopup.Enabled = true;
                }

                return _okComandPopup;
            }
            set { _okComandPopup = value; }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to find time span
        /// </summary>
        /// <returns></returns>
        public string ConvertToTimeLapsed()
        {
            TimeSpan timeSpan = new TimeSpan();
            timeSpan = DateTime.Now - App.selectedOrder.updated;
            if (timeSpan.TotalSeconds<0)
            {
                timeSpan = -timeSpan;
            }
            
            string timeRange = "0 minutes ago";
            if (timeSpan.TotalDays > 365)
            {
                int years = (int)(timeSpan.TotalDays / 365);
                if (years==1)
                timeRange = years + " year ago";
                else
                    timeRange = years + " years ago";
            }
            else if (timeSpan.TotalDays > 30)
            {
                int months = (int)(timeSpan.TotalDays / 30);
                if (months==1)
                timeRange = months + " month ago";
                else
                    timeRange = months + " months ago";
            }
            else if (timeSpan.TotalHours > 24)
            {
                int days = (int)(timeSpan.TotalHours / 24);
                if (days==1)
                timeRange = days + " day ago";
                else
                    timeRange = days + " days ago";
            }
            else if (timeSpan.TotalMinutes > 60)
            {
                int hours = (int)(timeSpan.TotalMinutes / 60);
                if (hours==1)
                timeRange = hours + " hour ago";
                else
                    timeRange = hours + " hours ago";
            }
            else if (timeSpan.TotalSeconds > 60)
            {
                int minutes = (int)(timeSpan.TotalSeconds / 60);
                if (minutes==1)
                timeRange = minutes + " minute ago";
                else
                    timeRange = minutes + " minutes ago";
            }

            else
            {
                timeRange = "0 minute ago";
            }
            return timeRange;
        }

		/// <summary>
		/// Method invoked on tapping cancel Order/repeat order
		/// </summary>
        private void CancelOrderTapped()
        {
            IsConfirmPopupOpen = true;
            HitVisibility = false;
            if (CancelReorder.Equals(RxConstants.ButtonContentCancel))
            {
                PopupTextConfirm = "Are you sure you want to cancel this order?";
            }
            else
            {
                PopupTextConfirm = "Are you sure you want to re-order this repeat?";
            }
        }

        /// <summary>
        /// Method invoked on tapping cancel button of confirm popup
        /// </summary>
        private void CancelPopupTapped()
        {
            IsConfirmPopupOpen = false;
            HitVisibility = true;
            
        }

        /// <summary>
        /// Method invoked on tapping OK button of confirm popup
        /// </summary>
        private void OkPopupTapped()
        {
            IsConfirmPopupOpen = false;
            HitVisibility = true;
            if (CancelReorder.Equals(RxConstants.ButtonContentCancel))
            {
                OrderDetailsModel OrderDetailsModel = new OrderDetailsModel(this, "cancel");
                PopupTextConfirm = "Are you sure you want to cancel this order?";
            }
            else
            {
                OrderDetailsModel OrderDetailsModel = new OrderDetailsModel(this, "reorder");
                PopupTextConfirm = "Are you sure you want to re-order this repeat?";
            }
        }

        /// <summary>
        /// Method invoked on tapping Ok button of popup order cancelled
        /// </summary>
        private void PopupCancelledOkTapped()
        {
            IsPopupCancelledOpen = false;

            INavigationService navigationService = this.GetService<INavigationService>();
            navigationService.Navigate(PageURL.navigateToHomePanoramaURL + "?goto=2");

            HitVisibility = true;
        }

	    #endregion 
    }
}
