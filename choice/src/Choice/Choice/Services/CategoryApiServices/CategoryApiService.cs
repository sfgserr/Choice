using Choice.Domain.Models;
using Choice.Services.ApiServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.CategoryApiServices
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly IApiService<Category> _categoryService;

        public CategoryApiService(IApiService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IList<Category>> GetAll()
        {
            return await _categoryService.GetAll("Category/Get");
        }

        public async Task<Category> Put(Category category)
        {
            return await _categoryService.Put("Category/Update", category);
        }
    }
}
