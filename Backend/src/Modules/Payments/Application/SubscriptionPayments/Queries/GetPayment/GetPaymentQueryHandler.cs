using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Payments.Domain.Payers;

namespace Payments.Application.SubscriptionPayments.Queries.GetPayment
{
    internal class GetPaymentQueryHandler : IQueryHandler<GetPaymentQuery, PaymentDto>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IPayerContext _payerContext;
        
        internal GetPaymentQueryHandler(ISqlConnectionFactory connectionFactory, IPayerContext payerContext)
        {
            _connectionFactory = connectionFactory;
            _payerContext = payerContext;
        }

        public async Task<PaymentDto> Handle(GetPaymentQuery query)
        {
            using var connection = _connectionFactory.GetConnection();
            
            const string sql = 
                $"""
                SELECT 
                    payments."SubscriptionPayments"."Id" as {nameof(PaymentDto.Id)},
                    payments."SubscriptionPayments"."PayerId" as {nameof(PaymentDto.PayerId)},
                    payments."SubscriptionPayments"."PeriodCost" as {nameof(PaymentDto.Cost)},
                    payments."SubscriptionPayments"."PeriodName" as {nameof(PaymentDto.Period)},
                    payments."SubscriptionPayments"."ExpirationDate" as {nameof(PaymentDto.ExpirationDate)},
                    payments."SubscriptionPayments"."Status" as {nameof(PaymentDto.Status)},
                FROM payments."SubscriptionPayments"
                WHERE payments."SubscriptionPayments"."PayerId" = @Id 
                """;

            return await connection.QuerySingleAsync<PaymentDto>(
                sql,
                new
                {
                    Id = _payerContext.Id.Value
                });
        }
    }
}