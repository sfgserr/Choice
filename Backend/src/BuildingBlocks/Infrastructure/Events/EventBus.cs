using BuildingBlocks.Application.Events;
using BuildingBlocks.Infrastructure.Serialization;
using MassTransit;
using Newtonsoft.Json;

namespace BuildingBlocks.Infrastructure.Events
{
    public class EventBus : IEventBus
    {
        private readonly IBus _bus;

        public EventBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<T>(T integrationEvent) where T : IIntegrationEvent
        {
            await _bus.Publish(integrationEvent);
        }
    }
}