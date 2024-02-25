using Choice.Ordering.Domain.OrderAggregate;

namespace Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentTime
{
    public interface IOutputPort
    {
        void Ok(Order order);

        void Invalid();

        void NotFound();
    }
}
