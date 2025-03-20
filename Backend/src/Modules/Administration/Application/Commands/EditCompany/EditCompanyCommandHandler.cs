using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Commands.EditCompany
{
    internal class EditCompanyCommandHandler : ICommandHandler<EditCompanyCommand>
    {
        private readonly ISqlConnectionFactory _factory;
        
        internal EditCompanyCommandHandler(ISqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task Execute(EditCompanyCommand command)
        {
            using var connection = _factory.GetConnection();
            
            const string sql = 
                $"""
                BEGIN;
                
                UPDATE users."Users"
                SET
                    "IconUri" = @IconUri,
                    "Name" = @Name,
                    "PhoneNumber" = @PhoneNumber,
                    "Email" = @Email,
                    "City" = @City,
                    "Street" = @Street
                WHERE "Id" = @Id;
                
                UPDATE users."Companies"
                SET
                    "Description" = @Description,
                    "SocialMedias" = @SocialMedias,
                    "CategoriesId" = @CategoryIds,
                    "PhotoUris" = @PhotoUris,
                    "IsPrepaymentAvailable" = @IsPrepaymentAvailable
                WHERE "Id" = @Id;
                
                COMMIT;
                """;

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
                    command.SocialMedias,
                    command.CategoryIds,
                    command.PhotoUris,
                    command.Description,
                    command.IsPrepaymentAvailable
                });
        }
    }
}