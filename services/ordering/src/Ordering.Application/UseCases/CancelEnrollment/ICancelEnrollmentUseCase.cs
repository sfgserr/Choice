
namespace Choice.Ordering.Application.UseCases.CancelEnrollment
{
    public interface ICancelEnrollmentUseCase
    {
        Task Execute(int orderId);

        void SetOutputPort(IOutputPort outputPort);
    }
}
