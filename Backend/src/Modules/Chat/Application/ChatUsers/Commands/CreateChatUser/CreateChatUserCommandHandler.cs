using BuildingBlocks.Application.Cqrs.Commands;
using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;

namespace Chat.Application.ChatUsers.Commands.CreateChatUser
{
    internal class CreateChatUserCommandHandler : ICommandHandler<CreateChatUserCommand>
    {
        private readonly IChatDbContext _dbContext;

        internal CreateChatUserCommandHandler(IChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(CreateChatUserCommand command)
        {
            var user = ChatUser.Create(
                new(command.UserId),
                command.Name,
                "default.png",
                command.DeviceName,
                command.DeviceToken);

            await _dbContext.ChatUsers.AddAsync(user);
        }
    }
}
