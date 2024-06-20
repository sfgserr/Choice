using Choice.ClientService.Domain.ClientAggregate;
using Choice.Common.SeedWork;

namespace Choice.ClientService.Domain.OrderRequests
{
    public class OrderRequest : Entity
    {
        public OrderRequest(int clientId, int categoryId, string description, List<string> photoUris, bool toKnowPrice, 
            bool toKnowDeadline, bool toKnowEnrollmentDate, int searchRadius, List<string> companiesWatched)
        {
            ClientId = clientId;
            CategoryId = categoryId;
            Description = description;
            PhotoUris = photoUris;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
            SearchRadius = searchRadius;
            CompaniesWatched = companiesWatched;
        }

        public int ClientId { get; }
        public Client? Client { get; set; }
        public int CategoryId { get; private set; }
        public string Description { get; private set; }
        public List<string> PhotoUris { get; private set; }
        public bool ToKnowPrice { get; private set; }
        public bool ToKnowDeadline { get; private set; }
        public bool ToKnowEnrollmentDate { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Active;
        public DateTime CreationDate { get; private set; } = DateTime.Now;
        public List<string> CompaniesWatched { get; private set; } = [];

        private int _searchRadius;

        public int SearchRadius
        {
            get => _searchRadius;
            private set => _searchRadius = value < 5000 ? 5000 : value > 25000 ? 25000 : value;
        }

        public void Update(string description, List<string> photoUris, int categoryId, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            Description = description;
            PhotoUris = photoUris;
            CategoryId = categoryId;
            SearchRadius = searchRadius;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
        }

        public void CompanyWatched(string id)
        {
            string? companyId = CompaniesWatched.FirstOrDefault(s => s == id);

            if (companyId is null)
            {
                CompaniesWatched.Add(id);
            }
        }

        public void SetStatus(OrderStatus status) =>
            Status = status;
    }
}
