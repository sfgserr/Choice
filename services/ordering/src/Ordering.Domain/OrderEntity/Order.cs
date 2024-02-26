using Choice.Ordering.Domain.Common;

namespace Choice.Ordering.Domain.OrderEntity
{
    public class Order : Entity
    {
        public Order(int orderRequestId, string senderGuid, string receiverId, double price, double prepayment,
            int deadline, DateTime enrollmentTime)
        {
            OrderRequestId = orderRequestId;
            SenderGuid = senderGuid;
            ReceiverId = receiverId;
            Price = price;
            Prepayment = prepayment;
            Deadline = deadline;
            EnrollmentDate = enrollmentTime;
        }

        public int OrderRequestId { get; private set; }
        public string SenderGuid { get; private set; }
        public string ReceiverId { get; private set; }
        public double Price { get; private set; }
        public double Prepayment { get; private set; }
        public int Deadline { get; private set; }
        public bool IsEnrolled { get; private set; } = false;
        public DateTime EnrollmentDate { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Active;

        public void SetEnrollmentDate(DateTime newDate) =>
            EnrollmentDate = newDate;

        public void SetOrderStatus(OrderStatus status) =>
            Status = status;

        public void Enroll() =>
            IsEnrolled = true;

        public void CancelEnrollment()
        {
            IsEnrolled = false;
            Status = OrderStatus.Canceled;
        }
    }
}
