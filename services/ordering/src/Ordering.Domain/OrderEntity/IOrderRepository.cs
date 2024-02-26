
namespace Choice.Ordering.Domain.OrderEntity
{
    public interface IOrderRepository
    {
        Task Add(Order order);

        Task<Order> GetOrders();

        Task<Order> GetOrder(int id);

        void Update(Order order);

        Task Delete(int id);
    }
}
