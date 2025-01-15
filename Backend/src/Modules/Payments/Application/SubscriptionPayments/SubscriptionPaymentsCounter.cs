using BuildingBlocks.Application.Data;
using Dapper;
using Payments.Domain.Payers;
using Payments.Domain.SubscritpionPayments;

namespace Payments.Application.SubscriptionPayments
{
    public class SubscriptionPaymentsCounter : ISubscriptionPaymentsCounter
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public SubscriptionPaymentsCounter(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int Count(PayerId id)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                SELECT COUNT(*)
                FROM payments."SubscriptionPayments"
                WHERE payments."SubscriptionPayments"."PayerId" = @PayerId AND payments."SubscriptionPayments"."Status" = 'Active'
                """;

            return connection.QueryFirst<int>(
                sql,
                new
                {
                    PayerId = id.Value
                });
        }
    }
}