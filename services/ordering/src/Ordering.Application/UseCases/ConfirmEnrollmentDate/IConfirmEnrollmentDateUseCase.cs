
namespace Choice.Ordering.Application.UseCases.ConfirmEnrollmentDate
{
    public interface IConfirmEnrollmentDateUseCase
    {
        Task Execute(int orderId);

        void SetOutputPort(IOutputPort outputPort);
    }
}
