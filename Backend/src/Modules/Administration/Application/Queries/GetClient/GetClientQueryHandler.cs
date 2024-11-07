using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Queries.GetClient
{
    internal class GetClientQueryHandler : IQueryHandler<GetClientQuery, ClientDto>
    {
        private readonly ISqlConnectionFactory _factory;

        internal GetClientQueryHandler(ISqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<ClientDto> Handle(GetClientQuery query)
        {
            using var connection = _factory.GetConnection();
            
            const string sql = 
                $"""
                 SELECT
                     users."Users"."Id" as {nameof(ClientDto.Id)},
                     users."Users"."IconUri" as {nameof(ClientDto.IconUri)},
                     users."Users"."AverageGrade" as {nameof(ClientDto.AverageGrade)},
                     users."Users"."Name" as {nameof(ClientDto.Name)},
                     users."Users"."PhoneNumber" as {nameof(ClientDto.PhoneNumber)},
                     users."Users"."Email" as {nameof(ClientDto.Email)},
                     users."Users"."City" as {nameof(ClientDto.City)},
                     users."Users"."Street" as {nameof(ClientDto.Street)}
                 FROM users."Users"
                 WHERE users."Users"."Id" = @Id 
                 """;

            return await connection.QuerySingleAsync<ClientDto>(
                sql,
                new
                {
                    Id = query.ClientId
                });
        }
    }
}