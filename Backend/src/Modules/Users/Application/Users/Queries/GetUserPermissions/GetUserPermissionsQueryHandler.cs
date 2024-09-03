using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.Users.Queries.GetUserPermissions
{
    internal class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, IList<PermissionDto>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IUserContext _userContext;

        internal GetUserPermissionsQueryHandler(ISqlConnectionFactory connectionFactory, IUserContext userContext)
        {
            _connectionFactory = connectionFactory;
            _userContext = userContext;
        }

        public async Task<IList<PermissionDto>> Handle(GetUserPermissionsQuery query)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql =
                $"""
                SELECT 
                    users."Permissions"."Code" as [{nameof(PermissionDto.Code)}]
                FROM users."Users"
                JOIN users."RolePermissions" ON users."RolePermissions"."RoleCode" = users."Users"."UserRole"
                JOIN users."Permissions" ON users."Permissions"."Code" = users."RolePermissions"."PermissionCode"
                WHERE users."Users"."Id" = @Id
                """;

            var permissions = await connection.QueryAsync<PermissionDto>(
                sql, 
                new { _userContext.Id });

            return permissions.ToList();
        }
    }
}
