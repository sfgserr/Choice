using BuildingBlocks.Application.Cqrs.Queries;

namespace Users.Application.Users.Queries.GetUserPermissions
{
    public class GetUserPermissionsQuery : IQuery<IList<PermissionDto>>
    {
        public GetUserPermissionsQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
