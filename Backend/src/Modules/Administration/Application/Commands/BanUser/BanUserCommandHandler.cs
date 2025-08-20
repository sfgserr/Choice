using Administration.IntegrationEvents;
using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Events;
using BuildingBlocks.Application.Exceptions;
using Dapper;

namespace Administration.Application.Commands.BanUser
{
    internal class BanUserCommandHandler : ICommandHandler<BanUserCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        
        internal BanUserCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Execute(BanUserCommand command)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = 
                $"""
                BEGIN;
                
                UPDATE identity."Users"
                SET "Banned" = true
                WHERE identity."Users"."Id" = @Id;

                UPDATE users."Users"
                SET "Banned" = true
                WHERE users."Users"."Id" = @Id;

                UPDATE chat."ChatUsers"
                SET "Banned" = true
                WHERE chat."ChatUsers"."Id" = @Id;
                
                COMMIT;
                """;

            int affections = await connection.ExecuteAsync(sql, command);
            
            if (affections == 0)
            {
                throw new InvalidCommandException(["User is not banned"]);
            }
        }
    }
}