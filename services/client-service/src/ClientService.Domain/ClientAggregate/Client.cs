using Choice.ClientService.Domain.Common;
using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Domain.ClientAggregate
{
    public class Client : Entity
    {
        public Client(string guid, string name, string surname, string email,
            Address address, List<OrderRequest> requests, double averageGrade)
        {
            Guid = guid;
            Name = name;
            Surname = surname;
            Email = email;
            Address = address;
            Requests = requests;
            AverageGrade = averageGrade;
        }

        public string Guid { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }
        public List<OrderRequest> Requests { get; private set; }
        public double AverageGrade { get; private set; }
        
        public void SendRequest(OrderRequest request) =>
            Requests.Add(request);

        public void ChangeData(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}
