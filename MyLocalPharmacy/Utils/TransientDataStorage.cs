using Microsoft.Phone.Shell;
using MyLocalPharmacy.Contract;

namespace MyLocalPharmacy.Utils
{
    public class TransientDataStorage<T>:IDataStorage<T>
    {
        public bool Backup(string token, T value)
        {
            if (null == value)
                return false;

            var store = PhoneApplicationService.Current.State;
            if (store.ContainsKey(token))
                store[token] = value;
            else
                store.Add(token, value);

            return true;
        }

        public T Restore<T>(string token)
        {
            var store = PhoneApplicationService.Current.State;
            if (!store.ContainsKey(token))
                return default(T);

            return (T)store[token];
        }

        public void Remove(string token)
        {
            var store = PhoneApplicationService.Current.State;
            if (store.ContainsKey(token))
                store.Remove(token);
        }

        public void Clear()
        {
            PhoneApplicationService.Current.State.Clear();
        }
    }
}
