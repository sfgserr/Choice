using Choice.Domain.Models;

namespace Choice.Application.UseCases.Orders.GetOrder
{
    public class GetOrderUseCasePresenter : IOutputPort
    {
        public bool IsNotFound { get; set; } = false;
        public Order Order { get; set; }

        public void NotFound()
        {
            IsNotFound = true;
        }

        public void Ok(Order order)
        {
            Order = order;
        }
    }
}
