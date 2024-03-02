using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.SendOrderRequest
{
    public interface IOutputPort
    {
        void Ok(OrderRequest request);

        void NotFound();

        void Invalid();
    }
}
