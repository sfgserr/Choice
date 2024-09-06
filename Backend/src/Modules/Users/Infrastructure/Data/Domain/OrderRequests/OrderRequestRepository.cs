using Microsoft.EntityFrameworkCore;
using Users.Domain.OrderRequests;

namespace Users.Infrastructure.Data.Domain.OrderRequests
{
    internal class OrderRequestRepository : IOrderRequestRepository
    {
        private readonly UsersContext _usersContext;

        internal OrderRequestRepository(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task Add(OrderRequest orderRequest)
        {
            await _usersContext.OrderRequests.AddAsync(orderRequest);
        }

        public async Task<IList<OrderRequest>> GetAll()
        {
            return await _usersContext.OrderRequests.ToListAsync();
        }

        public async Task<OrderRequest> Get(OrderRequestId id)
        {
            return await _usersContext.OrderRequests.FindAsync(id);
        }
    }
}
