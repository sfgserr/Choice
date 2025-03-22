using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Queries.GetCompany
{
    internal class GetCompanyQueryHandler : IQueryHandler<GetCompanyQuery, CompanyDto>
    {
        private readonly ISqlConnectionFactory _factory;

        internal GetCompanyQueryHandler(ISqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<CompanyDto> Handle(GetCompanyQuery query)
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
                    users."Users"."Street" as {nameof(CompanyDto.Street)},
                    users."Companies"."CategoriesId" as {nameof(CompanyDto.CategoryIds)},
                    users."Companies"."PhotoUris" as {nameof(CompanyDto.PhotoUris)},
                    users."Companies"."IsPrepaymentAvailable" as {nameof(CompanyDto.IsPrepaymentAvailable)}
                FROM users."Companies"
                JOIN users."Users" ON users."Users"."Id" = users."Companies"."Id"
                WHERE users."Companies"."Id" = @Id;

                SELECT 
                    users."SocialMedias"."Platform" as {nameof(SocialMediaDto.Platform)},
                    users."SocialMedias"."Url" as {nameof(SocialMediaDto.Url)}
                FROM users."SocialMedias"
                WHERE users."SocialMedias"."CompanyId" = @Id;
                """;

            var result = await connection.QueryMultipleAsync(
                sql,
                new
                {
                    Id = query.CompanyId
                });
            
            var company = result.ReadSingle<CompanyDto>();
            company.SocialMedias = result.Read<SocialMediaDto>();

            return company;
        }
    }
}