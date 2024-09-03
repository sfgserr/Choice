using BuildingBlocks.Domain;

namespace Users.Domain.Users.Companies
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> Get(CompanyId companyId);
    }
}
