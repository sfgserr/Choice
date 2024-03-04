using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetClientRequests
{
    public sealed class GetClientRequestsPresenter : IOutputPort
    {
        public IList<OrderRequest>? Requests { get; set; }
        public bool IsNotFound { get; set; }

        public void NotFound()
        {
            IsNotFound = true;
        }

        public void Ok(IList<OrderRequest> requests)
        {
            Requests = requests;
        }
    }
}
