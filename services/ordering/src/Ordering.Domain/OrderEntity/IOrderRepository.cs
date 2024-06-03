
namespace Choice.Ordering.Domain.OrderEntity
{
    public interface IOrderRepository
    {
        Task Add(Order order);

        Task<IList<Order>> GetOrders();

        Task<IList<Order>> GetOrders(string guid1, string guid2);

        Task<Order> GetOrder(int id);

        Task<Order> GetOrder(int requestId, string companyId);

        Task<Order> GetOrder(string clientId, string companyId);

        void Update(Order order);
    }
}
