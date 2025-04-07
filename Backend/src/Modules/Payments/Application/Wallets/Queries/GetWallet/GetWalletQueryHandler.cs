using BuildingBlocks.Application.Cqrs.Queries;
using BuildingBlocks.Application.Data;
using Dapper;
using Payments.Domain.Payers;

namespace Payments.Application.Wallets.Queries.GetWallet
{
    internal class GetWalletQueryHandler : IQueryHandler<GetWalletQuery, int>
    {
        private readonly IPayerContext _payerContext;
        private readonly ISqlConnectionFactory _factory;

        internal GetWalletQueryHandler(IPayerContext payerContext, ISqlConnectionFactory factory)
        {
            _payerContext = payerContext;
            _factory = factory;
        }

        public async Task<int> Handle(GetWalletQuery query)
        {
            using var connection = _factory.GetConnection();
            
            const string sql = 
                $"""
                SELECT payments."Wallets"."Copecks" 
                FROM payments."Wallets"
                WHERE payments."Wallets"."PayerId" = @PayerId
                """;
            
            return await connection.QuerySingleAsync<int>(sql, new { PayerId = _payerContext.Id.Value });
        }
    }
}