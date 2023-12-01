
namespace Choice.Application.UseCases.Companies.GetCompany
{
    public interface IGetCompanyUseCase
    {
        Task Execute(int id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
