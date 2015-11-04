using Microsoft.Phone.Controls;
using MyLocalPharmacy.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MyLocalPharmacy.Service
{
    public class PageNavigationService : INavigationService
    {
        public bool Navigate(string uri)
        {
            return this.Navigate(uri, null);
        }

        public bool Navigate(string uri, IDictionary<string, string> parameters)
        {
            PhoneApplicationFrame frame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (frame == null)
            {
                return false;
            }

            StringBuilder uriBuilder = new StringBuilder();
            uriBuilder.Append(uri);
            if (parameters != null && parameters.Count > 0)
            {
                uriBuilder.Append("?");
                bool prependAmp = false;
                foreach (KeyValuePair<string, string> parameterPair in parameters)
                {
                    if (prependAmp)
                    {
                        uriBuilder.Append("&");
                    }
                    uriBuilder.AppendFormat("{0}={1}", parameterPair.Key, parameterPair.Value);
                    prependAmp = true;
                }
            }

            uri = uriBuilder.ToString();
            return frame.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
        }
    }
}
