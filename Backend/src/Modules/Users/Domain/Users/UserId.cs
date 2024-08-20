using BuildingBlocks.Domain;

namespace Users.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value) : base(value) 
        {

        }
    }
}
