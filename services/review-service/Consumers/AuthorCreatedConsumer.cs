using Choice.EventBus.Messages.Events;
using Choice.ReviewService.Api.Entities;
using Choice.ReviewService.Api.Infrastructure.Data;
using MassTransit;

namespace Choice.ReviewService.Api.Consumers
{
    public class AuthorCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly ReviewContext _context;
        private readonly ILogger<AuthorCreatedConsumer> _logger;

        public AuthorCreatedConsumer(ReviewContext context, ILogger<AuthorCreatedConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            _logger.LogInformation("Consuming author created");

            UserCreatedEvent @event = context.Message;

            Author author = new(@event.UserGuid, @event.Name, "defaulturi-png");

            await _context.Authors.AddAsync(author);
            int affections = await _context.SaveChangesAsync();

            _logger.LogInformation($"{affections}");
        }
    }
}
