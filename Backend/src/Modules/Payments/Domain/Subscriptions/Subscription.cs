using BuildingBlocks.Domain;

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
            DateTime exprirationDate)
        {
            Id = id;
            SubscriberId = subscriberId;
            Period = period;
            Status = status;
            ExpirationDate = exprirationDate;
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
        }

        public SubscriptionId Id { get; }

        public SubscriberId SubscriberId { get; }

        public SubscriptionPeriod Period { get; }

        public SubscriptionStatus Status { get; private set; }

        public DateTime ExpirationDate { get; }
    }
}
