
namespace Choice.ClientService.UnitTests.SendOrderRequest
{
    internal sealed class ValidDataSetup : TheoryData<string, List<string>, int, bool, bool, bool>
    {
        public ValidDataSetup()
        {
            Add("FakeDescription", new List<string>() { "FakeCategory" }, 6, false, false, true);
        }
    }
}
