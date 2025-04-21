using Payments.Application.Wallets.Commands.Deposit;
using Payments.Application.Wallets.Commands.Withdraw;
using Payments.Infrastructure.Processing;

namespace Payments.Infrastructure.YooKassa.Events.Core 
{
    public static class YooKassaNotifications
    {
        private static bool _init = false;
        
        public static Dictionary<string, IEnumerable<Func<object, Task>>> _handlers = new();

        public static void Init()
        {
            if (_init) return;
            
            var paymentSucceedHandlers = new List<Func<object, Task>>();
            paymentSucceedHandlers.Add(async eventData =>
            {
                if (eventData is PaymentSucceededEvent @event)
                    await CommandsExecutor.ExecuteCommandAsync(new DepositCommand(@event.PaymentId));
            });
            
            var payoutSucceedHandlers = new List<Func<object, Task>>();
            payoutSucceedHandlers.Add(async eventData =>
            {
                if (eventData is PayoutSucceededEvent @event)
                    await CommandsExecutor.ExecuteCommandAsync(new WithdrawCommand(@event.PayoutId));
            });
            
            _handlers.Add("payment.succeeded", paymentSucceedHandlers);
            _handlers.Add("payout.succeeded", payoutSucceedHandlers);
        }
        
        public static async Task Handle(EventObject eventObject)
        {
            foreach (var handler in _handlers[eventObject.Event])
            {
                await handler(eventObject.Object);
            }
        }
    }
}