using Xunit;

namespace Choice.UnitTests.CreateCategory
{
    internal sealed class InvalidDataSetup : TheoryData<string, string>
    {
        public InvalidDataSetup()
        {
            Add("", "photo");
            Add("Title", "");
            Add("", "");
        }
    }
}
