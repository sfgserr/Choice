using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Commands.AddReviewText
{
    internal class AddReviewTextCommandHandler : ICommandHandler<AddReviewTextCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal AddReviewTextCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Execute(AddReviewTextCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                INSERT INTO administration."ReviewTexts" ("Text", "Grade") 
                VALUES (@Text, @Grade); 
                """;

            await connection.ExecuteAsync(sql, command);
        }
    }
}