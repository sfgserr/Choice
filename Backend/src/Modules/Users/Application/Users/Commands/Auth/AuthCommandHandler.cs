using BuildingBlocks.Application.Cqrs.Commands;
using Microsoft.EntityFrameworkCore;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Application.Users.Commands.Auth
{
    internal class AuthCommandHandler : ICommandHandlerWithResult<AuthCommand, AuthResult>
    {
        private readonly IUsersDbContext _dbContext;

        internal AuthCommandHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthResult> Execute(AuthCommand command)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == command.Email);

            if (user is not null && PasswordManager.VerifyHashedPassword(user.HashedPassword, command.Password))
            {
                return new AuthResult(user.Id.Value, user.Role.Value);
            }

            return new AuthResult("Email or password is wrong");
        }
    }
}
