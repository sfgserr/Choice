using Choice.Domain;
using Choice.Domain.Models;

namespace Choice.Infrastructure.Repositories.Fakes
{
    public class CompanyRepositoryFake : IRepository<Company>
    {
        private readonly ChoiceContextFake _context;

        public CompanyRepositoryFake(ChoiceContextFake context)
        {
            _context = context;
        }

        public async Task<Company> Create(Company entity)
        {
            _context.Companies.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task Delete(Company entity)
        {
            Company entityToRemove = _context.Companies.FirstOrDefault(c => c.Id == entity.Id);

            if (entityToRemove is null)
            {
                return;
            }

            _context.Companies.Remove(entityToRemove);
            await Task.CompletedTask;
        }

        public async Task<IList<Company>> Get()
        {
            IList<Company> companies = _context.Companies.ToList();

            return await Task.FromResult(companies);
        }

        public async Task<Company> GetBy(Func<Company, bool> func)
        {
            Company company = _context.Companies.FirstOrDefault(c => func(c));

            return await Task.FromResult(company);
        }

        public async Task<Company> Update(Company entity)
        {
            Company oldCompany = _context.Companies.FirstOrDefault(c => c.Id == entity.Id);

            if (oldCompany != null)
            {
                _context.Companies.Remove(oldCompany);
            }

            _context.Companies.Add(entity);

            return await Task.FromResult(entity);
        }
    }
}
