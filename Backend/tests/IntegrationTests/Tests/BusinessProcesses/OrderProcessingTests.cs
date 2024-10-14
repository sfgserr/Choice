using IntegrationTests.SeedWork;
using IntegrationTests.SeedWork.Probes;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.BusinessProcesses
{
    public class OrderProcessingTests : Sut
    {
        public OrderProcessingTests(
            ITestOutputHelper outputHelper, 
            Fixture testBed) : base(outputHelper, testBed)
        {
            
        }

        [Fact]
        public void OrderProcessExecutesSuccessfully()
        {
            var fillData = new TestChain(CompanyFillDataReturnsOk);
            var createOrderRequest = new TestChain(CreateOrderRequestReturnsOk);
            var createOrderResponse = new TestChain(CreateOrderResponseReturnsOk);

            fillData.SetNext(createOrderRequest);
            createOrderRequest.SetNext(createOrderResponse);

            var result = fillData.Execute();

            Assert.True(result);
        }

        private async Task<bool> CompanyFillDataReturnsOk()
        {
            return await ExecuteAuthorizedTest(async (factory, token) => 
            {
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(
                    HttpMethod.Put,
                    "api/companies/fillData")
                {
                    Content = JsonContent.Create(
                    new
                    {
                        Description = "string",
                        CategoryIds = new List<int> { 1 },
                        PhotoUris = new List<string> { "string" },
                        SocialMediaUris = new List<string> { "string" },
                        IsPrepaymentAvailable = true
                    })
                };
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);
 
                return new CheckSuccessStatusCodeProbe(response.IsSuccessStatusCode);
            }, 10000, false, true);
        }

        private async Task<bool> CreateOrderRequestReturnsOk()
        {
            return await ExecuteAuthorizedTest(async (factory, token) => 
            {
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(HttpMethod.Post, "api/orderRequests")
                {
                    Content = JsonContent.Create(
                        new
                        {
                            ToKnowPrice = true,
                            ToKnowDeadline = true,
                            ToKnowEnrollmentDate = true,
                            Distance = 12,
                            PhotoUris = new List<string> { "string" },
                            CategoryId = 1,
                            Description = "string"
                        })
                };
                request.Headers.Add("Authorization", $"Bearer {token}");
                
                var response = await client.SendAsync(request);

                return new CheckSuccessStatusCodeProbe(response.IsSuccessStatusCode);
            }, 5000, false);
        }

        private async Task<bool> CreateOrderResponseReturnsOk()
        {
            return await ExecuteAuthorizedTest(async (factory, token) =>
            {
                using var client = factory.CreateClient("Default");

                var getOrdersRequest = new HttpRequestMessage(
                    HttpMethod.Get,
                    "api/orderRequests/radius");
                getOrdersRequest.Headers.Add("Authorization", $"Bearer {token}");

                var orderRequests = await client.SendAsync(getOrdersRequest);

                if (!orderRequests.IsSuccessStatusCode)
                    return new CheckSuccessStatusCodeProbe(false);

                var content = await orderRequests.Content.ReadAsStringAsync();

                var jObject = JObject.Parse(content);

                var request = new HttpRequestMessage(HttpMethod.Post, "api/orderResponses")
                {
                    Content = JsonContent.Create(
                        new
                        {
                            RequestId = jObject.SelectToken("$[0].requestId")!.Value<string>(),
                            Price = 2000,
                            Deadline = 100,
                            EnrollmentDate = DateTime.UtcNow,
                            Prepayment = 500
                        })
                };
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);

                return new CheckSuccessStatusCodeProbe(response.IsSuccessStatusCode);
            }, 0, false, true);
        }
    }
}