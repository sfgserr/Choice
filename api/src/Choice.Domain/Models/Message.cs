
namespace Choice.Domain.Models
{
    public enum MessageStatus
    {
        Read = 0,
        Unread = 1
    }

    public class Message : DomainObject 
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public DateTime UploadDate { get; set; }
        public MessageStatus Status { get; set; } = MessageStatus.Unread;
    }
}
