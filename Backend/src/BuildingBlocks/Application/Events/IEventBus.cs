
namespace BuildingBlocks.Application.Events
{
    public interface IEventBus
    {
        Task PublishAsync(IIntegrationEvent integrationEvent);
    }
}
