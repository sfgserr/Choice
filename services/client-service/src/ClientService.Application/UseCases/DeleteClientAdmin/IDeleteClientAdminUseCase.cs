
namespace ClientService.Application.UseCases.DeleteClientAdmin
{
    public interface IDeleteClientAdminUseCase
    {
        Task Execute(string id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
