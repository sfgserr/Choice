namespace Choice.Chat.Api.Entities
{
    public class Order
    {
        public Order(int orderId, int price, int deadline, DateTime enrollmentTime, int prepayment, string senderId, 
            string receiverId)
        {
            OrderId = orderId;
            Price = price;
            Deadline = deadline;
            EnrollmentTime = enrollmentTime;
            Prepayment = prepayment;
            SenderId = senderId;
            ReceiverId = receiverId;
        }

        public int Id { get; }
        public int OrderId { get; }
        public string SenderId { get; }
        public string ReceiverId { get; }
        public int Price { get; private set; }
        public int Deadline { get; private set; }
        public DateTime EnrollmentTime { get; private set; }
        public int Prepayment { get; private set; }
        public bool IsEnrolled { get; private set; } = false;
        public DateTime CreationTime { get; } = DateTime.Now;
    }
}
