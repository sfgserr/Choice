
namespace Users.Domain.Users
{
    public interface IUserContext
    {
        UserId Id { get; }

        Address Address { get; }
    }
}
