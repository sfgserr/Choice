using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Queries.GetCompanies
{
    internal class GetCompaniesQueryHandler : IQueryHandler<GetCompaniesQuery, IEnumerable<CompanyDto>>
    {
        private readonly ISqlConnectionFactory _factory;

        internal GetCompaniesQueryHandler(ISqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<CompanyDto>> Handle(GetCompaniesQuery query)
        {
            using var connection = _factory.GetConnection();
            
            const string sql = 
                $"""
                SELECT 
                    users."Users"."Id" as {nameof(CompanyDto.Id)},
                    users."Users"."IconUri" as {nameof(CompanyDto.IconUri)},
                    users."Users"."Name" as {nameof(CompanyDto.Name)},
                    users."Users"."City" as {nameof(CompanyDto.City)},
                    users."Users"."Street" as {nameof(CompanyDto.Street)}
                FROM users."Users"
                WHERE users."Users"."Role" = 'Company'
                """;

            return await connection.QueryAsync<CompanyDto>(sql);
        }
    }
}