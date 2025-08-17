using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Identity.Application.Authorization.GetUser
{
    internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal GetUserQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<UserDto> Handle(GetUserQuery query)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql =
                $"""
                  SELECT 
                      identity."Users"."Role" as {nameof(UserDto.RoleCode)},
                      identity."Users"."City" as {nameof(UserDto.City)},
                      identity."Users"."Street" as {nameof(UserDto.Street)},
                      identity."Users"."Latitude" as {nameof(UserDto.Latitude)},
                      identity."Users"."Longitude" as {nameof(UserDto.Longitude)},
                      identity."Users"."IsSubscribed" as {nameof(UserDto.IsSubscribed)},
                      identity."Users"."Banned" as {nameof(UserDto.Banned)}
                  FROM identity."Users"
                  WHERE identity."Users"."Id" = @Id;

                  SELECT
                      identity."Permissions"."Code"
                  FROM identity."Users"
                  JOIN identity."RolePermissions" ON identity."RolePermissions"."RoleCode" = identity."Users"."Role"
                  JOIN identity."Permissions" ON identity."Permissions"."Code" = identity."RolePermissions"."PermissionCode"
                  WHERE identity."Users"."Id" = @Id; 
                  """;
                
            var result = await connection.QueryMultipleAsync(
                sql,
                new
                {
                    Id = query.UserId
                });
            
            var user = result.Read<UserDto>().First();
            user.Permissions.AddRange(result.Read<string>());

            return user;
        }
    }
}