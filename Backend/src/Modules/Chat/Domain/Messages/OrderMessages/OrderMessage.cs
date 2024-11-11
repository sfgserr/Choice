using BuildingBlocks.Domain;

namespace Chat.Domain.Messages.OrderMessages
{
    public class OrderMessage : Entity
    {
        private OrderMessage(OrderResponseId responseId, MessageId messageId, DateTime? enrollmentDate, bool isActive)
        {
            ResponseId = responseId;
            MessageId = messageId;
            EnrollmentDate = enrollmentDate;
            IsActive = isActive;
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

        public OrderResponseId ResponseId { get; }

        public MessageId MessageId { get; }

        public DateTime? EnrollmentDate { get; }

        public bool IsActive { get; private set; }

        public void SetAsInactive()
        {
            IsActive = false;
        }
    }
}
