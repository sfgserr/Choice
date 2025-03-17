using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Queries.GetClients
{
    internal class GetClientsQueryHandler : IQueryHandler<GetClientsQuery, IEnumerable<ClientDto>>
    {
        private readonly ISqlConnectionFactory _factory;

        internal GetClientsQueryHandler(ISqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ClientDto>> Handle(GetClientsQuery query)
        {
            using var connection = _factory.GetConnection();
            
            const string sql = 
                $"""
                SELECT
                    users."Users"."Id" as {nameof(ClientDto.Id)},
                    users."Users"."IconUri" as {nameof(ClientDto.IconUri)},
                    users."Users"."Name" as {nameof(ClientDto.Name)},
                    users."Users"."City" as {nameof(ClientDto.City)},
                    users."Users"."Street" as {nameof(ClientDto.Street)}
                FROM users."Users"
                WHERE users."Users"."Role" = 'Client' 
                """;

            return await connection.QueryAsync<ClientDto>(sql);
        }
    }
}