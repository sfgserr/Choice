using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;
using Microsoft.EntityFrameworkCore;

namespace Choice.ClientService.Infrastructure.Data.Repositories
{
    public sealed class ClientRepository : IClientRepository
    {
        private readonly ClientContext _context;

        public ClientRepository(ClientContext context)
        {
            _context = context;
        }

        public async Task Add(Client client)
        {
            await _context.Clients.AddAsync(client);
        }

        public async Task<Client> Get(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client> Get(string id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Guid == id);
        }

        public async Task<IList<Client>> GetAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<IList<OrderRequest>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        public void Update(Client client)
        {
            _context.Clients.Update(client);
        }

        public async Task Update(OrderRequest request)
        {
            await _context.Requests.AddAsync(request);
        }
    }
}
