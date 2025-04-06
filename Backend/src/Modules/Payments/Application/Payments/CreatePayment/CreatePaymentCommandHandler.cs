using BuildingBlocks.Application.Cqrs.Commands;
using Payments.Application.Services;
using Payments.Domain.Payers;

namespace Payments.Application.Payments.CreatePayment
{
    internal class CreatePaymentCommandHandler : ICommandHandlerWithResult<CreatePaymentCommand, string>
    {
        private readonly IPayerContext _payerContext;
        private readonly PaymentsService _paymentsService;
    
        internal CreatePaymentCommandHandler(IPayerContext payerContext, PaymentsService paymentsService)
        {
            _payerContext = payerContext;
            _paymentsService = paymentsService;
        }

        public async Task<string> Execute(CreatePaymentCommand command)
        {
            return await _paymentsService.CreatePayment(_payerContext.Id.Value, command.Copecks);
        }
    }
}