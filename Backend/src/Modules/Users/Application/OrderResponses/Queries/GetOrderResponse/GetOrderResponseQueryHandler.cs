using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Users.Application.OrderResponses.Queries.GetOrderResponse
{
    internal class GetOrderResponseQueryHandler : IQueryHandler<GetOrderResponseQuery, OrderResponseDto>
    {
        private readonly ISqlConnectionFactory _factory;

        internal GetOrderResponseQueryHandler(ISqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<OrderResponseDto> Handle(GetOrderResponseQuery query)
        {
            using var connection = _factory.GetConnection();
            
            const string sql = 
                $"""
                SELECT
                    users."OrderResponses"."Id" as {nameof(OrderResponseDto.Id)},
                    users."OrderResponses"."ClientId" as {nameof(OrderResponseDto.ClientId)},
                    users."OrderResponses"."CompanyId" as {nameof(OrderResponseDto.CompanyId)},
                    users."OrderResponses"."RequestId" as {nameof(OrderResponseDto.RequestId)},
                    users."OrderResponses"."Price" as {nameof(OrderResponseDto.Price)},
                    users."OrderResponses"."Deadline" as {nameof(OrderResponseDto.Deadline)},
                    users."OrderResponses"."EnrollmentDate" as {nameof(OrderResponseDto.EnrollmentDate)},
                    users."OrderResponses"."Prepayment" as {nameof(OrderResponseDto.Prepayment)},
                    users."OrderResponses"."Status" as {nameof(OrderResponseDto.Status)},
                    users."OrderResponses"."IsPaid" as {nameof(OrderResponseDto.IsPaid)},
                    users."OrderResponses"."IsEnrolled" as {nameof(OrderResponseDto.IsEnrolled)},
                    users."OrderResponses"."IsEnrollmentDateConfirmed" as {nameof(OrderResponseDto.IsEnrollmentDateConfirmed)},
                    users."OrderResponses"."UserChangedEnrollmentDate" as {nameof(OrderResponseDto.UserChangedEnrollmentDate)},
                    users."OrderResponses"."IsActive" as {nameof(OrderResponseDto.IsActive)}
                FROM users."OrderResponses"
                WHERE users."OrderResponses"."Id" = @Id 
                """;

            return await connection.QuerySingleAsync<OrderResponseDto>(
                sql,
                new
                {
                    query.Id
                });
        }
    }
}