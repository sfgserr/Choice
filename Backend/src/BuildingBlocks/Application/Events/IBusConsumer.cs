namespace BuildingBlocks.Application.Events
{
    public interface IBusConsumer<T> where T : IIntegrationEvent
    {
        Task Consume(T integrationEvent);
    }
}