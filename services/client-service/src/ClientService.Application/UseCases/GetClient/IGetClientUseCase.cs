
namespace Choice.ClientService.Application.UseCases.GetClient
{
    public interface IGetClientUseCase
    {
        void SetOutputPort(IOutputPort outputPort);

        Task Execute();
    }
}
