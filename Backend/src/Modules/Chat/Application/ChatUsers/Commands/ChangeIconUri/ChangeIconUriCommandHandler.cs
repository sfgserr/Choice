using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;

namespace Chat.Application.ChatUsers.Commands.ChangeIconUri
{
    internal class ChangeIconUriCommandHandler : ICommandHandler<ChangeIconUriCommand>
    {
        private readonly IChatDbContext _dbContext;

        internal ChangeIconUriCommandHandler(IChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(ChangeIconUriCommand command)
        {
            var user = await _dbContext.ChatUsers.Get(c => c.Id.Equals(new ChatUserId(command.UserId)));

            user.ChangeIconUri(command.IconUri);
        }
    }
}