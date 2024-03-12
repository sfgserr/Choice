using Choice.ReviewService.Api.Models;
using Newtonsoft.Json;

namespace Choice.ReviewService.Api.Infrastructure.Ordering
{
    public class OrderingService : IOrderingService
    {
        private readonly IHttpClientFactory _factory;

        public OrderingService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> CanSendReview(string guid)
        {
            IList<Order> orders = await GetOrders(guid);

            return orders.Count > 0 && orders.Any(o => o.Reviews.Any(i => i != guid));
        }

        private async Task<IList<Order>> GetOrders(string guid)
        {
            HttpClient client = _factory.CreateClient("Ordering");

            HttpResponseMessage response = await client.GetAsync($"/GetOrders?guid={guid}");

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IList<Order>>(json);
        }
    }
}
