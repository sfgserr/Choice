using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Users.Application.OrderResponses.Commands.MarkResponsesAsInactive
{
    internal class MarkResponsesAsInactiveCommandHandler : ICommandHandler<MarkResponsesAsInactiveCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal MarkResponsesAsInactiveCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Execute(MarkResponsesAsInactiveCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                UPDATE users."OrderRequests"
                SET "IsActive" = false WHERE "RequestId" = @RequestId AND "Id" != @Id
                """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    Id = command.ResponseId,
                    command.RequestId
                });
        }
    }
}