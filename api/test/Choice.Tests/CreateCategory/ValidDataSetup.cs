using Xunit;

namespace Choice.Tests.CreateCategory
{
    internal sealed class ValidDataSetup : TheoryData<string, string>
    {
        public ValidDataSetup() 
        {
            Add("Category", "/photo");
        }
    }
}
