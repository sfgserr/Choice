using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Infrastructure.Repositories.Fakes
{
    public class CategoryRepositoryFake : IRepository<Category>
    {
        private readonly ChoiceContextFake _context;

        public CategoryRepositoryFake(ChoiceContextFake context)
        {
            _context = context;
        }

        public async Task<Category> Create(Category entity)
        {
            _context.Categories.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task Delete(Category entity)
        {
            Category entityToRemove = _context.Categories.FirstOrDefault(c => c.Id == entity.Id);

            if (entityToRemove is null)
            {
                return;
            }

            _context.Categories.Remove(entityToRemove);
            await Task.CompletedTask;
        }

        public async Task<IList<Category>> Get()
        {
            IList<Category> categories = _context.Categories.ToList();

            return await Task.FromResult(categories);
        }

        public async Task<Category> GetBy(Func<Category, bool> func)
        {
            Category category = _context.Categories.FirstOrDefault(c => func(c));

            return await Task.FromResult(category);   
        }

        public async Task<Category> Update(Category entity)
        {
            Category oldCategory = _context.Categories.FirstOrDefault(c => c.Id == entity.Id);

            if (oldCategory != null) 
            {
                _context.Categories.Remove(oldCategory);
            }

            _context.Categories.Add(entity);

            return await Task.FromResult(entity);
        }
    }
}
