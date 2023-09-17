using Microsoft.Extensions.Caching.Memory;

namespace CacheCollector
{
    public class CacheCollector : ICacheCollector
    {
        private readonly IMemoryCache memoryCache;

        public CacheCollector(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void Collect()
        {
            throw new NotImplementedException();
        }
    }
}


