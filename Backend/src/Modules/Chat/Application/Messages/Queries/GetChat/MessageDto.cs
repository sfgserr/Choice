namespace Chat.Application.Messages.Queries.GetChat
{
    public class MessageDto
    {
        public MessageDto(
            Guid id, 
            Guid toUserId, 
            Guid fromUserId, 
            string? content, 
            string type,
            Guid? orderResponseId, 
            DateTime creationDate,
            DateTime? enrollmentDate,
            bool isActive)
        {
            Id = id;
            ToUserId = toUserId;
            FromUserId = fromUserId;
            Content = content;
            Type = type;
            OrderResponseId = orderResponseId;
            CreationDate = creationDate;
            EnrollmentDate = enrollmentDate;
            IsActive = isActive;
        }

        public Guid Id { get; }

        public Guid ToUserId { get; }

        public Guid FromUserId { get; }

        public string? Content { get; }

        public string Type { get; }

        public Guid? OrderResponseId { get; }

        public DateTime CreationDate { get; }

        internal DateTime? EnrollmentDate { get; private set; }

        internal bool IsActive { get; private set; }
    }
}
