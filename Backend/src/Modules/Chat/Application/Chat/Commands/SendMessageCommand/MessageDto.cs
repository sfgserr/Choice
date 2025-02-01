using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Application.Chat.Commands.SendMessageCommand
{
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
        
        public OrderResponseDto? OrderResponse { get; }
    }
}