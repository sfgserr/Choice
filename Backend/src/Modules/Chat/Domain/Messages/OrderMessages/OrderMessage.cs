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

        internal static OrderMessage Create(
            OrderResponseId responseId,
            MessageId messageId,
            DateTime? enrollmentDate)
        {
            return new OrderMessage(
                responseId,
                messageId,
                enrollmentDate,
                enrollmentDate is null);
        }

        internal OrderResponseId ResponseId { get; }

        internal MessageId MessageId { get; }

        internal DateTime? EnrollmentDate { get; private set; }

        internal bool IsActive { get; private set; }
    }
}
