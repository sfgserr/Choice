namespace Authentication.Api.Services
{
    public interface IVerificationService
    {
        Task SendCode(string phone);

        bool VerifyCode(string phone, string code);
    }
}
