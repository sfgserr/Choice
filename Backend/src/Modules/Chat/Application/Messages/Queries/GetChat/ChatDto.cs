using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Application.Messages.Queries.GetChat
{
    public class ChatDto
    {
        public UserDto User { get; set; }
        
        public IEnumerable<MessageDto> Messages { get; set; }
    }
    
    public class MessageDto
    {
        public MessageDto(
            Guid id, 
            Guid fromUserId, 
            Guid toUserId,
            string? body, 
            bool isRead, 
            string type, 
            Guid? orderResponseId, 
            DateTime creationDate, 
            DateTime? enrollmentDate, 
            bool? isActive, 
            OrderResponseDto? orderResponse)
        {
            Id = id;
            FromUserId = fromUserId;
            ToUserId = toUserId;
            Body = body;
            IsRead = isRead;
            Type = type;
            OrderResponseId = orderResponseId;
            CreationDate = creationDate;
            EnrollmentDate = enrollmentDate;
            IsActive = isActive;
            OrderResponse = orderResponse;
        }
        
        public MessageDto() {}
        
        public Guid Id { get; }
        
        public Guid FromUserId { get; }

        public Guid ToUserId { get; }

        public string? Body { get; }
        
        public bool IsRead { get; }
        
        public string Type { get; }

        public Guid? OrderResponseId { get; }

        public DateTime CreationDate { get; }

        public DateTime? EnrollmentDate { get; }

        public bool? IsActive { get; }
        
        public OrderResponseDto? OrderResponse { get; set; }
    }
    
    public class UserDto
    {
        public Guid Id { get; }
        
        public string IconUri { get; }
        
        public string Name { get; }
    }
}