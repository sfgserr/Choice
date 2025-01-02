namespace Users.Application.OrderRequests.Commands.CreateOrderRequest
{
    public class OrderRequestDto
    {
        public OrderRequestDto(Guid id, int categoryId, string description, DateTime creationDate)
        {
            Id = id;
            CategoryId = categoryId;
            Description = description;
            CreationDate = creationDate;
        }

        public Guid Id { get; }

        public int CategoryId { get; }
        
        public string Description { get; }

        public DateTime CreationDate { get; }
    }
}
