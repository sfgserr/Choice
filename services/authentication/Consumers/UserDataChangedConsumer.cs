using Choice.Authentication.Api.Models;
using Choice.Authentication.Api.Repositories;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.Authentication.Api.Consumers
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

            User user = await _repository.Get(@event.Guid);

            user.ChangeData(@event.Name, @event.Email, @event.PhoneNumber, @event.IconUri);

            await _repository.Update(user);
        }
    }
}
