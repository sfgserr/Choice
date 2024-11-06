using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Users.Application.OrderResponses.Commands.MarkResponseAsInactive
{
    internal class MarkResponseAsInactiveCommandHandler : ICommandHandler<MarkResponseAsInactiveCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal MarkResponseAsInactiveCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Execute(MarkResponseAsInactiveCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                 UPDATE users."OrderResponses"
                 SET "IsActive" = false WHERE "Id" = @Id
                 """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    Id = command.ResponseId
                });
        }
    }
}