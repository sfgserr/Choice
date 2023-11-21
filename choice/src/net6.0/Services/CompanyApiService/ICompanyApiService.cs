using Choice.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Services.CompanyApiService
{
    public interface ICompanyApiService
    {
        Task<Company> Post(Company company);

        Task<IList<Company>> GetAll();

        Task<Company> Get(int id);

        Task<Company> GetByPhone(string phone);

        Task<Company> Put(Company company);
    }
}
