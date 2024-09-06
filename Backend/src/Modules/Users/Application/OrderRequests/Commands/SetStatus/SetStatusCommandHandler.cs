using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.OrderRequests;

namespace Users.Application.OrderRequests.Commands.SetStatus
{
    internal class SetStatusCommandHandler : ICommandHandler<SetStatusCommand>
    {
        private readonly IOrderRequestRepository _repository;

        internal SetStatusCommandHandler(IOrderRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(SetStatusCommand command)
        {
            var orderRequest = await _repository.Get(new(command.RequestId));
            
            orderRequest.SetStatus(OrderStatus.Parse(command.OrderStatus));
        }
    }
}