using Microsoft.Extensions.Caching.Memory;

namespace CacheCollector
{
    public class CustomerAccessor : ICustomerRepository
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerSettings customerSettings;
        private readonly ICacheCollector cacheCollector;
        private const string GetCustomersCacheKey = "GetCustomerCacheKey";

        public CustomerAccessor(
            ICustomerRepository customerRepository,
            IMemoryCache memoryCache,
            ICustomerSettings customerSettings,
            ICacheCollector cacheCollector)
        {
            this.customerRepository = customerRepository;
            this.memoryCache = memoryCache;
            this.customerSettings = customerSettings;
            this.cacheCollector = cacheCollector;

            PrePopulateCustomers();
        }

        private void PrePopulateCustomers()
        {
            List<int> communityIds = customerSettings.GetPrePopulateCustomerCommunities();
            foreach (var communityId in communityIds)
            {
                var cacheKey = GetCustomersCacheKey + communityId;
                if (!memoryCache.TryGetValue(cacheKey, out _))
                {
                    memoryCache.Set(cacheKey, customerRepository.GetCustomers(communityId));
                }
            }
        }

        public List<string> GetCustomers(int communityId)
        {
            var cacheKey = GetCustomersCacheKey + communityId;
            if (!memoryCache.TryGetValue(cacheKey, out List<string> customers))
            {
                customers = customerRepository.GetCustomers(communityId);
                memoryCache.Set(cacheKey, customers);
            }

            return customers;
        }

        public int Add(Customer customer)
        {
            //add logic
            throw new NotImplementedException();
        }

        public bool Update(Customer customer)
        {
            //update logic
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            //delete logic

            cacheCollector.Collect();

            throw new NotImplementedException();
        }
    }
}



