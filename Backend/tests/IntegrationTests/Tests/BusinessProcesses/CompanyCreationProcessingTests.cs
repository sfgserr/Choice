using IntegrationTests.SeedWork;
using IntegrationTests.Services.Auth;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace IntegrationTests.Tests.BusinessProcesses
{
    public class CompanyCreationProcessingTests : Sut
    {
        public CompanyCreationProcessingTests(ITestOutputHelper outputHelper, Fixture testBed) : base(outputHelper, testBed)
        {
        }
        
        [Fact]
        public void ChangingDataNotCausesDuplicateSocialMedias()
        {
            var fillData = new TestChain(CompanyFillDataReturnsOk);
            var buySubscriptionPayment = new TestChain(BuySubscriptionPaymentReturnsOk);
            var paySubscription = new TestChain(PaySubscriptionReturnsOk);
            var changeData = new TestChain(ChangeDataReturnsOk);
            var companyHasTwoSocialMedias = new TestChain(CompanyHasTwoSocialMedias);
            
            fillData.SetNext(buySubscriptionPayment);
            buySubscriptionPayment.SetNext(paySubscription);
            changeData.SetNext(companyHasTwoSocialMedias);
            
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
                        SocialMediaUris = new List<string> { "https://facebook.com/Company" },
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
        
        private async Task<TestResult> ChangeDataReturnsOk(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) => 
            {
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(
                    HttpMethod.Put,
                    "api/companies")
                {
                    Content = JsonContent.Create(
                        new
                        {
                            Name = "Company",
                            Email = "s@gmail.com",
                            PhoneNumber = "1562791721",
                            City = "string",
                            Street = "string",
                            Description = "string",
                            Categories = new List<int> { 1 },
                            PhotoUris = new List<string> { "string" },
                            SocialMediaUris = new List<string> { "https://facebook.com/Company", "https://t.me/Company" },
                            IsPrepaymentAvailable = true
                        })
                };
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);
 
                return new TestResult(response.IsSuccessStatusCode);
            }, 10000, false, TokenType.Company);
        }
        
        private async Task<TestResult> CompanyHasTwoSocialMedias(object? arg)
        {
            return await ExecuteAuthorizedTest(async (factory, token) => 
            {
                using var client = factory.CreateClient("Default");

                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "api/companies");
                request.Headers.Add("Authorization", $"Bearer {token}");

                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return new TestResult(false);
                
                var json = await response.Content.ReadAsStringAsync();
                
                return new TestResult(
                    JObject.Parse(json)
                                      .SelectToken("socialMedias")?
                                      .Value<string[]>()!.Length > 2);
            }, 0, true, TokenType.Company);
        }
    }
}