using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.Users.Clients.Queries.GetClient
{
    internal class GetClientQueryHandler : IQueryHandler<GetClientQuery, GetClientDto>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IUserContext _userContext;

        internal GetClientQueryHandler(ISqlConnectionFactory connectionFactory, IUserContext userContext)
        {
            _connectionFactory = connectionFactory;
            _userContext = userContext;
        }

        public async Task<GetClientDto> Handle(GetClientQuery query)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = 
                $"""
                SELECT 
                    [User].Id as [{nameof(GetClientDto.Id)}]
                    [User].Name as [{nameof(GetClientDto.Name)}]
                    [User].IconUri as [{nameof(GetClientDto.IconUri)}]
                    [User].Email as [{nameof(GetClientDto.Email)}]
                    [User].PhoneNumber as [{nameof(GetClientDto.PhoneNumber)}]
                    [User].City as [{nameof(GetClientDto.City)}]
                    [User].Street as [{nameof(GetClientDto.Street)}]
                FROM [users].Users as [User]
                WHERE [User].Id = @Id
                """;

            return await connection.QuerySingleAsync<GetClientDto>(sql, new { _userContext.Id });
        }
    }
}
