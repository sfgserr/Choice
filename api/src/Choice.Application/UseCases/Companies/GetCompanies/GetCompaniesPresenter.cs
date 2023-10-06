using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompanies
{
    public class GetCompaniesPresenter : IOutputPort
    {
        public IList<Company> Companies { get; set; } = new List<Company>();

        public void Ok(IList<Company> companies)
        {
            Companies = companies;
        }
    }
}
