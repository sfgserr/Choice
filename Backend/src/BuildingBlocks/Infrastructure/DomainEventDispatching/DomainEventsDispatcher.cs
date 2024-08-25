using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Outbox;
using BuildingBlocks.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace BuildingBlocks.Infrastructure.DomainEventDispatching
{
    public class DomainEventsDispatcher
    {
        private readonly DomainEventsAccessor _domainEventsAccessor;
        private readonly IOutbox _outbox;

        public DomainEventsDispatcher(DomainEventsAccessor domainEventsAccessor, IOutbox outbox)
        {
            _domainEventsAccessor = domainEventsAccessor;
            _outbox = outbox;
        }

        public void DispatchDomainEvents()
        {
            List<IDomainEvent> domainEvents = _domainEventsAccessor.GetAllDomainEvents();

            _domainEventsAccessor.ClearAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                Type notificationType = domainEvent.GetNotificationType();

                string json = JsonConvert.SerializeObject(
                    Activator.CreateInstance(notificationType, domainEvent),
                    new JsonSerializerSettings() { ContractResolver = new AllPropertiesContractResolver() });

                OutboxMessage message = new(
                    domainEvent.Id,
                    notificationType.Name,
                    json,
                    DateTime.Now);

                _outbox.Add(message);
            }
        }
    }
}
