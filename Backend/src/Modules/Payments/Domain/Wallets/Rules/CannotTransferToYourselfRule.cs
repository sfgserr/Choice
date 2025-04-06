using BuildingBlocks.Domain;

namespace Payments.Domain.Wallets.Rules
{
    internal class CannotTransferToYourselfRule : IBusinessRule
    {
        private readonly WalletId _walletId;
        private readonly WalletId _toWalletId;

        internal CannotTransferToYourselfRule(WalletId walletId, WalletId toWalletId)
        {
            _walletId = walletId;
            _toWalletId = toWalletId;
        }

        public bool IsBroken => _walletId.Equals(_toWalletId);
        
        public string Message => "Вы не можете перевести деньги себе же";
    }
}