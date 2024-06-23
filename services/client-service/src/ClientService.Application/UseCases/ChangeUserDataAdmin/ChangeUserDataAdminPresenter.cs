using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeUserDataAdmin
{
    public sealed class ChangeUserDataAdminPresenter : IOutputPort
    {
        public Client? Client { get; set; }
        public bool IsInvalid { get; set; } 
        public bool IsNotFound { get; set; }

        public void Invalid()
        {
            IsInvalid = true;
        }

        public void NotFound()
        {
            IsNotFound = true;
        }

        public void Ok(Client client)
        {
            Client = client;
        }
    }
}
