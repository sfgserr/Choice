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
                    users."Companies"."Id" as {nameof(CompanyDto.Id)},
                    users."Users"."IconUri" as {nameof(CompanyDto.IconUri)},
                    users."Users"."AverageGrade" as {nameof(CompanyDto.AverageGrade)},
                    users."Users"."Name" as {nameof(CompanyDto.Name)},
                    users."Companies"."Description" as {nameof(CompanyDto.Description)},
                    users."Users"."Email" as {nameof(CompanyDto.Email)},
                    users."Users"."PhoneNumber" as {nameof(CompanyDto.PhoneNumber)},
                    users."Users"."City" as {nameof(CompanyDto.City)},
                    users."Companies"."Street" as {nameof(CompanyDto.Street)},
                    users."Companies"."SocialMedias" as {nameof(CompanyDto.SocialMedias)},
                    users."Companies"."Email" as {nameof(CompanyDto.Email)},
                    users."Companies"."CategoriesId" as {nameof(CompanyDto.CategoryIds)},
                    users."Companies"."PhotoUris" as {nameof(CompanyDto.PhotoUris)},
                    users."Companies"."IsPrepaymentAvailable" as {nameof(CompanyDto.IsPrepaymentAvailable)}
                FROM users."Companies"
                JOIN users."Users" ON users."Users"."Id" = users."Companies"."Id"
                """;

            return await connection.QueryAsync<CompanyDto>(sql);
        }
    }
}