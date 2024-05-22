
namespace Choice.ClientService.Application.UseCases.GetRequest
{
    public interface IGetRequestUseCase
    {
        Task Execute(int id);

        void SetOutputPort(IOutputPort outputPort);
    }
}
