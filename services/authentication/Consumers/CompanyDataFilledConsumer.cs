using Choice.Authentication.Api.Models;
using Choice.EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Choice.Authentication.Api.Consumers
{
    public class CompanyDataFilledConsumer : IConsumer<CompanyDataFilledEvent>
    {
        private readonly UserManager<User> _userManager;

        public CompanyDataFilledConsumer(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Consume(ConsumeContext<CompanyDataFilledEvent> context)
        {
            CompanyDataFilledEvent @event = context.Message;

            User user = (await _userManager.FindByIdAsync(@event.CompanyGuid))!;

            user.SetDataFilled();

            await _userManager.UpdateAsync(user);
        }
    }
}
