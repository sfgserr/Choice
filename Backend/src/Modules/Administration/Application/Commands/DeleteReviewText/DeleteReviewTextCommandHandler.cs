using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Commands.DeleteReviewText
{
    internal class DeleteReviewTextCommandHandler : ICommandHandler<DeleteReviewTextCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal DeleteReviewTextCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Execute(DeleteReviewTextCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                DELETE FROM administration."ReviewTexts"
                WHERE "Id" = @Id;
                """;

            await connection.ExecuteAsync(sql, command);
        }
    }
}
