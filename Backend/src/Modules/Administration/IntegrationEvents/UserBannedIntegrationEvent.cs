using BuildingBlocks.Application.Events;

namespace Administration.IntegrationEvents
{
    public class UserBannedIntegrationEvent : IntegrationEventBase
    {
        public UserBannedIntegrationEvent(Guid id, Guid clientId) : base(id)
        {
            ClientId = clientId;
        }
        
        public Guid ClientId { get; }
    }
}