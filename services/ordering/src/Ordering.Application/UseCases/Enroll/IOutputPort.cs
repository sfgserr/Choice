using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.Enroll
{
    public interface IOutputPort
    {
        void Ok(Order order);

        void NotFound();

        void Invalid();
    }
}
