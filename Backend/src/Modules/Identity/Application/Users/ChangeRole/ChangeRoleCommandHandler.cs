using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Identity.Application.Contracts;
using Identity.Domain.Users;

namespace Identity.Application.Users.ChangeRole
{
    internal class ChangeRoleCommandHandler : ICommandHandler<ChangeRoleCommand>
    {
        private readonly IIdentityDbContext _dbContext;

        internal ChangeRoleCommandHandler(IIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(ChangeRoleCommand command)
        {
            var user = await _dbContext.Users.Get(u => u.Id.Equals(new UserId(command.UserId)));
            
            user.ChangeRole(UserRole.Parse(command.UserRole));
        }
    }
}