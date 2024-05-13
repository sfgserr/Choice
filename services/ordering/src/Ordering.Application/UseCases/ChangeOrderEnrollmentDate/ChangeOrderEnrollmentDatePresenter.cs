using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.ChangeOrderEnrollmentDate
{
    public sealed class ChangeOrderEnrollmentDatePresenter : IOutputPort
    {
        public Order? Order { get; set; }
        public string? ReceiverId { get; set; }
        public bool IsInvalid { get; set; } 
        public bool IsNotFound { get; set; }

        public void Invalid()
        {
            IsInvalid = false;
        }

        public void Ok(Order order, string receiverId)
        {
            Order = order;
            ReceiverId = receiverId;
        }

        public void NotFound()
        {
            IsNotFound = true;
        }
    }
}
