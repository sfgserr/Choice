using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Chat.Domain.ChatUsers;
using Dapper;

namespace Chat.Application.Messages.Queries.GetChat
{
    internal class GetChatQueryHandler : IQueryHandler<GetChatQuery, ChatDto>
    {
        private readonly ISqlConnectionFactory _factory;
        private readonly IUserContext _userContext;

        internal GetChatQueryHandler(
            ISqlConnectionFactory factory, 
            IUserContext userContext)
        {
            _factory = factory;
            _userContext = userContext;
        }

        public async Task<ChatDto> Handle(GetChatQuery query)
        {
            using var connection = _factory.GetConnection();

            const string sql = 
            $"""
            SELECT
                chat."ChatUsers"."Id" as {nameof(UserDto.Id)},
                chat."ChatUsers"."Name" as {nameof(UserDto.Name)},
                chat."ChatUsers"."IconUri" as {nameof(UserDto.IconUri)}
            FROM chat."ChatUsers"
            WHERE chat."ChatUsers"."Id" = @Id2;
            
            SELECT 
                chat."Messages"."Id" as {nameof(MessageDto.Id)},
                chat."Messages"."Type" as {nameof(MessageDto.Type)},
                chat."Messages"."Body" as {nameof(MessageDto.Body)},
                chat."Messages"."IsRead" as {nameof(MessageDto.IsRead)},
                chat."Messages"."FromUserId" as {nameof(MessageDto.FromUserId)},
                chat."Messages"."CreationDate" as {nameof(MessageDto.CreationDate)},
                chat."OrderMessages"."ResponseId" as {nameof(MessageDto.OrderResponseId)},
                chat."OrderMessages"."IsActive" as {nameof(MessageDto.IsActive)},
                chat."OrderMessages"."EnrollmentDate" as {nameof(MessageDto.EnrollmentDate)}
            FROM chat."Messages"
            JOIN chat."OrderMessages" ON chat."OrderMessages"."MessageId" = chat."Messages"."Id"
            JOIN chat."ChatUsers" ON chat."ChatUsers"."Id" = chat."Messages"."ToUserId"
            WHERE 
                (chat."Messages"."ToUserId" = @Id1 AND chat."Messages"."FromUserId" = @Id2) OR
                (chat."Messages"."ToUserId" = @Id2 AND chat."Messages"."FromUserId" = @Id1)
            ORDER BY {nameof(MessageDto.CreationDate)}
            """;

            var result = await connection.QueryMultipleAsync(
                sql,
                new
                {
                    Id1 = _userContext.Id.Value,
                    Id2 = query.UserId
                });

            var user = result.Read<UserDto>().First();
            var messages = result.Read<MessageDto>();
            
            return new ChatDto { User = user, Messages = messages };
        }
    }
}
