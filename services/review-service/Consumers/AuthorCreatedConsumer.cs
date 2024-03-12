using Choice.EventBus.Messages.Events;
using Choice.ReviewService.Api.Entities;
using Choice.ReviewService.Api.Infrastructure.Data;
using MassTransit;

namespace Choice.ReviewService.Api.Consumers
{
    public class AuthorCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly ReviewContext _context;

        public AuthorCreatedConsumer(ReviewContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            UserCreatedEvent @event = context.Message;

            Author author = new(@event.UserGuid, @event.Name, @event.IconUri);

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }
    }
}
