namespace Users.Application.OrderRequests.Queries.GetOrderRequest
{
    public class OrderRequestDto
    {
        public OrderRequestDto(
            Guid id, 
            int categoryId, 
            string description, 
            bool toKnowPrice, 
            bool toKnowDeadline, 
            bool toKnowEnrollmentDate, 
            List<string> photoUris, 
            int distance)
        {
            Id = id;
            CategoryId = categoryId;
            Description = description;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
            PhotoUris = photoUris;
            Distance = distance;
        }

        public Guid Id { get; }

        public int CategoryId { get; }

        public string Description { get; }

        public bool ToKnowPrice { get; }

        public bool ToKnowDeadline { get; }

        public bool ToKnowEnrollmentDate { get; }

        public List<string> PhotoUris { get; }
        
        public int Distance { get; }
    }
}