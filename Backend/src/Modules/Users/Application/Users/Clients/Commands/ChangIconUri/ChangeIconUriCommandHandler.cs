using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Clients;

namespace Users.Application.Users.Clients.Commands.ChangIconUri
{
    internal class ChangeIconUriCommandHandler : ICommandHandler<ChangeIconUriCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;

        internal ChangeIconUriCommandHandler(IUsersDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Execute(ChangeIconUriCommand command)
        {
            var client = await _dbContext.Clients.FindAsync(_userContext.Id);

            if (client == null) throw new InvalidCommandException(["Client is not found"]);
            
            client.ChangeIconUri(command.IconUri);
        }
    }
}
