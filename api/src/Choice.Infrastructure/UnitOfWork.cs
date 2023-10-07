using Choice.Application.Services;

namespace Choice.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChoiceContext _context;

        public UnitOfWork(ChoiceContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
