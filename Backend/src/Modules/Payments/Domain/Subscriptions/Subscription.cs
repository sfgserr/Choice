using BuildingBlocks.Domain;
using Payments.Domain.Subscriptions.Events;

namespace Payments.Domain.Subscriptions
{
    public class Subscription : Entity, IAggregateRoot
    {
        private Subscription()
        {

        }

        private Subscription(
            SubscriptionId id,
            SubscriberId subscriberId,
            SubscriptionPeriod period,
            SubscriptionStatus status,
            DateTime expirationDate)
        {
            Id = id;
            SubscriberId = subscriberId;
            Period = period;
            Status = status;
            ExpirationDate = expirationDate;
            
            AddDomainEvent(new SubscriptionStatusChangedDomainEvent(SubscriberId));
        }

        public static Subscription Create(SubscriberId subscriberId, SubscriptionPeriod period)
        {
            return new Subscription(
                new(Guid.NewGuid()),
                subscriberId,
                period,
                SubscriptionStatus.Active,
                DateCalculator.CalculateExpirationDateForSubscription(period.Value));
        }

        public void Expire()
        {
            Status = SubscriptionStatus.Expired;
            
            AddDomainEvent(new SubscriptionStatusChangedDomainEvent(SubscriberId));
        }

        public SubscriptionId Id { get; }

        public SubscriberId SubscriberId { get; }

        public SubscriptionPeriod Period { get; }

        public SubscriptionStatus Status { get; private set; }

        public DateTime ExpirationDate { get; }
    }
}
