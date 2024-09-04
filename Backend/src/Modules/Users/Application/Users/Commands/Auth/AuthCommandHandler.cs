using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.Users;

namespace Users.Application.Users.Commands.Auth
{
    internal class AuthCommandHandler : ICommandHandlerWithResult<AuthCommand, AuthResult>
    {
        private readonly IUserRepository _repository;

        internal AuthCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuthResult> Execute(AuthCommand command)
        {
            var user = await _repository.GetByEmail(command.Email);

            if (user is not null && PasswordManager.VerifyHashedPassword(user.HashedPassword, command.Password))
            {
                return new AuthResult(user.Id.Value, user.Role.Value);
            }

            return new AuthResult("Email or password is wrong");
        }
    }
}
