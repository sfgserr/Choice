using BuildingBlocks.Application.Cqrs.Queries;

namespace Users.Application.OrderResponses.Queries.GetOrderResponse
{
    public class GetOrderResponseQuery : IQuery<OrderResponseDto>
    {
        public GetOrderResponseQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}