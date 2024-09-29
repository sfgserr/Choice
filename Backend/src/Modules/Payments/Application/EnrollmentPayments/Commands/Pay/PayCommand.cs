using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.EnrollmentPayments.Commands.Pay
{
    public class PayCommand : ICommand
    {
        public PayCommand(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public Guid PaymentId { get; }
    }
}