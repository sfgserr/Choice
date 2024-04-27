using Choice.EventBus.Messages.Events;
using Choice.ReviewService.Api.Entities;
using Choice.ReviewService.Api.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Choice.ReviewService.Api.Consumers
{
    public class UserDataChangedConsumer : IConsumer<UserDataChangedEvent>
    {
        private readonly ReviewContext _context;

        public UserDataChangedConsumer(ReviewContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserDataChangedEvent> context)
        {
            UserDataChangedEvent @event = context.Message;

            Author author = (await _context.Authors.FirstOrDefaultAsync(a => a.Guid == @event.Guid))!;

            author.ChangeName(@event.Name);

            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}
