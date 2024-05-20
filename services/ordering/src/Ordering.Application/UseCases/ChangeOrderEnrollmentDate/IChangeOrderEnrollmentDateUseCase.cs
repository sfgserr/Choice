
namespace Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentDate
{
    public interface IChangeOrderEnrollmentDateUseCase
    {
        Task Execute(int orderId, DateTime? newTime);

        void SetOutputPort(IOutputPort outputPort);
    }
}
