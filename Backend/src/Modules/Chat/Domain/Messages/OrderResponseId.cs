using BuildingBlocks.Domain;

namespace Chat.Domain.Messages
{
    public class OrderResponseId : TypedIdValueBase
    {
        public OrderResponseId(Guid value) : base(value)
        {

        }
    }
}
