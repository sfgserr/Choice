using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetRequest
{
    public sealed class GetRequestPresenter : IOutputPort
    {
        public OrderRequest? Request { get; set; }
        public bool IsNotFound { get; set; }

        public void Ok(OrderRequest request)
        {
            Request = request;
        }

        public void NotFound()
        {
            IsNotFound = true;
        }
    }
}
