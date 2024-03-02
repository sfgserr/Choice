using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.UseCases.CreateClient
{
    public interface IOutputPort
    {
        void Ok(IList<Client> clients);

        void Invalid();
    }
}
