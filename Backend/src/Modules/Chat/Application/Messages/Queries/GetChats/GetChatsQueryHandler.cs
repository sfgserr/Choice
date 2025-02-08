using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Chat.Domain.ChatUsers;
using Dapper;

namespace Chat.Application.Messages.Queries.GetChats
{
    internal class GetChatsQueryHandler : IQueryHandler<GetChatsQuery, IEnumerable<ChatDto>>
    {
        private readonly ISqlConnectionFactory _factory;
        private readonly IUserContext _userContext;

        internal GetChatsQueryHandler(
            ISqlConnectionFactory factory,
            IUserContext userContext)
        {
            _factory = factory;
            _userContext = userContext;
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
                        ELSE chat."Messages"."FromUserId"
                    END as {nameof(ChatDto.UserId)},
                    chat."Messages"."Body" as {nameof(ChatDto.LastMessage)},
                    chat."Messages"."IsRead" as {nameof(ChatDto.LastMessageIsRead)},
                    chat."Messages"."FromUserId" as {nameof(ChatDto.LastMessageUserSenderId)},
                    chat."Messages"."CreationDate" as {nameof(ChatDto.LastMessageCreationDate)}
                FROM chat."Messages"
                WHERE @Id IN (chat."Messages"."FromUserId", chat."Messages"."ToUserId")
            ),
            LastMessageWithUser AS (
            	SELECT
            		{nameof(ChatDto.LastMessageId)},
            		{nameof(ChatDto.UserId)},
            		{nameof(ChatDto.LastMessage)},
            		{nameof(ChatDto.LastMessageIsRead)},
            		{nameof(ChatDto.LastMessageUserSenderId)},
            		{nameof(ChatDto.LastMessageCreationDate)},
            		chat."ChatUsers"."Name" as {nameof(ChatDto.UserName)},
            		chat."ChatUsers"."IconUri" as {nameof(ChatDto.IconUri)}
            	FROM LastMessage
            	JOIN chat."ChatUsers" ON chat."ChatUsers"."Id" = {nameof(ChatDto.UserId)}
            )
            SELECT DISTINCT ON ({nameof(ChatDto.UserId)})
                {nameof(ChatDto.LastMessageId)},
                {nameof(ChatDto.UserId)},
                {nameof(ChatDto.LastMessageIsRead)},
                {nameof(ChatDto.LastMessageCreationDate)},
                {nameof(ChatDto.UserName)},
                {nameof(ChatDto.IconUri)},
                {nameof(ChatDto.LastMessage)},
                {nameof(ChatDto.LastMessageUserSenderId)}
            FROM LastMessageWithUser
            WHERE {nameof(ChatDto.UserId)} <> @Id
            ORDER BY {nameof(ChatDto.UserId)}, {nameof(ChatDto.LastMessageCreationDate)} DESC;
            """;

            var chats = await connection.QueryAsync<ChatDto>(
                usersSql,
                new
                {
                    Id = _userContext.Id.Value
                });

            return chats;
        }
    }
}
