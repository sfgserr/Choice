using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Infrastructure.Repositories.Fakes
{
    public class ClientRepositoryFake : IRepository<Client>
    {
        private readonly ChoiceContextFake _context;

        public ClientRepositoryFake(ChoiceContextFake context)
        {
            _context = context;
        }

        public async Task<Client> Create(Client entity)
        {
            _context.Clients.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task Delete(Client entity)
        {
            Client entityToRemove = _context.Clients.FirstOrDefault(c => c.Id == entity.Id);

            if (entityToRemove is null)
            {
                return;
            }

            _context.Clients.Remove(entityToRemove);
            await Task.CompletedTask;
        }

        public async Task<IList<Client>> Get()
        {
            IList<Client> clients = _context.Clients.ToList();

            return await Task.FromResult(clients);
        }

        public async Task<Client> GetBy(Func<Client, bool> func)
        {
            Client client = _context.Clients.FirstOrDefault(c => func(c));

            return await Task.FromResult(client);
        }

        public async Task<Client> Update(Client entity)
        {
            Client oldClient = _context.Clients.FirstOrDefault(c => c.Id == entity.Id);

            if (oldClient != null)
            {
                _context.Clients.Remove(oldClient);
            }

            _context.Clients.Add(entity);

            return await Task.FromResult(entity);
        }
    }
}
