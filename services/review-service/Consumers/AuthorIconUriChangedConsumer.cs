using Choice.EventBus.Messages.Events;
using MassTransit;

namespace ReviewService.Api.Consumers
{
    public class AuthorIconUriChangedConsumer : IConsumer<UserIconUriChangenEvent>
    {
        public Task Consume(ConsumeContext<UserIconUriChangenEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
