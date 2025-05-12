using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Users.Application.OrderResponses.Queries.GetReviews
{
    internal class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery, IEnumerable<ReviewDto>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal GetReviewsQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ReviewDto>> Handle(GetReviewsQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                SELECT 
                    users."Users"."Name" as {nameof(ReviewDto.Name)},
                    users."Reviews"."Text" as {nameof(ReviewDto.Text)},
                    users."Reviews"."Grade" as {nameof(ReviewDto.Grade)}
                FROM users."Reviews"
                JOIN users."Users" ON users."Users"."Id" = users."Reviews"."AuthorId"
                WHERE users."Reviews"."ToUserId" = @ToUserId
                """;

            return await connection.QueryAsync<ReviewDto>(
                sql,
                new
                {
                    query.ToUserId
                });
        }
    }
}