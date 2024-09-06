using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.OrderRequests;
using Users.Domain.Users;

namespace Users.Application.OrderRequests.Commands.ChangeOrderRequest
{
    internal class ChangeOrderRequestCommandHandler : ICommandHandler<ChangeOrderRequestCommand>
    {
        private readonly IOrderRequestRepository _repository;
        private readonly IUserContext _userContext;

        internal ChangeOrderRequestCommandHandler(IOrderRequestRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task Execute(ChangeOrderRequestCommand command)
        {
            var request = await _repository.Get(new(command.RequestId));
            
            request.Change(
                command.ToKnowPrice,
                command.ToKnowDeadline,
                command.ToKnowEnrollmentDate,
                command.Distance,
                command.PhotoUris,
                command.Description,
                new(command.CategoryId),
                new(_userContext.Id.Value));
        }
    }
}