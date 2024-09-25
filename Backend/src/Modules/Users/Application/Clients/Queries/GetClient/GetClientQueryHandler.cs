using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.Clients.Queries.GetClient
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
                    users."Users"."Id" as {nameof(ClientDto.Id)},
                    users."Users"."Name" as {nameof(ClientDto.Name)},
                    users."Users"."IconUri" as {nameof(ClientDto.IconUri)},
                    users."Users"."Email" as {nameof(ClientDto.Email)},
                    users."Users"."PhoneNumber" as {nameof(ClientDto.PhoneNumber)},
                    users."Users"."City" as {nameof(ClientDto.City)},
                    users."Users"."Street" as {nameof(ClientDto.Street)}
                FROM users."Users"
                WHERE users."Users"."Id" = @Id
                """;

            return await connection.QuerySingleAsync<ClientDto>(sql, new { Id = _userContext.Id.Value });
        }
    }
}
