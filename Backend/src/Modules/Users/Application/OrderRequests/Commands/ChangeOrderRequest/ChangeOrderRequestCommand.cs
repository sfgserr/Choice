using BuildingBlocks.Application.Cqrs.Commands;

namespace Users.Application.OrderRequests.Commands.ChangeOrderRequest
{
    public class ChangeOrderRequestCommand : ICommand
    {
        public ChangeOrderRequestCommand(
            Guid requestId,
            bool toKnowPrice,
            bool toKnowDeadline,
            bool toKnowEnrollmentDate,
            int distance,
            List<string> photoUris,
            string description,
            int categoryId,
            Guid changingClientId)
        {
            RequestId = requestId;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
            Distance = distance;
            PhotoUris = photoUris;
            Description = description;
            CategoryId = categoryId;
            ChangingClientId = changingClientId;
        }
        
        public Guid RequestId { get; }
        
        public bool ToKnowPrice { get; }
        
        public bool ToKnowDeadline { get; }
        
        public bool ToKnowEnrollmentDate { get; }
        
        public int Distance { get; }
        
        public List<string> PhotoUris { get; }
        
        public string Description { get; }
        
        public int CategoryId { get; }
        
        public Guid ChangingClientId { get; }
    }
}
