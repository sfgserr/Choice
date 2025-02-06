using BuildingBlocks.Application.Data;
using Chat.Application.Contracts;
using Chat.Application.RealTimeMessaging;
using Dapper;

namespace Chat.Application.Messages.Commands.Read
{
    internal class ReadCommandHandler : RealTimeCommandHandlerBase<ReadCommand, Guid>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        
        internal ReadCommandHandler(ISqlConnectionFactory connectionFactory, IChatService chatService) : base(chatService)
        {
            _connectionFactory = connectionFactory;
        }

        protected override async Task<Guid> HandleCommandAsync(ReadCommand command)
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

            return command.MessageId;
        }

        protected override Guid GetUserId(ReadCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = "SELECT chat.\"Messages\".\"ToUserId\" FROM chat.\"Messages\"\nWHERE chat.\"Messages\".\"Id\" = @Id;";

            var id = connection.QuerySingle<Guid>(
                sql, 
                new
                {
                    Id = command.MessageId
                });

            return id; 
        }
    }
}