namespace CacheCollector
{
    public interface ICustomerRepository
    {
        List<string> GetCustomers(int communityId);

        int Add(Customer customer);

        bool Update(Customer customer);

        bool Delete(int id);
    }
}


