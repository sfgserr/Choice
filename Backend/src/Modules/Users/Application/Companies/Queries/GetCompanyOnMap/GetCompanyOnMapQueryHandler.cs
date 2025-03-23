using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Extensions;
using Dapper;
using Users.Application.Contracts;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Companies.Queries.GetCompanyOnMap
{
    internal class GetCompanyOnMapQueryHandler : IQueryHandler<GetCompanyOnMapQuery, CompanyDto>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IDistanceService _distanceService;
        private readonly IUserContext _userContext;
        
        internal GetCompanyOnMapQueryHandler(
            ISqlConnectionFactory connectionFactory,
            IDistanceService distanceService, 
            IUserContext userContext)
        {
            _connectionFactory = connectionFactory;
            _distanceService = distanceService;
            _userContext = userContext;
        }

        public async Task<CompanyDto> Handle(GetCompanyOnMapQuery query)
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
                     users."Users"."IconUri" as {nameof(CompanyDto.IconUri)},
                     users."Users"."City" as {nameof(CompanyDto.City)},
                     users."Users"."Street" as {nameof(CompanyDto.Street)},
                     users."Users"."ReviewsCount" as {nameof(CompanyDto.ReviewsCount)},
                     users."Users"."AverageGrade" as {nameof(CompanyDto.AverageGrade)},
                     users."Users"."Latitude" as {nameof(CompanyDto.Latitude)},
                     users."Users"."Longitude" as {nameof(CompanyDto.Longitude)}
                 FROM users."Users"
                 JOIN users."Companies" ON users."Companies"."Id" = users."Users"."Id"
                 WHERE users."Users"."Id" = @CompanyId;

                 SELECT 
                     users."SocialMedias"."Platform" as {nameof(SocialMediaDto.Platform)},
                     users."SocialMedias"."Url" as {nameof(SocialMediaDto.Url)}
                 FROM users."SocialMedias"
                 WHERE users."SocialMedias"."CompanyId" = @CompanyId;
                 """;

            var result = await connection.QueryMultipleAsync(
                sql,
                new
                {
                    query.CompanyId
                });
            
            var company = result.ReadSingle<CompanyDto>();
            
            company.SocialMedias = result.Read<SocialMediaDto>();
            company.Distance = _distanceService.GetDistance(
                _userContext.Address.Coords, 
                new Coords(company.Latitude, company.Longitude));

            return company;
        }
    }
}
