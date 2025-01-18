using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Users.Application.OrderRequests.Queries.GetOrderRequestAsCompany
{
    internal class GetOrderRequestAsCompanyQueryHandler : IQueryHandler<GetOrderRequestAsCompanyQuery, OrderRequestDto>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal GetOrderRequestAsCompanyQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<OrderRequestDto> Handle(GetOrderRequestAsCompanyQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                SELECT
                    users."OrderRequests"."Id" as {nameof(OrderRequestDto.Id)},
                    users."OrderRequests"."ToKnowPrice" as {nameof(OrderRequestDto.ToKnowPrice)},
                    users."OrderRequests"."ToKnowDeadline" as {nameof(OrderRequestDto.ToKnowDeadline)},
                    users."OrderRequests"."ToKnowEnrollmentDate" as {nameof(OrderRequestDto.ToKnowEnrollmentDate)}
                FROM users."OrderRequests"
                WHERE users."OrderRequests"."Id" = @OrderRequestId 
                """;

            return await connection.QueryFirstAsync<OrderRequestDto>(
                sql,
                new
                {
                    query.OrderRequestId
                });
        }
    }
}