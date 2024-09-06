using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.OrderRequests.Queries.GetOrderRequestsQuery
{
    internal class GetOrderRequestsQueryHandler : IQueryHandler<GetOrderRequestsQuery, IEnumerable<OrderRequestDto>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IUserContext _userContext;
        
        internal GetOrderRequestsQueryHandler(ISqlConnectionFactory connectionFactory, IUserContext userContext)
        {
            _connectionFactory = connectionFactory;
            _userContext = userContext;
        }

        public async Task<IEnumerable<OrderRequestDto>> Handle(GetOrderRequestsQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                SELECT
                    users."OrderRequests"."Id" as [{nameof(OrderRequestDto.Id)}]
                    users."OrderRequests"."Status" as [{nameof(OrderRequestDto.OrderStatus)}]
                    users."OrderRequests"."Description" as [{nameof(OrderRequestDto.Description)}]
                    users."OrderRequests"."CreationDate" as [{nameof(OrderRequestDto.CreationDate)}]
                    administration."Categories"."Title" as [{nameof(OrderRequestDto.CategoryTitle)}]
                FROM "users"."OrderRequests"
                JOIN "users"."OrderRequests"."CategoryId" ON "users"."OrderRequests"."CategoryId" = "administration"."Categories"."Id"
                WHERE "users"."OrderRequests"."ClientCreatedId" = @Id    
                """;

            return await connection.QueryAsync<OrderRequestDto>(
                sql,
                new
                {
                    _userContext.Id
                });
        }
    }
}