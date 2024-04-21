
namespace Choice.ClientService.UnitTests.SendOrderRequest
{
    internal sealed class ValidDataSetup : TheoryData<string, List<string>, int, int, bool, bool, bool>
    {
        public ValidDataSetup()
        {
            Add("FakeDescription", [], 1, 6, true, true, true);
        }
    }
}
