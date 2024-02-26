
namespace Choice.Ordering.Application.UseCases.Enroll
{
    public interface IEnrollUseCase
    {
        Task Execute(int orderId);

        void SetOutputPort(IOutputPort outputPort);
    }
}
