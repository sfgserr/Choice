namespace WebApi.Modules.Identity
{
    public class ChangePasswordRequest
    {
        public ChangePasswordRequest(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public string OldPassword { get; }
        
        public string NewPassword { get; }
    }
}