namespace Authentication.Api.Services
{
    public interface IVerificationService
    {
        Task<bool> SendCode(string phone);

        bool VerifyCode(string phone, string code);
    }
}
