using Choice.ClientService.Domain.OrderRequests;
using Choice.Common.ValueObjects;
using Choice.Common.SeedWork;

namespace Choice.ClientService.Domain.ClientAggregate
{
    public class Client : Entity
    {
        private readonly List<OrderRequest> _requests = [];

        public Client(string guid, string name, string surname, string email,
            Address address, string iconUri, string phoneNumber)
        {
            Guid = guid;
            Name = name;
            Surname = surname;
            Email = email;
            Address = address;
            IconUri = iconUri;
            PhoneNumber = phoneNumber;
        }

        private Client() { }

        public string Guid { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }
        public double AverageGrade { get; private set; }
        public int ReviewCount { get; private set; }
        public string IconUri { get; private set; }
        public string PhoneNumber { get; private set; }
        public IReadOnlyCollection<OrderRequest> Requests => _requests.AsReadOnly();
        
        public void SendRequest(OrderRequest request) =>
            _requests.Add(request);

        public void SetRequestStatus(int requestId, OrderStatus status)
        {
            OrderRequest? request = _requests.FirstOrDefault(r => r.Id == requestId);

            request?.SetStatus(status);
        }

        public void ChangeIconUri(string iconUri)
        {   
            IconUri = iconUri;
        }

        public void ChangeData(string name, string surname, string email, string phoneNumber, string city, 
            string street)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = new(street, city);
        }

        public void AddReview(int grade)
        {
            AverageGrade = ReviewCount < 1 ? grade : (ReviewCount * AverageGrade+grade)/(ReviewCount+1);
            ReviewCount++;
        }
    }
}
