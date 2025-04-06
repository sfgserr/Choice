using System.Net.Http.Json;
using System.Text.Json;
using Payments.Application.Contracts;
using Payments.Application.Contracts.Dtos;

namespace Payments.Infrastructure.YooKassa
{
    internal class YooKassaClient : IPaymentsGateway
    {
        private readonly IHttpClientFactory _factory;

        internal YooKassaClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<PaymentDto> ProcessPayment(Guid payerId, double amount)
        {
            using var client = _factory.CreateClient("YooKassa");

            var response = await client.PostAsync(
                "payments", 
                JsonContent.Create(new
                {
                    Amount = new
                    {
                        Value = amount,
                        Currency = "RUB"
                    },
                    Capture = true,
                    Confirmation = new
                    {
                        Type = "redirect",
                        ReturnUrl = "app://"
                    },
                    Metadata = new Dictionary<string, string>
                    {
                        ["PaymentId"] = payerId.ToString(),
                    }
                }, options: new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase 
                }));
            
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<PaymentDto>() ?? throw new InvalidDataException();
        }

        public async Task<PaymentDto?> Get(Guid paymentId)
        {
            using var client = _factory.CreateClient("YooKassa");
            
            var response = await client.GetAsync($"payments/{paymentId}");
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<PaymentDto>() ?? throw new InvalidDataException();
        }

        public async Task<PayoutDto> ProcessPayout(Guid payerId, string bankCardNumber, double amount)
        {
            using var client = _factory.CreateClient("YooKassa");
            
            var response = await client.PostAsync(
                "payouts", 
                JsonContent.Create(new
                {
                    Amount = new
                    {
                        Value = amount,
                        Currency = "RUB"
                    },
                    PayoutDestinationData = new
                    {
                        Type = "bank_card",
                        Card = new { Number = bankCardNumber }
                    },
                    Metadata = new Dictionary<string, string>
                    {
                        ["PayerId"] = payerId.ToString(),
                    }
                }));
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<PayoutDto>() ?? throw new InvalidDataException();
        }

        public async Task<PayoutDto?> GetPayout(string payoutId)
        {
            using var client = _factory.CreateClient("YooKassa");
            
            var response = await client.GetAsync($"payouts/{payoutId}");
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<PayoutDto>() ?? throw new InvalidDataException();
        }
    }
}