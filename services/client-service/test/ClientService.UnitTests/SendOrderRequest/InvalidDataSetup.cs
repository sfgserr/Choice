
namespace ClientService.UnitTests.SendOrderRequest
{
    internal sealed class InvalidDataSetup : TheoryData<string, List<string>, int, bool, bool, bool>
    {
        public InvalidDataSetup()
        {
            Add("FakeDescription", new List<string>(), 3, false, false, false);
        }
    }
}
