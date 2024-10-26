using Users.Domain.Users.Clients;
using Users.Domain.Users.Companies;

namespace Users.Domain.Users
{
    public interface IUserContext
    {
        CompanyId CompanyId { get; }
        
        ClientId ClientId { get; }
        
        UserId Id { get; }
        
        Address Address { get; }
        
        UserRole Role { get; }
    }
}
