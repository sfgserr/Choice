using Choice.ClientService.Domain.Common;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Domain.ClientAggregate
{
    public class Client : Entity
    {
        private readonly List<OrderRequest> _requests = new();

        public Client(string guid, string name, string surname, string email,
            Address address, string iconUri)
        {
            Guid = guid;
            Name = name;
            Surname = surname;
            Email = email;
            Address = address;
            IconUri = iconUri;
        }

        private Client() { }

        public string Guid { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }
        public double AverageGrade { get; private set; }
        public double ReviewCount { get; private set; }
        public string IconUri { get; private set; }
        public IReadOnlyCollection<OrderRequest> Requests => _requests.AsReadOnly();
        
        public void SendRequest(OrderRequest request) =>
            _requests.Add(request);

        public void SetRequestStatus(int requestId, OrderStatus status)
        {
            OrderRequest? request = _requests.FirstOrDefault(r => r.Id == requestId);

            request?.SetStatus(status);
        }

        public void ChangeData(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public void AddReview(int grade)
        {
            AverageGrade = (ReviewCount * AverageGrade+grade)/(ReviewCount+1);
            ReviewCount++;
        }
    }
}
