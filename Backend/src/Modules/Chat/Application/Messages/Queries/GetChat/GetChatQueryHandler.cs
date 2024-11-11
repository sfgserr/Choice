using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Chat.Domain.ChatUsers;
using Dapper;
using Users.Application.Contracts;
using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Application.Messages.Queries.GetChat
{
    internal class GetChatQueryHandler : IQueryHandler<GetChatQuery, IEnumerable<MessageDto>>
    {
        private readonly ISqlConnectionFactory _factory;
        private readonly IUsersModule _usersModule;
        private readonly IUserContext _userContext;

        internal GetChatQueryHandler(
            ISqlConnectionFactory factory, 
            IUsersModule usersModule,
            IUserContext userContext)
        {
            _factory = factory;
            _usersModule = usersModule;
            _userContext = userContext;
        }

        public async Task<IEnumerable<MessageDto>> Handle(GetChatQuery query)
        {
            using var connection = _factory.GetConnection();

            const string sql = 
            $"""
            SELECT 
                chat."Messages"."Id" as {nameof(MessageDto.Id)},
                chat."Messages"."Type" as {nameof(MessageDto.Type)},
                chat."Messages"."Body" as {nameof(MessageDto.Content)},
                chat."Messages"."ToUserId" as {nameof(MessageDto.ToUserId)},
                chat."ChatUsers"."IconUri" as {nameof(MessageDto.ToUserIconUri)},
                chat."ChatUsers"."Name" as {nameof(MessageDto.ToUserName)},
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

            var messages = await connection.QueryAsync<MessageDto>(
                sql,
                new
                {
                    Id1 = _userContext.Id.Value,
                    Id2 = query.UserId
                });
            
            foreach (var message in messages)
            {
                if (message.OrderResponseId is { } responseId)
                {
                    message.OrderResponse = await _usersModule.Query<GetOrderResponseQuery, OrderResponseDto>(new(responseId));
                }
            }
            
            return messages;
        }
    }
}
