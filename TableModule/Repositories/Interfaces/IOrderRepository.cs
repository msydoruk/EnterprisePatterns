using TableModule.Repositories.Entities;

namespace TableModule.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);

        List<Order> GetOrders();
    }
}
