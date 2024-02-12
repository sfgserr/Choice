namespace Choice.Authentication.Infrastructure.Data
{
    public class UnitOfWork
    {
        private readonly UserContext _context;

        public UnitOfWork(UserContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
