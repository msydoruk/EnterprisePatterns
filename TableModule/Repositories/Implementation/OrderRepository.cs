using TableModule.Repositories.Entities;
using TableModule.Repositories.Interfaces;

namespace TableModule.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        public Order GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
