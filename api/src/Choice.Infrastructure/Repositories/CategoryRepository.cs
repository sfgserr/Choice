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
                .ExecuteSqlRawAsync($"DELETE FROM Categories WHERE Id={entity.Id}");
        }

        public async Task<IList<Category>> Get()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetBy(Func<Category, bool> func)
        {
            List<Category> categories = await _context.Categories.ToListAsync();

            return categories.FirstOrDefault(c => func(c));
        }

        public async Task<Category> Update(Category entity)
        {
            await Task.Run(() =>
            {
                _context.Categories.Update(entity);
            });

            return entity;
        }
    }
}
