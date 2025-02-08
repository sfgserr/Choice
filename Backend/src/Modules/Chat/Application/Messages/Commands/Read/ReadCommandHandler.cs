using BuildingBlocks.Application.Data;
using Chat.Application.Contracts;
using Chat.Application.RealTimeMessaging;
using Dapper;

namespace Chat.Application.Messages.Commands.Read
{
    internal class ReadCommandHandler : RealTimeCommandHandlerBase<ReadCommand, Guid>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        
        internal ReadCommandHandler(IChatService chatService, ISqlConnectionFactory connectionFactory) : 
            base(chatService, "read")
        {
            _connectionFactory = connectionFactory;
        }

        protected override async Task<Guid> HandleCommandAsync(ReadCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                UPDATE chat."Messages" 
                SET "IsRead" = TRUE
                WHERE "Id" = @MessageId 
                """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    command.MessageId
                });
            
            return command.MessageId;
        }

        protected override Guid GetUserId(ReadCommand command)
        {
            return command.UserId; 
        }
    }
}