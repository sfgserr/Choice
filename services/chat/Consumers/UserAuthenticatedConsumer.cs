using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Repositories.Interfaces;
using EventBus.Messages.Events;
using MassTransit;

namespace Chat.Api.Consumers
{
    public class UserAuthenticatedConsumer : IConsumer<UserAuthenticatedEvent>
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticatedConsumer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<UserAuthenticatedEvent> context)
        {
            UserAuthenticatedEvent @event = context.Message;

            User user = (await _userRepository.Get(@event.UserId))!;

            user.AddDevice(@event.DeviceToken);

            await _userRepository.Update(user);
        }
    }
}
