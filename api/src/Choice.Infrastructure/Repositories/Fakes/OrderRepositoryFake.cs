using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Infrastructure.Repositories.Fakes
{
    public class OrderRepositoryFake : IRepository<Order>
    {
        private readonly ChoiceContextFake _context;

        public OrderRepositoryFake(ChoiceContextFake context)
        {
            _context = context;
        }

        public async Task<Order> Create(Order entity)
        {
            _context.Orders.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task Delete(Order entity)
        {
            Order entityToRemove = _context.Orders.FirstOrDefault(c => c.Id == entity.Id);

            if (entityToRemove is null)
            {
                return;
            }

            _context.Orders.Remove(entityToRemove);
            await Task.CompletedTask;
        }

        public async Task<IList<Order>> Get()
        {
            IList<Order> orders = _context.Orders.ToList();

            return await Task.FromResult(orders);
        }

        public async Task<Order> GetBy(Func<Order, bool> func)
        {
            Order orders = _context.Orders.FirstOrDefault(c => func(c));

            return await Task.FromResult(orders);
        }

        public async Task<Order> Update(Order entity)
        {
            Order oldOrder = _context.Orders.FirstOrDefault(c => c.Id == entity.Id);

            if (oldOrder != null)
            {
                _context.Orders.Remove(oldOrder);
            }

            _context.Orders.Add(entity);

            return await Task.FromResult(entity);
        }
    }
}
