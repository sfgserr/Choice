
namespace Choice.ClientService.Application.UseCases.ChangeUserDataAdmin
{
    public interface IChangeUserDataAdminUseCase
    {
        Task Execute(string id, string name, string surname, string email, string phoneNumber, string city, string street);

        void SetOutputPort(IOutputPort outputPort);
    }
}
