using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Commands.ChangeData
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
            var company = await _dbContext.Companies.FindAsync(new CompanyId(_userContext.Id.Value));

            if (company == null) throw new InvalidCommandException(["Company is not found"]);
            
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