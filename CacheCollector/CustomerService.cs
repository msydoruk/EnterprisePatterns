namespace CacheCollector
{
    public class CustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public List<string> GetCustomers(int communityId)
        {
            return customerRepository.GetCustomers(communityId);
        }
    }
}

