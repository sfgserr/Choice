using Choice.CategoryService.Api.Entities;

namespace Choice.CategoryService.Api.Repositories
{
    public interface ICategoryRepository
    {
        Task Add(Category category);

        Task<Category> Get(int id);

        Task<IList<Category>> GetAll();

        Task<bool> Update(Category category);

        Task Delete(int id);
    }
}
