using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.GetClientAdmin
{
    public sealed class GetClientAdminPresenter : IOutputPort
    {
        public Client? Client { get; set; }
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
