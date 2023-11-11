
namespace Choice.Domain.Models
{
    public class Category : DomainObject
    {
        public string Title { get; set; } = string.Empty;
        public string IconUri { get; set; } = string.Empty;
    }
}
