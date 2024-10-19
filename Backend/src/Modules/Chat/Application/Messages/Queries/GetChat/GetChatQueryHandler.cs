using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;

namespace Chat.Application.Messages.Queries.GetChat
{
    internal class GetChatQueryHandler : IQueryHandler<GetChatQuery, IEnumerable<MessageDto>>
    {
        private readonly ISqlConnectionFactory _factory;

        internal GetChatQueryHandler(ISqlConnectionFactory factory)
        {
            _factory = factory;
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
                chat."Messages"."FromUserId" as {nameof(MessageDto.FromUserId)},
                chat."Messages"."CreationDate" as {nameof(MessageDto.CreationDate)},
                chat."Messages"."OrderResponseId" as {nameof(MessageDto.OrderResponseId)},
                chat."OrderMessages"."IsActive" as {nameof(MessageDto.IsActive)},
                chat."OrderMessages"."EnrollmentDate" as {nameof(MessageDto.EnrollmentDate)}
            FROM chat."Messages"
            JOIN chat."OrderMessages" ON chat."OrderMessages"."MessageId" = chat."Messages"."Id"
            """;
        }
    }
}
