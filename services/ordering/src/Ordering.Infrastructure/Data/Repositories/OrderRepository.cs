using Choice.Ordering.Domain.OrderEntity;
using Microsoft.EntityFrameworkCore;

namespace Choice.Ordering.Infrastructure.Data.Repositories
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly OrderingContext _context;

        public OrderRepository(OrderingContext context)
        {
            _context = context;
        }

        public async Task Add(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IList<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
