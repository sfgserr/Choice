using Choice.Ordering.Domain.OrderAggregate;

namespace Choice.Ordering.Application.UseCases.CreateOrder
{
    public sealed class CreateOrderPresenter : IOutputPort
    {
        public Order? Order { get; set; }
        public bool IsInvalid { get; set; }

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void Ok(Order order)
        {
            Order = order;
        }
    }
}
