namespace BuildingBlocks.Application.Events
{
    public abstract class IntegrationEventBase : IIntegrationEvent
    {
        protected IntegrationEventBase(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; }
    }
}