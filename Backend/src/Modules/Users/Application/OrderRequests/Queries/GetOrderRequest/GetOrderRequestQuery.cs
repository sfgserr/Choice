using BuildingBlocks.Application.Cqrs.Queries;

namespace Users.Application.OrderRequests.Queries.GetOrderRequest
{
    public class GetOrderRequestQuery : IQuery<OrderRequestDto>
    {
        public GetOrderRequestQuery(Guid requestId)
        {
            RequestId = requestId;
        }

        public Guid RequestId { get; }
    }
}