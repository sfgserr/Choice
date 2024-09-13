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
                    users."Users"."IconUri" as {nameof(CompanyDto.IconUri)}
                    users."Users"."AverageGrade" as {nameof(CompanyDto.AverageGrade)}
                    users."Users"."Latitude" as {nameof(CompanyDto.Latitude)}
                    users."Users"."Longitude" as {nameof(CompanyDto.Longitude)}
                FROM users."Users"
                WHERE users."Users"."Id" = @Id AND users."Users"."UserRole" = @Role    
                """;

            var companies = await connection.QueryAsync<CompanyDto>(
                sql,
                new
                {
                    _userContext.Id,
                    Role = UserRole.Company.Value
                });

            return companies.ToList();
        }
    }
}