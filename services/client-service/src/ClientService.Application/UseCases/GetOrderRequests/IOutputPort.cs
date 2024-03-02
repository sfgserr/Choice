using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetOrderRequests
{
    public interface IOutputPort
    {
        void Ok(IList<OrderRequest> requests);

        void NotFound();
    }
}
