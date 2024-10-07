using BuildingBlocks.Domain;
using Payments.Domain.SeedWork;

namespace Payments.Domain.EnrollmentPayments.Rules
{
    internal class CannotPayForPaidOrExpiredPaymentRule : IBusinessRule
    {
        private readonly PaymentStatus _status;

        internal CannotPayForPaidOrExpiredPaymentRule(PaymentStatus status)
        {
            _status = status;
        }

        public bool IsBroken => !_status.Equals(PaymentStatus.WaitingForPayment);

        public string Message => "Payment is inactive";
    }
}
