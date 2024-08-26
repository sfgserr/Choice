using Microsoft.EntityFrameworkCore;
using Users.Domain.OrderResponses;

namespace Users.Infrastructure.Data.Domain.OrderResponses
{
    internal class OrderResponseRepository : IOrderResponseRepository
    {
        private readonly UsersContext _usersContext;

        internal OrderResponseRepository(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task Add(OrderResponse orderResponse)
        {
            await _usersContext.OrderResponses.AddAsync(orderResponse);
        }

        public async Task<IList<OrderResponse>> GetAll()
        {
            return await _usersContext.OrderResponses.ToListAsync();    
        }
    }
}
