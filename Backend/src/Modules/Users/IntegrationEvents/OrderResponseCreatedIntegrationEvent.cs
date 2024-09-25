using BuildingBlocks.Application.Events;

namespace Users.IntegrationEvents
{
    public class OrderResponseCreatedIntegrationEvent : IntegrationEventBase
    {
        public OrderResponseCreatedIntegrationEvent(Guid id, Guid responseId) : base(id)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}
