using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.CreateCompany
{
    public interface IOutputPort
    {
        void Ok(Company company);

        void Invalid();
    }
}
