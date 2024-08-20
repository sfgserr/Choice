using BuildingBlocks.Domain;

namespace Users.Domain.Users.Companies.Rules
{
    internal class CannotChangeDataWhenDataIsNotFilledRule : IBusinessRule
    {
        private readonly bool _isDataFilled;

        internal CannotChangeDataWhenDataIsNotFilledRule(bool isDataFilled)
        {
            _isDataFilled = isDataFilled;
        }

        public bool IsBroken => !_isDataFilled;

        public string Message => "Data is not filled";
    }
}
