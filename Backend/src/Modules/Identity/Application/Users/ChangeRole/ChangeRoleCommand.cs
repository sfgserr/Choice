using BuildingBlocks.Application.Cqrs.Commands;

namespace Identity.Application.Users.ChangeRole
{
    public class ChangeRoleCommand : InternalCommandBase
    {
        public ChangeRoleCommand(Guid id, Guid userId, string userRole) : base(id)
        {
            UserId = userId;
            UserRole = userRole;
        }
        
        public Guid UserId { get; }
        
        public string UserRole { get; }
    }
}