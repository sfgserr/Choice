using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.Categories;
using Users.Domain.Users;

namespace Users.Application.Companies.Commands.FillData
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
            var company = await _dbContext.Companies.Get(c => c.Id.Equals(_usersContext.Id));
            
            company.FillData(
                command.Description,
                command.CategoryIds.Select(c => new CategoryId(c)).ToList(),
                command.PhotoUris,
                command.SocialMediaUris,
                command.IsPrepaymentAvailable);
        }
    }
}