
namespace Choice.Domain.Models
{
    public enum OrderStatus
    {
        Active = 0,
        Canceled = 1,
        Finished = 2
    }

    public class Order : DomainObject
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public string Description { get; set; } = string.Empty;
        public bool ToKnowPrice { get; set; } = false;
        public bool ToKnowDeadLine { get; set; } = false;
        public bool ToKnowAppointmentTime { get; set; } = false;
        public bool IsClientAppointed { get; set; } = false;
        public List<string> PhotoUris { get; set; } = new List<string>();
        public int SearchingRadius { get; set; } = 5;
        public DateTime TimeCreated { get; set; }
        public OrderStatus Status { get; set; }
    }
}
