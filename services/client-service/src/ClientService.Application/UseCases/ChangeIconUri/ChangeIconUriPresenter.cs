using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.ChangeIconUri
{
    public sealed class ChangeIconUriPresenter : IOutputPort
    {
        public Client? Client { get; set; }
        public bool IsNotFound { get; set; }
        public bool IsInvalid { get; set; }
        
        public void Ok(Client client)
        {
            Client = client;
        }

        public void NotFound()
        {
            IsNotFound = true;
        }

        public void Invalid()
        {
            IsInvalid = true;
        }
    }
}
