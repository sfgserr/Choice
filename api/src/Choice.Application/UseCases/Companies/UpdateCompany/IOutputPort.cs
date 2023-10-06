using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.UpdateCompany
{
    public interface IOutputPort
    {
        void Ok(Company company);
    }
}
