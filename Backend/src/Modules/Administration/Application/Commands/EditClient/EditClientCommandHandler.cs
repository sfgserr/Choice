using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Commands.EditClient
{
    internal class EditClientCommandHandler : ICommandHandler<EditClientCommand>
    {
        private readonly ISqlConnectionFactory _factory;
        
        internal EditClientCommandHandler(ISqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task Execute(EditClientCommand command)
        {
            using var connection = _factory.GetConnection();
            
            const string sql = 
                $"""
                UPDATE users."Users" SET
                    "IconUri" = @IconUri,
                    "Name" = @Name,
                    "PhoneNumber" = @PhoneNumber,
                    "Email" = @Email,
                    "City" = @City,
                    "Street" = @Street
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
                    command.Street
                });
        }
    }
}