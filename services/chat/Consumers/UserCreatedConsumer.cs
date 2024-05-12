using Choice.Chat.Api.Repositories.Interfaces;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.Chat.Api.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IUserRepository _repository;

        public UserCreatedConsumer(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            UserCreatedEvent @event = context.Message;

            await _repository.Add(new(@event.UserGuid, @event.Name, "defaulturi"));
        }
    }
}
