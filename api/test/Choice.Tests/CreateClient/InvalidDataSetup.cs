using Xunit;

namespace Choice.UnitTests.CreateClient
{
    internal sealed class InvalidDataSetup : TheoryData<string, string, string, string, string>
    {
        public InvalidDataSetup()
        {
            Add("", "", "", "", "");
            Add("Name", "Surname", "Password", "Email", "");
        }
    }
}
