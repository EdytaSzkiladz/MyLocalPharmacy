using System.IO.IsolatedStorage;
using MyLocalPharmacy.Contract;
using Microsoft.Phone.Shell;

namespace MyLocalPharmacy.Utils
{
    public class PersistentDataStorage<T>:IDataStorage<T>
    {
        public bool Backup(string token, T value)
        {
            if (null == value)
                return false;

            IsolatedStorageSettings store = IsolatedStorageSettings.ApplicationSettings;
            if (store.Contains(token))
                store[token] = value;
            else
                store.Add(token, value);

            store.Save();
            
            return true;
        }

        public T Restore<T>(string token)
        {
            IsolatedStorageSettings store = IsolatedStorageSettings.ApplicationSettings;
            if (!store.Contains(token))
                return default(T);

            return (T)store[token];
        }

        public void Remove(string token)
        {
            IsolatedStorageSettings store = IsolatedStorageSettings.ApplicationSettings;
            if (store.Contains(token))
                store.Remove(token);
        }
        public void Clear()
        {
            PhoneApplicationService.Current.State.Clear();
        }
    }
}
