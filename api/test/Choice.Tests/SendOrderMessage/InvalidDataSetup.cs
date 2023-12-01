using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.SendOrderMessage
{
    internal sealed class InvalidDataSetup : TheoryData<User, Room, double, DateTime, int, Order>
    {
        public InvalidDataSetup() 
        {
            Add(null, null, -12, DateTime.Now, -3, null);
        }
    }
}
