
namespace Chat.Application.Messages.Queries.GetChat
{
    public class ChatDto
    {
        public UserDto User { get; set; }
        
        public IEnumerable<MessageDto> Messages { get; set; }
    }
    
    public class MessageDto
    {   
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
    
    public class UserDto
    {
        public Guid Id { get; }
        
        public string IconUri { get; }
        
        public string Name { get; }
    }
}