using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Infrastructure.Data.Repositories
{
    public sealed class OrderRepositoryFake : IOrderRepository
    {
        private readonly OrderingContextFake _context;

        public OrderRepositoryFake(OrderingContextFake context)
        {
            _context = context;
        }

        public async Task Add(Order order)
        {
            _context.Orders.Add(order);
            await Task.CompletedTask;
        }

        public async Task<Order> GetOrder(int id)
        {
            return await Task.Run(() => _context.Orders.FirstOrDefault(o => o.Id == id));
        }

        public async Task<Order> GetOrder(int requestId, string companyId)
        {
            return await Task.Run(() => 
                _context.Orders.FirstOrDefault(o => o.OrderRequestId == requestId && o.CompanyId == companyId));
        }

        public async Task<IList<Order>> GetOrders()
        {
            return await Task.Run(() => _context.Orders);
        }

        public async Task<IList<Order>> GetOrders(string guid1, string guid2)
        {
            return await Task.Run(() => _context.Orders.Where(o => (o.CompanyId == guid1 && o.ClientId == guid2) ||
                                                    (o.ClientId == guid1 && o.CompanyId == guid2)).ToList());
        }

        public void Update(Order order)
        {
            Order orderToRemove = _context.Orders.FirstOrDefault(o => o.Id == order.Id);

            _context.Orders.Remove(orderToRemove);

            _context.Orders.Add(order);
        }
    }
}
