using Xunit;

namespace Choice.UnitTests.CreateCategory
{
    internal sealed class ValidDataSetup : TheoryData<string, string>
    {
        public ValidDataSetup() 
        {
            Add("Category", "/photo");
            Add("Services", "/photo-url");
            Add("RepairServics", "/url");
        }
    }
}
