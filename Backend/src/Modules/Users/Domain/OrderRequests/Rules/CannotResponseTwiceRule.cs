using BuildingBlocks.Domain;
using Users.Domain.Users.Companies;

namespace Users.Domain.OrderRequests.Rules
{
    internal class CannotResponseTwiceRule : IBusinessRule
    {
        private readonly IOrderResponsesCounter _сounter;
        private readonly CompanyId _companyId;

        internal CannotResponseTwiceRule(IOrderResponsesCounter сounter, CompanyId companyId)
        {
            _сounter = сounter;
            _companyId = companyId;
        }

        public bool IsBroken => _сounter.Count(_companyId) > 0;
        
        public string Message => "Вы уже ответили на этот заказ";
    }
}