using BuildingBlocks.Application.Cqrs.Commands;

namespace Identity.Application.Authentication.Phone.GetCode
{
    public class GetCodeCommand : ICommand
    {
        public GetCodeCommand(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public string PhoneNumber { get; }
    }
}