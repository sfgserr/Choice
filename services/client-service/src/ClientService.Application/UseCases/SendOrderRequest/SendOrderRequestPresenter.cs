using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.SendOrderRequest
{
    public sealed class SendOrderRequestPresenter : IOutputPort
    {
        public OrderRequest? Request { get; set; }
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

        public void Ok(OrderRequest request)
        {
            Request = request;
        }
    }
}
