using BuildingBlocks.Application.Cqrs.Queries;

namespace Chat.Application.Messages.Queries.GetChats
{
    public class GetChatsQuery : IQuery<IEnumerable<ChatDto>>
    {
        
    }
}
