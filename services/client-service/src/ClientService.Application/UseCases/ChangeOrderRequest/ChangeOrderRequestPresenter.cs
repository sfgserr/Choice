using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.ChangeOrderRequest
{
    public sealed class ChangeOrderRequestPresenter : IOutputPort
    {
        public OrderRequest? Request { get; set; }
        public bool IsNotFound { get; set; }
        public bool IsInvalid { get; set; }

        public void Ok(OrderRequest request)
        {
            Request = request;
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
