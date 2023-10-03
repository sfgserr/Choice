
namespace Choice.Domain.Models
{
    public class Category : DomainObject
    {
        public string Title { get; set; } = string.Empty;
        public List<Company> Companies { get; set;} = new List<Company>();
    }
}
