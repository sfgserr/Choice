using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace BuildingBlocks.Infrastructure.Events
{
    public class EventBus : IEventBus
    {
        private readonly InMemoryQueue _queue;

        public EventBus(InMemoryQueue queue)
        {
            _queue = queue;
        }

        public async Task PublishAsync(IIntegrationEvent integrationEvent)
        {
            var type = integrationEvent.GetType().FullName!;

            var content = JsonConvert.SerializeObject(integrationEvent, new JsonSerializerSettings()
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            await _queue.Writer.WriteAsync(new IntegrationEventBase(type, content));
        }
    }
}