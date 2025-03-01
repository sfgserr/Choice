using IntegrationTests.SeedWork;
using IntegrationTests.Services.Auth;
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
            var buySubscription = new TestChain(BuySubscriptionPaymentReturnsOk);
            var paySubscription = new TestChain(PaySubscriptionReturnsOk);
            var createOrderRequest = new TestChain(CreateOrderRequestReturnsOk);
            var getOrderRequests = new TestChain(GetOrderRequestsReturnsOk);
            var createOrderResponse = new TestChain(CreateOrderResponseReturnsOk);
            var getChats = new TestChain(GetChatsReturnsOk);
            var getChat = new TestChain(GetChatReturnsOk);
            var changeEnrollmentDate = new TestChain(ChangeEnrollmentDateReturnsOk);
            var confirm = new TestChain(ConfirmEnrollmentDateReturnsOk);
            var finish = new TestChain(Finish);
            
            fillData.SetNext(buySubscription);
            buySubscription.SetNext(paySubscription);
            paySubscription.SetNext(createOrderRequest);
            createOrderRequest.SetNext(getOrderRequests);
            getOrderRequests.SetNext(createOrderResponse);
            createOrderResponse.SetNext(getChats);
            getChats.SetNext(getChat);
            getChat.SetNext(changeEnrollmentDate);
            changeEnrollmentDate.SetNext(confirm);
            confirm.SetNext(finish);
            
            var result = fillData.Execute(null);

            Assert.True(result);
        }

        private async Task<TestResult> CompanyFillDataReturnsOk(object? arg)
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
                        SocialMediaUris = new List<string> { "https://instagram.com/com" },
                        IsPrepaymentAvailable = true
                    })
                };
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);
 
                return new TestResult(response.IsSuccessStatusCode);
            }, 10000, false, TokenType.Company);
        }
        
        private async Task<TestResult> BuySubscriptionPaymentReturnsOk(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) => 
            {
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    "api/subscriptionPayment/Month");
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);
 
                return new TestResult(response.IsSuccessStatusCode);
            }, 0, false, TokenType.Company);
        }
        
        private async Task<TestResult> PaySubscriptionReturnsOk(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) => 
            {
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(
                    HttpMethod.Put,
                    $"api/subscriptionPayment");
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);
                
                return new TestResult(response.IsSuccessStatusCode);
            }, 5000, false, TokenType.Company);
        }
        
        private async Task<TestResult> CreateOrderRequestReturnsOk(object? arg)
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

                return new TestResult(response.IsSuccessStatusCode);
            }, 5000);
        }
        
        private async Task<TestResult> GetOrderRequestsReturnsOk(object? arg)
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
                    return new TestResult(false);

                var content = await orderRequests.Content.ReadAsStringAsync();
                var array = JArray.Parse(content);

                return new TestResult(true, array[0].Value<string>("id"));
            }, 0, false, TokenType.Company);
        }
        
        private async Task<TestResult> CreateOrderResponseReturnsOk(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) =>
            {
                if (arg is not string id) return new TestResult(false);
                
                using var client = factory.CreateClient("Default");
                
                var request = new HttpRequestMessage(HttpMethod.Post, "api/orderResponses")
                {
                    Content = JsonContent.Create(
                        new
                        {
                            RequestId = id,
                            Price = 2000,
                            Deadline = 100,
                            EnrollmentDate = DateTime.UtcNow,
                            Prepayment = 500
                        })
                };
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);

                return new TestResult(response.IsSuccessStatusCode);
            }, 9000, false, TokenType.Company);
        }

        private async Task<TestResult> GetChatsReturnsOk(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) =>
            {
                using var client = factory.CreateClient("Default");
                
                var getChatsRequest = new HttpRequestMessage(HttpMethod.Get, "api/messages");
                getChatsRequest.Headers.Add("Authorization", $"Bearer {token}");

                var chatsResponse = await client.SendAsync(getChatsRequest);

                if (!chatsResponse.IsSuccessStatusCode)
                    return new TestResult(false);

                var content = await chatsResponse.Content.ReadAsStringAsync();
                var array = JArray.Parse(content);

                return new TestResult(true, array[0].Value<string>("userId"));
            });
        }
    
        private async Task<TestResult> GetChatReturnsOk(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) =>
            {
                if (arg is not string id) return new TestResult(false);

                using var client = factory.CreateClient("Default");
                
                var getChatsRequest = new HttpRequestMessage(HttpMethod.Get, $"api/messages/{id}");
                getChatsRequest.Headers.Add("Authorization", $"Bearer {token}");

                var chatsResponse = await client.SendAsync(getChatsRequest);

                if (!chatsResponse.IsSuccessStatusCode)
                    return new TestResult(false);

                var content = await chatsResponse.Content.ReadAsStringAsync();
                var chat = JObject.Parse(content);

                return new TestResult(true, chat.SelectToken("messages[0].orderResponseId")!.Value<string>());
            });
        }
        
        private async Task<TestResult> ChangeEnrollmentDateReturnsOk(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) =>
            {
                if (arg is not string id) return new TestResult(false);
                
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(
                    HttpMethod.Put, 
                    $"api/orderResponses/{id}/{DateTime.UtcNow:yyyy-MM-dd hh:mm:ss}Z");
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);

                return new TestResult(response.IsSuccessStatusCode, id);
            }, 4000);
        }
        
        private async Task<TestResult> ConfirmEnrollmentDateReturnsOk(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) =>
            {
                if (arg is not string id) return new TestResult(false);
                
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(HttpMethod.Put, $"api/orderResponses/confirm/{id}");
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);

                return new TestResult(response.IsSuccessStatusCode, id);
            }, 8000, false, TokenType.Company);
        }
        
        private async Task<TestResult> EnrollReturnsOk(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) =>
            {
                if (arg is not string id) return new TestResult(false);
                
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(HttpMethod.Put, $"api/orderResponses/enroll/{id}");
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);

                return new TestResult(response.IsSuccessStatusCode, id);
            }, 8000);
        }
        
        private async Task<TestResult> Finish(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) =>
            {
                if (arg is not string id) return new TestResult(false);
                
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(HttpMethod.Put, $"api/orderResponses/finish/{id}");
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);

                return new TestResult(response.IsSuccessStatusCode, id);
            }, 3000, true, TokenType.Company);
        }
    }
}