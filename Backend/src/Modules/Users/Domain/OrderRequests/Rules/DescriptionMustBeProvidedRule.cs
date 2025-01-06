using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.Rules
{
    internal class DescriptionMustBeProvidedRule : IBusinessRule
    {
        private readonly string _description;

        internal DescriptionMustBeProvidedRule(string description)
        {
            _description = description;
        }

        public bool IsBroken => string.IsNullOrEmpty(_description);

        public string Message => "Описание должно быть заполнено";
    }
}
