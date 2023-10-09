using Choice.Domain;
using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure.Repositories
{
    public class OrderMessageRepository : IRepository<OrderMessage>
    {
        private readonly ChoiceContext _context;

        public OrderMessageRepository(ChoiceContext context)
        {
            _context = context;
        }

        public async Task<OrderMessage> Create(OrderMessage entity)
        {
            await _context.OrderMessages.AddAsync(entity);

            return entity;
        }

        public async Task Delete(OrderMessage entity)
        {
            await _context
                .Database
                .ExecuteSqlRawAsync($"DELETE FROM OrderMessages WHERE Id={entity.Id}");
        }

        public async Task<IList<OrderMessage>> Get()
        {
            return await _context.OrderMessages.ToListAsync();
        }

        public async Task<OrderMessage> GetBy(Func<OrderMessage, bool> func)
        {
            List<OrderMessage> orderMessages = await _context.OrderMessages.ToListAsync();

            return orderMessages.FirstOrDefault(c => func(c));
        }

        public async Task<OrderMessage> Update(OrderMessage entity)
        {
            await Task.Run(() =>
            {
                _context.OrderMessages.Update(entity);
            });

            return entity;
        }
    }
}
