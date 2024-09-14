using System.Threading.Channels;

namespace BuildingBlocks.Infrastructure.Events
{
    public class InMemoryQueue
    {
        private readonly Channel<IntegrationEventBase> _channel = Channel.CreateUnbounded<IntegrationEventBase>();

        public ChannelWriter<IntegrationEventBase> Writer => _channel.Writer;

        public ChannelReader<IntegrationEventBase> Reader => _channel.Reader;
    }
}