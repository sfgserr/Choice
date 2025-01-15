namespace Payments.Application.SubscriptionPayments.Queries.GetPayment
{
    public class PaymentDto
    {
        public Guid Id { get; }

        public string Period { get; }

        public double Cost { get; }

        public Guid PayerId { get; }

        public DateTime ExpirationDate { get; }

        public string Status { get; }
    }
}