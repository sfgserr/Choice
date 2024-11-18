using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR;
using BuildingBlocks.Infrastructure.DomainEventDispatching;
using Newtonsoft.Json;
using MediatR;
using Payments.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;

namespace Payments.Infrastructure.Processing.Outbox
{
    internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
    {
        private readonly PaymentsContext _paymentsContext;
        private readonly IMediator _mediator;
        private readonly DomainEventsMapper _mapper;

        internal ProcessOutboxCommandHandler(PaymentsContext paymentsContext, IMediator mediator, DomainEventsMapper mapper)
        {
            _paymentsContext = paymentsContext;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Execute(ProcessOutboxCommand command)
        {
            var messages = await _paymentsContext.OutboxMessages.Where(m => m.Processed == null)
                .ToListAsync();

            foreach (var message in messages)
            {
                var notification = JsonConvert.DeserializeObject(message.Message, _mapper.GetType(message.Type)) as IDomainNotification;

                await _mediator.Publish(notification!);

                message.Processed = DateTime.UtcNow;
            }
        }
    }
}
