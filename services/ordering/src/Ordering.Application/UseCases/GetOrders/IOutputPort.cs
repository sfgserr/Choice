using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.GetOrders
{
    public interface IOutputPort
    {
        void Ok(IList<Order> orders);
    }
}
