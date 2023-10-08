using Choice.Domain;
using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly ChoiceContext _context;

        public CategoryRepository(ChoiceContext context)
        {
            _context = context;
        }

        public async Task<Category> Create(Category entity)
        {
            await _context.Categories.AddAsync(entity);

            return entity;
        }

        public async Task Delete(Category entity)
        {
            await _context
                .Database
                .ExecuteSqlRawAsync($"DELETE FROM Categories WHERE CategoryId={entity.Id}");
        }

        public async Task<IList<Category>> Get()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetBy(Func<Category, bool> func)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => func(c));
        }

        public async Task<Category> Update(Category entity)
        {
            await Delete(entity);

            return await Create(entity);
        }
    }
}
