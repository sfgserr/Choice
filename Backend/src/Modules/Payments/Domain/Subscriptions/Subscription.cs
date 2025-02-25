using BuildingBlocks.Domain;
using Payments.Domain.Subscriptions.Events;

namespace Payments.Domain.Subscriptions
{
    public class Subscription : Entity, IAggregateRoot
    {
        private SubscriberId _subscriberId;
        
        private SubscriptionPeriod _period;

        private SubscriptionStatus _status;

        private DateTime _expirationDate;
        
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
            
            _subscriberId = subscriberId;
            _period = period;
            _status = status;
            _expirationDate = expirationDate;
            
            AddDomainEvent(new SubscriptionStatusChangedDomainEvent(_subscriberId));
        }
        
        public bool IsActive => _status.Equals(SubscriptionStatus.Active);
        
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
            if (_expirationDate < DateTime.UtcNow)
            {
                _status = SubscriptionStatus.Expired;
            
                AddDomainEvent(new SubscriptionStatusChangedDomainEvent(_subscriberId));
            }
        }

        public SubscriptionId Id { get; }
    }
}
