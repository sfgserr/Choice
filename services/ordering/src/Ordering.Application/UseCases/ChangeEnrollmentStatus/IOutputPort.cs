using Choice.Ordering.Domain.OrderAggregate;

namespace Choice.Ordering.Application.UseCases.ChangeEnrollmentStatus
{
    public interface IOutputPort
    {
        void Ok(Order order);

        void NotFound();

        void Invalid();
    }
}
