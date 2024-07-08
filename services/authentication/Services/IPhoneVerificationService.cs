namespace Authentication.Api.Services
{
    public interface IPhoneVerificationService
    {
        Task<bool> SendCode(string contact);

        bool VerifyCode(string contact, string code);
    }
}
