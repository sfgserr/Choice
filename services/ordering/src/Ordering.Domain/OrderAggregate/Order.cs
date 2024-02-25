using Choice.Ordering.Domain.Common;

namespace Choice.Ordering.Domain.OrderAggregate
{
    public class Order : Entity
    {
        public Order(int orderRequestId, string senderGuid, string receiverId, double price, double prepayment
            int deadline, DateTime enrollmentTime)
        {
            OrderRequestId = orderRequestId;
            SenderGuid = senderGuid;
            ReceiverId = receiverId;
            Price = price;
            Prepayment = prepayment;
            Deadline = deadline;
            EnrollmentTime = enrollmentTime;
        }

        public int OrderRequestId { get; private set; }
        public string SenderGuid { get; private set; }
        public string ReceiverId { get; private set; }
        public double Price { get; private set; }
        public double Prepayment { get; private set; }
        public int Deadline { get; private set; }
        public bool IsEnrolled { get; private set; } = false;
        public DateTime EnrollmentTime { get; private set; }
        public bool IsCanceled { get; private set; } = false;

        public void ChangeEnrollmentStaus() =>
            IsEnrolled = !IsEnrolled;

        public void ChangeEnrollmentTime(DateTime newDate) =>
            EnrollmentTime = newDate;

        public void CancelOrder() =>
            IsCanceled = true;
    }
}
