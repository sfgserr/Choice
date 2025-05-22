using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Queries.GetAuthorReviews
{
    internal class GetAuthorReviewsQueryHandler : IQueryHandler<GetAuthorReviewsQuery, IEnumerable<ReviewDto>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal GetAuthorReviewsQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ReviewDto>> Handle(GetAuthorReviewsQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                 SELECT
                     users."Reviews"."Id" as {nameof(ReviewDto.Id)},
                     users."Users"."Name" as {nameof(ReviewDto.Name)},
                     users."Reviews"."Text" as {nameof(ReviewDto.Text)},
                     users."Reviews"."Grade" as {nameof(ReviewDto.Grade)}
                 FROM users."Reviews"
                 JOIN users."Users" ON users."Users"."Id" = users."Reviews"."AuthorId"
                 WHERE users."Reviews"."AuthorId" = @AuthorId
                 """;

            return await connection.QueryAsync<ReviewDto>(
                sql,
                new
                {
                    query.AuthorId
                });
        }
    }
}