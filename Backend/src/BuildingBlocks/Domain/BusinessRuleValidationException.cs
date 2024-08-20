
namespace BuildingBlocks.Domain
{
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(IBusinessRule rule) : base(rule.Message)
        {

        }
    }
}
