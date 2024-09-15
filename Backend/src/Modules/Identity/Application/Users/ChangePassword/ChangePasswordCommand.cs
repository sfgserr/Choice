using BuildingBlocks.Application.Cqrs.Commands;

namespace Identity.Application.Users.ChangePassword
{
    public class ChangePasswordCommand : ICommand
    {
        public ChangePasswordCommand(string password)
        {
            Password = password;
        }

        public string Password { get; }
    }
}