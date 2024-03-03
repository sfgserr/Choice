using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Infrastructure.Data.Repositories
{
    public sealed class ClientRepositoryFake : IClientRepository
    {
        private readonly ClientContextFake _context;

        public ClientRepositoryFake(ClientContextFake context)
        {
            _context = context;
        }

        public async Task Add(Client client)
        {
            await Task.Run(() => _context.Clients.Add(client));
        }

        public async Task<Client> Get(int id)
        {
            return await Task.Run(() => _context.Clients.FirstOrDefault(c => c.Id == id));
        }

        public async Task<Client> Get(string id)
        {
            return await Task.Run(() => _context.Clients.FirstOrDefault(c => c.Guid == id));
        }

        public async Task<IList<Client>> GetAll()
        {
            return await Task.Run(() => _context.Clients);
        }

        public async Task<IList<OrderRequest>> GetRequests()
        {
            return await Task.Run(() => _context.Requests);
        }

        public void Update(Client client)
        {
            Client clientToRemove = _context.Clients.FirstOrDefault(c => c.Id == client.Id)!;
            
            _context.Clients.Remove(clientToRemove);

            _context.Clients.Add(client);
        }

        public async Task Update(OrderRequest request)
        {
            await Task.Run(() => _context.Requests.Add(request));
        }
    }
}
