using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.Enroll
{
    public sealed class EnrollPresenter : IOutputPort
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
