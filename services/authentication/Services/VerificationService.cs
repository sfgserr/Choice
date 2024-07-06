using System.Text;
using Vonage;

namespace Authentication.Api.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly Dictionary<string, string> _verificationResources = [];

        private readonly VonageClient _vonageClient;

        public VerificationService(VonageClient vonageClient)
        {
            _vonageClient = vonageClient;
        }

        public async Task SendCode(string phone)
        {
            string code = GenerateCode();

            await _vonageClient.SmsClient.SendAnSmsAsync(new()
            {
                To = phone,
                From = "Выбор",
                Text = code
            });

            _verificationResources.Add(phone, code);
        }

        public bool VerifyCode(string phone, string code)
        {
            return _verificationResources[phone] == code;
        }

        private string GenerateCode()
        {
            var random = new Random();

            var stringBuilder = new StringBuilder();

            while (_verificationResources.ContainsValue(stringBuilder.ToString()))
            {
                stringBuilder.Clear();

                for (int i = 0; i < 6; i++)
                    stringBuilder.Append(random.Next(1, 10));
            }

            return stringBuilder.ToString();
        }
    }
}
