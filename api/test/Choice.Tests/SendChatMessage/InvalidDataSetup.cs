using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.SendChatMessage
{
    internal sealed class InvalidDataSetup : TheoryData<User, Room, string>
    {
        public InvalidDataSetup()
        {
            Add(new Client(), new Room(), "");
        }
    }
}
