
namespace Choice.Domain.Models
{
    public class Review : DomainObject
    {
        public Client Client { get; set; }
        public Company Company { get; set; }
        public List<string> PhotoUris { get; set; } = new List<string>();
        public string Text { get; set; } = string.Empty;

        private int _grade = 0;

        public int Grade
        {
            get => _grade;
            set => _grade = value > 5 ? 5 : value < 0 ? 0 : value;
        }
    }
}
