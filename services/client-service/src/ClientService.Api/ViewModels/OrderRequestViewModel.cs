using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Api.ViewModels
{
    public class OrderRequestViewModel
    {
        public OrderRequestViewModel(OrderRequest request)
        {
            Id = request.Id;
            Categories = request.Categories;
            Description = request.Description;
            ToKnowPrice = request.ToKnowPrice;
            ToKnowDeadline = request.ToKnowDeadline;
            ToKnowEnrollmentDate = request.ToKnowEnrollmentDate;
            SearchRadius = request.SearchRadius;
            Status = request.Status;
        }

        public int Id { get; }
        public List<int> Categories { get; private set; }
        public string Description { get; private set; }
        public bool ToKnowPrice { get; private set; }
        public bool ToKnowDeadline { get; private set; }
        public bool ToKnowEnrollmentDate { get; private set; }
        public int SearchRadius { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Active;
    }
}
