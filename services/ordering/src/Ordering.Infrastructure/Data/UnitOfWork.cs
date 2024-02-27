using Choice.Ordering.Application.Services;

namespace Choice.Ordering.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderingContext _context;

        public UnitOfWork(OrderingContext context)
        {
            _context = context;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
