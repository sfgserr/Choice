
namespace Choice.Domain.Models
{
    public class OrderMessage : Message
    {
        public Order Order { get; set; }
        public List<string> PhotoUris { get; set; } = new List<string>();
        public double Price { get; set; }   
        public int Duration { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}
