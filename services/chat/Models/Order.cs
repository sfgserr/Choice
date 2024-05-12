namespace Choice.Chat.Api.Models
{
    public class Order
    {
        public Order(int orderId, int orderRequestId, int price, int prepayment, int deadline, bool isEnrolled, 
            DateTime enrollmentTime, int status)
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
        public bool IsEnrolled { get; private set; } = false;
        public DateTime EnrollmentTime { get; private set; }
        public int Status { get; private set; }
        public bool IsActive { get; private set; } = true;

        public void ChangeEnrollmentTime(DateTime newTime)
        {
            EnrollmentTime = newTime;
            IsActive = false;
        }

        public void ChangeStatus(int status)
        {
            if (status > 0 && status < 4)
            {
                Status = status;
            }
        }

        public void Enroll()
        {
            IsEnrolled = true;
        }

        public Order Copy() => 
            new(OrderId, OrderRequestId, Price, Prepayment, Deadline, IsEnrolled, EnrollmentTime, Status);
    }
}
