using BuildingBlocks.Domain;

namespace Chat.Domain.Messages.OrderMessages
{
    public class OrderMessage : Entity
    {
        private DateTime? _enrollmentDate;

        private bool _isActive;
        
        private OrderMessage(OrderResponseId orderResponseId, MessageId messageId, DateTime? enrollmentDate, bool isActive)
        {
            OrderResponseId = orderResponseId;
            MessageId = messageId;
            
            _enrollmentDate = enrollmentDate;
            _isActive = isActive;
        }

        private OrderMessage()
        {
            
        }

        internal static OrderMessage Create(
            OrderResponseId responseId,
            MessageId messageId,
            DateTime? enrollmentDate)
        {
            return new OrderMessage(
                responseId,
                messageId,
                enrollmentDate,
                true);
        }

        public OrderResponseId OrderResponseId { get; }

        public MessageId MessageId { get; }

        public void SetAsInactive()
        {
            _isActive = false;
        }
    }
}
