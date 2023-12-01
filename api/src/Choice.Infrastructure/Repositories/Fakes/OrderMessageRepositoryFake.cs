using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Infrastructure.Repositories.Fakes
{
    public class OrderMessageRepositoryFake : IRepository<OrderMessage>
    {
        private readonly ChoiceContextFake _context;

        public OrderMessageRepositoryFake(ChoiceContextFake context)
        {
            _context = context;
        }

        public async Task<OrderMessage> Create(OrderMessage entity)
        {
            _context.OrderMessages.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task Delete(OrderMessage entity)
        {
            OrderMessage entityToRemove = _context.OrderMessages.FirstOrDefault(c => c.Id == entity.Id);

            if (entityToRemove is null)
            {
                return;
            }

            _context.OrderMessages.Remove(entityToRemove);
            await Task.CompletedTask;
        }

        public async Task<IList<OrderMessage>> Get()
        {
            IList<OrderMessage> orderMessages = _context.OrderMessages.ToList();

            return await Task.FromResult(orderMessages);
        }

        public async Task<OrderMessage> GetBy(Func<OrderMessage, bool> func)
        {
            OrderMessage orderMessage = _context.OrderMessages.FirstOrDefault(c => func(c));

            return await Task.FromResult(orderMessage);
        }

        public async Task<OrderMessage> Update(OrderMessage entity)
        {
            OrderMessage oldOrderMessage = _context.OrderMessages.FirstOrDefault(c => c.Id == entity.Id);

            if (oldOrderMessage != null)
            {
                _context.OrderMessages.Remove(oldOrderMessage);
            }

            _context.OrderMessages.Add(entity);

            return await Task.FromResult(entity);
        }
    }
}
