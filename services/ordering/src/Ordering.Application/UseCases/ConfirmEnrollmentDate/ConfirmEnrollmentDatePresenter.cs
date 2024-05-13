using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.ConfirmEnrollmentDate
{
    public sealed class ConfirmEnrollmentDatePresenter : IOutputPort
    {
        public bool IsInvalid { get; set; }
        public bool IsNotFound { get; set; }
        public Order? Order { get; set; }

        public void Invalid()
        {
            IsInvalid = true;
        }

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
