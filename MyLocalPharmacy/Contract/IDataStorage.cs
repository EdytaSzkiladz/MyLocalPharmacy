namespace MyLocalPharmacy.Contract
{
    public interface IDataStorage<T>
    {
        bool Backup(string token, T value);
        T Restore<T>(string token);
        void Remove(string token);
    }
}
