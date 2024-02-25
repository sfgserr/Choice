
namespace Choice.Ordering.Application.UseCases.ChangeEnrollmentStatus
{
    public interface IChangeEnrollmentStatusUseCase
    {
        Task Execute(int orderId);

        void SetOutputPort(IOutputPort outputPort);
    }
}
