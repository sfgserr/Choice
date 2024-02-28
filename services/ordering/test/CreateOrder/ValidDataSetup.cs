
namespace Ordering.UnitTests.CreateOrder
{
    internal sealed class ValidDataSetup : TheoryData<string, int, double, double, int, DateTime>
    {
        public ValidDataSetup() 
        {
            this.Add("2", 1, 100, 0, 3600, new DateTime(2024, 2, 29));
        }
    }
}
