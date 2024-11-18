using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Infrastructure.DomainEventDispatching;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR;
using BuildingBlocks.Infrastructure.DomainEventDispatching.MediatR.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Users.Infrastructure.Data;

namespace Users.Infrastructure.Processing.Outbox
{
    internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand>
    {
        private readonly UsersContext _usersContext;
        private readonly IMediator _mediator;
        private readonly DomainEventsMapper _mapper;

        internal ProcessOutboxCommandHandler(UsersContext usersContext, IMediator mediator, DomainEventsMapper mapper)
        {
            _usersContext = usersContext;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Execute(ProcessOutboxCommand command)
        {
            var messages = await _usersContext.OutboxMessages.Where(m => m.Processed == null)
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
