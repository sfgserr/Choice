using System.Net.Http.Json;
using System.Text.Json;
using Payments.Application.Contracts;
using Payments.Application.Contracts.Dtos;
using Serilog;

namespace Payments.Infrastructure.YooKassa
{
    internal class YooKassaClient : IPaymentsGateway
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILogger _logger;
        
        internal YooKassaClient(IHttpClientFactory factory, ILogger logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public async Task<PaymentDto> ProcessPayment(Guid payerId, double amount)
        {
            using var client = _factory.CreateClient("YooKassaPayments");
            
            _logger.Information("Processing payment");
            
            var request = new HttpRequestMessage(
                HttpMethod.Post, 
                $"{client.BaseAddress}/v3/payments");
            request.Headers.Add("Idempotence-Key", Guid.NewGuid().ToString());
            request.Content = JsonContent.Create(new
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
                    ["payerId"] = payerId.ToString(),
                }
            }, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            });
            
            var response = await client.SendAsync(request);
            
            var responseString = await response.Content.ReadAsStringAsync();
            
            _logger.Information($"Payment processing response body: {responseString}");
            
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<PaymentDto>() ?? throw new InvalidDataException();
        }

        public async Task<PaymentDto?> Get(Guid paymentId)
        {
            using var client = _factory.CreateClient("YooKassaPayments");
            
            var response = await client.GetAsync($"v3/payments/{paymentId}");
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<PaymentDto>() ?? throw new InvalidDataException();
        }

        public async Task<PayoutDto> ProcessPayout(Guid payerId, string bankCardNumber, double amount)
        {
            using var client = _factory.CreateClient("YooKassaPayouts");
            
            _logger.Information("Start processing payout");
            
            var request = new HttpRequestMessage(
                HttpMethod.Post, 
                $"{client.BaseAddress}/v3/payouts");
            request.Headers.Add("Idempotence-Key", Guid.NewGuid().ToString());
            
            request.Content = JsonContent.Create(new
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
                    ["payerId"] = payerId.ToString(),
                }
            }, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            });
            
            var response = await client.SendAsync(request);
            
            var responseString = await response.Content.ReadAsStringAsync();
            _logger.Information($"Payout processing response body: {responseString}");
            
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<PayoutDto>() ?? throw new InvalidDataException();
        }

        public async Task<PayoutDto?> GetPayout(string payoutId)
        {
            using var client = _factory.CreateClient("YooKassaPayouts");
            
            var response = await client.GetAsync($"v3/payouts/{payoutId}");
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadFromJsonAsync<PayoutDto>() ?? throw new InvalidDataException();
        }
    }
}