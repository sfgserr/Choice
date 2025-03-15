using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Identity.Application.Contracts;
using Identity.Domain.Users;

namespace Identity.Application.Users.ChangePassword
{
    internal class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IIdentityDbContext _dbContext;
        private readonly IUserContext _userContext;

        internal ChangePasswordCommandHandler(IIdentityDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task Execute(ChangePasswordCommand command)
        {
            var user = await _dbContext.Users.Get(u => u.Id.Equals(_userContext.Id));
            
            user.ChangePassword(command.OldPassword, command.NewPassword);
        }
    }
}