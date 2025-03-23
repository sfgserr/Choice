using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.GeoCoding;
using Dapper;

namespace Administration.Application.Commands.EditCompany
{
    internal class EditCompanyCommandHandler : ICommandHandler<EditCompanyCommand>
    {
        private readonly ISqlConnectionFactory _factory;
        private readonly IGeoCodingService _geoCodingService;
        
        internal EditCompanyCommandHandler(ISqlConnectionFactory factory, IGeoCodingService geoCodingService)
        {
            _factory = factory;
            _geoCodingService = geoCodingService;
        }

        public async Task Execute(EditCompanyCommand command)
        {
            using var connection = _factory.GetConnection();

            var coords = await _geoCodingService.GetCoords(command.City, command.Street);
            
            string sql = 
                $"""
                BEGIN;
                
                UPDATE users."Users"
                SET
                    "IconUri" = @IconUri,
                    "Name" = @Name,
                    "PhoneNumber" = @PhoneNumber,
                    "Email" = @Email,
                    "City" = @City,
                    "Street" = @Street,
                    "Latitude" = @Latitude,
                    "Longitude" = @Longitude
                WHERE "Id" = @Id;
                
                UPDATE users."Companies"
                SET
                    "Description" = @Description,
                    "CategoriesId" = @CategoryIds,
                    "PhotoUris" = @PhotoUris,
                    "IsPrepaymentAvailable" = @IsPrepaymentAvailable
                WHERE "Id" = @Id;
                
                DELETE FROM users."SocialMedias" WHERE users."SocialMedias"."CompanyId" = @Id;
                """;

            foreach (var url in command.SocialMedias) sql += " " + SocialMediaParser.Parse(url, command.Id);
            
            sql += " COMMIT;";
            
            await connection.ExecuteAsync(
                sql,
                new
                {
                    command.Id,
                    command.IconUri,
                    command.Name,
                    command.PhoneNumber,
                    command.Email,
                    command.City,
                    command.Street,
                    command.CategoryIds,
                    command.PhotoUris,
                    command.Description,
                    command.IsPrepaymentAvailable,
                    Latitude = coords[0],
                    Longitude = coords[1]
                });
        }
    }
}