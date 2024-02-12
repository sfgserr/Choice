using Choice.Authentication.Entities;
using Choice.Authentication.Infrastructure.Data;
using Choice.Authentication.Infrastructure.Data.Repositories;
using EventBus.Messages.Events;
using MassTransit;

namespace Choice.Authentication.EventBusConsumer
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly UnitOfWork _unitOfWork;

        public UserCreatedConsumer(IUserRepository userRepository, UnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var user = new User()
            {
                Id = context.Message.UserId,
                Email = context.Message.Email,
                Phone = context.Message.Phone,
                Password = context.Message.Password
            };

            await _userRepository.Create(user);
            await _unitOfWork.Save();
        }
    }
}
