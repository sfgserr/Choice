using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using BuildingBlocks.Infrastructure.Outbox;
using BuildingBlocks.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace BuildingBlocks.Infrastructure.DomainEventDispatching
{
    public class DomainEventsDispatcher
    {
        private readonly DomainEventsAccessor _domainEventsAccessor;
        private readonly DomainEventsMapper _mapper;
        private readonly IOutbox _outbox;

        public DomainEventsDispatcher(
            DomainEventsAccessor domainEventsAccessor, 
            DomainEventsMapper mapper,
            IOutbox outbox)
        {
            _domainEventsAccessor = domainEventsAccessor;
            _outbox = outbox;
            _mapper = mapper;
        }

        public void DispatchDomainEvents()
        {
            List<IDomainEvent> domainEvents = _domainEventsAccessor.GetAllDomainEvents();

            _domainEventsAccessor.ClearAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                var notificationType = _mapper
                    .GetType(domainEvent.GetType().Name.Replace("DomainEvent", "DomainNotification"));

                var notification =  Activator.CreateInstance(notificationType, domainEvent) as IDomainNotification;

                var json = JsonConvert.SerializeObject(
                    notification,
                    new JsonSerializerSettings() { ContractResolver = new AllPropertiesContractResolver() });

                var message = new OutboxMessage(
                    domainEvent.Id,
                    notificationType.Name,
                    json,
                    DateTime.UtcNow);

                _outbox.Add(message);
            }
        }
    }
}
