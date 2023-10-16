using Choice.Domain.Models;
using Xunit;

namespace Choice.Tests.SendChatMessage
{
    internal sealed class ValidDataSetup : TheoryData<User, Room, string>
    {
        public ValidDataSetup() 
        {
            Add(new Client(), new Room(), "Hi");
        }
    }
}
