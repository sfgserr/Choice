using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompanyByEmail
{
    public interface IOutputPort
    {
        void Ok(Company company);

        void NotFound();
    }
}
