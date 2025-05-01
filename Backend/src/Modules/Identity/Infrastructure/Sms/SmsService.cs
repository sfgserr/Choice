using System.Net.Http.Json;
using System.Text.Json;
using Identity.Application.Contracts;
using Serilog;

namespace Identity.Infrastructure.Sms
{
    internal class SmsService : ISmsService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILogger _logger;
        
        internal SmsService(IHttpClientFactory factory, ILogger logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            using var client = _factory.CreateClient("SmsApi");
            
            _logger.Information("Sending sms...");
            
            var response = await client.PostAsync($"http-api/v1/messages", JsonContent.Create(new
            {
                Messages = new[] 
                {
                    new
                    {
                        Content = new { ShortText = message },
                        To = new[] { new { Msisdn = $"7{phoneNumber}" } }
                    }
                },
                Options = new { From = new { SmsAddress = "Выбор" } }
            }, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            }));
            
            var responseString = await response.Content.ReadAsStringAsync();
            
            _logger.Information($"Payment processing response body: {responseString}");
            
            response.EnsureSuccessStatusCode();
        }
    }
}