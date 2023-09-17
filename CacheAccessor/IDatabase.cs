namespace CacheAccessor
{
    public interface IDatabase
    {
        List<string> Select(string query);

        void Insert(string item);
    }
}
