using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompanies
{
    public interface IOutputPort
    {
        void Ok(IList<Company> companies);
    }
}
