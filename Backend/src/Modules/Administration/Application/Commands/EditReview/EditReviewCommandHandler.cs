using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Commands.EditReview
{
    internal class EditReviewCommandHandler : ICommandHandler<EditReviewCommand>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal EditReviewCommandHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Execute(EditReviewCommand command)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                UPDATE users."Reviews" SET
                    "Text" = @Text,
                    "Grade" = @Grade
                WHERE users."Reviews"."Id" = @ReviewId
                """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    command.ReviewId,
                    command.Grade,
                    command.Text
                });
        }
    }
}