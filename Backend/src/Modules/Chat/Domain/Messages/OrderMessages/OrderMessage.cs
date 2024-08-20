using BuildingBlocks.Domain;
using Chat.Domain.ChatUsers;

namespace Chat.Domain.Messages.OrderMessages
{
    public class OrderMessage : Entity
    {
        private OrderMessage(
            MessageId messageId, 
            ChatUserId clientId,
            ChatUserId companyId, 
            double price, 
            int deadline, 
            DateTime? enrollmentDate, 
            bool isEnrolled, 
            OrderMessageStatus status, 
            bool isEnrollmentDateConfirmed)
        {
            MessageId = messageId;
            ClientId = clientId;
            CompanyId = companyId;
            Price = price;
            Deadline = deadline;
            EnrollmentDate = enrollmentDate;
            IsEnrolled = isEnrolled;
            Status = status;
            IsEnrollmentDateConfirmed = isEnrollmentDateConfirmed;
        }

        private OrderMessage()
        {

        }

        public MessageId MessageId { get; }

        public ChatUserId ClientId { get; }

        public ChatUserId CompanyId { get; }

        public double Price { get; }

        public int Deadline { get; }

        public DateTime? EnrollmentDate { get; }

        public bool IsEnrolled { get; }

        public OrderMessageStatus Status { get; private set; }

        public bool IsEnrollmentDateConfirmed { get; }
    }
}
