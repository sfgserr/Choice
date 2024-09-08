namespace WebApi.Modules.Users.OrderRequests
{
    public class CreateOrderRequestRequest
    {
        public CreateOrderRequestRequest(
            Guid clientCreatedId,
            bool toKnowPrice,
            bool toKnowDeadline,
            bool toKnowEnrollmentDate,
            int distance,
            List<string> photoUris,
            string description,
            int categoryId)
        {
            ClientCreatedId = clientCreatedId;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
            Distance = distance;
            PhotoUris = photoUris;
            Description = description;
            CategoryId = categoryId;
        }
        
        public Guid ClientCreatedId { get; }
        
        public bool ToKnowPrice { get; }
        
        public bool ToKnowDeadline { get; }
        
        public bool ToKnowEnrollmentDate { get; }
        
        public int Distance { get; }
        
        public List<string> PhotoUris { get; }
        
        public string Description { get; }
        
        public int CategoryId { get; }
    }
}