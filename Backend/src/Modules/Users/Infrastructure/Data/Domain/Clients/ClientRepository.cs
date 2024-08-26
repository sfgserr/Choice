using Microsoft.EntityFrameworkCore;
using Users.Domain.Users.Clients;

namespace Users.Infrastructure.Data.Domain.Clients
{
    internal class ClientRepository : IClientRepository
    {
        private readonly UsersContext _usersContext;

        internal ClientRepository(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task Add(Client client)
        {
            await _usersContext.Clients.AddAsync(client);
        }

        public async Task<Client> Get(ClientId id)
        {
            return await _usersContext.Clients.FindAsync(id);
        }

        public async Task<IList<Client>> GetAll()
        {
            return await _usersContext.Clients.ToListAsync();
        }
    }
}
