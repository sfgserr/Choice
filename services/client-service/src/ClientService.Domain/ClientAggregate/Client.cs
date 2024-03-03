using Choice.ClientService.Domain.Common;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Domain.ClientAggregate
{
    public class Client : Entity
    {
        private readonly List<OrderRequest> _requests = new();

        public Client(string guid, string name, string surname, string email,
            Address address)
        {
            Guid = guid;
            Name = name;
            Surname = surname;
            Email = email;
            Address = address;
        }

        public string Guid { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<OrderRequest> Requests => _requests.AsReadOnly();
        
        public void SendRequest(OrderRequest request) =>
            _requests.Add(request);

        public void ChangeData(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}
