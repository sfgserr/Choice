using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.Users.Queries.GetAuthorizedUser
{
    internal class GetAuthorizedUserQueryHandler : IQueryHandler<GetAuthorizedUserQuery, GetAuthorizedUserDto>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IUserContext _userContext;

        internal GetAuthorizedUserQueryHandler(ISqlConnectionFactory connectionFactory, IUserContext userContext)
        {
            _connectionFactory = connectionFactory;
            _userContext = userContext;
        }

        public async Task<GetAuthorizedUserDto> Handle(GetAuthorizedUserQuery query)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql =
                $"""
                SELECT
                    [User].IsDataFilled as [{nameof(GetAuthorizedUserDto.IsDataFilled)}]
                    [Permission].Code as [{nameof(PermissionDto.Code)}]
                    [Permission].IsDataFillingRequired as [{nameof(PermissionDto.IsDataFillingRequired)}]
                FROM [users].Users as [User]
                JOIN [users].RolePermissions as [RolePermission] ON [User].RoleCode = [RolePermission].RoleCode
                JOIN [users].Permissions as [Permission] ON [Permission].Code = [RolePermission].PermissionCode
                WHERE [User].Id = @Id
                """;

            var authorizedUser = await connection.QuerySingleAsync<GetAuthorizedUserDto>(
                sql, 
                new { _userContext.Id });

            return authorizedUser;
        }
    }
}
