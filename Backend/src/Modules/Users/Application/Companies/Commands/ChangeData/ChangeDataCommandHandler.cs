using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Application.Companies.Commands.ChangeData
{
    internal class ChangeDataCommandHandler : ICommandHandler<ChangeDataCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IUsersCounter _usersCounter;

        internal ChangeDataCommandHandler(
            IUsersDbContext dbContext, 
            IUserContext userContext, 
            IUsersCounter usersCounter)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _usersCounter = usersCounter;
        }

        public async Task Execute(ChangeDataCommand command)
        {
            var company = await _dbContext.Companies.Get(c => 
                c.Id.Equals(_userContext.Id));
            
            company.ChangeData(
                command.Name,
                command.Email,
                command.PhoneNumber,
                command.Address,
                _usersCounter,
                command.Description,
                command.Categories,
                command.PhotoUris,
                command.SocialMediaUris,
                command.IsPrepaymentAvailable);
        }
    }
}