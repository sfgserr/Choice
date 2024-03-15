
namespace ClientService.UnitTests.SendOrderRequest
{
    internal sealed class InvalidDataSetup : TheoryData<string, List<int>, int, bool, bool, bool>
    {
        public InvalidDataSetup()
        {
            Add("FakeDescription", new List<int>(), 3, false, false, false);
        }
    }
}
