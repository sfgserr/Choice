using BuildingBlocks.Application.Events;

namespace Administration.IntegrationEvents
{
    public class ClientDeletedIntegrationEvent : IntegrationEventBase
    {
        public ClientDeletedIntegrationEvent(Guid id, Guid clientId) : base(id)
        {
            ClientId = clientId;
        }
        
        public Guid ClientId { get; }
    }
}