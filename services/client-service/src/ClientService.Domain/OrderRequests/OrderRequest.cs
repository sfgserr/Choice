using Choice.ClientService.Domain.Common;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Domain.OrderRequests
{
    public class OrderRequest : Entity
    {
        public OrderRequest(int clientId, List<string> categories, string description,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate, int searchRadius)
        {
            ClientId = clientId;
            Categories = categories;
            Description = description;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
            SearchRadius = searchRadius;
        }

        public int ClientId { get; }
        public Client? Client { get; set; }
        public List<string> Categories { get; private set; }
        public string Description { get; private set; }
        public bool ToKnowPrice { get; private set; }
        public bool ToKnowDeadline { get; private set; }
        public bool ToKnowEnrollmentDate { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Active;

        private int _searchRadius;

        public int SearchRadius
        {
            get => _searchRadius;
            private set => _searchRadius = value < 5000 ? 5000 : value > 25000 ? 25000 : value;
        }

        public void SetStatus(OrderStatus status) =>
            Status = status;
    }
}
