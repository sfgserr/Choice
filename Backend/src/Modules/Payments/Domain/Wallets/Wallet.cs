using BuildingBlocks.Domain;
using Payments.Domain.Payers;
using Payments.Domain.Wallets.Rules;

namespace Payments.Domain.Wallets
{
    public class Wallet : Entity, IAggregateRoot
    {
        private int _copecks;

        private Wallet()
        {
            
        }
        
        private Wallet(WalletId id, PayerId payerId)
        {
            Id = id;
            PayerId = payerId;
        }
        
        public static Wallet Create(PayerId payerId)
        {
            return new Wallet(new(Guid.NewGuid()), payerId);
        }
        
        public WalletId Id { get; }
        
        public PayerId PayerId { get; }

        public void Transfer(int copecks, Wallet toWallet)
        {
            CheckRule(new CannotTransferToYourselfRule(Id, toWallet.Id));
            CheckRule(new AmountOfMoneyCannotBeNegativeRule(copecks));
            CheckRule(new AmountOfMoneyCannotBeNegativeRule(_copecks - copecks));
            
            _copecks -= copecks;
            
            toWallet.ReceiveTransfer(copecks);
        }

        private void ReceiveTransfer(int copecks)
        {
            _copecks += copecks;
        }

        public void Withdraw(int copecks)
        {
            CheckRule(new AmountOfMoneyCannotBeNegativeRule(copecks));
            CheckRule(new AmountOfMoneyCannotBeNegativeRule(_copecks - copecks));

            _copecks -= copecks;
        }

        public void Deposit(int copecks)
        {
            CheckRule(new AmountOfMoneyCannotBeNegativeRule(copecks));
            
            _copecks += copecks;
        }
    }
}