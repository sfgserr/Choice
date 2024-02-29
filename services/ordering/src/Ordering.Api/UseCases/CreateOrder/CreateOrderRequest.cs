namespace Choice.Ordering.Api.UseCases.CreateOrder
{
    public class CreateOrderRequest
    {
        public CreateOrderRequest(string receiverId, int orderRequestId, int price, int prepayment, int deadline,
            DateTime enrollmentTime)
        {
            ReceiverId = receiverId;
            OrderRequestId = orderRequestId;
            Price = price;
            Prepayment = prepayment;
            Deadline = deadline;
            EnrollmentTime = enrollmentTime;
        }

        public string ReceiverId { get; }
        public int OrderRequestId { get; }
        public int Price { get; }
        public int Prepayment { get; }
        public int Deadline { get; }
        public DateTime EnrollmentTime { get; }
    }
}
