namespace Users.Application.OrderRequests.Queries.GetOrderRequests
{
    public class OrderRequestDto
    {
        public Guid Id { get; }

        public string CategoryTitle { get; }
        
        public string OrderStatus { get; }

        public string Description { get; }

        public DateTime CreationDate { get; }
    }
}