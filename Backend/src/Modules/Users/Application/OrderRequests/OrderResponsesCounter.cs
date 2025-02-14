using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.OrderRequests;
using Users.Domain.Users.Companies;

namespace Users.Application.OrderRequests
{
    public class OrderResponsesCounter : IOrderResponsesCounter
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public OrderResponsesCounter(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int Count(CompanyId companyId)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = 
                $"""
                SELECT COUNT(*)
                FROM users."OrderResponses"
                WHERE users."OrderResponses"."CompanyId" = @Id 
                """;
            
            return connection.QuerySingle<int>(
                sql, 
                new
                {
                    Id = companyId.Value
                });
        }
    }
}