using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.CancelEnrollment
{
    public class CancelEnrollmentPresenter : IOutputPort
    {
        public Order? Order { get; set; }
        public string? ReceiverId { get; set; }
        public bool IsInvalid { get; set; }
        public bool IsNotFound { get; set; }

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void NotFound()
        {
            IsNotFound = true;
        }

        public void Ok(Order order, string receiverId)
        {
            Order = order;
            ReceiverId = receiverId;
        }
    }
}
