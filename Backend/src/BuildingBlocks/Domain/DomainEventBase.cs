
namespace BuildingBlocks.Domain
{
    public abstract class DomainEventBase : IDomainEvent
    {
        protected DomainEventBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}
