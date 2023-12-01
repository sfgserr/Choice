
namespace Choice.Application.UseCases.Companies.GetCompanies
{
    public interface IGetCompaniesUseCase
    {
        Task Execute();

        void SetOutputPort(IOutputPort outputPort);
    }
}
