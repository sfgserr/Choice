using Choice.EventBus.Messages.Events;
using MassTransit;

namespace ReviewService.Api.Consumers
{
    public class UserIconUriChangedConsumer : IConsumer<UserIconUriChangenEvent>
    {
        public Task Consume(ConsumeContext<UserIconUriChangenEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
