using Choice.ClientService.Domain.ClientAggregate;
using Choice.ClientService.Domain.OrderRequests;

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
            IconUri = client.IconUri;
            FinishedOrdersCount = client.Requests.Where(r => r.Status == OrderStatus.Finished).Count();
        }

        public int Id { get; }
        public string UserId { get; }
        public string Name { get; }
        public string Surname { get; }
        public double AverageGrade { get; }
        public int FinishedOrdersCount { get; }
        public string IconUri { get; }
    }
}
