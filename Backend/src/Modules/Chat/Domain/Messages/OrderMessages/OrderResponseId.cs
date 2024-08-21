using BuildingBlocks.Domain;

namespace Chat.Domain.Messages.OrderMessages
{
    public class OrderResponseId : TypedIdValueBase
    {
        public OrderResponseId(Guid value) : base(value)
        {

        }
    }
}
