using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;
using Dapper;

namespace Chat.Application.Messages.Queries.GetChats
{
    internal class GetChatsQueryHandler : IQueryHandler<GetChatsQuery, IEnumerable<ChatDto>>
    {
        private readonly ISqlConnectionFactory _factory;
        private readonly IUserContext _userContext;
        private readonly IChatService _chatService;

        internal GetChatsQueryHandler(
            ISqlConnectionFactory factory,
            IUserContext userContext, 
            IChatService chatService)
        {
            _factory = factory;
            _userContext = userContext;
            _chatService = chatService;
        }

        public async Task<IEnumerable<ChatDto>> Handle(GetChatsQuery query)
        {
            using var connection = _factory.GetConnection();

            const string usersSql = 
            $"""
            WITH LastMessage AS (
                SELECT 
                    chat."Messages"."Id" as {nameof(ChatDto.LastMessageId)},
                    CASE 
                        WHEN chat."Messages"."FromUserId" = @Id THEN chat."Messages"."ToUserId"
                        ELSE chat."Messages"."ToUserId"
                    END AS {nameof(ChatDto.UserId)},
                    chat."Messages"."CreationDate" as {nameof(ChatDto.LastMessageCreationDate)}
                FROM chat."Messages"
                WHERE @Id IN (chat."Messages"."FromUserId", chat."Messages"."ToUserId")
            )
            SELECT DISTINCT ON ({nameof(ChatDto.UserId)})
                {nameof(ChatDto.LastMessageId)},
                {nameof(ChatDto.UserId)},
                {nameof(ChatDto.LastMessageCreationDate)}
            FROM LastMessage
            WHERE {nameof(ChatDto.UserId)} <> @Id
            ORDER BY {nameof(ChatDto.LastMessageCreationDate)} DESC;
            """;

            var chats = await connection.QueryAsync<ChatDto>(
                usersSql,
                new
                {
                    Id = _userContext.Id.Value
                });

            var chatArray = chats.ToArray();
            
            foreach (var chat in chatArray) chat.IsOnline = _chatService.IsUserOnline(chat.UserId);
            
            return chatArray;
        }
    }
}
