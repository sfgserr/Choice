using BuildingBlocks.Application.Cqrs.Queries;

namespace Chat.Application.Messages.Queries.GetChat
{
    public class GetChatQuery : IQuery<ChatDto>
    {
        public GetChatQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
