using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.GetOrders
{
    public sealed class GetOrdersPresenter : IOutputPort
    {
        public IList<Order>? Orders { get; set; }

        public void Ok(IList<Order> orders)
        {
            Orders = orders;
        }
    }
}
