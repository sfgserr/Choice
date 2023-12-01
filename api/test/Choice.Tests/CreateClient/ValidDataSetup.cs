using Xunit;

namespace Choice.UnitTests.CreateClient
{
    internal sealed class ValidDataSetup : TheoryData<string, string, string, string, string>
    {
        public ValidDataSetup()
        {
            Add("Name", "Surname", "Password", "Email", "Uri");
            Add("Bob", "Johnson", "1541336", "bobjohnson@email.com", "/bob-photo");
        }
    }
}
