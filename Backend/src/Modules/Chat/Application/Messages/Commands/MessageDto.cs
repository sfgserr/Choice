
namespace Chat.Application.Messages.Commands
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        
        public Guid FromUserId { get; set; }

        public string? Body { get; set; }
        
        public bool IsRead { get; set; }
        
        public string Type { get; set; }

        public Guid? OrderResponseId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public bool? IsActive { get; set; }
    }
}