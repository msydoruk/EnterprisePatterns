using System.Collections.Concurrent;

namespace CacheAccessor
{
    public class Cache : ICache
    {
        private readonly ConcurrentDictionary<string, IDatabase> databases = new();
        private readonly Func<IDatabase> databaseGenerator;

        public Cache(Func<IDatabase> databaseGenerator)
        {
            this.databaseGenerator = databaseGenerator;
        }

        public IDatabase GetDatabaseCache(string cacheConnectionString)
        {
            return databases.GetOrAdd(cacheConnectionString, databaseGenerator());
        }
    }
}
