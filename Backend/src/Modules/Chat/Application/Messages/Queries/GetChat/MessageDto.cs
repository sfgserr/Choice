using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Application.Messages.Queries.GetChat
{
    public class MessageDto
    {
        public Guid Id { get; }

        public Guid ToUserId { get; }
        
        public string ToUserIconUri { get; }
        
        public string ToUserName { get; }
        
        public Guid FromUserId { get; }

        public string? Content { get; }

        public string Type { get; }

        public Guid? OrderResponseId { get; }

        public DateTime CreationDate { get; }

        public DateTime? EnrollmentDate { get; }

        public bool? IsActive { get; }
        
        public OrderResponseDto? OrderResponse { get; set; }
    }
}
