using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Api.ViewModels
{
    public class OrderRequestDetailsViewModel
    {
        public OrderRequestDetailsViewModel(OrderRequest request)
        {
            Id = request.Id;
            Client = new(request.Client!);
            CategoryId = request.CategoryId;
            PhotoUris = request.PhotoUris;
            Description = request.Description;
            ToKnowPrice = request.ToKnowPrice;
            ToKnowDeadline = request.ToKnowDeadline;
            ToKnowEnrollmentDate = request.ToKnowEnrollmentDate;
            SearchRadius = request.SearchRadius;
            Status = request.Status;
            CreationDate = request.CreationDate;
        }

        public int Id { get; }
        public ClientViewModel Client { get; }
        public int CategoryId { get; }
        public List<string> PhotoUris { get; }
        public string Description { get; }
        public bool ToKnowPrice { get; }
        public bool ToKnowDeadline { get; }
        public bool ToKnowEnrollmentDate { get; }
        public int SearchRadius { get; }
        public OrderStatus Status { get; }
        public DateTime CreationDate { get; }
    }
}
