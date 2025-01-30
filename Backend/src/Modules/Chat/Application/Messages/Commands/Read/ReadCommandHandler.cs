using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Chat.Application.Messages.Commands.Read
{
    internal class ReadCommandHandler : ICommandHandler<ReadCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal ReadCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Execute(ReadCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                UPDATE chat."Messages"
                SET chat."Messages"."IsRead" = TRUE
                WHERE chat."Messages"."Id" = @Id;
                """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    Id = command.MessageId
                });
        }
    }
}