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
                    [Permission].Code as [{nameof(PermissionDto.Code)}]
                FROM [users].Users as [User]
                JOIN [users].RolePermissions as [RolePermission] ON [RolePermission].RoleCode = [User].RoleCode
                JOIN [users].Permissions as [Permission] ON [Permission].Code = [RolePermission].PermissionCode
                WHERE [User].Id = @Id
                """;

            var permissions = await connection.QueryAsync<PermissionDto>(
                sql, 
                new { _userContext.Id });

            return permissions.ToList();
        }
    }
}
