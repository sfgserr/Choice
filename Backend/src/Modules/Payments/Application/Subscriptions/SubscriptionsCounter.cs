using BuildingBlocks.Application.Data;
using Dapper;
using Payments.Domain.Payers;
using Payments.Domain.SubscritpionPayments;

namespace Payments.Application.Subscriptions
{
    public class SubscriptionsCounter : ISubscriptionsCounter
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SubscriptionsCounter(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int Count(PayerId payerId)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql =
                $"""
                SELECT COUNT(*)
                FROM payments."Subscription"
                WHERE payments."Subscription"."SubscriberId" = @Id
                """;

            return connection.QuerySingle<int>(sql, new { Id = payerId });
        }
    }
}
