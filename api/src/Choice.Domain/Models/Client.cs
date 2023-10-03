namespace Choice.Domain.Models
{
    public class Client : User
    {
        public string Surname { get; set; } = string.Empty;
        public string PhotoUri { get; set; } = string.Empty;
        public bool ShowReviews { get; set; } = true;
    }
}
