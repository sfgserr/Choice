using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;

namespace Chat.Application.ChatUsers.Commands.DeleteUserCommand
{
    internal class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IChatDbContext _dbContext;

        internal DeleteUserCommandHandler(IChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(DeleteUserCommand command)
        {
            var user = await _dbContext.ChatUsers.Get(c => c.Id.Value.Equals(command.UserId));

            user.Delete();
        }
    }
}