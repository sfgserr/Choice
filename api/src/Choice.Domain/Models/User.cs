
namespace Choice.Domain.Models
{
    public class User : DomainObject
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string IconUri { get; set; } = "defaulticon";
    }
}
