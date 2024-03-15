
namespace Choice.ClientService.UnitTests.SendOrderRequest
{
    internal sealed class ValidDataSetup : TheoryData<string, List<int>, int, bool, bool, bool>
    {
        public ValidDataSetup()
        {
            Add("FakeDescription", new List<int>() { 1 }, 6, false, false, true);
        }
    }
}
