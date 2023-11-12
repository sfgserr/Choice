using Choice.Domain;
using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly ChoiceContext _context;

        public CompanyRepository(ChoiceContext context)
        {
            _context = context;
        }

        public async Task<Company> Create(Company entity)
        {
            entity.Categories.ForEach(e => _context.Categories.Attach(e));

            await _context.Companies.AddAsync(entity);

            return entity;
        }

        public async Task Delete(Company entity)
        {
            await _context
                .Database
                .ExecuteSqlRawAsync($"DELETE FROM Companies WHERE Id={entity.Id}");
        }

        public async Task<IList<Company>> Get()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetBy(Func<Company, bool> func)
        {
            List<Company> companies = await _context.Companies.ToListAsync();

            return companies.FirstOrDefault(c => func(c));
        }

        public async Task<Company> Update(Company entity)
        {
            await Task.Run(() =>
            {
                _context.Companies.Update(entity);
            });

            return entity;
        }
    }
}
