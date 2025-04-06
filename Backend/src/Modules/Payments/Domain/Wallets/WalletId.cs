using BuildingBlocks.Domain;

namespace Payments.Domain.Wallets
{
    public class WalletId : TypedIdValueBase
    {
        public WalletId(Guid value) : base(value)
        {
        }
    }
}