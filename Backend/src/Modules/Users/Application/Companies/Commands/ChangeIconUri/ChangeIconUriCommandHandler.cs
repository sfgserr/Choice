using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Application.Companies.Commands.ChangeIconUri
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
            var company = await _dbContext.Companies.Get(c => c.Id.Equals(_userContext.CompanyId));
            
            company.ChangeIconUri(command.IconUri);
        }
    }
}