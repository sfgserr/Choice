using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Commands.ChangeIconUri
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
            var company = await _dbContext.Companies.FindAsync(_userContext.Id);

            if (company == null) throw new InvalidCommandException(["Company is not found"]);
            
            company.ChangeIconUri(command.IconUri);
        }
    }
}