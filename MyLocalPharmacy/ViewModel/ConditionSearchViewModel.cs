using MyLocalPharmacy.Common;
using MyLocalPharmacy.Entities;
using MyLocalPharmacy.Model;
using System.Windows;

namespace MyLocalPharmacy.ViewModel
{
    public class ConditionSearchViewModel : BaseViewModel
    {
        #region Declarations
        ConditionSearchModel ConditionSearchModel; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ConditionSearchViewModel()
        {
            NoLeafletTextVisibility = Visibility.Visible;
        } 
        #endregion

        #region Properties
        /// <summary>
        /// Itemsource property of LongListSelector
        /// </summary>
        private ConditionSearchCollection _searchCollection;

        public ConditionSearchCollection SearchCollection
        {
            get
            {
                return _searchCollection;
            }

            set
            {
                if (value != null)
                {
                    
                        _searchCollection = value;
                        OnPropertyChanged("SearchCollection");
                }

            }
        }

        /// <summary>
        /// Set progress bar visibility
        /// </summary>
        private Visibility _progressBarVisibilty = Visibility.Collapsed;
        public Visibility ProgressBarVisibilty
        {
            get
            {
                return _progressBarVisibilty;
            }
            set
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                _progressBarVisibilty = value;
                OnPropertyChanged("ProgressBarVisibilty");
                });

            }
        }

        /// <summary>
        /// Set progress bar visibility
        /// </summary>
        private Visibility _noLeafletTextVisibility = Visibility.Collapsed;
        public Visibility NoLeafletTextVisibility
        {
            get
            {
                return _noLeafletTextVisibility;
            }
            set
            {
               
                _noLeafletTextVisibility = value;
                OnPropertyChanged("NoLeafletTextVisibility");
               
            }
        }

        /// <summary>
        /// Property for ActionIcon in keypad
        /// </summary>
        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {

                    _searchCommand = new RelayCommand(Search);
                    _searchCommand.Enabled = true;


                }
                return _searchCommand;
            }
            set
            {
                _searchCommand = value;
            }
        }
        #endregion

        #region Methods
        private void Search()
        {
        
        }   
        /// <summary>
        /// Method to fill list
        /// </summary>
        public void FillList()
        {
            ProgressBarVisibilty = Visibility.Visible;
            ConditionSearchModel = new ConditionSearchModel(this);
        }
        #endregion
    }
}
