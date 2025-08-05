using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Administration.Application.Queries.GetReviewTexts
{
    internal class GetReviewTextsQueryHandler : 
        IQueryHandler<GetReviewTextsQuery, IEnumerable<ReviewTextDto>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal GetReviewTextsQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ReviewTextDto>> Handle(GetReviewTextsQuery query)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = 
                $"""
                SELECT 
                    administration."ReviewTexts"."Id" as ${nameof(ReviewTextDto.Id)},
                    administration."ReviewTexts"."Grade" as ${nameof(ReviewTextDto.Grade)},
                    administration."ReviewTexts"."Text" as ${nameof(ReviewTextDto.Text)}
                FROM administration."ReviewTexts";
                """;

            return await connection.QueryAsync<ReviewTextDto>(sql);
        }
    } 
}