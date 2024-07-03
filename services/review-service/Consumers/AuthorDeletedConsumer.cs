using Choice.ReviewService.Api.Entities;
using Choice.ReviewService.Api.Infrastructure.Data;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ReviewService.Api.Consumers
{
    public class AuthorDeletedConsumer : IConsumer<UserDeletedEvent>
    {
        private readonly ReviewContext _reviewContext;

        public AuthorDeletedConsumer(ReviewContext reviewContext)
        {
            _reviewContext = reviewContext;
        }

        public async Task Consume(ConsumeContext<UserDeletedEvent> context)
        {
            UserDeletedEvent @event = context.Message;

            Author author = (await _reviewContext.Authors.FirstOrDefaultAsync(a => a.Guid == @event.UserId))!;

            author.Delete();

            _reviewContext.Authors.Update(author);

            await _reviewContext.SaveChangesAsync();
        }
    }
}
