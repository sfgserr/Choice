using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Commands.FillData
{
    internal class FillDataCommandHandler : ICommandHandler<FillDataCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _usersContext;

        internal FillDataCommandHandler(IUsersDbContext dbContext, IUserContext usersContext)
        {
            _dbContext = dbContext;
            _usersContext = usersContext;
        }

        public async Task Execute(FillDataCommand command)
        {
            var company = await _dbContext.Companies.FindAsync(_usersContext.Id);

            if (company == null) throw new InvalidCommandException(["Company is not found"]);
            
            company.FillData(
                command.Description,
                command.CategoryIds,
                command.PhotoUris,
                command.SocialMediaUris,
                command.IsPrepaymentAvailable);
        }
    }
}