using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentDate
{
    public sealed class ChangeOrderEnrollmentDatePresenter : IOutputPort
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
