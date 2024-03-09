using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClient
{
    public sealed class GetClientPresenter : IOutputPort
    {
        public Client Client { get; set; }
        public bool IsNotFound { get; set; }

        public void Ok(Client client)
        {
            Client = client;
        }

        public void NotFound()
        {
            IsNotFound = true;
        }
    }
}
