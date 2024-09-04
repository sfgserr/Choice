using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.Users.Clients.Queries.GetClient
{
    internal class GetClientQueryHandler : IQueryHandler<GetClientQuery, ClientDto>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IUserContext _userContext;

        internal GetClientQueryHandler(ISqlConnectionFactory connectionFactory, IUserContext userContext)
        {
            _connectionFactory = connectionFactory;
            _userContext = userContext;
        }

        public async Task<ClientDto> Handle(GetClientQuery query)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = 
                $"""
                SELECT 
                    [User].Id as [{nameof(ClientDto.Id)}]
                    [User].Name as [{nameof(ClientDto.Name)}]
                    [User].IconUri as [{nameof(ClientDto.IconUri)}]
                    [User].Email as [{nameof(ClientDto.Email)}]
                    [User].PhoneNumber as [{nameof(ClientDto.PhoneNumber)}]
                    [User].City as [{nameof(ClientDto.City)}]
                    [User].Street as [{nameof(ClientDto.Street)}]
                FROM [users].Users as [User]
                WHERE [User].Id = @Id
                """;

            return await connection.QuerySingleAsync<ClientDto>(sql, new { _userContext.Id });
        }
    }
}
