namespace Identity.Application.Contracts
{
    public interface ISmsService
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }
}