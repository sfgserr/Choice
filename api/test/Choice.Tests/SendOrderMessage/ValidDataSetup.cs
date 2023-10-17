using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.SendOrderMessage
{
    internal sealed class ValidDataSetup : TheoryData<User, Room, double, DateTime, int, Order>
    {
        public ValidDataSetup() 
        {
            Add(new User(), new Room(), 199.99, DateTime.Now, 12, new Order());
        }
    }
}
