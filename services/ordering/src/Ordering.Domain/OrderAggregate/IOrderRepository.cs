
namespace Choice.Ordering.Domain.OrderAggregate
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
