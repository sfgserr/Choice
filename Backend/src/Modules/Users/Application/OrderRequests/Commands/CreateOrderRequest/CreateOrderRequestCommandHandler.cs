using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.OrderRequests;
using Users.Domain.Users;
using Users.Domain.Users.Clients;

namespace Users.Application.OrderRequests.Commands.CreateOrderRequest
{
    internal class CreateOrderRequestCommandHandler : ICommandHandler<CreateOrderRequestCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRequestRepository _orderRequestRepository;
        private readonly IUserContext _userContext;
        
        internal CreateOrderRequestCommandHandler(
            IClientRepository clientRepository, 
            IOrderRequestRepository orderRequestRepository, 
            IUserContext userContext)
        {
            _clientRepository = clientRepository;
            _orderRequestRepository = orderRequestRepository;
            _userContext = userContext;
        }

        public async Task Execute(CreateOrderRequestCommand command)
        {
            var client = await _clientRepository.Get(new(_userContext.Id.Value));

            var orderRequest = client.CreateRequest(
                command.ToKnowPrice,
                command.ToKnowDeadline,
                command.ToKnowEnrollmentDate,
                command.Distance,
                command.PhotoUris,
                command.Description,
                new(command.CategoryId));

            await _orderRequestRepository.Add(orderRequest);
        }
    }
}