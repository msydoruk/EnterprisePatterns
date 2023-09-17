namespace CacheCollector
{
    public class CustomerSettings : ICustomerSettings
    {
        public List<int> GetPrePopulateCustomerCommunities()
        {
            return new List<int> { 1, 2 };
        }
    }
}

