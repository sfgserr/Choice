using BuildingBlocks.Application.Events;
using Users.IntegrationEvents;

namespace Chat.Infrastructure.Consumers
{
    public class UserCreatedConsumer : IBusConsumer<UserCreatedIntegrationEvent>
    {
        public Task Consume(UserCreatedIntegrationEvent integrationEvent)
        {
            throw new NotImplementedException();
        }
    }
}