using BuildingBlocks.Application.Cqrs.Commands;

namespace Payments.Application.Payments.CreatePayment
{
    public class CreatePaymentCommand : ICommandWithResult<string>
    {
        public CreatePaymentCommand(int copecks)
        {
            Copecks = copecks;
        }
        
        public int Copecks { get; }
    }
}