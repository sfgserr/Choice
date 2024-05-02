using Choice.Authentication.Api.Models;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Choice.Authentication.Api.Consumers
{
    public class UserDataChangedConsumer : IConsumer<UserDataChangedEvent>
    {
        private readonly UserManager<User> _userManager;

        public UserDataChangedConsumer(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<UserDataChangedEvent> context)
        {
            UserDataChangedEvent @event = context.Message;

            User user = (await _userManager.FindByIdAsync(@event.Guid))!;

            user.ChangeData(@event.Name, @event.Email, @event.PhoneNumber);

            await _userManager.UpdateAsync(user);
        }
    }
}
