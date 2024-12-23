using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.Companies.Queries.GetCompanies
{
    internal class GetCompaniesQueryHandler : IQueryHandler<GetCompaniesQuery, IList<CompanyDto>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IUserContext _userContext;

        internal GetCompaniesQueryHandler(ISqlConnectionFactory connectionFactory, IUserContext userContext)
        {
            _connectionFactory = connectionFactory;
            _userContext = userContext;
        }

        public async Task<IList<CompanyDto>> Handle(GetCompaniesQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                SELECT 
                    users."Users"."IconUri" as {nameof(CompanyDto.IconUri)},
                    users."Users"."AverageGrade" as {nameof(CompanyDto.AverageGrade)},
                    users."Users"."Latitude" as {nameof(CompanyDto.Latitude)},
                    users."Users"."Longitude" as {nameof(CompanyDto.Longitude)}
                FROM users."Companies"
                JOIN users."Users" ON users."Users"."Id" = users."Companies"."Id"
                WHERE @CategoryId = ANY(users."Companies"."CategoriesId")
                
                UNION
                
                SELECT 
                    users."Users"."IconUri" as {nameof(CompanyDto.IconUri)},
                    users."Users"."AverageGrade" as {nameof(CompanyDto.AverageGrade)},
                    users."Users"."Latitude" as {nameof(CompanyDto.Latitude)},
                    users."Users"."Longitude" as {nameof(CompanyDto.Longitude)}
                FROM users."Users"
                WHERE users."Users"."Id" = @Id; 
                """;
            
            var companies = await connection.QueryAsync<CompanyDto>(
                sql,
                new
                {
                    query.CategoryId,
                    Id = _userContext.ClientId.Value
                });
            
            return companies.ToList();
        }
    }
}