using Administration.IntegrationEvents;
using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Events;
using BuildingBlocks.Application.Exceptions;
using Dapper;

namespace Administration.Application.Commands.UnbanUser
{
    internal class UnbanUserCommandHandler : ICommandHandler<UnbanUserCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        
        internal UnbanUserCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Execute(UnbanUserCommand command)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = 
                $"""
                UPDATE identity."Users"
                SET identity."Users"."Banned" = false
                WHERE identity."Users"."Id" = @Id;

                UPDATE users."Users"
                SET users."Users"."Banned" = false
                WHERE users."Users"."Id" = @Id;

                UPDATE chat."ChatUsers"
                SET chat."ChatUsers"."Banned" = false
                WHERE chat."ChatUsers"."Id" = @Id;
                """;

            int affections = await connection.ExecuteAsync(sql, command);
            
            if (affections == 0)
            {
                throw new InvalidCommandException(["User is not banned"]);
            }
        }
    }
}