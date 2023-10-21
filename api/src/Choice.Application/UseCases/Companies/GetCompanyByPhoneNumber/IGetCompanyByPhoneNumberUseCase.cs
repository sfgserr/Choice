
namespace Choice.Application.UseCases.Companies.GetCompanyByPhoneNumber
{
    public interface IGetCompanyByPhoneNumberUseCase
    {
        Task Execute(string phoneNumber);

        void SetOutputPort(IOutputPort outputPort);
    }
}
