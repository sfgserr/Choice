using BuildingBlocks.Domain;

namespace Chat.Domain.Messages
{
    public class MessageId : TypedIdValueBase
    {
        public MessageId(Guid value) : base(value)
        {

        }
    }
}
