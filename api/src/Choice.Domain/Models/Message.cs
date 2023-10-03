
namespace Choice.Domain.Models
{
    public class Message : DomainObject 
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
