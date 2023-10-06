using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.CreateOrder
{
    public class CreateOrderPresenter : IOutputPort
    {
        public bool IsInvalid { get; set; } = false;
        public Order? Order { get; set; }

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
