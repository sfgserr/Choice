using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;
using Identity.Application.Contracts;
using Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Authentication.Authenticate
{
    internal class AuthenticateCommandHandler : ICommandHandlerWithResult<AuthenticateCommand, AuthenticationResult>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal AuthenticateCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<AuthenticationResult> Execute(AuthenticateCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                 SELECT 
                     identity."Users"."Id" as {nameof(UserDto.UserId)},
                     identity."Users"."Role" as {nameof(UserDto.UserType)},
                     identity."Users"."IsSubscribed" as {nameof(UserDto.IsSubscribed)},
                     identity."Users"."HashedPassword" as {nameof(UserDto.Password)}
                 FROM identity."Users"
                 WHERE identity."Users"."Email" = @Email 
                 """;

            var user = await connection.QuerySingleOrDefaultAsync<UserDto>(
                sql,
                new
                {
                    command.Email
                });

            if (user == null || !PasswordManager.VerifyHashedPassword(user.Password, command.Password))
            {
                return new AuthenticationResult("Неправильный логин или пароль");
            }
            
            return new AuthenticationResult(user);
        }
    }
}