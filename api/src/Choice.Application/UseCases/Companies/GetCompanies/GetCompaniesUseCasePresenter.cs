using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompanies
{
    public class GetCompaniesUseCasePresenter : IOutputPort
    {
        public IList<Company> Companies { get; set; }

        public void Ok(IList<Company> companies)
        {
            Companies = companies;
        }
    }
}
