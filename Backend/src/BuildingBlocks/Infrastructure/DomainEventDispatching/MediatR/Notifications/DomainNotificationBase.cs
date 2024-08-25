using BuildingBlocks.Domain;

namespace BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications
{
    public class DomainNotificationBase<TDomainEvent> : IDomainNotification where TDomainEvent : IDomainEvent
    {
        public DomainNotificationBase(TDomainEvent domainEvent)
        {
            Id = Guid.NewGuid();
            DomainEvent = domainEvent;
        }

        public Guid Id { get; }

        public TDomainEvent DomainEvent { get; }
    }
}
