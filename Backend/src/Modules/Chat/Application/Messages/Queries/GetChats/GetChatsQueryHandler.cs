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

            const string sql = 
            $"""
            SELECT 
                chat."ChatUsers"."Id" as {nameof(ChatDto.UserId)},
                chat."ChatUsers"."IconUri" as {nameof(ChatDto.IconUri)},
                chat."ChatUsers"."Name" as {nameof(ChatDto.UserName)},
                chat."Messages"."Body" as {nameof(ChatDto.LastMessage)},
                chat."Messages"."Id" as {nameof(ChatDto.LastMessageId)},
                chat."Messages"."CreationDate" as {nameof(ChatDto.LastMessageCreationDate)}
            FROM chat."ChatUsers"
            JOIN chat."Messages" ON chat."Messages"."ToUserId" = chat."ChatUsers"."Id" OR chat."Messages"."FromUserId" = chat."ChatUsers"."Id"
            WHERE chat."ChatUsers"."Id" = @Id
            """;

            var chats = await connection.QueryAsync<ChatDto>(
                sql,
                new
                {
                    Id = _userContext.Id.Value
                });
        }
    }
}
