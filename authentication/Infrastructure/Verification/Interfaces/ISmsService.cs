namespace Choice.Authentication.Infrastructure.Verification.Interfaces
{
    public interface ISmsService
    {
        Task<bool> SendVerificationCode(string phone);

        Task<bool> CheckVerificationCode(string phone, string code);
    }
}
