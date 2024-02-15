using Choice.Authentication.Infrastructure.Verification.Interfaces;
using Twilio.Rest.Verify.V2.Service;

namespace Choice.Authentication.Infrastructure.Verification
{
    public class SmsService : ISmsService
    {
        private readonly string _verificationServiceSid;

        public SmsService(string verificationServiceSid)
        {
            _verificationServiceSid = verificationServiceSid;
        }

        public async Task<bool> CheckVerificationCode(string phone, string code)
        {
            var resource = await VerificationCheckResource.CreateAsync(
                to: $"+{phone}",
                code: code,
                pathServiceSid: _verificationServiceSid);

            return resource.Status != "canceled";
        }

        public async Task<bool> SendVerificationCode(string phone)
        {
            var resource = await VerificationResource.CreateAsync(
                to: $"+{phone}",
                channel: "sms",
                pathServiceSid: _verificationServiceSid);

            return resource.Status != "canceled";
        }
    }
}
