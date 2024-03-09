using Choice.Application.Services;

namespace Choice.Infrastructure.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IContext _context;

        public UnitOfWork(IContext context)
        {
            _context = context;
        }

        public async Task SaveChanges() =>
            await _context.SaveEntities();
    }
}
