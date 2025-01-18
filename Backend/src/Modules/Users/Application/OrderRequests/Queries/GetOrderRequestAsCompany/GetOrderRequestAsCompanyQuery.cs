using BuildingBlocks.Application.Cqrs.Queries;

namespace Users.Application.OrderRequests.Queries.GetOrderRequestAsCompany
{
    public class GetOrderRequestAsCompanyQuery : IQuery<OrderRequestDto>
    {
        public GetOrderRequestAsCompanyQuery(Guid orderRequestId)
        {
            OrderRequestId = orderRequestId;
        }

        public Guid OrderRequestId { get; }
    }
}