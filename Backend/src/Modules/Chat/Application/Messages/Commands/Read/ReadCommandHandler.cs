using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Chat.Application.Contracts;
using Dapper;

namespace Chat.Application.Messages.Commands.Read
{
    internal class ReadCommandHandler : ICommandHandler<ReadCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IChatService _chatService;
        
        internal ReadCommandHandler(ISqlConnectionFactory connectionFactory, IChatService chatService)
        {
            _connectionFactory = connectionFactory;
            _chatService = chatService;
        }

        public async Task Execute(ReadCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            string sql = 
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

            sql = "SELECT chat.\"Messages\".\"ToUserId\" FROM chat.\"Messages\"\nWHERE chat.\"Messages\".\"Id\" = @Id;";

            var id = await connection.QuerySingleAsync<Guid>(
                sql, 
                new
                {
                    Id = command.MessageId
                });

            await _chatService.SendMessageRead(id, command.MessageId);
        }
    }
}