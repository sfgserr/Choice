using Choice.Domain;
using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly ChoiceContext _context;

        public ClientRepository(ChoiceContext context)
        {
            _context = context;
        }

        public async Task<Client> Create(Client entity)
        {
            await _context.Clients.AddAsync(entity);

            return entity;
        }

        public async Task Delete(Client entity)
        {
            await _context
                .Database
                .ExecuteSqlRawAsync($"DELETE FROM Clients WHERE ClientId={entity.Id}");
        }

        public async Task<IList<Client>> Get()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetBy(Func<Client, bool> func)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => func(c));
        }

        public async Task<Client> Update(Client entity)
        {
            await Delete(entity);

            return await Create(entity);
        }
    }
}
