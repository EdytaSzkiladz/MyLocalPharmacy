using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Contract
{
    public interface INavigationService
    {
        bool Navigate(string uri);
        bool Navigate(string uri, IDictionary<string, string> parameters);
    }
}
