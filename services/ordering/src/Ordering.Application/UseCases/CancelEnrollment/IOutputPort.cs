using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.CancelEnrollment
{
    public interface IOutputPort
    {
        void Ok(Order order, string receiverId);

        void Invalid();

        void NotFound();
    }
}
