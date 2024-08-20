using BuildingBlocks.Domain;

namespace Users.Domain.OrderResponses.Rules
{
    internal class PrepaymentShouldBeInRangeBetweenTenAndTwentyFivePercentOfPriceRule : IBusinessRule
    {
        private readonly double _prepayment;
        private readonly double _price;

        internal PrepaymentShouldBeInRangeBetweenTenAndTwentyFivePercentOfPriceRule(double prepayment, double price)
        {
            _prepayment = prepayment;
            _price = price;
        }

        public bool IsBroken => _prepayment > 0 && (_prepayment < _price * 0.1 || _prepayment > _price * 0.25);

        public string Message => "Prepayment should be in range between ten and twenty five percent of price";
    }
}
