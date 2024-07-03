using Choice.Authentication.Api.Models;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Api.Consumers
{
    public class UserDeletedConsumer : IConsumer<UserDeletedEvent>
    {
        private readonly UserManager<User> _userManager;

        public UserDeletedConsumer(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<UserDeletedEvent> context)
        {
            UserDeletedEvent @event = context.Message;

            User user = (await _userManager.FindByIdAsync(@event.UserId))!;
            
            await _userManager.DeleteAsync(user);
        }
    }
}
