namespace Users.Application.OrderRequests.Queries.GetOrderRequestsInRadius
{
    public class OrderRequestDto
    {
        public Guid Id { get; }

        public int CategoryId { get; }

        public string Description { get; }

        public string[] PhotoUris { get; }

        public Guid ClientCreatedId { get; }

        public string IconUri { get; }

        public string ClientName { get; }

        public string Latitude { get; }
        
        public string Longitude { get; }

        public int ReviewsCount { get; }
        
        public double AverageGrade { get; }
        
        public int Distance { get; }
    }
}