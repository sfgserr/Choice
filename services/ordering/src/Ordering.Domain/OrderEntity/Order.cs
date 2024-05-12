using Choice.Common.SeedWork;

namespace Choice.Ordering.Domain.OrderEntity
{
    public class Order : Entity
    {
        private readonly List<string> _reviews = new();

        public Order(int orderRequestId, string senderId, string receiverId, int price, int prepayment,
            int deadline, DateTime enrollmentDate)
        {
            OrderRequestId = orderRequestId;
            SenderId = senderId;
            ReceiverId = receiverId;
            Price = price;
            Prepayment = prepayment;
            Deadline = deadline;
            EnrollmentDate = enrollmentDate;
        }

        public int OrderRequestId { get; private set; }
        public string SenderId { get; private set; }
        public string ReceiverId { get; private set; }
        public int Price { get; private set; }
        public int Prepayment { get; private set; }
        public int Deadline { get; private set; }
        public bool IsEnrolled { get; private set; } = false;
        public DateTime EnrollmentDate { get; private set; }
        public bool IsDateConfirmed { get; private set; } = true;
        public IReadOnlyCollection<string> Reviews => _reviews.AsReadOnly();
        public OrderStatus Status { get; private set; } = OrderStatus.Active;

        public void SetEnrollmentDate(DateTime newDate)
        {
            EnrollmentDate = newDate;
            IsDateConfirmed = false;
        }

        public void FinishOrder() =>
            Status = OrderStatus.Finished;

        public void Enroll() =>
            IsEnrolled = true;

        public void AddReview(string guid) =>
            _reviews.Add(guid);

        public void CancelEnrollment()
        {
            IsEnrolled = false;
            Status = OrderStatus.Canceled;
        }
    }
}
