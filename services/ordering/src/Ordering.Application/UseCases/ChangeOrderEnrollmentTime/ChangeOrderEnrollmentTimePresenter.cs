using Choice.Ordering.Domain.OrderAggregate;

namespace Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentTime
{
    public sealed class ChangeOrderEnrollmentTimePresenter : IOutputPort
    {
        public Order? Order { get; set; }
        public bool IsInvalid { get; set; } 
        public bool IsNotFound { get; set; }

        public void Invalid()
        {
            IsInvalid = false;
        }

        public void Ok(Order order)
        {
            Order = order;
        }

        public void NotFound()
        {
            IsNotFound = true;
        }
    }
}
