using BuildingBlocks.Domain;

namespace Payments.Domain.Wallets.Rules
{
    internal class AmountOfMoneyCannotBeNegativeRule : IBusinessRule
    {
        private readonly int _copecks;

        internal AmountOfMoneyCannotBeNegativeRule(int copecks)
        {
            _copecks = copecks;
        }

        public bool IsBroken => _copecks < 0;
        
        public string Message => "Недостаточно средств";
    }
}