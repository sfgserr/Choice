using Xunit;

namespace Choice.UnitTests.CreateRoom
{
    internal sealed class InvalidDataSetup : TheoryData<string>
    {
        public InvalidDataSetup()
        {
            Add("");
        }
    }
}
