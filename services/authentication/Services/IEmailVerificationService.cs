namespace Authentication.Api.Services
{
    public interface IEmailVerificationService
    {
        Task SendCode(string email);

        bool VerifyCode(string email, string code);
    }
}
