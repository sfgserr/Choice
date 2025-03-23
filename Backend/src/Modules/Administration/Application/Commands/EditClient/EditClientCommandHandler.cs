using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.GeoCoding;
using Dapper;

namespace Administration.Application.Commands.EditClient
{
    internal class EditClientCommandHandler : ICommandHandler<EditClientCommand>
    {
        private readonly ISqlConnectionFactory _factory;
        private readonly IGeoCodingService _geoCodingService;
        
        internal EditClientCommandHandler(ISqlConnectionFactory factory, IGeoCodingService geoCodingService)
        {
            _factory = factory;
            _geoCodingService = geoCodingService;
        }

        public async Task Execute(EditClientCommand command)
        {
            using var connection = _factory.GetConnection();

            var coords = await _geoCodingService.GetCoords(command.City, command.Street);
            
            const string sql = 
                $"""
                UPDATE users."Users" SET
                    "IconUri" = @IconUri,
                    "Name" = @Name,
                    "PhoneNumber" = @PhoneNumber,
                    "Email" = @Email,
                    "City" = @City,
                    "Street" = @Street,
                    "Latitude" = @Latitude,
                    "Longitude" = @Longitude
                WHERE "Id" = @ClientId
                """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    command.ClientId,
                    command.IconUri,
                    command.Name,
                    command.PhoneNumber,
                    command.Email,
                    command.City,
                    command.Street,
                    Latitude = coords[0],
                    Longitude = coords[1],
                });
        }
    }
}