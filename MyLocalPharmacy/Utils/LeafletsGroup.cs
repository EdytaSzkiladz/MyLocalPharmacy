using Microsoft.Phone.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLocalPharmacy.Utils
{
    public class LeafletsGroup<T> : List<T>
    {
        /// <summary>
        /// The delegate that is used to get the key information.
        /// </summary>
        /// <param name="item">An object of type T</param>
        /// <returns>The key value to use for this object</returns>
        public delegate string GetKeyDelegate(T item);

        /// <summary>
        /// The Key of this group.
        /// </summary>
        public string Key { get; private set; }
        public LeafletsGroup()
        { }
    
        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="key">The key for this group.</param>
        public LeafletsGroup(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Create a list of LeafletsGroup<T> with keys set by a SortedLocaleGrouping.
        /// </summary>
        /// <param name="slg">The </param>
        /// <returns>Theitems source for a LongListSelector</returns>
        private static List<LeafletsGroup<T>> CreateGroups(SortedLocaleGrouping slg)
        {
            List<LeafletsGroup<T>> list = new List<LeafletsGroup<T>>();

            foreach (string key in slg.GroupDisplayNames)
            {
                list.Add(new LeafletsGroup<T>(key));
            }

            return list;
        }

        /// <summary>
        /// Create a list of LeafletsGroup<T> with keys set by a SortedLocaleGrouping.
        /// </summary>
        /// <param name="items">The items to place in the groups.</param>
        /// <param name="ci">The CultureInfo to group and sort by.</param>
        /// <param name="getKey">A delegate to get the key from an item.</param>
        /// <param name="sort">Will sort the data if true.</param>
        /// <returns>An items source for a LongListSelector</returns>
        public static List<LeafletsGroup<T>> CreateGroups(IEnumerable<T> items, CultureInfo ci, GetKeyDelegate getKey, bool sort)
        {
            SortedLocaleGrouping slg = new SortedLocaleGrouping(ci);
            List<LeafletsGroup<T>> list = CreateGroups(slg);

            foreach (T item in items)
            {
                int index = 0;
                index = slg.GetGroupIndex(getKey(item));
                if (index >= 0 && index < list.Count)
                {
                    list[index].Add(item);
                }
            }

            if (sort)
            {
                foreach (LeafletsGroup<T> group in list)
                {
                    group.Sort((c0, c1) => { return ci.CompareInfo.Compare(getKey(c0), getKey(c1)); });
                }
            }

            return list;
        }

    }
}
