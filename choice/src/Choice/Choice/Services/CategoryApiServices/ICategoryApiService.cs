using Choice.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.CategoryApiServices
{
    public interface ICategoryApiService
    {
        Task<IList<Category>> GetAll();

        Task<Category> Put(Category client);
    }
}
