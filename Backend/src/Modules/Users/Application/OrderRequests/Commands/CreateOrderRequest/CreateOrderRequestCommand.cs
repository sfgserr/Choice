using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderRequests.Commands.CreateOrderRequest
{
    public class CreateOrderRequestCommand : ICommandWithResult<OrderRequestDto>
    {
        public CreateOrderRequestCommand(
            bool toKnowPrice,
            bool toKnowDeadline,
            bool toKnowEnrollmentDate,
            int distance,
            List<string> photoUris,
            string description,
            int categoryId)
        {
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
            Distance = distance;
            PhotoUris = photoUris;
            Description = description;
            CategoryId = categoryId;
        }
        
        public bool ToKnowPrice { get; }
        
        public bool ToKnowDeadline { get; }
        
        public bool ToKnowEnrollmentDate { get; }
        
        public int Distance { get; }
        
        public List<string> PhotoUris { get; }
        
        public string Description { get; }
        
        public int CategoryId { get; }
    }
}
