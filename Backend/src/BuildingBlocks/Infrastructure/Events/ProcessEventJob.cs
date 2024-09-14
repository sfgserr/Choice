using MassTransit;
using Newtonsoft.Json;
using Quartz;

namespace BuildingBlocks.Infrastructure.Events
{
    [DisallowConcurrentExecution]
    public class ProcessEventJob : IJob
    {
        private readonly InMemoryQueue _queue;
        private readonly IPublishEndpoint _endpoint;
        
        public ProcessEventJob(InMemoryQueue queue, IPublishEndpoint endpoint)
        {
            _queue = queue;
            _endpoint = endpoint;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var @event = await _queue.Reader.ReadAsync();

            var integrationEvent = JsonConvert.DeserializeObject(
                @event.Content, 
                Type.GetType(@event.Type)!);

            if (integrationEvent == null)
                throw new ApplicationException($"Cannot convert {@event.Type}");
            
            await _endpoint.Publish(integrationEvent);
        }
    }
}