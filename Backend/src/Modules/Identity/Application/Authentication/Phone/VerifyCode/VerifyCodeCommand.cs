using BuildingBlocks.Application.Cqrs.Commands;

namespace Identity.Application.Authentication.Phone.VerifyCode
{
    public class VerifyCodeCommand : ICommandWithResult<AuthenticationResult>
    {
        public VerifyCodeCommand(string code)
        {
            Code = code;
        }

        public string Code { get; }
    }
}