using BuildingBlocks.Application.Cqrs.Queries;

namespace Chat.Application.Chat.Queries.GetUserStatus
{
    public class GetUserStatusQuery : IQuery<bool>
    {
        public GetUserStatusQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}