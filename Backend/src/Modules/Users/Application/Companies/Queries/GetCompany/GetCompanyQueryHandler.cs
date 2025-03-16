using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.Companies.Queries.GetCompany
{
    internal class GetCompanyQueryHandler : IQueryHandler<GetCompanyQuery, CompanyDto>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IUserContext _userContext;
    
        internal GetCompanyQueryHandler(ISqlConnectionFactory connectionFactory, IUserContext userContext)
        { 
            _connectionFactory = connectionFactory;
            _userContext = userContext;
        }

        public async Task<CompanyDto> Handle(GetCompanyQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                SELECT 
                    users."Users"."Id" as {nameof(CompanyDto.Id)},
                    users."Users"."Name" as {nameof(CompanyDto.Name)},
                    users."Users"."Email" as {nameof(CompanyDto.Email)},
                    users."Users"."PhoneNumber" as {nameof(CompanyDto.PhoneNumber)},
                    users."Companies"."Description" as {nameof(CompanyDto.Description)},
                    users."Companies"."PhotoUris" as {nameof(CompanyDto.PhotoUris)},
                    users."Companies"."CategoriesId" as {nameof(CompanyDto.Categories)},
                    users."Users"."City" as {nameof(CompanyDto.City)},
                    users."Users"."Street" as {nameof(CompanyDto.Street)},
                    users."Users"."IconUri" as {nameof(CompanyDto.IconUri)},
                    users."Companies"."IsPrepaymentAvailable" as {nameof(CompanyDto.IsPrepaymentAvailable)}
                FROM users."Users"
                JOIN users."Companies" ON users."Companies"."Id" = users."Users"."Id"
                WHERE users."Users"."Id" = @Id;
                
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
                    Id = _userContext.CompanyId.Value
                });
            
            var company = result.ReadSingle<CompanyDto>();
            company.SocialMedias = result.Read<SocialMediaDto>();

            return company;
        }
    }
}