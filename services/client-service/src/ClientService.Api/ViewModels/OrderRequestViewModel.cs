using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Api.ViewModels
{
    public class OrderRequestViewModel
    {
        public OrderRequestViewModel(OrderRequest request)
        {
            Id = request.Id;
            CategoryId = request.CategoryId;
            Description = request.Description;
            ToKnowPrice = request.ToKnowPrice;
            ToKnowDeadline = request.ToKnowDeadline;
            ToKnowEnrollmentDate = request.ToKnowEnrollmentDate;
            SearchRadius = request.SearchRadius;
            Status = request.Status;
            PhotoUris = request.PhotoUris;
            CreationDate = request.CreationDate;
        }

        public int Id { get; }
        public int CategoryId { get; private set; }
        public string Description { get; private set; }
        public bool ToKnowPrice { get; private set; }
        public bool ToKnowDeadline { get; private set; }
        public bool ToKnowEnrollmentDate { get; private set; }
        public int SearchRadius { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Active;
        public List<string> PhotoUris { get; private set; } = [];
        public DateTime CreationDate { get; private set; }
    }
}
