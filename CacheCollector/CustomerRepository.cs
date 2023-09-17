using AutoFixture;

namespace CacheCollector
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<string> GetCustomers(int communityId)
        {
            List<string> customers = new Fixture().Create<List<string>>();

            return customers;
        }

        public int Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}


