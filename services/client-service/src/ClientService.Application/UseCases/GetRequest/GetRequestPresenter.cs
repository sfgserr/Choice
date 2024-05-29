using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetRequest
{
    public sealed class GetRequestPresenter : IOutputPort
    {
        public OrderRequest? Request { get; set; }
        public bool IsUserCompany { get; set; }
        public bool IsNotFound { get; set; }

        public void Ok(OrderRequest request, bool isUserCompany)
        {
            Request = request;
            IsUserCompany = isUserCompany;
        }

        public void NotFound()
        {
            IsNotFound = true;
        }
    }
}
