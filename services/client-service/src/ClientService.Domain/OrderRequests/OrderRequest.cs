using Choice.ClientService.Domain.Common;
using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Domain.OrderRequests
{
    public class OrderRequest : Entity
    {
        public OrderRequest(int clientId, List<string> categories, string description,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            ClientId = clientId;
            Categories = categories;
            Description = description;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
        }

        public int ClientId { get; }
        public Client? Client { get; set; }
        public List<string> Categories { get; private set; }
        public string Description { get; private set; }
        public bool ToKnowPrice { get; private set; }
        public bool ToKnowDeadline { get; private set; }
        public bool ToKnowEnrollmentDate { get; private set; }
        public int SearchRadius { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Active;

        public void SetRadius(int radius)
        {
            if (radius < 5000)
                SearchRadius = 5000;

            if (radius > 25000)
                SearchRadius = 25000;

            SearchRadius = radius;
        }
    }
}
