using BuildingBlocks.Domain;

namespace Chat.Domain.Messages.Events
{
    public class MessageCreatedDomainEvent : DomainEventBase
    {
        public MessageCreatedDomainEvent(MessageId messageId)
        {
            MessageId = messageId;
        }

        public MessageId MessageId { get; }
    }
}
