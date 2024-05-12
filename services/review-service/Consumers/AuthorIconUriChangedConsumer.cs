using Choice.EventBus.Messages.Events;
using Choice.ReviewService.Api.Entities;
using Choice.ReviewService.Api.Infrastructure.Data;
using MassTransit;

namespace Choice.ReviewService.Api.Consumers
{
    public class AuthorIconUriChangedConsumer : IConsumer<UserIconUriChangedEvent>
    {
        private readonly ReviewContext _context;

        public AuthorIconUriChangedConsumer(ReviewContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserIconUriChangedEvent> context)
        {
            UserIconUriChangedEvent @event = context.Message;

            Author author = _context.Authors.FirstOrDefault(a => a.Guid == @event.Guid);

            author!.ChangeIconUri(@event.IconUri);

            await _context.SaveChangesAsync();
        }
    }
}
