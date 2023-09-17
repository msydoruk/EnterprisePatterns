namespace CacheAccessor
{
    public interface ICache
    {
        IDatabase GetDatabaseCache(string cacheConnectionString);
    }
}
