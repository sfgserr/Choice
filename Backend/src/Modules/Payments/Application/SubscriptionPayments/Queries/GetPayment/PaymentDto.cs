namespace Payments.Application.SubscriptionPayments.Queries.GetPayment
{
    public class PaymentDto
    {
        public PaymentDto(
            Guid id, 
            string period, 
            double cost, 
            Guid payerId, 
            DateTime expirationDate, 
            string status)
        {
            Id = id;
            Period = period;
            Cost = cost;
            PayerId = payerId;
            ExpirationDate = expirationDate;
            Status = status;
        }

        public Guid Id { get; }

        public string Period { get; }

        public double Cost { get; }

        public Guid PayerId { get; }

        public DateTime ExpirationDate { get; }

        public string Status { get; private set; }
    }
}