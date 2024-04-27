
namespace Choice.ClientService.Application.UseCases.ChangeUserData
{
    public interface IChangeUserDataUseCase
    {
        Task Execute(string name, string surname, string email, string phoneNumber, string city, string street);

        void SetOutputPort(IOutputPort outputPort);
    }
}
