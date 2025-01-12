using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;

namespace Payments.Application.Subscriptions.Queries.CheckActiveSubscription
{
    internal class CheckActiveSubscriptionQueryHandler : IQueryHandler<CheckActiveSubscriptionQuery, bool>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        internal CheckActiveSubscriptionQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> Handle(CheckActiveSubscriptionQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                SELECT EXISTS(
                    SELECT 1 FROM payments."Subscriptions"
                    WHERE payments."Subscriptions"."SubscriberId" = @SubscriberId AND payments."Subscriptions"."Status" = 'Active'
                ) 
                """;
            
            return await connection.QuerySingleAsync<bool>(
                sql,
                new
                {
                    query.SubscriberId
                });
        }
    }
}