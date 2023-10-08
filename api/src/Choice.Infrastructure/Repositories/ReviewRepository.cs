using Choice.Domain;
using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly ChoiceContext _context;

        public ReviewRepository(ChoiceContext context)
        {
            _context = context;
        }

        public async Task<Review> Create(Review entity)
        {
            await _context.Reviews.AddAsync(entity);

            return entity;
        }

        public async Task Delete(Review entity)
        {
            await _context
                .Database
                .ExecuteSqlRawAsync($"DELETE FROM Reviews WHERE Id={entity.Id}");
        }

        public async Task<IList<Review>> Get()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetBy(Func<Review, bool> func)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => func(r));
        }

        public async Task<Review> Update(Review entity)
        {
            await Task.Run(() =>
            {
                _context.Reviews.Update(entity);
            });

            return entity;
        }
    }
}
