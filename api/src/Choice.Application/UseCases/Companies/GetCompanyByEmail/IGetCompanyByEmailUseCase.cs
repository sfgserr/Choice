
namespace Choice.Application.UseCases.Companies.GetCompanyByEmail
{
    public interface IGetCompanyByEmailUseCase
    {
        Task Execute(string phoneNumber);

        void SetOutputPort(IOutputPort outputPort);
    }
}
