
namespace Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentTime
{
    public interface IChangeOrderEnrollmentTimeUseCase
    {
        Task Execute(int orderId, DateTime newTime);

        void SetOutputPort(IOutputPort outputPort);
    }
}
