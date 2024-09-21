
namespace BuildingBlocks.Application.Events
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T integrationEvent) where T : IIntegrationEvent;
    }
}
