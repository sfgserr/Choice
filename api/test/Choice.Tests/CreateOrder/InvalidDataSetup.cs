using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.CreateOrder
{
    internal sealed class InvalidDataSetup : TheoryData<List<Category>, string, bool, bool, bool, List<string>, int>
    {
        public InvalidDataSetup()
        {
            Add(new List<Category>(), "Desctription", false, false, false, new List<string>(), 26);
        }
    }
}
