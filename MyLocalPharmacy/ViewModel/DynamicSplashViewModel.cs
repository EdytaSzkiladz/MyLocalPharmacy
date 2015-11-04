using Microsoft.Phone.Controls;
using MyLocalPharmacy.Model;
using MyLocalPharmacy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyLocalPharmacy.ViewModel
{
    public class DynamicSplashViewModel:BaseViewModel
    {
        #region Declaration
        DynamicSplashModel objDynamicSplash;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DynamicSplashViewModel()
        {
            objDynamicSplash = new DynamicSplashModel(this);

        }
        #endregion

        #region Properties
        /// <summary>
        /// Property for Image Url
        /// </summary>
        private ImageSource _imageUrl;
        public ImageSource DynamicSplashImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged("DynamicSplashImageUrl");  
            }
        }
        #endregion

    }
}
