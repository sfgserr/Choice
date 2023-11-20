namespace Choice.Domain.Models
{
    public class Client : User
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public bool ShowReviews { get; set; } = true;
    }
}
