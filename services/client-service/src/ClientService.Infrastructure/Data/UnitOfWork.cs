using Choice.ClientService.Application.Services;

namespace Choice.ClientService.Infrastructure.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ClientContext _context;

        public UnitOfWork(ClientContext context)
        {
            _context = context;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
