using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.UpdateCompany
{
    public interface IUpdateCompanyUseCase
    {
        Task Execute(Company company);

        void SetOutputPort(IOutputPort outputPort);
    }
}
