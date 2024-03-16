using Choice.CompanyService.Api.Entities;

namespace Choice.CompanyService.Api.Repositories
{
    public interface ICompanyRepository
    {
        Task Add(Company company);

        Task<Company> Get(string guid);

        Task<IList<Company>> GetAll();

        Task<bool> Update(Company company);

        Task<bool> Delete(int id);
    }
}
