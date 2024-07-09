using MailKit.Net.Smtp;
using MimeKit;

namespace Authentication.Api.Services
{
    public class EmailVerificationService : IEmailVerificationService
    {
        public async Task SendCode(string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Выбор", "sfgserr@gmail.com"));
            message.To.Add(new MailboxAddress("User", email));
            message.Subject = "Сброс пароля";

            string code = CodeManager.GenerateCode(email);

            message.Body = new TextPart("plain")
            {
                Text = code
            };

            using var client = new SmtpClient();

            await client.ConnectAsync("smtp.gmail.com", 587);

            await client.AuthenticateAsync("sfgserr@gmail.com", "129598Ec!");

            await client.SendAsync(message);
        }

        public bool VerifyCode(string email, string code)
        {
            return CodeManager.Check(email, code);
        }
    }
}
