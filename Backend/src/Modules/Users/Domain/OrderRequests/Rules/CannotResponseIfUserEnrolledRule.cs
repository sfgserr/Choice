using BuildingBlocks.Domain;

namespace Users.Domain.OrderRequests.Rules
{
    internal class CannotResponseIfUserEnrolledRule : IBusinessRule
    {
        internal CannotResponseIfUserEnrolledRule(bool isEnrolled)
        {
            IsBroken = isEnrolled;
        }

        public bool IsBroken { get; }

        public string Message => "User is enrolled to another response";
    }
}