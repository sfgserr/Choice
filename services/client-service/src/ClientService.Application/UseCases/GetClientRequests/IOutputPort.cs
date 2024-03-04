using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetClientRequests
{
    public interface IOutputPort
    {
        void Ok(IList<OrderRequest> requests);

        void NotFound();
    }
}
