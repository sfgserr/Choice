using Users.Domain.Users;
using Users.Domain.Users.Clients;

namespace UnitTests.BuildingBlocks
{
    public class TypedIdTests
    {
        [Fact]
        public void Different_TypedId_Must_Be_Equal()
        {
            var guid = Guid.NewGuid();
            
            var userId = new UserId(guid);

            var companyId = new ClientId(guid);
            
            Assert.True(userId.Equals(companyId));
        }
    }
}