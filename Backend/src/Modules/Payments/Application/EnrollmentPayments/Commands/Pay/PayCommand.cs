using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.EnrollmentPayments.Commands.Pay
{
    public class PayCommand : ICommand
    {
        public PayCommand(Guid responseId)
        {
            ResponseId = responseId;
        }

        public Guid ResponseId { get; }
    }
}