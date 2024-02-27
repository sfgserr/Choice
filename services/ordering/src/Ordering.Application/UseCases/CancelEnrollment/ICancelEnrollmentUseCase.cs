using Choice.Ordering.Application.UseCases.CancelEnrollment;

namespace Ordering.Application.UseCases.CancelEnrollment
{
    public interface ICancelEnrollmentUseCase
    {
        Task Execute(int orderId);

        void SetOutputPort(IOutputPort outputPort);
    }
}
