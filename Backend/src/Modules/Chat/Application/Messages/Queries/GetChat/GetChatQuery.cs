using BuildingBlocks.Application.Cqrs.Queries;

namespace Chat.Application.Messages.Queries.GetChat
{
    public class GetChatQuery : IQuery<IEnumerable<MessageDto>>
    {
        public GetChatQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
