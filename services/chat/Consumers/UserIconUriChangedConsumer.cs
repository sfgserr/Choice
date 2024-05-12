using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.Chat.Api.Consumers
{
    public class UserIconUriChangedConsumer : IConsumer<UserIconUriChangedEvent>
    {
        private readonly IUserRepository _repository;

        public UserIconUriChangedConsumer(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserIconUriChangedEvent> context)
        {
            UserIconUriChangedEvent @event = context.Message;

            User user = (await _repository.Get(@event.Guid))!;

            user.ChangeIconUri(@event.IconUri);

            _repository.Update(user);
        }
    }
}
