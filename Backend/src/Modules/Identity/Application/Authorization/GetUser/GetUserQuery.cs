using BuildingBlocks.Application.Cqrs.Queries;

namespace Identity.Application.Authorization.GetUser
{
    public class GetUserQuery : IQuery<UserDto>
    {
        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}