using Xunit;

namespace Choice.UnitTests.CreateRoom
{
    internal sealed class ValidDataSetup : TheoryData<string>
    {
        public ValidDataSetup()
        {
            Add("Name");
        }
    }
}
