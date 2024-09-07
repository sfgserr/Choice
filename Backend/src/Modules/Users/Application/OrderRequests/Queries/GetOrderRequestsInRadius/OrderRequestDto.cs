namespace Users.Application.OrderRequests.Queries.GetOrderRequestsInRadius
{
    public class OrderRequestDto
    {
        public OrderRequestDto(
            Guid id, 
            string categoryId, 
            string description, 
            List<string> photoUris, 
            Guid clientCreatedId, 
            string iconUri, 
            string clientName,
            string latitude,
            string longitude,
            int reviewCount, 
            double averageGrade, 
            int distance)
        {
            Id = id;
            CategoryId = categoryId;
            Description = description;
            PhotoUris = photoUris;
            ClientCreatedId = clientCreatedId;
            IconUri = iconUri;
            ClientName = clientName;
            Latitude = latitude;
            Longitude = longitude;
            ReviewCount = reviewCount;
            AverageGrade = averageGrade;
            Distance = distance;
        }

        public Guid Id { get; }

        public string CategoryId { get; }

        public string Description { get; }

        public List<string> PhotoUris { get; }

        public Guid ClientCreatedId { get; }

        public string IconUri { get; }

        public string ClientName { get; }

        public string Latitude { get; }
        
        public string Longitude { get; }

        public int ReviewCount { get; }
        
        public double AverageGrade { get; }
        
        public int Distance { get; }
    }
}