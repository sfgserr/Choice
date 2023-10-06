using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.UpdateOrder
{
    public class UpdateOrderUseCasePresenter : IOutputPort
    {
        public Order Order { get; set; }

        public void Ok(Order order)
        {
            Order = order;
        }
    }
}
