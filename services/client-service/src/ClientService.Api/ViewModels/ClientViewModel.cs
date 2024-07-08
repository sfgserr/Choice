using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;
using Choice.Common.ValueObjects;

namespace Choice.ClientService.Api.ViewModels
{
    public class ClientViewModel
    {
        public ClientViewModel(Client client)
        {
            Id = client.Id;
            UserId = client.Guid;
            Name = client.Name;
            Surname = client.Surname;
            AverageGrade = client.AverageGrade;
            Address = client.Address;
            IconUri = client.IconUri;
            FinishedOrdersCount = client.Requests.Where(r => r.Status == OrderStatus.Finished).Count();
        }

        public int Id { get; }
        public string UserId { get; }
        public string Name { get; }
        public string Surname { get; }
        public double AverageGrade { get; }
        public Address Address { get; }
        public int FinishedOrdersCount { get; }
        public string IconUri { get; }
    }
}
