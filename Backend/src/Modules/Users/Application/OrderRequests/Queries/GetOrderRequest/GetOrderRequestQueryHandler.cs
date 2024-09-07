using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.OrderRequests.Queries.GetOrderRequest
{
    internal class GetOrderRequestQueryHandler : IQueryHandler<GetOrderRequestQuery, OrderRequestDto>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IUserContext _userContext;

        internal GetOrderRequestQueryHandler(ISqlConnectionFactory connectionFactory, IUserContext userContext)
        {
            _connectionFactory = connectionFactory;
            _userContext = userContext;
        }

        public async Task<OrderRequestDto> Handle(GetOrderRequestQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                    SELECT 
                        users."OrderRequests"."Id" as [{nameof(OrderRequestDto.Id)}]
                        users."OrderRequests"."CategoryId" as [{nameof(OrderRequestDto.CategoryId)}]
                        users."OrderRequests"."Description" as [{nameof(OrderRequestDto.Description)}]
                        users."OrderRequests"."ToKnowPrice" as [{nameof(OrderRequestDto.ToKnowPrice)}]
                        users."OrderRequests"."ToKnowDeadline" as [{nameof(OrderRequestDto.ToKnowDeadline)}]
                        users."OrderRequests"."ToKnowEnrollmentDate" as [{nameof(OrderRequestDto.ToKnowEnrollmentDate)}]
                        users."OrderRequests"."PhotoUris" as [{nameof(OrderRequestDto.PhotoUris)}]
                        users."OrderRequests"."Distance" as [{nameof(OrderRequestDto.Distance)}]
                    FROM users."OrderRequests"
                    WHERE users."OrderRequests"."Id" = @RequestId AND users."OrderRequests"."ClientCreatedId" = @Id 
                """;

            return await connection.QuerySingleAsync<OrderRequestDto>(
                sql,
                new
                {
                    query.RequestId,
                    _userContext.Id
                });
        }
    }
}