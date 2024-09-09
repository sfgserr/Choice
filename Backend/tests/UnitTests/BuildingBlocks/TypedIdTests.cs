using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace UnitTests.BuildingBlocks
{
    public class TypedIdTests
    {
        [Fact]
        public void Different_TypedId_Must_Be_Equal()
        {
            var guid = Guid.NewGuid();
            
            var userId = new UserId(guid);

            var companyId = new CompanyId(guid);
            
            Assert.True(userId.Equals(companyId));
        }
    }
}