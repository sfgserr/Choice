using BuildingBlocks.Domain;
using Payments.Domain.Payers;
using Payments.Domain.SeedWork;

namespace Payments.Domain.EnrollmentPayments
{
    public class EnrollmentPayment : Entity, IAggregateRoot
    {
        private EnrollmentPayment()
        {

        }

        private EnrollmentPayment(
            EnrollmentPaymentId id, 
            PayerId payerId, 
            OrderResponseId responseId, 
            PaymentStatus status, 
            MoneyValue cost)
        {
            Id = id;
            PayerId = payerId;
            ResponseId = responseId;
            Status = status;
            Cost = cost;
        }

        public static EnrollmentPayment Buy(
            PayerId payerId,
            OrderResponseId responseId,
            MoneyValue cost)
        {
            return new EnrollmentPayment(
                new(Guid.NewGuid()),
                payerId,
                responseId,
                PaymentStatus.WaitingForPayment,
                cost);
        }

        public void Pay()
        {
            Status = PaymentStatus.Paid;
        }

        public EnrollmentPaymentId Id { get; }

        public PayerId PayerId { get; }
        
        public OrderResponseId ResponseId { get; }

        public PaymentStatus Status { get; private set; }

        public MoneyValue Cost { get; }

    }
}
