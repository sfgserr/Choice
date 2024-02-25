using Choice.Ordering.Domain.OrderAggregate;

namespace Choice.Ordering.Application.UseCases.CreateOrder
{
    public interface IOutputPort
    {
        void Ok(Order order);

        void Invalid();
    }
}
