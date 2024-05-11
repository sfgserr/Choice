namespace Choice.Chat.Api.Models
{
    public class Order
    {
        public Order(int orderId, int orderRequestId, int price, int prepayment, int deadline, bool isEnrolled, 
            DateTime enrollmentTime, string status)
        {
            OrderId = orderId;
            OrderRequestId = orderRequestId;
            Price = price;
            Prepayment = prepayment;
            Deadline = deadline;
            IsEnrolled = isEnrolled;
            EnrollmentTime = enrollmentTime;
            Status = status;
        }

        public int OrderId { get; }
        public int OrderRequestId { get; }
        public int Price { get; }
        public int Prepayment { get; }
        public int Deadline { get; }
        public bool IsEnrolled { get; } = false;
        public DateTime EnrollmentTime { get; }
        public string Status { get; }
    }
}
