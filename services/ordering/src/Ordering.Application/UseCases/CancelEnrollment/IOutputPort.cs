using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.CancelEnrollment
{
    public interface IOutputPort
    {
        void Ok(Order order);

        void Invalid();

        void NotFound();
    }
}
