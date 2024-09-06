namespace Users.Application.OrderRequests.Queries.GetOrderRequests
{
    public class OrderRequestDto
    {
        public OrderRequestDto(
            Guid id, 
            string categoryTitle, 
            string orderStatus, 
            string description, 
            DateTime creationDate)
        {
            Id = id;
            CategoryTitle = categoryTitle;
            OrderStatus = orderStatus;
            Description = description;
            CreationDate = creationDate;
        }

        public Guid Id { get; }

        public string CategoryTitle { get; }
        
        public string OrderStatus { get; }

        public string Description { get; }

        public DateTime CreationDate { get; }
    }
}