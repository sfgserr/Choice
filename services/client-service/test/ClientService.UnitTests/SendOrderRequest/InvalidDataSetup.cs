
namespace ClientService.UnitTests.SendOrderRequest
{
    internal sealed class InvalidDataSetup : TheoryData<string, List<string>, int, int, bool, bool, bool>
    {
        public InvalidDataSetup()
        {
            Add("FakeDescription", new List<string>(), 1, 3, false, false, false);
        }
    }
}
