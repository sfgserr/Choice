
namespace Users.Domain.Users
{
    public interface IUsersCounter
    {
        int CountUsersByEmail(string email);

        int CountUsersByPhoneNumber(string phoneNumber);
    }
}
