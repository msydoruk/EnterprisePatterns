namespace CacheAccessor
{
    public class CacheAccessor
    {
        private readonly ICache databaseCache;

        public CacheAccessor(ICache databaseCache)
        {
            this.databaseCache = databaseCache;
        }

        public List<string> Read(string cacheConnectionString, string query)
        {
            var database = databaseCache.GetDatabaseCache(cacheConnectionString);

            return database.Select(query);
        }
    }
}
