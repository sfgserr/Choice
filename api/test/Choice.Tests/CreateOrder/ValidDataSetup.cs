using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.CreateOrder
{
    internal sealed class ValidDataSetup : TheoryData<List<Category>, string, bool, bool, bool, List<string>, int>
    {
        public ValidDataSetup()
        {
            Add(new List<Category>() { new Category() }, "Description", true, true, true, 
                new List<string>() { "photo" }, 5);
        }
    }
}
