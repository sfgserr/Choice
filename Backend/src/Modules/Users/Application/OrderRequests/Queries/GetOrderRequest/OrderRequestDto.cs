namespace Users.Application.OrderRequests.Queries.GetOrderRequest
{
    public class OrderRequestDto
    {
        public Guid Id { get; }

	public string Status { get; }

        public int CategoryId { get; }

        public string Description { get; }

        public bool ToKnowPrice { get; }

        public bool ToKnowDeadline { get; }

        public bool ToKnowEnrollmentDate { get; }

        public List<string> PhotoUris { get; }
        
        public int Distance { get; }

	public DateTime CreationDate { get; }
    }
}
