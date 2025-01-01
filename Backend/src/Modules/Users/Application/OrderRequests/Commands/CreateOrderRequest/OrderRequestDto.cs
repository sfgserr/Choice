namespace Users.Application.OrderRequests.Commands.CreateOrderRequest
{
    public class OrderRequestDto
    {
        public OrderRequestDto(Guid id, int categoryId, string description, DateTime creationTime)
        {
            Id = id;
            CategoryId = categoryId;
            Description = description;
            CreationTime = creationTime;
        }

        public Guid Id { get; }

        public int CategoryId { get; }
        
        public string Description { get; }

        public DateTime CreationTime { get; }
    }
}