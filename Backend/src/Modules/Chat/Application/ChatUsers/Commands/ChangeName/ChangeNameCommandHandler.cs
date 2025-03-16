using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;

namespace Chat.Application.ChatUsers.Commands.ChangeName
{
    internal class ChangeNameCommandHandler : ICommandHandler<ChangeNameCommand>
    {
        private readonly IChatDbContext _dbContext;

        internal ChangeNameCommandHandler(IChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(ChangeNameCommand command)
        {
            var user = await _dbContext.ChatUsers.Get(u => u.Id.Equals(new ChatUserId(command.UserId)));
            
            user.ChangeName(command.Name);
        }
    }
}