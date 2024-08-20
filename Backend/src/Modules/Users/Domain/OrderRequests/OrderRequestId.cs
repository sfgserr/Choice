using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests
{
    public class OrderRequestId : TypedIdValueBase
    {
        public OrderRequestId(Guid value) : base(value)
        {

        }
    }
}
