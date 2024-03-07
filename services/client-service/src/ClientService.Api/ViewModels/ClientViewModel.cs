using Choice.ClientService.Domain.ClientAggregate;

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
        }

        public int Id { get; }
        public string UserId { get; }
        public string Name { get; }
        public string Surname { get; }
        public double AverageGrade { get; }
        public string IconUri { get; }
    }
}
