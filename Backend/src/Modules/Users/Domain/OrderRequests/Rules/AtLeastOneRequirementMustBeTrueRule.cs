using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.Rules
{
    internal class AtLeastOneRequirementMustBeTrueRule : IBusinessRule
    {
        private readonly bool[] _requirements;

        internal AtLeastOneRequirementMustBeTrueRule(bool[] requirements)
        {
            _requirements = requirements;
        }

        public bool IsBroken => _requirements.All(r => !r);

        public string Message => "All requirements are false";
    }
}
