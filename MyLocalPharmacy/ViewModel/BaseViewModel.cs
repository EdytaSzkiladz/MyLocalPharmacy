using MyLocalPharmacy.Contract;
using MyLocalPharmacy.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace MyLocalPharmacy.ViewModel
{
    [DataContract]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Declarations
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region INotifyImplementation


        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INavigationService
        protected T GetService<T>() where T : class
        {
            if (typeof(T) == typeof(INavigationService))
            {
                return new PageNavigationService() as T;
            }

            return null;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to pass parameters in Querystring
        /// </summary>
        /// <param name="dictionary"></param>
        public virtual void Initialize(IDictionary<string, string> dictionary)
        {

        } 
        #endregion
    }
    
}
