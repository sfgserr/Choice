using BuildingBlocks.Application.Events;
using Identity.Application.Users.CreateUser;
using Identity.Infrastructure.Data.InternalCommands;
using Users.IntegrationEvents;

namespace Identity.Infrastructure.Consumers
{
    internal class UserCreatedConsumer : IBusConsumer<UserCreatedIntegrationEvent>
    {
        private readonly CommandsScheduler _scheduler;

        public UserCreatedConsumer(CommandsScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Consume(UserCreatedIntegrationEvent @event)
        {
            await _scheduler.EnqueueAsync(new CreateUserCommand(
                @event.Id,
                @event.UserId,
                @event.Email,
                @event.Password,
                @event.PhoneNumber,
                @event.UserRole,
                @event.City,
                @event.Street,
                @event.Latitude,
                @event.Longitude));
        }
    }
}