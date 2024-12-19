using BuildingBlocks.Application.Cqrs.Commands;
using Identity.Application.Contracts;
using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Authentication.Authenticate
{
    internal class AuthenticateCommandHandler : ICommandHandlerWithResult<AuthenticateCommand, AuthenticationResult>
    {
        private readonly IIdentityDbContext _dbContext;

        internal AuthenticateCommandHandler(IIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthenticationResult> Execute(AuthenticateCommand command)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == command.Email);

            if (user == null || !PasswordManager.VerifyHashedPassword(user.HashedPassword, command.Password))
            {
                return new AuthenticationResult("User doesn't exist or password is not matched");
            }

            return new AuthenticationResult(new UserDto(user.Id.Value, user.Role.Value));
        }
    }
}