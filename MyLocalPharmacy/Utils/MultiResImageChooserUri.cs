using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MyLocalPharmacy.Utils
{
    public class MultiResImageChooserUri
    { 
        /// <summary>
        /// Property for background image based on resolution
        /// </summary>
        public Uri Background  
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD720p:
                        return new Uri("/Assets/Images/720p/Background_720p.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("/Assets/Images/WXGA/Background_WXGA.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("/Assets/Images/WVGA/Background_WVGA.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }
        /// <summary>
        /// Property for static splash image based on resolution
        /// </summary>
        public Uri StaticSplash
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.WVGA:
                        return new Uri("/Assets/Images/WVGA/StaticSplashWVGA.jpg", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("/Assets/Images/WXGA/StaticSplashWXGA.jpg", UriKind.Relative);
                    case Resolutions.HD720p:
                        return new Uri("/Assets/Images/720p/StaticSplash720p.jpg", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }
    }
}
