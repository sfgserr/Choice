using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompany
{
    public interface IOutputPort
    {
        void Ok(Company company);

        void NotFound();
    }
}
