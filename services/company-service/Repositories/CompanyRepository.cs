using Choice.CompanyService.Api.Entities;
using Choice.CompanyService.Api.Infrastructure.Data;
using Choice.CompanyService.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Choice.CompanyService.Api.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyContext _context;

        public CompanyRepository(CompanyContext context)
        {
            _context = context;
        }

        public async Task Add(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task<Company> Get(string guid)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Guid == guid);
        }

        public async Task<IList<Company>> GetAll()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<bool> Update(Company company)
        {
            _context.Companies.Update(company);

            int affections = await _context.SaveChangesAsync();

            return affections > 0;
        }

        public async Task<bool> Delete(int id)
        {
            int affections = await _context.Database.ExecuteSqlRawAsync("DELETE FROM Categories WHERE Id = @p0", id);

            return affections > 0;
        }
    }
}
