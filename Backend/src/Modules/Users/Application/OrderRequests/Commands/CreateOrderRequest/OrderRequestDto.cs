namespace Users.Application.OrderRequests.Commands.CreateOrderRequest
{
    public class OrderRequestDto
    {
        public Guid Id { get; set; }

        public int CategoryId { get; set; }
        
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}