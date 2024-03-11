
namespace Choice.ReviewService.Api.Models
{
    public class Order
    {
        public Order(int id, string[] reviews)
        {
            Id = id;
            Reviews = reviews;
        }

        public int Id { get; set; } 
        public string[] Reviews { get; set; }
    }
}
