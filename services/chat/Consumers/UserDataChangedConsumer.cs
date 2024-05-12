using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.Chat.Api.Consumers
{
    public class UserDataChangedConsumer : IConsumer<UserDataChangedEvent>
    {
        private readonly IUserRepository _repository;

        public UserDataChangedConsumer(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserDataChangedEvent> context)
        {
            UserDataChangedEvent @event = context.Message;

            User user = (await _repository.Get(@event.Guid))!;

            user.ChangeName(@event.Name);

            _repository.Update(user);
        }
    }
}
