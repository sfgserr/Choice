
namespace BuildingBlocks.Infrastructure.DomainEventDispatching
{
    public class DomainEventsMapper
    {
        private readonly Dictionary<string, Type> _mappings;

        internal DomainEventsMapper(Dictionary<string, Type> mappings)
        {
            _mappings = mappings;
        }

        public Type GetType(string type)
        {
            return _mappings[type];
        }
    }
}
