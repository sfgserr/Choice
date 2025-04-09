using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;
using Identity.Domain.Users;

namespace Identity.Application.Authentication.Phone.VerifyCode
{
    internal class VerifyCodeCommandHandler : ICommandHandlerWithResult<VerifyCodeCommand, AuthenticationResult>
    {
        private readonly IUserContext _userContext;
        private readonly ISqlConnectionFactory _connectionFactory;
        
        internal VerifyCodeCommandHandler(IUserContext userContext, ISqlConnectionFactory connectionFactory)
        {
            _userContext = userContext;
            _connectionFactory = connectionFactory;
        }

        public async Task<AuthenticationResult> Execute(VerifyCodeCommand command)
        {
            if (!CodeStore.Verify(_userContext.Id.Value, command.Code)) return new AuthenticationResult("Неверный код");
            
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                 SELECT 
                     identity."Users"."Id" as {nameof(UserDto.UserId)},
                     identity."Users"."Role" as {nameof(UserDto.UserType)},
                     identity."Users"."IsSubscribed" as {nameof(UserDto.IsSubscribed)},
                     identity."Users"."HashedPassword" as {nameof(UserDto.Password)}
                 FROM identity."Users"
                 WHERE identity."Users"."Id" = @Id 
                 """;

            var user = await connection.QuerySingleOrDefaultAsync<UserDto>(
                sql,
                new
                {
                    Id = _userContext.Id.Value
                });

            return new AuthenticationResult(user!);
        }
    }
}