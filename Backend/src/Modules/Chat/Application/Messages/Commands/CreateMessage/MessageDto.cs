namespace Chat.Application.Messages.Commands.CreateMessage
{
    public class MessageDto
    {
        public MessageDto(
            Guid id, 
            Guid fromUserId, 
            string? body, 
            bool isRead,
            string type, 
            Guid? orderResponseId, 
            DateTime creationDate, 
            DateTime? enrollmentDate, 
            bool? isActive)
        {
            Id = id;
            FromUserId = fromUserId;
            Body = body;
            IsRead = isRead;
            Type = type;
            OrderResponseId = orderResponseId;
            CreationDate = creationDate;
            EnrollmentDate = enrollmentDate;
            IsActive = isActive;
        }

        public Guid Id { get; }
        
        public Guid FromUserId { get; }

        public string? Body { get; }
        
        public bool IsRead { get; }
        
        public string Type { get; }

        public Guid? OrderResponseId { get; }

        public DateTime CreationDate { get; }

        public DateTime? EnrollmentDate { get; }

        public bool? IsActive { get; }
    }
}