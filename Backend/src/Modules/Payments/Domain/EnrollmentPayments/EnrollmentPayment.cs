using BuildingBlocks.Domain;
using Payments.Domain.EnrollmentPayments.Events;
using Payments.Domain.EnrollmentPayments.Rules;
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
            ExpirationDate = DateCalculator.CalculateExpirationDateForPayment();
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
            CheckRule(new CannotPayForPaidOrExpiredPaymentRule(Status));

            Status = PaymentStatus.Paid;

            AddDomainEvent(new EnrollmentPaymentPaidDomainEvent(ResponseId));
        }

        public void Expire()
        {
            Status = PaymentStatus.Expired;
            
            AddDomainEvent(new EnrollmentPaymentExpiredDomainEvent(ResponseId));
        }
        
        public EnrollmentPaymentId Id { get; }

        public PayerId PayerId { get; }
        
        public OrderResponseId ResponseId { get; }

        public PaymentStatus Status { get; private set; }
        
        public DateTime ExpirationDate { get; }

        public MoneyValue Cost { get; }
    }
}
