using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Application.OrderRequests.Queries.GetOrderRequestsInRadius
{
    internal class GetOrderRequestsInRadiusQueryHandler :
        IQueryHandler<GetOrderRequestsInRadiusQuery, IEnumerable<OrderRequestDto>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IGeoService _geoService;
        private readonly IUserContext _userContext;
        
        internal GetOrderRequestsInRadiusQueryHandler(
            ISqlConnectionFactory connectionFactory, 
            IGeoService geoService, 
            IUserContext userContext)
        {
            _connectionFactory = connectionFactory;
            _geoService = geoService;
            _userContext = userContext;
        }

        public async Task<IEnumerable<OrderRequestDto>> Handle(GetOrderRequestsInRadiusQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                SELECT
                    users."OrderRequests"."Id" as [{nameof(OrderRequestDto.Id)}]
                    users."OrderRequests"."CategoryId" as [{nameof(OrderRequestDto.CategoryId)}]
                    users."OrderRequests"."Description" as [{nameof(OrderRequestDto.Description)}]
                    users."OrderRequests"."PhotoUris" as [{nameof(OrderRequestDto.PhotoUris)}]
                    users."Users"."Name" as [{nameof(OrderRequestDto.ClientName)}]
                    users."OrderRequests"."ClientCreatedId" as [{nameof(OrderRequestDto.Description)}]
                    users."Users"."IconUri" as [{nameof(OrderRequestDto.IconUri)}]
                    users."Users"."Latitude" as [{nameof(OrderRequestDto.Latitude)}]
                    users."Users"."Longitude" as [{nameof(OrderRequestDto.Longitude)}]
                    users."Users"."ReviewsCount" as [{nameof(OrderRequestDto.ReviewCount)}]
                    users."OrderRequests"."AverageGrade" as [{nameof(OrderRequestDto.AverageGrade)}]
                    users."OrderRequests"."Distance" as [{nameof(OrderRequestDto.Distance)}]
                FROM users."OrderRequests"
                JOIN users."OrderRequests" ON users."OrderRequests"."ClientCreatedId" = users."Users"."Id"            
                """;

            var orderRequests = await connection.QueryAsync<OrderRequestDto>(sql);

            var requestsInRadius = new List<OrderRequestDto>();

            foreach (var request in orderRequests)
            {
                var distance = await _geoService.GetDistance(
                    new(request.Latitude, request.Latitude),
                    _userContext.Address.Coords);

                if (request.Distance <= distance)
                    requestsInRadius.Add(request);
            }

            return requestsInRadius;
        }
    }
}