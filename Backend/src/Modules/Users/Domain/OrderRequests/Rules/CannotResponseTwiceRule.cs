using BuildingBlocks.Domain;
using Users.Domain.Users.Companies;

namespace Users.Domain.OrderRequests.Rules
{
    internal class CannotResponseTwiceRule : IBusinessRule
    {
        private readonly IOrderResponsesCounter _сounter;
        private readonly OrderRequestId _orderRequestId;
        private readonly CompanyId _companyId;

        internal CannotResponseTwiceRule(IOrderResponsesCounter сounter, OrderRequestId orderRequestId, CompanyId companyId)
        {
            _сounter = сounter;
            _orderRequestId = orderRequestId;
            _companyId = companyId;
        }

        public bool IsBroken => _сounter.Count(_orderRequestId, _companyId) > 0;
        
        public string Message => "Вы уже ответили на этот заказ";
    }
}