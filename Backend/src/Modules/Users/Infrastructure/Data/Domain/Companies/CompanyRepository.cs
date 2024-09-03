using Microsoft.EntityFrameworkCore;
using Users.Domain.Users.Companies;

namespace Users.Infrastructure.Data.Domain.Companies
{
    internal class CompanyRepository : ICompanyRepository
    {
        private readonly UsersContext _usersContext;

        internal CompanyRepository(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task Add(Company company)
        {
            await _usersContext.Companies.AddAsync(company);
        }

        public async Task<Company> Get(CompanyId id)
        {
            return await _usersContext.Companies.FindAsync(id);
        }
        
        public async Task<IList<Company>> GetAll()
        {
            return await _usersContext.Companies.ToListAsync();
        }
    }
}
