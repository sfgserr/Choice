using BuildingBlocks.Domain;
using Chat.Domain.Messages.OrderMessages;

namespace Chat.Domain.Messages.Events
{
    public class IsActiveChangedDomainEvent : DomainEventBase
    {
        public IsActiveChangedDomainEvent(OrderResponseId responseId)
        {
            ResponseId = responseId;
        }

        public OrderResponseId ResponseId { get; }

        public bool IsActive { get; }  
    }
}
