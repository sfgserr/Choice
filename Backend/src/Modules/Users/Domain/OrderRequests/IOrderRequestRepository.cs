using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests
{
    public interface IOrderRequestRepository : IRepository<OrderRequest>
    {
        Task<OrderRequest> Get(OrderRequestId requestId);
    }
}
