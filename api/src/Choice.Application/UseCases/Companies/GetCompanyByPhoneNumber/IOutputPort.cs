using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompanyByPhoneNumber
{
    public interface IOutputPort
    {
        void Ok(Company company);

        void NotFound();
    }
}
