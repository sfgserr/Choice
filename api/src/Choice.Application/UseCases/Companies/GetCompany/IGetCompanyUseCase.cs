using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.GetCompany
{
    public interface IGetCompanyUseCase
    {
        Task Execute(Func<Company, bool> func);

        void SetOutputPort(IOutputPort outputPort);
    }
}
