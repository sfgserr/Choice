using System.Net.Http.Json;
using System.Text.Json;
using Identity.Application.Contracts;

namespace Identity.Infrastructure.Sms
{
    internal class SmsService : ISmsService
    {
        private readonly IHttpClientFactory _factory;

        internal SmsService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            using var client = _factory.CreateClient("SmsApi");

            var response = await client.PostAsync($"messages", JsonContent.Create(new
            {
                Messages = new[] 
                {
                    new
                    {
                        Content = new { ShortText = message },
                        To = new { Mssidn = phoneNumber }
                    }
                },
                Options = new { From = new { SmsAddress = "Выбор" } }
            }, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase 
            }));
            response.EnsureSuccessStatusCode();
        }
    }
}