namespace Choice.Chat.Api.Models
{
    public class Order
    {
        public Order(int orderId, int orderRequestId, int price, int prepayment, int deadline, bool isEnrolled, 
            DateTime? enrollmentTime, int status, bool isDateConfirmed = true)
        {
            OrderId = orderId;
            OrderRequestId = orderRequestId;
            Price = price;
            Prepayment = prepayment;
            Deadline = deadline;
            IsEnrolled = isEnrolled;
            EnrollmentTime = enrollmentTime;
            Status = status;
            IsDateConfirmed = isDateConfirmed;
        }

        public int OrderId { get; }
        public int OrderRequestId { get; }
        public int Price { get; }
        public int Prepayment { get; }
        public int Deadline { get; }
        public bool IsEnrolled { get; private set; } = false;
        public DateTime? EnrollmentTime { get; private set; }
        public int Status { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsDateConfirmed { get; private set; }

        public void ChangeEnrollmentTime(DateTime? newTime, bool clientChanged)
        {
            EnrollmentTime = newTime;
            IsActive = false;
            IsDateConfirmed = !clientChanged;
        }

        public void ConfirmDate()
        {
            IsDateConfirmed = true;
            IsEnrolled = true;
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
            if (IsDateConfirmed)
                IsEnrolled = true;
        }

        public Order Copy() => 
            new(OrderId, OrderRequestId, Price, Prepayment, Deadline, IsEnrolled, EnrollmentTime, Status, IsDateConfirmed);
    }
}
