using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Repositories.Interfaces;
using EventBus.Messages.Events;
using MassTransit;

namespace Chat.Api.Consumers
{
    public class UserDeletedConsumer : IConsumer<UserDeletedEvent>
    {
        private readonly IUserRepository _userRepository;

        public UserDeletedConsumer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<UserDeletedEvent> context)
        {
            UserDeletedEvent @event = context.Message;

            User user = (await _userRepository.Get(@event.UserId))!;

            user.Delete();

            await _userRepository.Update(user);
        }
    }
}
