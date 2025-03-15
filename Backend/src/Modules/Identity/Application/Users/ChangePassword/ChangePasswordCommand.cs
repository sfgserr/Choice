using BuildingBlocks.Application.Cqrs.Commands;

namespace Identity.Application.Users.ChangePassword
{
    public class ChangePasswordCommand : ICommand
    {
        public ChangePasswordCommand(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
        
        public string OldPassword { get; }
        
        public string NewPassword { get; }
    }
}